using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class RegistryXmlEntries
    {
        int patientID = 0;
        public int PatientID
        {
            get { return patientID; }
            set { patientID = value; }
        }

        string patientUID = string.Empty;
        public string PatientUID
        {
            get { return patientUID; }
            set { patientUID = value; }
        }

        List<DocumentEntry> documentEntryList = null;
        public List<DocumentEntry> DocumentEntryList
        {
            get
            {
                if (documentEntryList == null)
                    documentEntryList = new List<DocumentEntry>();

                return documentEntryList;
            }
            set
            {
                documentEntryList = value;
            }
        }

        List<Folder> folderList = null;
        public List<Folder> FolderList
        {
            get
            {
                if (folderList == null)
                    folderList = new List<Folder>();

                return folderList;
            }
            set
            {
                folderList = value;
            }
        }

        List<SubmissionSet> submissionSetList = null;
        public List<SubmissionSet> SubmissionSetList
        {
            get
            {
                if (submissionSetList == null)
                    submissionSetList = new List<SubmissionSet>();

                return submissionSetList;
            }
            set
            {
                submissionSetList = value;
            }
        }

    }
}
