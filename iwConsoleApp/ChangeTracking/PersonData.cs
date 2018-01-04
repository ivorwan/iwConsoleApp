using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.ChangeTracking
{
    public class PersonData : EmployeeComponent
    {
        private PersonIdentificationData _personIdentificationData;
        public PersonIdentificationData PersonIdentificationData
        {
            get { return _personIdentificationData; }
            set { SetProperty(ref _personIdentificationData, value); }
        }

        private NameData _nameData;
        public NameData NameData
        {
            get { return _nameData; }
            set { SetProperty(ref _nameData, value); }
        }

        private ContactData _contactData;
        public ContactData ContactData
        {
            get { return _contactData; }
            set { SetProperty(ref _contactData, value); }
        }

        public PersonData()
        {
        }
    }
}
