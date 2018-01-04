using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.ChangeTracking
{
    public class ContactData : EmployeeComponent
    {
        private Dictionary<EmailAddressType, EmailAddressData> _emailAddressData;
        public Dictionary<EmailAddressType, EmailAddressData> EmailAddressData
        {
            get { return _emailAddressData; }
            set { SetProperty(ref _emailAddressData, value); }
        }

        //private Dictionary<PhoneNumberType, PhoneNumberData> _phoneNumberData;
        //public Dictionary<PhoneNumberType, PhoneNumberData> PhoneNumberData
        //{
        //    get { return _phoneNumberData; }
        //    set { SetProperty(ref _phoneNumberData, value); }
        //}

        //private Dictionary<AddressType, AddressData> _addressData;
        //public Dictionary<AddressType, AddressData> AddressData
        //{
        //    get { return _addressData; }
        //    set { SetProperty(ref _addressData, value); }
        //}

        public ContactData()
        {
        }
    }
}
