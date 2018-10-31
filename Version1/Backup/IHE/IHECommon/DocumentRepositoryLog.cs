using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class DocumentRepositoryLog
    {
        private int logId;

        public int LogID
        {
            get { return logId; }
            set { logId = value; }
        }
        private string requesterIdentity;

        public string RequesterIdentity
        {
            get { return requesterIdentity; }
            set { requesterIdentity = value; }
        }
        private string requestMetadata;

        public string RequestMetadata
        {
            get { return requestMetadata; }
            set { requestMetadata = value; }
        }
        private DateTime startTime = DateTime.Now;

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        private DateTime endTime = DateTime.Now;

        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        private string result;

        public string Result
        {
            get { return result; }
            set { result = value; }
        }
        private string transaction;

        public string Transaction
        {
            get { return transaction; }
            set { transaction = value; }
        }

    }
}
