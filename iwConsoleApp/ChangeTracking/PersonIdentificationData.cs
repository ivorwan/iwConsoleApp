using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.ChangeTracking
{
    public class PersonIdentificationData : EmployeeComponent
    {
        private string _encryptedDOB;
        public string EncryptedDOB
        {
            get { return _encryptedDOB; }
            set { SetProperty(ref _encryptedDOB, value); }
        }

        private string _encryptedSSN;
        public string EncryptedSSN
        {
            get { return _encryptedSSN; }
            set { SetProperty(ref _encryptedSSN, value); }
        }

        private string _initializationVector;
        public string InitializationVector
        {
            get { return _initializationVector; }
            set { SetProperty(ref _initializationVector, value); }
        }

        private int? _version;
        public int? Version
        {
            get { return _version; }
            set { SetProperty(ref _version, value); }
        }

        public PersonIdentificationData()
        {
        }

    }
}
