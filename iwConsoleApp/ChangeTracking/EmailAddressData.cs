using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.ChangeTracking
{
    public class EmailAddressData : EmployeeComponent, IEquatable<EmailAddressData>
    {
        public EmailAddressType Type { get; private set; }

        private string _emailAddress;
        public string EmailAddress
        {
            get { return _emailAddress; }
            set { SetProperty(ref _emailAddress, value); } //, Type + ".EmailAddress"); }
        }

        public EmailAddressData(EmailAddressType type)
        {
            Type = type;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            EmailAddressData other = obj as EmailAddressData;

            return Equals(other);
        }

        //Equating only on Fields for this class Type
        public virtual bool Equals(EmailAddressData other)
        {
            if (other == null)
                return false;

            Type t = GetType();
            Type otherType = other.GetType();

            if (t != otherType)
                return false;

            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (FieldInfo field in fields)
            {
                object value1 = field.GetValue(other);
                object value2 = field.GetValue(this);

                if (!TwoObjectsEqual(value1, value2))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            int startValue = 17;
            int multiplier = 59;

            int hashCode = startValue;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                    hashCode = hashCode * multiplier + value.GetHashCode();
            }

            return hashCode;
        }

        private bool TwoObjectsEqual(object value1, object value2)
        {
            if (value1 == null)
            {
                if (value2 != null)
                    return false;
            }
            else if (value1.GetType().IsGenericType && value1.GetType().GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                if (value2.GetType().IsGenericType && value2.GetType().GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    IDictionary d1 = value1 as IDictionary;
                    IDictionary d2 = value2 as IDictionary;

                    if (d1.Count != d2.Count)
                    {
                        return false;
                    }

                    foreach (var key in d1.Keys)
                    {
                        if (!d2.Contains(key))
                        {
                            return false;
                        }
                        else if (!d2[key].Equals(d1[key]))
                        {
                            return false;
                        }
                    }
                }
            }
            else if (value1.GetType().IsGenericType && value1.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                if (value2.GetType().IsGenericType && value2.GetType().GetGenericTypeDefinition() == typeof(List<>))
                {
                    IList list1 = value1 as IList;
                    IList list2 = value2 as IList;

                    if (list1.Count != list2.Count)
                    {
                        return false;
                    }

                    foreach (var v in list1)
                    {
                        if (!list2.Contains(v))
                        {
                            return false;
                        }
                    }
                }
            }
            else if (!value1.Equals(value2))
            {
                return false;
            }

            return true;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            Type t = GetType();

            List<FieldInfo> fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));

                t = t.BaseType;
            }

            return fields;
        }

    }
}
