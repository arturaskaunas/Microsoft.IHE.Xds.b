using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

using Microsoft.IHE.XDS.Common;

namespace Microsoft.IHE.XDS.BusinessLogic.ATNA
{
    public class ATNAEvent
    {

        public static readonly string DATETIME_FORMAT = "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz";
        public static readonly string XDSREGISTRY_TYPE = "XDSRegistry";
        public static readonly string XDSREPOSITORY_TYPE = "XDSRepository";
        public static readonly string XDSREPOSITORY_SUBMISSIONSET_CLASSIFICATIONNODE_UUID = "urn:uuid:a54d6aa5-d40d-43f9-88c5-b4633d873bdd";
        public static readonly string XDSREPOSITORY_DOCUMENT_CLASSIFICATIONNODE_UUID = "urn:uuid:7edca82f-054d-47f2-a032-9b2a5b5186c1";
        public static readonly string UDP_TAG_APPNAME_REPOSITORY = "Repository";
        public static readonly string UDP_TAG_APPNAME_REGISTRY = "Registry";

        public static readonly string SYSLOG_SERVER = string.Empty;
        public static readonly int SYSLOG_PORT;
        public static readonly string XDSREPOSITORY_SERVICE_ADDRESS = string.Empty;
        public static readonly string XDSREGISTRY_SERVICE_ADDRESS = string.Empty;




        static ATNAEvent()
        {
            string sysLogPort = null;
                        
            SYSLOG_SERVER = ConfigurationManager.AppSettings.Get("SYSLOG_SERVER");
            sysLogPort = ConfigurationManager.AppSettings.Get("SYSLOG_PORT");

            if (string.IsNullOrEmpty(sysLogPort))
                throw new ArgumentException("SYSLOG PORT cannot be null or empty!");

            if (!int.TryParse(sysLogPort, out SYSLOG_PORT))
                throw new ArgumentException("SYSLOG PORT should be numeric!");

            XDSREPOSITORY_SERVICE_ADDRESS = ConfigurationManager.AppSettings.Get("XDSREPOSITORY_SERVICE_ADDRESS");

            if (XDSREPOSITORY_SERVICE_ADDRESS == null)
                XDSREPOSITORY_SERVICE_ADDRESS = string.Empty;

            XDSREGISTRY_SERVICE_ADDRESS = ConfigurationManager.AppSettings.Get("XDSREGISTRY_SERVICE_ADDRESS");

            if (XDSREGISTRY_SERVICE_ADDRESS == null)
                XDSREGISTRY_SERVICE_ADDRESS = string.Empty;

        }

        public string GetParameterValue(AuditMessageConfiguration auditMsgConfig, string parameterName)
        {
            string parameterValue = string.Empty;

            AuditMessageParameterConfiguration paramConfig = auditMsgConfig.Parameters.Find(
                delegate(AuditMessageParameterConfiguration parameterConfig)
                {
                    if (parameterConfig.ParameterName == parameterName)
                        return true;

                    return false;
                }
            );

            if (paramConfig != null)
                parameterValue = paramConfig.ParameterValue;

            return parameterValue;
        }

        public string GetAuditSourceID(string xdsType)
        {
            return xdsType + "@" + Environment.MachineName;
        }

        public string UpdateXMLWithDefaultParameterValues(string auditMessageXml, string xdsType, string eventOutcomeIndicator)
        {
            if (string.IsNullOrEmpty(auditMessageXml))
                throw new ArgumentException("Parameter auditMessageXml cannot be null or empty.");

            if (string.IsNullOrEmpty(xdsType))
                throw new ArgumentException("Parameter xdsType cannot be null or empty.");

            //$EventIdentification.EventDateTime$
            auditMessageXml = auditMessageXml.Replace("$EventIdentification.EventDateTime$", DateTime.Now.ToUniversalTime().ToString(DATETIME_FORMAT));

            //$EventIdentification.EventOutcomeIndicator$
            auditMessageXml = auditMessageXml.Replace("$EventIdentification.EventOutcomeIndicator$", eventOutcomeIndicator);

            //User ID is the URI of the webservice endpoint
            //$ActiveParticipant.UserID$
            if(xdsType == XDSREPOSITORY_TYPE)
                auditMessageXml = auditMessageXml.Replace("$ActiveParticipant.UserID$", XDSREPOSITORY_SERVICE_ADDRESS);
            else
                auditMessageXml = auditMessageXml.Replace("$ActiveParticipant.UserID$", XDSREGISTRY_SERVICE_ADDRESS);

            //AuditSourceID can be the name of the service and the affinity domain.
            //$AuditSourceIdentification.AuditSourceID$
            auditMessageXml = auditMessageXml.Replace("$AuditSourceIdentification.AuditSourceID$", GetAuditSourceID(xdsType));

            return auditMessageXml;
        }

        public void ProcessEvent(AuditMessageConfiguration auditMsgConfig, string xdsType, string eventOutcomeIndicator, string appName)
        {
            string auditMessageXml = string.Empty;
            string pri = "13";
            string timeStamp = string.Empty;
            string hostName = string.Empty;
            
            if (auditMsgConfig == null)
                throw new ArgumentNullException("auditMsgConfig");

            if (auditMsgConfig.Parameters == null)
                throw new Exception("Audit message parameters cannot be null!");

            auditMessageXml = auditMsgConfig.MessageValue;

            auditMessageXml = UpdateXMLWithDefaultParameterValues(auditMessageXml, xdsType, eventOutcomeIndicator);

            for (int count = 0; count < auditMsgConfig.Parameters.Count; count++)
            {
                auditMessageXml = auditMessageXml.Replace(auditMsgConfig.Parameters[count].ParameterName, auditMsgConfig.Parameters[count].ParameterValue);
            }

            timeStamp = GetTimeStamp();
            hostName = System.Net.Dns.GetHostName();

            auditMessageXml = String.Format(@"<{0}>{1} {2} {3}:{4}", pri, timeStamp, hostName, appName, auditMessageXml);

            BSDSysLogUDP.BSDSysLogAppend(auditMessageXml, SYSLOG_SERVER, SYSLOG_PORT);
        }

        private static string GetTimeStamp()
        {
            string timestamp = string.Empty; ;
            DateTime currentDate = DateTime.Now;
            
            if (currentDate.Day >= 10)
                timestamp = currentDate.ToString("MMM d HH:mm:ss");
            else
                timestamp = currentDate.ToString("MMM  d HH:mm:ss");

            return timestamp;
        }


    }
}
