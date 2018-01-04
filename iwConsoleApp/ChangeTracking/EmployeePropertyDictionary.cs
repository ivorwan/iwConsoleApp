using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.ChangeTracking
{
    public interface IEmployeePropertyDictionaryValue
    {
        int GetEmployeePropertyKey();
    }

    public class EmployeePropertyDictionary<V> : Dictionary<int, V>
        where V : class, IEmployeePropertyDictionaryValue
    {
        //private Dictionary<int, V> Dictionary = new Dictionary<int, V>();
        //private string DictionaryName { get; set; }

        //public EmployeePropertyDictionary(string dictionaryName)
        //{
        //    DictionaryName = dictionaryName;
        //}

        public EmployeePropertyDictionary()
        {

        }

        public EmployeePropertyDictionary(IDictionary<int, V> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                EnsureCompatibleKeyAndValue(kvp.Key, kvp.Value);
                this.Add(kvp.Key, kvp.Value);
            }
        }

        public EmployeePropertyDictionary(IDictionary<int, V> dictionary, IEqualityComparer<int> comparer) : base(comparer)
        {
            foreach (var kvp in dictionary)
            {
                EnsureCompatibleKeyAndValue(kvp.Key, kvp.Value);
                this.Add(kvp.Key, kvp.Value);
            }
        }

        // using "new" keyword because we want to hide the base implementation
        public new V this[int key]
        {
            get
            {
                return base[key];
            }

            set
            {
                EnsureCompatibleKeyAndValue(key, value);
                base[key] = value;
            }
        }

        public void AddKey(int key)
        {
            this.Add(key, null);
        }

        // using "new" keyword because we want to hide the base implementation
        public new void Add(int key, V value)
        {
            EnsureCompatibleKeyAndValue(key, value);
            base.Add(key, value);
        }

        public void Add(V value)
        {
            this.Add(value.GetEmployeePropertyKey(), value);
        }

        //public string GetPropertyName(int propertyKey)
        //{
        //    return DictionaryName + "." + propertyKey.ToString();
        //}

        //public int GetPropertyKeyFromPropertyName(string propertyName)
        //{
        //    int id;
        //    if (int.TryParse(propertyName.Substring(propertyName.IndexOf(DictionaryName) + 1), out id))
        //    {
        //        return id;
        //    }
        //    return 0;
        //}

        //public bool IsChanged()
        //{
        //    return Dictionary.Values.Any(x => x.IsChanged());
        //}

        private static void EnsureCompatibleKeyAndValue(int key, V value)
        {
            if (value != null && !key.Equals(value.GetEmployeePropertyKey()))
            {
                throw new EmployeePropertyDictionaryMismatchedKeyException
                    (
                        string.Format("You tried to add a value of type '{0}' with a key of type '{1}'. " +
                                      "The values and keys of EmployeePropertyDicitonary must be the same.",
                                      value.GetEmployeePropertyKey(),
                                      key)
                    );
            }
        }

        //public bool IsChanged(K key)
        //{
        //    if (Dictionary.ContainsKey(key))
        //    {
        //        return Dictionary[key].IsChanged();
        //    }
        //    return false;
        //}

        //public bool IsInitialized(K key)
        //{
        //    if (Dictionary.ContainsKey(key))
        //    {
        //        return Dictionary[key].IsInitialized();
        //    }
        //    return false;
        //}

        //public bool HasNonNullValueByKey(K key)
        //{
        //    if (Dictionary.ContainsKey(key))
        //    {
        //        var value = Dictionary[key].GetValue();
        //        return (value != null);
        //    }
        //    return false;
        //}

        //public void SetValue(V value)
        //{
        //    K key = value.GetGenericPropertyDictionaryKey();
        //    if (!Dictionary.ContainsKey(key))
        //    {
        //        AddKey(key);
        //    }
        //    Dictionary[key].SetValue(value);
        //}

        //public void Remove(K key)
        //{
        //    if (Dictionary.ContainsKey(key))
        //    {
        //        Dictionary[key].SetValue(null);
        //    }
        //}
    }

    public class EmployeePropertyDictionaryMismatchedKeyException : Exception
    {
        public EmployeePropertyDictionaryMismatchedKeyException(string msg) : base(msg)
        {
        }
    }
}
