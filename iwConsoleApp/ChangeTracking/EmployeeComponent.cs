using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.ChangeTracking
{
    public class EmployeeComponent
    {
        private ThreadSafeHashSet<string> _changeTracking = new ThreadSafeHashSet<string>();
        private ReadOnlyThreadSafeHashSet<string> _readonlyChangeTracking;

        public EmployeeComponent()
        {
             _readonlyChangeTracking = new ReadOnlyThreadSafeHashSet<string>(_changeTracking);
        }

        [JsonIgnore]
        public ReadOnlyThreadSafeHashSet<string> ChangeTracking
        {
            get
            {
                RollUpChangeTracking();

                // Always return a copy because we don't want the original modified
                //return new ReadOnlyThreadSafeHashSet<string>(_changeTracking);
                return _readonlyChangeTracking;
            }
        }

        private bool _changedSinceLastRollUp = false;
        [JsonIgnore]
        public bool ChangedSinceLastRollUp
        {
            get
            {
                return _changedSinceLastRollUp;
            }
            set
            {
                _changedSinceLastRollUp = value;
            }
        }

        private bool _hasChildrenWithUnRolledUpChanges;
        [JsonIgnore]
        public bool HasChildrenWithUnRolledUpChanges
        {
            get
            {
                _hasChildrenWithUnRolledUpChanges = false;
                foreach (var p in EmployeeComponents)
                {
                    EmployeeComponent child = (EmployeeComponent)p.GetValue(this);
                    if (child != null)
                    {
                        if (child.ChangedSinceLastRollUp || child.HasChildrenWithUnRolledUpChanges)
                        {
                            _hasChildrenWithUnRolledUpChanges = true;
                            break;
                        }
                    }
                }
                return _hasChildrenWithUnRolledUpChanges;
            }
        }

        protected void SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "")
        {
            _changeTracking.Add(propertyName);
            field = newValue;
            _changedSinceLastRollUp = true;
        }

        public bool IsChanged(string path)
        {
            return ChangeTracking.Contains(path);
        }

        // instance-level cache of all the child properties that inherit from BaseEntity
        private HashSet<PropertyInfo> _employeeComponents;
        [JsonIgnore]
        private HashSet<PropertyInfo> EmployeeComponents
        {
            get
            {
                if (_employeeComponents == null)
                {
                    _employeeComponents = new HashSet<PropertyInfo>(InstanceProperties.Where(x => x.PropertyType.IsSubclassOf(typeof(EmployeeComponent))));
                }
                return _employeeComponents;
            }
        }

        // instance-level cache of all the child properties are dictionaries
        private HashSet<PropertyInfo> _dictionaries;
        [JsonIgnore]
        private HashSet<PropertyInfo> Dictionaries
        {
            get
            {
                if (_dictionaries == null)
                {
                    _dictionaries = new HashSet<PropertyInfo>(InstanceProperties.Where(x => x.PropertyType.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IDictionary<,>))));
                }
                return _dictionaries;
            }
        }

        private void RollUpChangeTracking()
        {
            foreach (var p in EmployeeComponents)
            {
                EmployeeComponent child = (EmployeeComponent)p.GetValue(this);
                RollUpChild(p.Name, child);
            }

            // Adding a case for dictionaries
            foreach (var dictionaryProperty in Dictionaries)
            {
                IDictionary d = dictionaryProperty.GetValue(this) as IDictionary;
                if (d != null)
                {
                    foreach (var key in d.Keys)
                    {
                        string changeTrackingKey = dictionaryProperty.Name + "." + key.ToString();
                        _changeTracking.Add(changeTrackingKey);
                        var thing = d[key];
                        if (thing is EmployeeComponent)
                        {
                            EmployeeComponent child = thing as EmployeeComponent;
                            RollUpChild(changeTrackingKey, child);
                        }
                    }
                }
            }
        }


        private void RollUpChild(string pathStart, EmployeeComponent child)
        {
            if (child == null)
            {
                return;
            }

            // Roll up the descendants first
            if (child.HasChildrenWithUnRolledUpChanges)
            {
                child.RollUpChangeTracking();
                child.ChangedSinceLastRollUp = true;
            }

            // Roll up the child
            if (child.ChangedSinceLastRollUp)
            {
                RollUpChildChanges(pathStart, child);
                child.ChangedSinceLastRollUp = false;
            }
        }

        private void RollUpChildChanges(string pathStart, EmployeeComponent child)
        {
            var changedChildProperties = child.ChangeTracking;
            foreach (var item in changedChildProperties)
            {
                string key = pathStart + "." + item;
                _changeTracking.Add(key);
            }
        }

        // instance-level cache of all the child properties
        private HashSet<PropertyInfo> _instanceProperties;
        private HashSet<PropertyInfo> InstanceProperties
        {
            get
            {
                if (_instanceProperties == null)
                {
                    _instanceProperties = new HashSet<PropertyInfo>(this.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public));
                }
                return _instanceProperties;
            }
        }

        // static cache for EmployeeComponent paths
        private static ConcurrentDictionary<Type, ThreadSafeHashSet<string>> _pathsForType = new ConcurrentDictionary<Type, ThreadSafeHashSet<string>>();
        public static ThreadSafeHashSet<string> GetAllPaths(Type t)
        {
            if (!t.IsSubclassOf(typeof(EmployeeComponent)))
            {
                throw new Exception("EmployeeComponent.GetAllPaths(Type t) only works whe t is a subclass of EmployeeComponent");
            }

            if (!_pathsForType.ContainsKey(t))
            {
                ThreadSafeHashSet<string> paths = new ThreadSafeHashSet<string>();
                var properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var p in properties)
                {
                    if (p.PropertyType.IsSubclassOf(typeof(EmployeeComponent)))
                    {
                        var stuff = GetAllPaths(p.PropertyType);
                        foreach (var thing in stuff)
                        {
                            paths.Add(p.Name + "." + thing);
                        }
                    }
                    else if ((p.PropertyType.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IDictionary<,>))) &&
                             (p.PropertyType.GetGenericTypeDefinition() != typeof(EmployeePropertyDictionary<>)))
                    {
                        // get all the possible key values for this dictionary
                        var keyNames = GetPossibleKeyNames(p);
                        Type valueType = GetDictionaryValueType(p);

                        var stuff = GetAllPaths(valueType);
                        foreach (var keyName in keyNames)
                        {
                            foreach (var thing in stuff)
                            {
                                paths.Add(p.Name + "." + keyName + "." + thing);
                            }
                        }
                    }
                    else
                    {
                        paths.Add(p.Name);
                    }
                }
                _pathsForType[t] = paths;
            }

            // Always return a copy, because we don't want the original to be modified
            return new ThreadSafeHashSet<string>(_pathsForType[t]);
        }


        //static cache for EmployeeComponent paths
        private static ConcurrentDictionary<Type, ThreadSafeHashSet<string>> _terminalPathsForType = new ConcurrentDictionary<Type, ThreadSafeHashSet<string>>();
        public static ThreadSafeHashSet<string> GetAllTerminalPaths(Type t)
        {
            if (!t.IsSubclassOf(typeof(EmployeeComponent)))
            {
                throw new Exception("GetAllTerminalPaths.GetAllTerminalPaths(Type t) only works whe t is a subclass of EmployeeComponent");
            }

            if (!_terminalPathsForType.ContainsKey(t))
            {
                var paths = GetAllPaths(t);
                ThreadSafeHashSet<string> terminalPaths = new ThreadSafeHashSet<string>();
                foreach (string p in paths)
                {
                    if (paths.Any(x => x.CompareTo(p) != 0 && x.Contains(p)))
                        continue;

                    terminalPaths.Add(p);
                }
                _terminalPathsForType[t] = terminalPaths;
            }

            // Always return a copy, because we don't want the original to be modified
            return new ThreadSafeHashSet<string>(_terminalPathsForType[t]);
        }

        private static IEnumerable<string> GetPossibleKeyNames(PropertyInfo p)
        {
            Type dType = p.PropertyType.GetInterface(typeof(IDictionary<,>).Name);
            Type[] types = dType.GetGenericArguments();
            Type keyType = types[0];
            if (keyType.IsEnum)
            {
                var keys = Enum.GetValues(keyType);
                foreach (var key in keys)
                {
                    yield return key.ToString();
                }
            }
        }

        private static Type GetDictionaryValueType(PropertyInfo p)
        {
            Type dType = p.PropertyType.GetInterface(typeof(IDictionary<,>).Name);
            Type[] types = dType.GetGenericArguments();
            Type valueType = types[1];
            return valueType;
        }

        public static Member Parse(EmployeeComponent root, string path)
        {
            return Parse(root, path.Split('.'));
        }


        public static bool Set(EmployeeComponent root, string path, object value)
        {
            //root.ChangeTracking.Properties.Add(path, null);
            return Set(root, path.Split('.'), value);
        }




        protected static bool Set(object current, IEnumerable<string> descendants, object value)
        {
            if (!descendants.Any())
            {
                throw new Exception();
            }

            string nextProperty = descendants.ElementAt(0);
            descendants = descendants.Skip(1);


            if (current.GetType().GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
            {
                object key = GetKeyRepresentedByProperty(current, nextProperty);

                IDictionary d = current as IDictionary;
                if (!descendants.Any())
                {
                    if (d.Contains(key))
                    {
                        d[key] = value;
                    }
                    else
                    {
                        d.Add(key, value);
                    }
                    return true;
                }
                else
                {
                    var propertyValue = d[key];

                    if (propertyValue == null)
                    {
                        Type valueType = GetValueTypeOfDictionary(current);
                        ConstructorInfo ctor = valueType.GetConstructor(new Type[] { key.GetType() });
                        object newThing = ctor.Invoke(new object[] { key });
                        d[key] = newThing;
                        propertyValue = d[key];
                    }

                    return Set(propertyValue, descendants, value);
                }


            }
            else if (current is EmployeeComponent)
            {
                EmployeeComponent p = current as EmployeeComponent;

                var property = current.GetType().GetProperty(nextProperty);
                if (property == null)
                {
                    throw new Exception(string.Format("There is no property named '{0}' on the {1} class", nextProperty, current.GetType().Name));
                }

                //object oldValue = property.GetValue(current);
                //p.ChangeTracking.Properties.Add(nextProperty, oldValue);

                if (!descendants.Any())
                {
                    property.SetValue(current, value);
                    return true;
                }
                else
                {
                    //if (!property.PropertyType.IsSubclassOf(typeof(EmployeeComponent)))
                    //{
                    //    throw new Exception(string.Format("The only non-primitive types we expect to see on an EmployeeComponent are other EmployeeComponents. However, you're trying to set a value on a {0} that has a relationship to a {1}.", current.GetType().Name, property.PropertyType.Name));
                    //}

                    if (property.GetValue(current) == null)
                    {
                        ConstructorInfo ctor = property.PropertyType.GetConstructor(new Type[] { });
                        object newThing = ctor.Invoke(new object[] { });
                        property.SetValue(current, newThing);
                    }

                    var propertyValue = property.GetValue(current);
                    return Set(propertyValue, descendants, value);
                }
            }
            else
            {
                // we might want to set properties that are not tracked in ChangeTracking (like EmployeeMetaData)
                // simply sets the property value

                var property = current.GetType().GetProperty(nextProperty);
                if (property == null)
                {
                    throw new Exception(string.Format("There is no property named '{0}' on the {1} class", nextProperty, current.GetType().Name));
                }
                property.SetValue(current, value);
                return true;

                //if (property.GetValue(current) == null)
                //{
                //    ConstructorInfo ctor = property.PropertyType.GetConstructor(new Type[] { });
                //    object newThing = ctor.Invoke(new object[] { });
                //    property.SetValue(current, newThing);
                //}

                //var propertyValue = property.GetValue(current);
                //return Set(propertyValue, descendants, value);

            }
        }

        private static object GetKeyRepresentedByProperty(object current, string nextProperty)
        {
            Type dType = current.GetType().GetInterface(typeof(IDictionary<,>).Name);
            // Get the key represented by nextProperty
            Type[] types = dType.GetGenericArguments();
            Type keyType = types[0];
            object key;
            if (keyType.IsEnum)
            {
                key = Enum.Parse(keyType, nextProperty);
            }
            else if (keyType == typeof(int))
            {
                key = int.Parse(nextProperty);
            }
            else
            {
                throw new Exception(string.Format("EmployeeComponent.Set only handles dictionaries with keys that are enums or ints. Cannot parse key type: {0}", keyType.Name));
            }

            return key;
        }

        private static Type GetValueTypeOfDictionary(object current)
        {
            Type dType = current.GetType().GetInterface(typeof(IDictionary<,>).Name);
            // Get the key represented by nextProperty
            Type[] types = dType.GetGenericArguments();
            Type valueType = types[1];

            return valueType;
        }

        protected static Member Parse(object current, IEnumerable<string> descendants)
        {
            Member nothing = new Member(false, null);

            if (!descendants.Any())
            {
                return new Member(true, current);
            }
            else
            {
                if (current == null)
                {
                    return nothing;
                }
            }

            string nextProperty = descendants.ElementAt(0);
            descendants = descendants.Skip(1);


            if (current.GetType().GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
            {
                IDictionary d = current as IDictionary;
                object key = GetKeyRepresentedByProperty(current, nextProperty);



                if (!descendants.Any())
                {
                    if (d.Contains(key))
                    {
                        return new Member(true, d[key]);
                    }
                    else
                    {
                        return nothing;
                    }
                }
                else
                {
                    var propertyValue = d[key];
                    return Parse(propertyValue, descendants);
                }




            }
            else if (current is EmployeeComponent)
            {
                EmployeeComponent p = current as EmployeeComponent;
                if (p.ChangeTracking.Contains(nextProperty))
                {
                    var property = current.GetType().GetProperty(nextProperty);
                    if (property == null)
                    {
                        throw new Exception(string.Format("There is no property named '{0}' on the {1} class", nextProperty, current.GetType().Name));
                    }

                    var propertyValue = property.GetValue(current);
                    return Parse(propertyValue, descendants);
                }
                else
                {
                    return nothing;
                }

            }
            else
            {
                // string
                // int
                // bool
                // date
                // decimal?
                return new Member(true, current);
            }


        }

    }
}
