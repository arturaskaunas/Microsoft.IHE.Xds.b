using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Microsoft.IHE.XDS.Common;
using System.Collections;


namespace Microsoft.IHE.XDS.DataAccess
{
    public class RepositoryDataAccess
    {
        private string CONST_CONNECTIONSTRING_NAME = "XDSRepository";

        public string SaveDocument(System.IO.Stream content, string documentName)
        {
            //Code to insert the content into the database.
            string _storageUniqueIdentifier = "";
            byte[] buffer = null;
            try
            {
                buffer = new byte[content.Length];
                content.Read(buffer, 0, buffer.Length);
                Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
                DbCommand dbSaveDocumentCommand = dbIHEDB.GetStoredProcCommand("SaveDocument_DocumentContent");
                dbIHEDB.AddInParameter(dbSaveDocumentCommand, "ContentUUIDDocument", DbType.Binary, buffer);
                dbIHEDB.AddOutParameter(dbSaveDocumentCommand, "ContentUUID", DbType.Int32, 50);
                int noOfrows = dbIHEDB.ExecuteNonQuery(dbSaveDocumentCommand);
                if (noOfrows > 0)
                {
                    Int32 contentUUID = (Int32)dbIHEDB.GetParameterValue(dbSaveDocumentCommand, "ContentUUID");
                    _storageUniqueIdentifier = contentUUID.ToString();
                }
                //MetaData Save in a Single transaction ,requeires accessing Document entry Meta data Data
                //_storageUniwueIdentifier will return after sucessfull transaction.

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return _storageUniqueIdentifier;
        }


        public void SaveMetaData(DocumentEntry _documentEntry)
        {
            //Code to insert the content into the database.

            try
            {
                Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
                DbCommand dbSaveCommand = dbIHEDB.GetStoredProcCommand("SaveDocumentMetadata_DocumentRepositoryMetadata");
                dbIHEDB.AddInParameter(dbSaveCommand, "DocumentEntryUUID", DbType.String, _documentEntry.UniqueID);
                dbIHEDB.AddInParameter(dbSaveCommand, "DocumentEntryURI", DbType.String, _documentEntry.URI);
                dbIHEDB.AddInParameter(dbSaveCommand, "DocumentEntryHash", DbType.String, _documentEntry.Hash);
                dbIHEDB.AddInParameter(dbSaveCommand, "DocumentEntrySize", DbType.Int32, _documentEntry.Size);
                dbIHEDB.AddInParameter(dbSaveCommand, "DocumentEntryMimeType", DbType.String, _documentEntry.MimeType);
                dbIHEDB.AddInParameter(dbSaveCommand, "DocumentName", DbType.String, _documentEntry.DocumentAuthor);
                int noOfRows = dbIHEDB.ExecuteNonQuery(dbSaveCommand);
                //MetaData Save in a Single transaction ,requeires accessing Document entry Meta data Data
                //_storageUniwueIdentifier will return after sucessfull transaction.
            }
            catch
            {

                throw;
            }

        }
        public Int32 LogDocumentEntry()
        {
            Int32 noOfRows = 0;
            try
            {

                Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
                DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("SaveDocumentEntryLog_DocumentEntryLog");
                noOfRows = dbIHEDB.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return noOfRows;
        }
        public int LogRepositoryData(DocumentRepositoryLog objDocumentRepositoryLog)
        {
            Int32 noOfRows = 0;
            try
            {

                Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
                DbCommand dbLogRepositoryCommand = dbIHEDB.GetStoredProcCommand("SaveRepositoryLogData_DocumentRepositoryLog");
                dbIHEDB.AddInParameter(dbLogRepositoryCommand, "RequesterIdentity", DbType.String, objDocumentRepositoryLog.RequesterIdentity);
                dbIHEDB.AddInParameter(dbLogRepositoryCommand, "RequestMetadata", DbType.String, objDocumentRepositoryLog.RequestMetadata);
                dbIHEDB.AddInParameter(dbLogRepositoryCommand, "Transaction", DbType.String, objDocumentRepositoryLog.Transaction);
                dbIHEDB.AddInParameter(dbLogRepositoryCommand, "StartTime", DbType.DateTime, objDocumentRepositoryLog.StartTime);
                dbIHEDB.AddInParameter(dbLogRepositoryCommand, "FinishTime", DbType.DateTime, objDocumentRepositoryLog.EndTime);
                dbIHEDB.AddInParameter(dbLogRepositoryCommand, "Result", DbType.String, objDocumentRepositoryLog.Result);
                noOfRows = dbIHEDB.ExecuteNonQuery(dbLogRepositoryCommand);
            }
            catch
            {

                throw;
            }
            return noOfRows;
        }
        public Stream RetireveDocument(string contentUUID)
        {
            //Returns the Memory stream for the Document in Retrieve DOcument Set Transaction
            System.IO.Stream _stream = null;
            byte[] buffer = null;
            try
            {
                Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
                DbCommand dbCbmmand = dbIHEDB.GetStoredProcCommand("LoadDocumentContent_DocumentContent");
                dbIHEDB.AddInParameter(dbCbmmand, "ContentID", DbType.Int32, Convert.ToInt32(contentUUID));
                buffer = (byte[])dbIHEDB.ExecuteScalar(dbCbmmand);
                _stream = new MemoryStream(buffer);
                
            }
            catch
            {

                throw;
            }
            return _stream;

        }
        public bool IsValidUniqueID(string documentEntryUUID)
        {
            bool IsValidUniqueID = true;
            try
            {
                Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
                DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("LoadDocumentID_DocumentRepositoryMetadata");
                dbIHEDB.AddInParameter(dbCommand, "DocumentEntryUUID", DbType.String, documentEntryUUID);
                IDataReader dataReader = dbIHEDB.ExecuteReader(dbCommand);
                if (dataReader.Read())
                {
                    IsValidUniqueID = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //MetaData Save in a Single transaction ,requeires accessing Document entry Meta data Data
            //_storageUniwueIdentifier will return after sucessfull transaction.
            return IsValidUniqueID;
        }
        public bool IsHashMatching(string documentEntryUUID)
        {
            bool IsHashMatching = false;
            try
            {
                Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
                DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("LoadDocumentHashByDocID_DocumentRepositoryMetadata");
                dbIHEDB.AddInParameter(dbCommand, "DocumentEntryUUID", DbType.String, documentEntryUUID);
                IDataReader dataReader = dbIHEDB.ExecuteReader(dbCommand);
                if (dataReader.Read())
                {
                    IsHashMatching = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //MetaData Save in a Single transaction ,requeires accessing Document entry Meta data Data
            //_storageUniwueIdentifier will return after sucessfull transaction.
            return IsHashMatching;
        }
        //Gets the Repository Unqiue ID form the Configuration Table
        public string GetRepositoryUniqueID(string strRepositoryKey)
        {
            string strRepositoryUniqueID = "";
            try
            {
                Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
                DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("LoadRepositoryUniqueIDByRepositoryKey_ConfigurationEntry");
                dbIHEDB.AddInParameter(dbCommand, "RepositoryKey", DbType.String, strRepositoryKey);
                strRepositoryUniqueID = (string)dbIHEDB.ExecuteScalar(dbCommand);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return strRepositoryUniqueID;
        }
        public RetrieveDocumentSet GetDocumentRepositoryMetaData(string documentID)
        {
            object objValue = null;
            RetrieveDocumentSet objRetrieveDocumentSet = null;

            try
            {
                Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
                DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("LoadContentIDByDocumentID_DocumentRepositoryMetadata");
                dbIHEDB.AddInParameter(dbCommand, "DocumentID", DbType.String, documentID);

                using (IDataReader dr = dbIHEDB.ExecuteReader(dbCommand))
                {
                    objRetrieveDocumentSet = new RetrieveDocumentSet();

                    while (dr.Read())
                    {
                        objValue = dr["ContentUUID"];
                        if (objValue != null)
                            objRetrieveDocumentSet.ContentID = (int)objValue;

                        objValue = dr["DocumentEntryMimeType"];
                        if (objValue != null)
                            objRetrieveDocumentSet.MimeType = (string)objValue;
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objRetrieveDocumentSet;
        }
        public bool IsRepositoryIDExists(string repositoryUniqueID)
        {
            bool IsRepositoryIDExists = false;
            IDataReader reader = null;
            try
            {
                Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
                DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("LoadRepositoryUniqueId_ConfigurationEntry");
                dbIHEDB.AddInParameter(dbCommand, "RepositoryUniqueId", DbType.String, repositoryUniqueID);
                reader = dbIHEDB.ExecuteReader(dbCommand);
                if (reader.Read())
                {
                    IsRepositoryIDExists = true;
                }
            }
            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                throw ex;
            }
            reader.Close();
            reader.Dispose();
            return IsRepositoryIDExists;
        }

        public string GetConfigurationValue(string configurationKey)
        {
            string configurationValue = string.Empty;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_configurationDetails_ConfigurationEntry");
            db.AddInParameter(selectCommand, "configurationKey", DbType.String, configurationKey);

            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                if (dbReader.Read())
                {
                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("ConfigurationValue")))
                        configurationValue = dbReader.GetString(dbReader.GetOrdinal("ConfigurationValue"));

                }
            }

            return configurationValue;
        }

    }
}
