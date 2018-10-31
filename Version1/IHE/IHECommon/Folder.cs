using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class Folder : SubmitObjectBase
    {
        private int folderID;

        public int FolderID
        {
            get { return folderID; }
            set { folderID = value; }
        }

        private DateTime lastUpdateTime;

        public DateTime LastUpdateTime
        {
            get { return lastUpdateTime; }
            set { lastUpdateTime = value; }
        }

        private List<DocumentEntry> documentList;

        public List<DocumentEntry> DocumentList
        {
            get { return documentList; }
            set { documentList = value; }
        }

        private List<CodeValue> codeList;

        public List<CodeValue> CodeList
        {
            get { return codeList; }
            set { codeList = value; }
        }

        private Patient patient;

        public Patient FolderPatient
        {
            get { return patient; }
            set { patient = value; }
        }

        private string folderXml;

        public string FolderXml
        {
            get { return folderXml; }
            set { folderXml = value; }
        }

        private string associationXml;
        public string AssociationXml
        {
            get { return associationXml; }
            set { associationXml = value; }
        }

        //private string sourceObjectID;
        //public string SourceObjectID
        //{
        //    get { return sourceObjectID; }
        //    set { sourceObjectID = value; }
        //}

        //private string targetObjectID;
        //public string TargetObjectID
        //{
        //    get { return targetObjectID; }
        //    set { targetObjectID = value; }
        //}

        //private string documentEntryEntryUUID;
        //public string DocumentEntryEntryUUID
        //{
        //    get { return documentEntryEntryUUID; }
        //    set { documentEntryEntryUUID = value; }
        //}

        //private string documentEntryUniqueID;
        //public string DocumentEntryUniqueID
        //{
        //    get { return documentEntryUniqueID; }
        //    set { documentEntryUniqueID = value; }
        //}

    }
}
