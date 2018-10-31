using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using Microsoft.IHE.XDS.Common;

namespace Microsoft.IHE.XDS.DataAccess
{
    public class ATNADataAccess
    {

        private string CONST_CONNECTIONSTRING_NAME = "ATNA";

        public AuditMessageConfiguration GetAuditMessageConfigurationDetails(string messageKey)
        {
            IDataReader dbReader = null;
            AuditMessageConfiguration auditMessage = null;
            AuditMessageParameterConfiguration auditMsgParameter = null;


            Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("usp_get_auditMessageConfigurationDetails");

            dbIHEDB.AddInParameter(dbCommand, "messageKey", DbType.String, messageKey);

            using (dbReader = dbIHEDB.ExecuteReader(dbCommand))
            {
                while (dbReader.Read())
                {
                    if (auditMessage == null)
                    {
                        if (!dbReader.IsDBNull(dbReader.GetOrdinal("auditMessageID")))
                        {
                            auditMessage = new AuditMessageConfiguration(dbReader.GetInt32(dbReader.GetOrdinal("auditMessageID")));
                        }

                        if (!dbReader.IsDBNull(dbReader.GetOrdinal("messageKey")))
                        {
                            auditMessage.MessageKey = dbReader.GetString(dbReader.GetOrdinal("messageKey"));
                        }

                        if (!dbReader.IsDBNull(dbReader.GetOrdinal("messageValue")))
                        {
                            auditMessage.MessageValue = dbReader.GetString(dbReader.GetOrdinal("messageValue"));
                        }
                    }


                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("parameterID")))
                    {
                        auditMsgParameter = new AuditMessageParameterConfiguration(dbReader.GetInt32(dbReader.GetOrdinal("parameterID")));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("parameterName")))
                    {
                        auditMsgParameter.ParameterName = dbReader.GetString(dbReader.GetOrdinal("parameterName"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("parameterType")))
                    {
                        auditMsgParameter.ParameterType = dbReader.GetString(dbReader.GetOrdinal("parameterType"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("parameterValue")))
                    {
                        auditMsgParameter.ParameterValue = dbReader.GetString(dbReader.GetOrdinal("parameterValue"));
                    }

                    auditMessage.Parameters.Add(auditMsgParameter);
                }
            }

            return auditMessage;
        }

    }
}
