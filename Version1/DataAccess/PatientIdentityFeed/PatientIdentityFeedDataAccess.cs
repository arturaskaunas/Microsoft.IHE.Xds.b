using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Microsoft.IHE.XDS.Common;
using System.Collections;


namespace Microsoft.IHE.XDS.DataAccess
{
    public class PatientIdentityFeedDataAccess
    {
        private string CONST_CONNECTIONSTRING_NAME = "XDSRegistry";

        public PatientIdentityFeedRecord PatientRegistryRecordAdded(PatientIdentityFeedRecord patientIdentityFeedRecord)
        {
            int numberOfRowsAffected = 0;

            Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("usp_patient_insert");

            dbIHEDB.AddInParameter(dbCommand, "patientUID", DbType.String, patientIdentityFeedRecord.PatientUID);
            dbIHEDB.AddOutParameter(dbCommand, "patientID", DbType.Int32, int.MaxValue);

            numberOfRowsAffected = dbIHEDB.ExecuteNonQuery(dbCommand);

            if (numberOfRowsAffected == 0)
            {
                patientIdentityFeedRecord.ResultCode = PatientIdentityFeedResultCode.FAILURE;
            }
            else
            {
                patientIdentityFeedRecord.PatientID = (Int32)dbIHEDB.GetParameterValue(dbCommand, "patientID");
                patientIdentityFeedRecord.ResultCode = PatientIdentityFeedResultCode.SUCCESS;
            }

            return patientIdentityFeedRecord;
        }

        public PatientMessageConfiguration GetPatientMessageConfiguration(string patientMessageKey)
        {
            PatientMessageConfiguration patientMsgConfig = null;
            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_PatientMessageConfiguration_get");

            db.AddInParameter(selectCommand, "patientMessageKey", DbType.String, patientMessageKey);

            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                while (dbReader.Read())
                {
                    patientMsgConfig = new PatientMessageConfiguration();

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("patientMessageID")))
                        patientMsgConfig.PatientMessageID = dbReader.GetInt32(dbReader.GetOrdinal("patientMessageID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("patientMessageKey")))
                        patientMsgConfig.PatientMessageKey = dbReader.GetString(dbReader.GetOrdinal("patientMessageKey"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("patientMessageValue")))
                        patientMsgConfig.PatientMessageValue = dbReader.GetString(dbReader.GetOrdinal("patientMessageValue"));
                }
            }

            return patientMsgConfig;

        }

        public int UpdateRegistryPatientID(string oldPatientUID, string newPatientUID)
        {
            int numberOfRowsAffected = 0;

            Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("usp_update_PatientIDs");

            dbIHEDB.AddInParameter(dbCommand, "oldPatientUID", DbType.String, oldPatientUID);
            dbIHEDB.AddInParameter(dbCommand, "newPatientUID", DbType.String, newPatientUID);

            numberOfRowsAffected = dbIHEDB.ExecuteNonQuery(dbCommand);
            
            return numberOfRowsAffected;
        }


        public int DeleteRegistryPatientUID(string patientUID)
        {
            int numberOfRowsAffected = 0;

            Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("usp_delete_PatientUID_Patient");

            dbIHEDB.AddInParameter(dbCommand, "patientUID", DbType.String, patientUID);

            numberOfRowsAffected = dbIHEDB.ExecuteNonQuery(dbCommand);

            return numberOfRowsAffected;
        }


        public RegistryXmlEntries GetRegistryXmlEntries(int patientID)
        {
            int resultSetCount = 0;
            RegistryXmlEntries registryXmlEntries = null;
            DocumentEntry documentEntry = null;
            Folder folder = null;
            SubmissionSet submissionSet = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_xmlEntries");

            db.AddInParameter(selectCommand, "patientID", DbType.Int32, patientID);

            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                registryXmlEntries = new RegistryXmlEntries();

                do
                {
                    
                    while (dbReader.Read())
                    {

                        if (resultSetCount == 0)
                        {
                            documentEntry = new DocumentEntry();

                            if (!dbReader.IsDBNull(dbReader.GetOrdinal("documentEntryID")))
                                documentEntry.ID = dbReader.GetInt32(dbReader.GetOrdinal("documentEntryID"));

                            if (!dbReader.IsDBNull(dbReader.GetOrdinal("extrinsicObjectXML")))
                                documentEntry.ExtrinsicObjectXML = dbReader.GetString(dbReader.GetOrdinal("extrinsicObjectXML"));

                            registryXmlEntries.DocumentEntryList.Add(documentEntry);
                        }
                        else if (resultSetCount == 1)
                        {
                            folder = new Folder();

                            if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderID")))
                                folder.ID = dbReader.GetInt32(dbReader.GetOrdinal("folderID"));

                            if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderXml")))
                                folder.FolderXml = dbReader.GetString(dbReader.GetOrdinal("folderXml"));

                            registryXmlEntries.FolderList.Add(folder);

                        }
                        else if (resultSetCount == 2)
                        {
                            submissionSet = new SubmissionSet();

                            if (!dbReader.IsDBNull(dbReader.GetOrdinal("submissionSetID")))
                                submissionSet.ID = dbReader.GetInt32(dbReader.GetOrdinal("submissionSetID"));

                            if (!dbReader.IsDBNull(dbReader.GetOrdinal("submissionSetXml")))
                                submissionSet.SubmissionSetXml = dbReader.GetString(dbReader.GetOrdinal("submissionSetXml"));

                            registryXmlEntries.SubmissionSetList.Add(submissionSet);
                        }

                    }

                    resultSetCount++;
                } while (dbReader.NextResult());
            }

            return registryXmlEntries;
        }

 
        public void UpdateDocumentEntryPatientUID(string patientUID, List<DocumentEntry> lstDocumentEntry)
        {

            Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("usp_update_patientUID_documentEntry");

            for (int count = 0; count < lstDocumentEntry.Count; count++)
            {
                dbCommand.Parameters.Clear();
                dbIHEDB.AddInParameter(dbCommand, "documentEntryID", DbType.Int32, lstDocumentEntry[count].ID);
                dbIHEDB.AddInParameter(dbCommand, "patientUID", DbType.String, patientUID);
                dbIHEDB.AddInParameter(dbCommand, "extrinsicObjectXML", DbType.String, lstDocumentEntry[count].ExtrinsicObjectXML);

                dbIHEDB.ExecuteNonQuery(dbCommand);
            }

        }

        public void UpdateFolderPatientUID(List<Folder> lstFolder)
        {

            Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("usp_update_patientUID_Folder");

            for (int count = 0; count < lstFolder.Count; count++)
            {
                dbCommand.Parameters.Clear();
                dbIHEDB.AddInParameter(dbCommand, "folderID", DbType.Int32, lstFolder[count].ID);
                dbIHEDB.AddInParameter(dbCommand, "folderXml", DbType.String, lstFolder[count].FolderXml);

                dbIHEDB.ExecuteNonQuery(dbCommand);
            }

        }

        public void UpdateSubmissionSetPatientUID(List<SubmissionSet> lstSubmissionSet)
        {
            Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("usp_update_patientUID_SubmissionSet");

            for (int count = 0; count < lstSubmissionSet.Count; count++)
            {
                dbCommand.Parameters.Clear();
                dbIHEDB.AddInParameter(dbCommand, "submissionSetID", DbType.Int32, lstSubmissionSet[count].ID);
                dbIHEDB.AddInParameter(dbCommand, "submissionSetXml", DbType.String, lstSubmissionSet[count].SubmissionSetXml);

                dbIHEDB.ExecuteNonQuery(dbCommand);
            }

        }

    }
}
