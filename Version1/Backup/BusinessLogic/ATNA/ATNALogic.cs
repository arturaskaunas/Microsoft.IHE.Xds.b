using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Xml.Serialization;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;

namespace Microsoft.IHE.XDS.BusinessLogic.ATNA
{
    public class ATNALogic
    {
        public AuditMessage DeserializeAuditMessage(string auditMessageXml)
        {
            AuditMessage auditMsg = null;
            XmlSerializer xmlSerializer = null;
            byte[] bytes = Encoding.UTF8.GetBytes(auditMessageXml);

            xmlSerializer = new XmlSerializer(typeof(AuditMessage));

            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
            auditMsg = (AuditMessage)xmlSerializer.Deserialize(ms);

            return auditMsg;
        }

        public string SerializeAuditMessage(AuditMessage auditMessage)
        {
            string message = null;
            XmlSerializer xmlSerializer = null;
            System.IO.MemoryStream stream = null;
            byte[] bytes;

            xmlSerializer = new XmlSerializer(typeof(AuditMessage));
            stream = new System.IO.MemoryStream();
            xmlSerializer.Serialize(stream, auditMessage);
            bytes = stream.ToArray();
            message = Encoding.UTF8.GetString(bytes);

            bytes = null;
            stream.Close();
            stream = null;
            xmlSerializer = null;

            return message;
        }

        public AuditMessageConfiguration GetAuditMessageConfigurationDetails(string messageKey)
        {
            ATNADataAccess atnaDal = new ATNADataAccess();
            return atnaDal.GetAuditMessageConfigurationDetails(messageKey);
        }


        public void ProcessEvent(string messageKey, string xdsType, string eventOutcomeIndicator, string appName)
        {
            string atnaMessage = string.Empty;
            ATNA.ATNAEvent atnaEvent = null;         
            AuditMessageConfiguration auditMsgConfig = null;
            ATNADataAccess atnaDAL = null;

            atnaDAL = new ATNADataAccess();
            auditMsgConfig = atnaDAL.GetAuditMessageConfigurationDetails(messageKey);

            atnaEvent = new ATNA.ATNAEvent();
            atnaEvent.ProcessEvent(auditMsgConfig, xdsType, eventOutcomeIndicator, appName);
        }

        public void ProcessEvent(AuditMessageConfiguration auditMsgConfig, string xdsType, string eventOutcomeIndicator, string appName)
        {
            string atnaMessage = string.Empty;
            ATNA.ATNAEvent atnaEvent = null;

            atnaEvent = new ATNA.ATNAEvent();
            atnaEvent.ProcessEvent(auditMsgConfig, xdsType, eventOutcomeIndicator, appName);
        }


        public static AuditMessageConfiguration SetParameterValue(AuditMessageConfiguration auditMsgConfig, string parameterName, string parameterValue)
        {
            AuditMessageParameterConfiguration paramConfig = auditMsgConfig.Parameters.Find(
                delegate(AuditMessageParameterConfiguration parameterConfig)
                {
                    if (parameterConfig.ParameterName == parameterName)
                        return true;

                    return false;
                }
            );

            if (paramConfig != null)
                paramConfig.ParameterValue = parameterValue;

            return auditMsgConfig;
        }

    }
}
