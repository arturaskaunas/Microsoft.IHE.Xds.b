using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class RegistryLog
    {
        private string requesterIdentity;
        private string requestMetadata;
        private string transactionName;
        private DateTime startTime;
        private DateTime finishTime;
        private string result;
        private int submissionSetID;

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
        public string RequesterIdentity
        {
            get
            {
                return requesterIdentity;
            }
            set
            {
                requesterIdentity = value;
            }

        }
        public string RequestMetadata
        {
            get
            {
                return requestMetadata;
            }
            set
            {
                requestMetadata = value;
            }

        }
        public string TransactionName
        {
            get
            {
                return transactionName;
            }
            set
            {
                transactionName = value;
            }

        }

        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
            }

        }

        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }

        }

        public DateTime FinishTime
        {
            get
            {
                return finishTime;
            }
            set
            {
                finishTime = value;
            }

        }

    }
}
