using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class PatientMessageConfiguration
    {
        int patientMessageID = 0;
        public int PatientMessageID
        {
            get { return patientMessageID; }
            set { patientMessageID = value; }
        }

        string patientMessageKey = string.Empty;
        public string PatientMessageKey
        {
            get { return patientMessageKey; }
            set { patientMessageKey = value; }
        }

        string patientMessageValue = string.Empty;
        public string PatientMessageValue
        {
            get { return patientMessageValue; }
            set { patientMessageValue = value; }
        }

    }
}
