using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Microsoft.IHE.XDS.Common
{
    public enum PatientIdentityFeedResultCode
    {
        SUCCESS,
        FAILURE,
        PATIENT_ALREADY_EXISTS,
        PATIENT_NOT_FOUND
    }

    public class PatientIdentityFeedRecord
    {
        static readonly string PATIENT_ROOT_EXTENSION_SEPARATOR = "^";        
        static string PATIENT_UID = string.Empty;

        static PatientIdentityFeedRecord()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PATIENT_ROOT_EXTENSION_SEPARATOR"]))
            {
                PATIENT_ROOT_EXTENSION_SEPARATOR = ConfigurationManager.AppSettings["PATIENT_ROOT_EXTENSION_SEPARATOR"];
                PATIENT_UID = PATIENT_ROOT_EXTENSION_SEPARATOR;
            }


        }

        string root = string.Empty;
        public string Root
        {
            get
            {
                return root;
            }
            set
            {
                if (value != null)
                    root = value;
                else
                    root = string.Empty;
            }
        }

        string extension = string.Empty;
        public string Extension
        {
            get
            {
                return extension;
            }
            set
            {
                if (value != null)
                    extension = value;
                else
                    extension = string.Empty;
            }
        }

        string patientUID = string.Empty;
        public string PatientUID
        {
            get
            {
                return string.Format(PATIENT_UID, extension, root);
            }
        }

        int patientID = 0;
        public int PatientID
        {
            get
            {
                return patientID;
            }
            set
            {
                patientID = value;
            }
        }

        PatientIdentityFeedResultCode resultCode = PatientIdentityFeedResultCode.SUCCESS;
        public PatientIdentityFeedResultCode ResultCode
        {
            get
            {
                return resultCode;
            }
            set
            {
                resultCode = value;
            }
        }


    }
}
