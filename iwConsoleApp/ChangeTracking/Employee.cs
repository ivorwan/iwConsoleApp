using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.ChangeTracking
{
    public class Employee : EmployeeComponent
    {
        public int Id { get; set; }

        private PersonData _personData;
        public PersonData PersonData
        {
            get { return _personData; }
            set { SetProperty(ref _personData, value); }
        }


        #region paths
        // Employee key fields
        public const string ID_PATH = "Id";
        public const string EFFECTIVE_DATE_PATH = "EffectiveDate";



        //Employee sub entities
        public const string DISPLAY_PREFERENCE_DATA_PATH = "DisplayPreferenceData";
        public const string EMPLOYEE_IDENTIFICATION_DATA_PATH = "EmployeeIdentificationData";
        public const string PERSON_DATA_PATH = "PersonData";
        public const string SETTINGS_DATA_PATH = "SettingsData";
        public const string WORKER_STATUS_DATA_PATH = "WorkerStatusData";

        public const string DIRECT_MANAGER_PATH = "DirectManager";
        public const string APPROVER_PATH = "Approver";
        public const string CLIENT_ACCOUNT_PATH = "ClientAccount";
        public const string COST_CENTER_PATH = "CostCenter";
        public const string DIVISION_PATH = "Division";
        public const string GRADE_PATH = "Grade";
        public const string GROUP_PATH = "Groups";
        public const string LEGAL_ENTITY_PATH = "LegalEntity";
        public const string LOCATION_PATH = "Location";
        public const string POSITION_PATH = "Position";
        public const string SELF_REG_GROUP_PATH = "SelfRegistrationGroup";
        public const string META_DATA_PATH = "EmployeeMetaData";

        // MetaData
        public const string CREATED_DATE_PATH = META_DATA_PATH + ".CreatedDate";
        public const string MODIFIED_DATE_PATH = META_DATA_PATH + ".ModifiedDate";

        // EmployeeIdentificationData sub entities
        public const string USERNAME_PATH = EMPLOYEE_IDENTIFICATION_DATA_PATH + ".Username";
        public const string USER_ID_PATH = EMPLOYEE_IDENTIFICATION_DATA_PATH + ".UserId";
        public const string LOCAL_SYSTEM_ID_PATH = EMPLOYEE_IDENTIFICATION_DATA_PATH + ".LocalSystemId";
        public const string SOURCE_PATH = EMPLOYEE_IDENTIFICATION_DATA_PATH + ".Source";
        public const string GUID_PATH = EMPLOYEE_IDENTIFICATION_DATA_PATH + ".Guid";





        // DisplayPreferenceData sub entities
        public const string PHOTO_PATH = "DisplayPreferenceData.Photo";
        public const string LANGUAGE_PATH = "DisplayPreferenceData.DisplayLanguage";
        public const string SIGNATURE_PATH = "DisplayPreferenceData.Signature";
        public const string TIMEZONE_PATH = "DisplayPreferenceData.TimeZone";

        // PersonData sub entities
        public const string CONTACT_DATA_PATH = PERSON_DATA_PATH + ".ContactData";
        public const string NAME_DATA_PATH = PERSON_DATA_PATH + ".NameData";
        public const string PERSON_IDENTIFICATION_DATA_PATH = PERSON_DATA_PATH + ".PersonIdentificationData";

        // ContactData sub entities
        public const string ADDRESS_DATA_PATH = CONTACT_DATA_PATH + ".AddressData";
        public const string EMAIL_ADDRESS_PATH = CONTACT_DATA_PATH + ".EmailAddressData.Primary.EmailAddress";
        public const string PERSONAL_EMAIL_ADDRESS_PATH = CONTACT_DATA_PATH + ".EmailAddressData.Personal.EmailAddress";
        public const string WORK_PHONE_NUMBER_PATH = CONTACT_DATA_PATH + ".PhoneNumberData.Work1.PhoneNumber";
        public const string HOME_PHONE_NUMBER_PATH = CONTACT_DATA_PATH + ".PhoneNumberData.Home1.PhoneNumber";
        public const string MOBILE_PHONE_NUMBER_PATH = CONTACT_DATA_PATH + ".PhoneNumberData.Mobile1.PhoneNumber";
        public const string FAX_PHONE_NUMBER_PATH = CONTACT_DATA_PATH + ".PhoneNumberData.Fax1.PhoneNumber";

        // AddressData sub entities
        public const string ADDRESS_LINE_1_PATH = ADDRESS_DATA_PATH + ".Primary.AddressLineData.Line1.AddressLine";
        public const string ADDRESS_LINE_2_PATH = ADDRESS_DATA_PATH + ".Primary.AddressLineData.Line2.AddressLine";
        public const string CITY_PATH = ADDRESS_DATA_PATH + ".Primary.Municipality";
        public const string STATE_PATH = ADDRESS_DATA_PATH + ".Primary.Region";
        public const string POSTAL_CODE_PATH = ADDRESS_DATA_PATH + ".Primary.PostalCode";
        public const string COUNTRY_PATH = ADDRESS_DATA_PATH + ".Primary.Country";
        public const string SUBMUNICIPALITY_PATH = ADDRESS_DATA_PATH + ".Primary.Submunicipality";
        public const string SUBREGION_PATH = ADDRESS_DATA_PATH + ".Primary.Subregion";
        public const string ACCURACY_CODE_PATH = ADDRESS_DATA_PATH + ".Primary.AccuracyCode";
        public const string GEOCODE_ID_PATH = ADDRESS_DATA_PATH + ".Primary.GeoCodeId";

        // NameData sub entities
        public const string FIRST_NAME_PATH = NAME_DATA_PATH + ".FirstName";
        public const string MIDDLE_NAME_PATH = NAME_DATA_PATH + ".MiddleName";
        public const string LAST_NAME_PATH = NAME_DATA_PATH + ".LastName";
        public const string PREFIX_PATH = NAME_DATA_PATH + ".Prefix";
        public const string SUFFIX_PATH = NAME_DATA_PATH + ".Suffix";

        // PersonIdentificationData sub entities
        public const string DOB_PATH = PERSON_IDENTIFICATION_DATA_PATH + ".EncryptedDOB";
        public const string SSN_PATH = PERSON_IDENTIFICATION_DATA_PATH + ".EncryptedSSN";
        public const string VERSION_PATH = PERSON_IDENTIFICATION_DATA_PATH + ".Version";
        public const string INITIALIZATION_VECTOR_PATH = PERSON_IDENTIFICATION_DATA_PATH + ".InitializationVector";

        // SettingsData sub entities
        public const string REQUIRED_APPROVALS_PATH = SETTINGS_DATA_PATH + ".RequiredApprovals";
        public const string ALLOW_RECONCILIATION_PATH = SETTINGS_DATA_PATH + ".AllowReconciliation";
        public const string IS_ANONYMIZED_PATH = SETTINGS_DATA_PATH + ".IsAnonymized";

        // WorkerStatusData sub entities        
        public const string STATUS_PATH = WORKER_STATUS_DATA_PATH + ".Status";
        public const string USER_TYPE_ID_PATH = WORKER_STATUS_DATA_PATH + ".UserTypeId";
        public const string ABSENT_PATH = WORKER_STATUS_DATA_PATH + ".Absent";
        public const string ORIGINAL_HIRE_DATE_PATH = WORKER_STATUS_DATA_PATH + ".OriginalHireDate";
        public const string LAST_HIRE_DATE_PATH = WORKER_STATUS_DATA_PATH + ".LastHireDate";
        public const string IS_REHIRED_EMPLOYEE_PATH = WORKER_STATUS_DATA_PATH + ".IsRehiredEmployee";
        public const string USER_CATEGORY_PATH = WORKER_STATUS_DATA_PATH + ".UserCategory";
        public const string USER_SUB_CATEGORY_PATH = WORKER_STATUS_DATA_PATH + ".UserSubCategory";
        public const string USER_EMPLOYMENT_STATUS_PATH = WORKER_STATUS_DATA_PATH + ".UserEmploymentStatus";
        public const string USER_LEAVE_REASON_PATH = WORKER_STATUS_DATA_PATH + ".LeaveStatusData.LeaveReason";
        public const string USER_LEAVE_START_DATE_PATH = WORKER_STATUS_DATA_PATH + ".LeaveStatusData.LeaveStartDate";
        public const string USER_LEAVE_END_DATE_PATH = WORKER_STATUS_DATA_PATH + ".LeaveStatusData.LeaveEndDate";
        public const string USER_TERMINATION_REASON_PATH = WORKER_STATUS_DATA_PATH + ".TerminationStatusData.TerminationReason";
        public const string USER_TERMINATION_DATE_PATH = WORKER_STATUS_DATA_PATH + ".TerminationStatusData.TerminationDate";
        public const string ELIGIBLE_FOR_REHIRE_PATH = WORKER_STATUS_DATA_PATH + ".TerminationStatusData.EligibleForRehire";



        // dynamic paths
        public static string DYNAMIC_USER_RELATION_PATH(int userRelationTypeId)
        {
            return "DynamicUserRelations." + userRelationTypeId.ToString();
        }
        public static string DYNAMIC_OU_PATH(int ouTypeId)
        {
            return "DynamicOUs." + ouTypeId.ToString();
        }
        public static string DYNAMIC_OU_PATH(int ouTypeId, int ouId)
        {
            return "DynamicOUs." + ouTypeId.ToString() + "." + ouId.ToString();
        }
        public static string CUSTOM_FIELD_PATH(int customFieldId)
        {
            return "CustomFields." + customFieldId.ToString();
        }
        #endregion
    }
}
