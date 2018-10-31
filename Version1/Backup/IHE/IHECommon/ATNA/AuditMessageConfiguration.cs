using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class AuditMessageConfiguration
    {        
        int auditMessageID;
        string messageKey;
        string messageValue;
        List<AuditMessageParameterConfiguration> lstAuditMessageParameter;

        public AuditMessageConfiguration()
        {
            //
        }

        public AuditMessageConfiguration(int auditMsgID)
        {
            auditMessageID = auditMsgID;
        }

        public int AuditMessageID
        {
            get { return auditMessageID; }
        }

        public string MessageKey
        {
            get { return messageKey; }
            set { messageKey = value; }
        }

        public string MessageValue
        {
            get { return messageValue; }
            set { messageValue = value; }
        }

        public List<AuditMessageParameterConfiguration> Parameters
        {
            get 
            {
                if (lstAuditMessageParameter == null)
                    lstAuditMessageParameter = new List<AuditMessageParameterConfiguration>();

                return lstAuditMessageParameter; 
            }
            set { lstAuditMessageParameter = value; }
        }

    }
}
