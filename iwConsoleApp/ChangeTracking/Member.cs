using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.ChangeTracking
{
    public class Member
    {
        public bool IsInitialized { get; private set; }
        public object Value { get; private set; }

        public Member(bool isInitialized, object value)
        {
            IsInitialized = isInitialized;
            Value = value;
        }
    }
}
