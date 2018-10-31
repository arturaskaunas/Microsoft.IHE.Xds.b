using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class XDSError
    {

        string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        string requestId;
        public string RequestId
        {
            get { return requestId; }
            set { requestId = value; }
        }

        string codeContext;
        public string CodeContext
        {
            get { return codeContext; }
            set { codeContext = value; }
        }

        string errorCode;
        public string ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }

        string severity;
        public string Severity
        {
            get { return severity; }
            set { severity = value; }
        }

        string location;
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

    }
}
