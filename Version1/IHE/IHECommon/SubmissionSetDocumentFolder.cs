using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class SubmissionSetDocumentFolder: SubmitObjectBase
    {
        private int submissionSetDocumentFolderID;
        public int SubmissionSetDocumentFolderID
        {
            get { return submissionSetDocumentFolderID; }
            set { submissionSetDocumentFolderID = value; } 
        }

        private int submissionSetID;
        public int SubmissionSetID
        {
            get { return submissionSetID; }
            set { submissionSetID = value; }
        }

        private int folderID;
        public int FolderID
        {
            get { return folderID; }
            set { folderID = value; }
        }

        private int documentEntryID;
        public int DocumentEntryID
        {
            get { return documentEntryID; }
            set { documentEntryID = value; }
        }

        private string sourceObject;
        public string SourceObject
        {
            get { return sourceObject; }
            set { sourceObject = value; }
        }

        private string targetObject;
        public string TargetObject
        {
            get { return targetObject; }
            set { targetObject = value; }
        }

        private string associationXml;
        public string AssociationXml
        {
            get { return associationXml; }
            set { associationXml = value; }
        }


    }
}
