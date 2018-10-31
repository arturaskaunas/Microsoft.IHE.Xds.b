using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class SubmissionSetAssociation
    {
        private List<SubmissionSet> submissionSets;
        public List<SubmissionSet> SubmissionSets
        {
            get 
            {
                if (submissionSets == null)
                    submissionSets = new List<SubmissionSet>();

                return submissionSets; 
            }
            set { submissionSets = value; }
        }

        private List<SubmissionSetDocumentFolder> submissionSetDocumentFolders;
        public List<SubmissionSetDocumentFolder> SubmissionSetDocumentFolders
        {
            get 
            {
                if (submissionSetDocumentFolders == null)
                    submissionSetDocumentFolders = new List<SubmissionSetDocumentFolder>();

                return submissionSetDocumentFolders; 
            }
            set { submissionSetDocumentFolders = value; }
        }

    }
}
