using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.ChangeTracking
{
    public class NameData : EmployeeComponent
    {
        private string _prefix;
        public string Prefix
        {
            get { return _prefix; }
            set { SetProperty(ref _prefix, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _middleName;
        public string MiddleName
        {
            get { return _middleName; }
            set { SetProperty(ref _middleName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _suffix;
        public string Suffix
        {
            get { return _suffix; }
            set { SetProperty(ref _suffix, value); }
        }

        public NameData()
        {
        }
    }
}
