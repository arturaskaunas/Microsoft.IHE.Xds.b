using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class AuditMessageParameterConfiguration
    {
        int parameterID;
        int auditMessageID;
        string parameterName;
        string parameterType;
        string parameterValue;

        public AuditMessageParameterConfiguration()
        {
            //
        }

        public AuditMessageParameterConfiguration(int paramID)
        {
            parameterID = paramID;
        }


        public int ParameterID
        {
            get { return parameterID; }
        }

        public int AuditMessageID
        {
            get { return auditMessageID; }
            set { auditMessageID = value; }
        }

        public string ParameterName
        {
            get { return parameterName; }
            set { parameterName = value; }
        }

        public string ParameterType
        {
            get { return parameterType; }
            set { parameterType = value; }
        }

        public string ParameterValue
        {
            get { return parameterValue; }
            set { parameterValue = value; }
        }

    }
}
