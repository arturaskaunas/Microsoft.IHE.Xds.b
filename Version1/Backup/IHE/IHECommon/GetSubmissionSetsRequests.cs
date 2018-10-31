using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class GetSubmissionSetsRequest
    {

        int submissionSetID = 0;
        public int SubmissionSetID
        {
            get
            {
                return submissionSetID;
            }
            set
            {
                submissionSetID = value;
            }
        }

        int submissionSetDocumentFolderID = 0;
        public int SubmissionSetDocumentFolderID
        {
            get
            {
                return submissionSetDocumentFolderID;
            }
            set
            {
                submissionSetDocumentFolderID = value;
            }
        }


        string submissionSetXml = string.Empty;
        public string SubmissionSetXml
        {
            get
            {
                return submissionSetXml;
            }
            set
            {
                submissionSetXml = value;
            }
        }


        string associationXml = string.Empty;
        public string AssociationXml
        {
            get
            {
                return associationXml;
            }
            set
            {
                associationXml = value;
            }
        }

    }
}
