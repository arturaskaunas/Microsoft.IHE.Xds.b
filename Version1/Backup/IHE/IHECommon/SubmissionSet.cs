using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class SubmissionSet : SubmitObjectBase
    {
        private List<Author> submissionAuthor;

        public List<Author> SubmissionAuthor
        {
            get { return submissionAuthor; }
            set { submissionAuthor = value; }
        }

        private CodeValue contentType;

        public CodeValue ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        private Patient submissionpatient;

        public Patient SubmissionPatient
        {
            get { return submissionpatient; }
            set { submissionpatient = value; }
        }

        private DateTime submissionTime;

        public DateTime SubmissionTime
        {
            get { return submissionTime; }
            set { submissionTime = value; }
        }

        private List<Folder> folderList;

        public List<Folder> FolderList
        {
            get { return folderList; }
            set { folderList = value; }
        }

        private List<DocumentEntry> documentList;

        public List<DocumentEntry> DocumentList
        {
            get { return documentList; }
            set { documentList = value; }
        }

        private string associationXml;

        public string AssociationXml
        {
            get { return associationXml; }
            set { associationXml = value; }
        }

        private string sourceID;

        public string SourceID
        {
            get
            {
                return sourceID;
            }
            set
            {
                sourceID = value;
            }
        }

        private string submissionSetXml;

        public string SubmissionSetXml
        {
            get { return submissionSetXml; }
            set { submissionSetXml = value; }
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


