using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class PatientDuplicateEntry
    {
        PatientIdentityFeedRecord newPatient = null;
        public PatientIdentityFeedRecord NewPatient
        {
            get 
            {
                if (newPatient == null)
                    newPatient = new PatientIdentityFeedRecord();

                return newPatient; 
            }
            set { newPatient = value; }
        }

        List<PatientIdentityFeedRecord> oldPatientList = null;
        public List<PatientIdentityFeedRecord> OldPatientList
        {
            get 
            {
                if (oldPatientList == null)
                    oldPatientList = new List<PatientIdentityFeedRecord>();

                return oldPatientList; 
            }
            set { oldPatientList = value; }            
        }

    }
}
