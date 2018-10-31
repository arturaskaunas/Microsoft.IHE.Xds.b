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
    public class RegistryDataAccess
    {
        private string CONST_CONNECTIONSTRING_NAME = "XDSRegistry";

        public void CreateRegistryLogEntry(RegistryLog log)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
                DbCommand insertCommand = db.GetStoredProcCommand("usp_registryLog_insert");
                //insertCommand.Connection = db.CreateConnection();
                db.AddInParameter(insertCommand, "requesterIdentity", DbType.String, log.RequesterIdentity);
                db.AddInParameter(insertCommand, "requestMetadata", DbType.String, log.RequestMetadata);
                db.AddInParameter(insertCommand, "transactionName", DbType.String, log.TransactionName);
                db.AddInParameter(insertCommand, "startTime", DbType.DateTime, log.StartTime);
                db.AddInParameter(insertCommand, "finishTime", DbType.DateTime, log.FinishTime);
                db.AddInParameter(insertCommand, "result", DbType.String, log.Result);
                db.AddInParameter(insertCommand, "submissionSetID", DbType.Int32, log.SubmissionSetID);
                db.ExecuteNonQuery(insertCommand);
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("IHE", ex.ToString(), System.Diagnostics.EventLogEntryType.Error);

                throw;
            }
        }

        public bool IsPatientIdExistinFeed(string patientUId, out int patientID)
        {
            bool IsExist = false;
            int patientIDinDB = 0;
            patientID = 0;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
                DbCommand selectCommand = db.GetStoredProcCommand("usp_get_patient_Patientid");
                //selectCommand.Connection = db.CreateConnection();
                db.AddInParameter(selectCommand, "patientUID", DbType.String, patientUId);
                object objPatID = db.ExecuteScalar(selectCommand);
                if (objPatID != null)
                {
                    patientIDinDB = int.Parse(objPatID.ToString());
                }
                if (patientIDinDB > 0)
                {
                    patientID = patientIDinDB;
                    IsExist = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsExist;
        }

        public bool IsDuplicateUniqueIDinRegistry(string uniqueIDs)
        {
            bool IsDuplicate = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
                DbCommand selectCommand = db.GetStoredProcCommand("usp_UniqueID_Duplicate_select");
                //selectCommand.Connection = db.CreateConnection();
                db.AddInParameter(selectCommand, "uniqueIds", DbType.String, uniqueIDs);

                using (IDataReader dr = db.ExecuteReader(selectCommand))
                {
                    if (dr.Read())
                    {
                        IsDuplicate = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsDuplicate;
        }

        public void RegisterDocumentSet(List<DocumentEntry> lstDocs, List<Folder> lstFolders, SubmissionSet subSet, List<DocumentEntry> lstReplacedDocumentEntry, int patientID, string docMetadata, DateTime startTime)
        {
            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            Hashtable folderHash = new Hashtable();
            Hashtable documentHash = new Hashtable();
            //DbTransaction transaction = null;

            int submissionSetID;
            try
            {

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();

                    //transaction = connection.BeginTransaction();

                    //INSERT SUBMISSIONSET Related Tables
                    #region "SubmissionSet Table Insert"
                    DbCommand insertSubSetCommand = db.GetStoredProcCommand("usp_submissionSet_insert");
                    db.AddInParameter(insertSubSetCommand, "availabilityStatus", DbType.String, subSet.AvailabilityStatus);
                    db.AddInParameter(insertSubSetCommand, "comments", DbType.String, subSet.Comments);
                    db.AddInParameter(insertSubSetCommand, "contentTypeCodeValue", DbType.String, subSet.ContentType.Value);
                    db.AddInParameter(insertSubSetCommand, "contentTypeCodeDisplayName", DbType.String, subSet.ContentType.CodingScheme);
                    db.AddInParameter(insertSubSetCommand, "entryUUID", DbType.String, subSet.EntryUUID);
                    db.AddInParameter(insertSubSetCommand, "patientID", DbType.Int32, patientID);
                    db.AddInParameter(insertSubSetCommand, "sourceID", DbType.String, subSet.SourceID);
                    db.AddInParameter(insertSubSetCommand, "submissionTime", DbType.DateTime, subSet.SubmissionTime);
                    db.AddInParameter(insertSubSetCommand, "title", DbType.String, subSet.Title);
                    db.AddInParameter(insertSubSetCommand, "uniqueID", DbType.String, subSet.UniqueID);
                    db.AddInParameter(insertSubSetCommand, "submissionSetXml", DbType.String, subSet.SubmissionSetXml);
                    db.AddOutParameter(insertSubSetCommand, "submissionSetID", DbType.Int32, sizeof(Int32));

                    db.ExecuteNonQuery(insertSubSetCommand);
                    submissionSetID = int.Parse(db.GetParameterValue(insertSubSetCommand, "submissionSetID").ToString());
                    insertSubSetCommand.Parameters.Clear();
                    #endregion

                    #region "SubmissionSet Author Table Insert"
                    if (subSet.SubmissionAuthor != null)
                    {
                        foreach (Author subAuthor in subSet.SubmissionAuthor)
                        {
                            DbCommand insertSubSetAuthorCommand = db.GetStoredProcCommand("usp_submissionSet_author_insert");
                            db.AddInParameter(insertSubSetAuthorCommand, "submissionSetID", DbType.Int32, submissionSetID);
                            db.AddInParameter(insertSubSetAuthorCommand, "authorInstitution", DbType.String, subAuthor.Institution);
                            db.AddInParameter(insertSubSetAuthorCommand, "authorPerson", DbType.String, subAuthor.Person);
                            db.AddInParameter(insertSubSetAuthorCommand, "authorRole", DbType.String, subAuthor.Role);
                            db.AddInParameter(insertSubSetAuthorCommand, "authorSpeciality", DbType.String, subAuthor.Specialty);
                            db.AddOutParameter(insertSubSetAuthorCommand, "authorID", DbType.Int32, sizeof(Int32));
                            //db.ExecuteNonQuery(insertSubSetAuthorCommand, transaction);
                            db.ExecuteNonQuery(insertSubSetAuthorCommand);
                            insertSubSetAuthorCommand.Parameters.Clear();
                        }
                    }
                    #endregion


                    //INSERT FOLDER 

                    #region"Folder Table Insert"

                    foreach (Folder fd in lstFolders)
                    {
                        DbCommand insertFolderCommand = db.GetStoredProcCommand("usp_folder_insert");
                        db.AddInParameter(insertFolderCommand, "availabilityStatus", DbType.String, fd.AvailabilityStatus);
                        db.AddInParameter(insertFolderCommand, "comments", DbType.String, fd.Comments);
                        db.AddInParameter(insertFolderCommand, "entryUUID", DbType.String, fd.EntryUUID);
                        db.AddInParameter(insertFolderCommand, "lastUpdateTime", DbType.DateTime, DateTime.Now);//fd.LastUpdateTime
                        db.AddInParameter(insertFolderCommand, "patientID", DbType.String, patientID);
                        db.AddInParameter(insertFolderCommand, "title", DbType.String, fd.Title);
                        db.AddInParameter(insertFolderCommand, "uniqueID", DbType.String, fd.UniqueID);
                        db.AddInParameter(insertFolderCommand, "folderXml", DbType.String, fd.FolderXml);
                        db.AddOutParameter(insertFolderCommand, "folderID", DbType.Int32, sizeof(Int32));
                        //db.ExecuteNonQuery(insertFolderCommand, transaction);
                        db.ExecuteNonQuery(insertFolderCommand);
                        int folderID = int.Parse(db.GetParameterValue(insertFolderCommand, "folderID").ToString());
                        folderHash.Add(fd.EntryUUID, folderID);
                        insertFolderCommand.Parameters.Clear();
                        if (fd.CodeList != null)
                        {
                            foreach (CodeValue eventcode in fd.CodeList)
                            {
                                DbCommand insertEventCodesCommand = db.GetStoredProcCommand("usp_FolderCodeList_insert");
                                db.AddInParameter(insertEventCodesCommand, "folderID", DbType.Int32, folderID);
                                db.AddInParameter(insertEventCodesCommand, "eventCodeValue", DbType.String, eventcode.Value);
                                db.AddInParameter(insertEventCodesCommand, "eventCodeDisplayName", DbType.String, eventcode.CodingScheme);
                                //db.ExecuteNonQuery(insertEventCodesCommand, transaction);
                                db.ExecuteNonQuery(insertEventCodesCommand);
                                insertEventCodesCommand.Parameters.Clear();
                            }
                        }
                    }

                    #endregion

                    //INSERT DOCUMENT ENTRY
                    #region"DocumentEntry Insertions"
                    foreach (DocumentEntry doc in lstDocs)
                    {
                        #region "DocumentEntry Params"
                        DbCommand insertCommand = db.GetStoredProcCommand("usp_documentEntry_insert");
                        insertCommand.Parameters.Clear();
                        db.AddInParameter(insertCommand, "availabilityStatus", DbType.String, doc.AvailabilityStatus);
                        db.AddInParameter(insertCommand, "classCodeValue", DbType.String, doc.ClassCode.Value);
                        db.AddInParameter(insertCommand, "classCodeDisplayName", DbType.String, doc.ClassCode.CodingScheme);
                        db.AddInParameter(insertCommand, "comments", DbType.String, doc.Comments);
                        db.AddInParameter(insertCommand, "confidentialityCodeValue", DbType.String, doc.ConfidentialityCode.Value);
                        db.AddInParameter(insertCommand, "confidentialityCodeDisplayName", DbType.String, doc.ConfidentialityCode.CodingScheme);
                        db.AddInParameter(insertCommand, "creationTime", DbType.DateTime, doc.CreationTime);
                        db.AddInParameter(insertCommand, "entryUUID", DbType.String, doc.EntryUUID);
                        db.AddInParameter(insertCommand, "formatCodeValue", DbType.String, doc.FormatCode.Value);
                        db.AddInParameter(insertCommand, "formatCodeDisplayName", DbType.String, doc.FormatCode.CodingScheme);
                        db.AddInParameter(insertCommand, "hash", DbType.String, doc.Hash);
                        db.AddInParameter(insertCommand, "healthcareFacilityTypeCodeValue", DbType.String, doc.HealthcareFacilityCode.Value);
                        db.AddInParameter(insertCommand, "healthcareFacilityTypeCodeDisplayName", DbType.String, doc.HealthcareFacilityCode.CodingScheme);
                        db.AddInParameter(insertCommand, "languageCodeValue", DbType.String, doc.LanguageCode.Value);
                        db.AddInParameter(insertCommand, "languageCodeDisplayName", DbType.String, doc.LanguageCode.CodingScheme);
                        db.AddInParameter(insertCommand, "legalAuthenticator", DbType.String, doc.LegalAuthenticator);
                        db.AddInParameter(insertCommand, "mimeType", DbType.String, doc.MimeType);
                        db.AddInParameter(insertCommand, "parentDocumentID", DbType.String, doc.ParentDocumentID);
                        db.AddInParameter(insertCommand, "parentDocumentRelationship", DbType.String, doc.ParentDocumentRelationship);
                        db.AddInParameter(insertCommand, "patientID", DbType.Int32, patientID);
                        db.AddInParameter(insertCommand, "practiceSettingCodeValue", DbType.String, doc.PracticeSettingsCode.Value);
                        db.AddInParameter(insertCommand, "practiceSettingCodeDisplayName", DbType.String, doc.PracticeSettingsCode.CodingScheme);
                        db.AddInParameter(insertCommand, "serviceStartTime", DbType.DateTime, doc.ServiceStartTime);
                        db.AddInParameter(insertCommand, "serviceStopTime", DbType.DateTime, doc.ServiceStopTime);
                        db.AddInParameter(insertCommand, "size", DbType.Int32, doc.Size);
                        db.AddInParameter(insertCommand, "sourcePatientID", DbType.String, doc.SourcePatientID);
                        db.AddInParameter(insertCommand, "sourcePatientInfo", DbType.String, doc.SourcePatientInfo);
                        db.AddInParameter(insertCommand, "title", DbType.String, doc.Title);
                        db.AddInParameter(insertCommand, "typeCodeValue", DbType.String, doc.TypeCode.Value);
                        db.AddInParameter(insertCommand, "typeCodeDisplayName", DbType.String, doc.TypeCode.CodingScheme);
                        db.AddInParameter(insertCommand, "uniqueID", DbType.String, doc.UniqueID);
                        db.AddInParameter(insertCommand, "URI", DbType.String, doc.URI);
                        db.AddInParameter(insertCommand, "repositoryUniqueID", DbType.String, doc.RepositoryUniqueID);
                        db.AddInParameter(insertCommand, "extrinsicObjectXML", DbType.String, doc.ExtrinsicObjectXML);
                                                
                        db.AddOutParameter(insertCommand, "documentEntryID", DbType.Int32, sizeof(Int32));
                        #endregion

                        // insert the document Entry object.
                        //db.ExecuteNonQuery(insertCommand, transaction);
                        db.ExecuteNonQuery(insertCommand);
                        int documentID = int.Parse(db.GetParameterValue(insertCommand, "documentEntryID").ToString());
                        documentHash.Add(doc.EntryUUID, documentID);
                        insertCommand.Parameters.Clear();
                        #region "Eventcodes and Authors"
                        //loop for inserting eventcode values
                        if (doc.EventCodeList != null)
                        {
                            foreach (CodeValue evcode in doc.EventCodeList)
                            {
                                DbCommand insertEventCodesCommand = db.GetStoredProcCommand("usp_documentEntryEventCodeList_insert");
                                db.AddInParameter(insertEventCodesCommand, "documentEntryID", DbType.Int32, documentID);
                                db.AddInParameter(insertEventCodesCommand, "eventCodeValue", DbType.String, evcode.Value);
                                db.AddInParameter(insertEventCodesCommand, "eventCodeDisplayName", DbType.String, evcode.CodingScheme);
                                //db.ExecuteNonQuery(insertEventCodesCommand, transaction);
                                db.ExecuteNonQuery(insertEventCodesCommand);
                                insertEventCodesCommand.Parameters.Clear();
                            }
                        }
                        //loop for inserting Authors values
                        if (doc.DocumentAuthor != null)
                        {
                            foreach (Author author in doc.DocumentAuthor)
                            {
                                DbCommand insertAuthorCommand = db.GetStoredProcCommand("usp_document_author_insert");
                                db.AddInParameter(insertAuthorCommand, "documentID", DbType.Int32, documentID);
                                db.AddInParameter(insertAuthorCommand, "authorInstitution", DbType.String, author.Institution);
                                db.AddInParameter(insertAuthorCommand, "authorPerson", DbType.String, author.Person);
                                db.AddInParameter(insertAuthorCommand, "authorRole", DbType.String, author.Role);
                                db.AddInParameter(insertAuthorCommand, "authorSpeciality", DbType.String, author.Specialty);
                                db.AddOutParameter(insertAuthorCommand, "authorID", DbType.Int32, sizeof(Int32));
                                //db.ExecuteNonQuery(insertAuthorCommand, transaction);
                                db.ExecuteNonQuery(insertAuthorCommand);
                                insertAuthorCommand.Parameters.Clear();
                            }
                        }
                        #endregion

                    } //foreach
                    #endregion

                    #region "Associations Entry in Table SubmissionSetDocumentFolder"
                    //insert Associations in the Table SubmissionSetDocumentFolder
                    if (subSet.DocumentList != null)
                    {
                        foreach (DocumentEntry doc in subSet.DocumentList)
                        {
                            //SubmissionSet -> Document
                            DbCommand insertCommand = db.GetStoredProcCommand("usp_submissionSetDocumentFolder_insert");
                            db.AddInParameter(insertCommand, "documentEntryID", DbType.Int32, int.Parse(documentHash[doc.EntryUUID].ToString()));
                            db.AddInParameter(insertCommand, "submissionSetID", DbType.Int32, submissionSetID);
                            db.AddInParameter(insertCommand, "folderID", DbType.Int32, DBNull.Value);
                            db.AddInParameter(insertCommand, "sourceObject", DbType.String, subSet.EntryUUID);
                            db.AddInParameter(insertCommand, "targetObject", DbType.String, doc.EntryUUID);
                            db.AddInParameter(insertCommand, "associationXml", DbType.String, doc.AssociationXml);
                            db.AddInParameter(insertCommand, "documentEntryEntryUUID", DbType.String, doc.EntryUUID);
                            db.AddInParameter(insertCommand, "documentEntryUniqueID", DbType.String, doc.UniqueID);
                            db.AddInParameter(insertCommand, "associationType", DbType.String, doc.AssociationType);
                            
                            db.ExecuteNonQuery(insertCommand);
                            insertCommand.Parameters.Clear();

                        }
                    }
                    if (subSet.FolderList != null)
                    {
                        foreach (Folder fd in subSet.FolderList)
                        {
                            //SubmissionSet -> Folder
                            DbCommand insertCommand = db.GetStoredProcCommand("usp_submissionSetDocumentFolder_insert");
                            db.AddInParameter(insertCommand, "documentEntryID", DbType.Int32, DBNull.Value);
                            db.AddInParameter(insertCommand, "submissionSetID", DbType.Int32, submissionSetID);
                            db.AddInParameter(insertCommand, "folderID", DbType.Int32, int.Parse(folderHash[fd.EntryUUID].ToString()));
                            db.AddInParameter(insertCommand, "sourceObject", DbType.String, subSet.EntryUUID);
                            db.AddInParameter(insertCommand, "targetObject", DbType.String, fd.EntryUUID);
                            db.AddInParameter(insertCommand, "associationXml", DbType.String, fd.AssociationXml);
                            db.AddInParameter(insertCommand, "documentEntryEntryUUID", DbType.String, DBNull.Value);
                            db.AddInParameter(insertCommand, "documentEntryUniqueID", DbType.String, DBNull.Value);
                            db.AddInParameter(insertCommand, "associationType", DbType.String, fd.AssociationType);

                            db.ExecuteNonQuery(insertCommand);
                            insertCommand.Parameters.Clear();
                            if (fd.DocumentList != null)
                            {
                                foreach (DocumentEntry documentEntry in fd.DocumentList)
                                {
                                    //Folder -> Document
                                    DbCommand insertCommand1 = db.GetStoredProcCommand("usp_submissionSetDocumentFolder_insert");
                                    db.AddInParameter(insertCommand1, "documentEntryID", DbType.Int32, int.Parse(documentHash[documentEntry.EntryUUID].ToString()));
                                    db.AddInParameter(insertCommand1, "submissionSetID", DbType.Int32, DBNull.Value);
                                    db.AddInParameter(insertCommand1, "folderID", DbType.Int32, int.Parse(folderHash[fd.EntryUUID].ToString()));
                                    db.AddInParameter(insertCommand1, "sourceObject", DbType.String, fd.EntryUUID);
                                    db.AddInParameter(insertCommand1, "targetObject", DbType.String, documentEntry.EntryUUID);
                                    db.AddInParameter(insertCommand1, "associationXml", DbType.String, documentEntry.AssociationXml);
                                    db.AddInParameter(insertCommand1, "documentEntryEntryUUID", DbType.String, documentEntry.EntryUUID);
                                    db.AddInParameter(insertCommand1, "documentEntryUniqueID", DbType.String, documentEntry.UniqueID);
                                    db.AddInParameter(insertCommand1, "associationType", DbType.String, documentEntry.AssociationType);

                                    db.ExecuteNonQuery(insertCommand1);
                                    insertCommand1.Parameters.Clear();
                                }
                            }
                        }
                    }
                    #endregion



                    if ((lstReplacedDocumentEntry != null) && (lstReplacedDocumentEntry.Count > 0))
                    {
                        DbCommand cmdUpdate = null;

                        foreach (DocumentEntry documentEntry in lstReplacedDocumentEntry)
                        {
                            cmdUpdate = db.GetStoredProcCommand("usp_update_AvailabilityStatus_ExtrinsicObjectXml_documentEntry");

                            db.AddInParameter(cmdUpdate, "entryUUID", DbType.String, documentEntry.EntryUUID);
                            db.AddInParameter(cmdUpdate, "availabilityStatus", DbType.String, documentEntry.AvailabilityStatus);
                            db.AddInParameter(cmdUpdate, "extrinsicObjectXML", DbType.String, documentEntry.ExtrinsicObjectXML);

                            db.ExecuteNonQuery(cmdUpdate);
                        }

                    }




                } //using               
                //Insert RegistryLog here

                RegistryLog nlog = new RegistryLog();
                nlog.RequesterIdentity = System.Environment.UserName;
                nlog.RequestMetadata = docMetadata;
                nlog.Result = GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS;
                nlog.SubmissionSetID = submissionSetID;
                nlog.TransactionName = "RegisterDocumentSet-b";
                nlog.StartTime = startTime;
                nlog.FinishTime = DateTime.Now;
                CreateRegistryLogEntry(nlog);
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }


        //public int UpdateDocumentEntryAvailabilityStatus(string entryUUID, string availabilityStatus)
        //{
        //    int numOfRowsAffected = 0;
        //    Database db = null;
        //    DbCommand cmdUpdate = null;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
        //        cmdUpdate = db.GetStoredProcCommand("usp_update_AvailabilityStatus_documentEntry");

        //        db.AddInParameter(cmdUpdate, "entryUUID", DbType.String, entryUUID);
        //        db.AddInParameter(cmdUpdate, "availabilityStatus", DbType.String, availabilityStatus);

        //        numOfRowsAffected = db.ExecuteNonQuery(cmdUpdate);                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return numOfRowsAffected;
        //}
        
    }
}
