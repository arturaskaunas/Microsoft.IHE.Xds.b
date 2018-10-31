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
    public class RegistryStoredQueryDataAccess
    {

        private string CONST_CONNECTIONSTRING_NAME = "XDSRegistry";

        public StoredQuery GetStoredQueryDetails(string queryUUID, string returnType)
        {
            StoredQuery objStoredQuery = null;
            StoredQueryParameter objParameter = null;
            IDataReader dbReader = null;
            bool bIsStoredQueryAssigned = false;
            object objValue = null;

            Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("usp_get_storedQueryDetails_StoredQuery_StoredQueryParameter");
            
            dbIHEDB.AddInParameter(dbCommand, "queryUUID", DbType.String, queryUUID);
            dbIHEDB.AddInParameter(dbCommand, "returnType", DbType.String, returnType.ToLower());
            
            dbReader = dbIHEDB.ExecuteReader(dbCommand);

            using (dbReader)
            {
                while (dbReader.Read())
                {
                    if (!bIsStoredQueryAssigned)
                    {
                        objStoredQuery = new StoredQuery();

                        objValue = dbReader["storedQueryID"];
                        if (!Convert.IsDBNull(objValue))
                            objStoredQuery.StoredQueryID = (int)objValue;

                        objValue = dbReader["storedQueryUniqueID"];
                        if (!Convert.IsDBNull(objValue))
                            objStoredQuery.StoredQueryUniqueID = (string)objValue;

                        objValue = dbReader["storedQueryName"];
                        if (!Convert.IsDBNull(objValue))
                            objStoredQuery.StoredQueryName = (string)objValue;

                        objValue = dbReader["storedQuerySQLCode"];
                        if (!Convert.IsDBNull(objValue))
                            objStoredQuery.StoredQuerySQLCode = (string)objValue;

                        objValue = dbReader["storedQuerySequence"];
                        if (!Convert.IsDBNull(objValue))
                            objStoredQuery.StoredQuerySequence = (int)objValue;

                        bIsStoredQueryAssigned = true;
                    }

                    //Parameters
                    objParameter = new StoredQueryParameter();

                    objValue = dbReader["storedQueryParameterID"];
                    if (!Convert.IsDBNull(objValue))
                        objParameter.StoredQueryParameterID = (int)objValue;

                    objValue = dbReader["storedQueryParameterName"];
                    if (!Convert.IsDBNull(objValue))
                        objParameter.ParameterName = (string)objValue;

                    objValue = dbReader["attribute"];
                    if (!Convert.IsDBNull(objValue))
                        objParameter.Attribute = (string)objValue;

                    objValue = dbReader["isMandatory"];
                    if (!Convert.IsDBNull(objValue))
                        objParameter.IsMandatory = (bool)objValue;

                    objValue = dbReader["isMultiple"];
                    if (!Convert.IsDBNull(objValue))
                        objParameter.IsMultiple = (bool)objValue;

                    objValue = dbReader["dependentParameterName"];
                    if (!Convert.IsDBNull(objValue))
                        objParameter.DependentParameterName = (string)objValue;

                    objValue = dbReader["tableName"];
                    if (!Convert.IsDBNull(objValue))
                        objParameter.TableName = (string)objValue;

                    objValue = dbReader["joinConditionSQLCode"];
                    if (!Convert.IsDBNull(objValue))
                        objParameter.JoinConditionSQLCode = (string)objValue;

                    objValue = dbReader["whereConditionSQLCode"];
                    if (!Convert.IsDBNull(objValue))
                        objParameter.WhereConditionSQLCode = (string)objValue;

                    objValue = dbReader["storedQueryParameterSequence"];
                    if (!Convert.IsDBNull(objValue))
                        objParameter.SequenceNumber = (int)objValue;


                    objParameter.StoredQueryID = objStoredQuery.StoredQueryID;
                    objStoredQuery.ParameterList.Add(objParameter);


                }

            }


            return objStoredQuery;

        }

        public CodeType GetCodeDetails(string codingScheme)
        {
            bool bIsCodeTypeAssigned = false;
            IDataReader dbReader = null;
            object objValue = null;
            CodeValue objCodeValue = null;
            CodeType objCodeType = new CodeType();
            objCodeType.CodeValues = new List<CodeValue>();

            Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("usp_get_codeDetails_CodeType_CodeValue");

            dbIHEDB.AddInParameter(dbCommand, "codingScheme", DbType.String, codingScheme);
            
            dbReader = dbIHEDB.ExecuteReader(dbCommand);

            using (dbReader)
            {
                while (dbReader.Read())
                {
                    if (!bIsCodeTypeAssigned)
                    {
                        objValue = dbReader["codeTypeID"];
                        if (!Convert.IsDBNull(objValue))
                            objCodeType.CodeTypeID = (int)objValue;

                        objValue = dbReader["codeTypeDisplayName"];
                        if (!Convert.IsDBNull(objValue))
                            objCodeType.DisplayName = (string)objValue;

                        objValue = dbReader["classSchemeUUID"];
                        if (!Convert.IsDBNull(objValue))
                            objCodeType.ClassSchemeUUID = (string)objValue;

                        objValue = dbReader["codingScheme"];
                        if (!Convert.IsDBNull(objValue))
                            objCodeType.CodingScheme = (string)objValue;

                        bIsCodeTypeAssigned = true;
                    }

                    //Code Value
                    objCodeValue = new CodeValue();
                    

                    objValue = dbReader["codeValueID"];
                    if (!Convert.IsDBNull(objValue))
                        objCodeValue.CodeValueID = (int)objValue;

                    objValue = dbReader["code"];
                    if (!Convert.IsDBNull(objValue))
                        objCodeValue.Value = (string)objValue;

                    objValue = dbReader["codeValueDisplayName"];
                    if (!Convert.IsDBNull(objValue))
                        objCodeValue.CodingScheme = (string)objValue;

                    objCodeValue.CodeTypeID = objCodeType.CodeTypeID;
                    objCodeType.CodeValues.Add(objCodeValue);
                }
            }

            return objCodeType;
        }

        public int GetPatientID(string patientUUID)
        {
            int patientID = 0;
            object objValue = null;

            Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = dbIHEDB.GetStoredProcCommand("usp_get_PatientID_Patient");
            dbIHEDB.AddInParameter(dbCommand, "patientUUID", DbType.String, patientUUID);
            objValue = dbIHEDB.ExecuteScalar(dbCommand);

            if (!Convert.IsDBNull(objValue))
                patientID = (int)objValue;

            return patientID;
        }

        public string GetConfigurationValue(string configurationKey)
        {
            string configurationValue = string.Empty;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_configurationDetails_ConfigurationEntry");
            db.AddInParameter(selectCommand, "configurationKey", DbType.String, configurationKey);

            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                while(dbReader.Read())
                {
                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("ConfigurationValue")))
                        configurationValue = dbReader.GetString(dbReader.GetOrdinal("ConfigurationValue"));

                }
            }

            return configurationValue;
        }

        public List<string> FindDocumentsObjectRef(string sql)
        {
            object objValue = null;
            IDataReader dbReader = null;
            List<string> lstDocumentIds = new List<string>();
            

            Database dbIHEDB = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = dbIHEDB.GetSqlStringCommand(sql);
                       
            dbReader = dbIHEDB.ExecuteReader(dbCommand);

            using (dbReader)
            {
                while (dbReader.Read())
                {
                    objValue = dbReader["entryUUID"];

                    if (objValue != null)
                        lstDocumentIds.Add((string)objValue);
                }
            }



            return lstDocumentIds;
        }


















        public List<SubmissionSetDocumentFolder> GetAssociations(string uuids)
        {
            List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolder = new List<SubmissionSetDocumentFolder>();
            SubmissionSetDocumentFolder objSubmissionSetDocumentFolder = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_AssociationsBy_sourceObject_Or_targetObject");
            db.AddInParameter(selectCommand, "uuids", DbType.String, uuids);

            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                while (dbReader.Read())
                {
                    objSubmissionSetDocumentFolder = new SubmissionSetDocumentFolder();

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("SubmissionSetDocumentFolderID")))
                        objSubmissionSetDocumentFolder.SubmissionSetDocumentFolderID = dbReader.GetInt32(dbReader.GetOrdinal("SubmissionSetDocumentFolderID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("submissionSetID")))
                        objSubmissionSetDocumentFolder.SubmissionSetID = dbReader.GetInt32(dbReader.GetOrdinal("submissionSetID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderID")))
                        objSubmissionSetDocumentFolder.FolderID = dbReader.GetInt32(dbReader.GetOrdinal("folderID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("documentEntryID")))
                        objSubmissionSetDocumentFolder.DocumentEntryID = dbReader.GetInt32(dbReader.GetOrdinal("documentEntryID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("sourceObject")))
                        objSubmissionSetDocumentFolder.SourceObject = dbReader.GetString(dbReader.GetOrdinal("sourceObject"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("targetObject")))
                        objSubmissionSetDocumentFolder.TargetObject = dbReader.GetString(dbReader.GetOrdinal("targetObject"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("associationXml")))
                        objSubmissionSetDocumentFolder.AssociationXml = dbReader.GetString(dbReader.GetOrdinal("associationXml"));


                    lstSubmissionSetDocumentFolder.Add(objSubmissionSetDocumentFolder);
                }
            }

            return lstSubmissionSetDocumentFolder;
        }


        public List<DocumentEntry> GetDocuments(string documentEntryEntryUUIDs, string documentEntryUniqueIDs)
        {
            List<DocumentEntry> lstDocumentEntries = new List<DocumentEntry>();
            DocumentEntry objDocumentEntry = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_DocumentsBy_EntryUUID_Or_UniqueID");

            if(string.IsNullOrEmpty(documentEntryEntryUUIDs))
                db.AddInParameter(selectCommand, "documentEntryEntryUUIDs", DbType.String, DBNull.Value);
            else
                db.AddInParameter(selectCommand, "documentEntryEntryUUIDs", DbType.String, documentEntryEntryUUIDs);

            if (string.IsNullOrEmpty(documentEntryUniqueIDs))
                db.AddInParameter(selectCommand, "documentEntryUniqueIDs", DbType.String, DBNull.Value);
            else
                db.AddInParameter(selectCommand, "documentEntryUniqueIDs", DbType.String, documentEntryUniqueIDs);


            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                    while (dbReader.Read())
                    {

                        objDocumentEntry = new DocumentEntry();

                        if (!dbReader.IsDBNull(dbReader.GetOrdinal("availabilityStatus")))
                            objDocumentEntry.AvailabilityStatus = dbReader.GetString(dbReader.GetOrdinal("availabilityStatus"));

                        if (!dbReader.IsDBNull(dbReader.GetOrdinal("entryUUID")))
                            objDocumentEntry.EntryUUID = dbReader.GetString(dbReader.GetOrdinal("entryUUID"));

                        if (!dbReader.IsDBNull(dbReader.GetOrdinal("uniqueID")))
                            objDocumentEntry.UniqueID = dbReader.GetString(dbReader.GetOrdinal("uniqueID"));

                        if (!dbReader.IsDBNull(dbReader.GetOrdinal("repositoryUniqueID")))
                            objDocumentEntry.RepositoryUniqueID = dbReader.GetString(dbReader.GetOrdinal("repositoryUniqueID"));

                        if (!dbReader.IsDBNull(dbReader.GetOrdinal("hash")))
                            objDocumentEntry.Hash = dbReader.GetString(dbReader.GetOrdinal("hash"));

                        if (!dbReader.IsDBNull(dbReader.GetOrdinal("size")))
                            objDocumentEntry.Size = dbReader.GetInt32(dbReader.GetOrdinal("size"));

                        if (!dbReader.IsDBNull(dbReader.GetOrdinal("extrinsicObjectXML")))
                            objDocumentEntry.ExtrinsicObjectXML = dbReader.GetString(dbReader.GetOrdinal("extrinsicObjectXML"));

                        lstDocumentEntries.Add(objDocumentEntry);
                    }

            }

            return lstDocumentEntries;

        }

        public List<Association> GetAssociations(string documentEntryEntryUUIDs, string documentEntryUniqueIDs)
        {
            List<Association> lstAssociations = new List<Association>();
            Association objAssociation = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_AssociationsBy_EntryUUID_Or_UniqueID");

            if (string.IsNullOrEmpty(documentEntryEntryUUIDs))
                db.AddInParameter(selectCommand, "documentEntryEntryUUIDs", DbType.String, DBNull.Value);
            else
                db.AddInParameter(selectCommand, "documentEntryEntryUUIDs", DbType.String, documentEntryEntryUUIDs);

            if (string.IsNullOrEmpty(documentEntryUniqueIDs))
                db.AddInParameter(selectCommand, "documentEntryUniqueIDs", DbType.String, DBNull.Value);
            else
                db.AddInParameter(selectCommand, "documentEntryUniqueIDs", DbType.String, documentEntryUniqueIDs);


            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                while (dbReader.Read())
                {

                    objAssociation = new Association();

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("associationXml")))
                        objAssociation.AssociationXml = dbReader.GetString(dbReader.GetOrdinal("associationXml"));

                    lstAssociations.Add(objAssociation);
                }

            }

            return lstAssociations;

        }

        public List<GetSubmissionSetsRequest> GetSubmissionSetsByTargetObjects(string targetObjectIDs)
        {
            List<GetSubmissionSetsRequest> lstGetSubmissionSetsRequest = new List<GetSubmissionSetsRequest>();
            GetSubmissionSetsRequest objGetSubmissionSetsRequest = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_SubmissionSetsByTargetObjectIDs");

            db.AddInParameter(selectCommand, "targetObjectIDs", DbType.String, targetObjectIDs);


            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                while (dbReader.Read())
                {

                    objGetSubmissionSetsRequest = new GetSubmissionSetsRequest();

                    //submissionSetID
                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("submissionSetID")))
                        objGetSubmissionSetsRequest.SubmissionSetID = dbReader.GetInt32(dbReader.GetOrdinal("submissionSetID"));

                    //submissionSetXml
                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("submissionSetXml")))
                        objGetSubmissionSetsRequest.SubmissionSetXml = dbReader.GetString(dbReader.GetOrdinal("submissionSetXml"));

                    //SubmissionSetDocumentFolderID
                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("SubmissionSetDocumentFolderID")))
                        objGetSubmissionSetsRequest.SubmissionSetDocumentFolderID = dbReader.GetInt32(dbReader.GetOrdinal("SubmissionSetDocumentFolderID"));

                    //associationXml
                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("associationXml")))
                        objGetSubmissionSetsRequest.AssociationXml = dbReader.GetString(dbReader.GetOrdinal("associationXml"));

                    lstGetSubmissionSetsRequest.Add(objGetSubmissionSetsRequest);
                }

            }

            return lstGetSubmissionSetsRequest;

        }


        public SubmissionSetAssociation GetSubmissionSetAndAssociation(string entryUUID, string uniqueID)
        {
            SubmissionSetAssociation objSubmissionSetAssociation = new SubmissionSetAssociation();
            SubmissionSet objSubmissionSet = null;
            SubmissionSetDocumentFolder objSubmissionSetDocumentFolder = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_SubmissionSetAndAssociation_By_entryUUID_Or_uniqueID");

            if (string.IsNullOrEmpty(entryUUID))
                db.AddInParameter(selectCommand, "entryUUID", DbType.String, DBNull.Value);
            else
                db.AddInParameter(selectCommand, "entryUUID", DbType.String, entryUUID);

            if (string.IsNullOrEmpty(uniqueID))
                db.AddInParameter(selectCommand, "uniqueID", DbType.String, DBNull.Value);
            else
                db.AddInParameter(selectCommand, "uniqueID", DbType.String, uniqueID);


            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                while (dbReader.Read())
                {

                    objSubmissionSet = new SubmissionSet();
                    objSubmissionSetDocumentFolder = new SubmissionSetDocumentFolder();

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("submissionSetID")))
                        objSubmissionSet.ID = dbReader.GetInt32(dbReader.GetOrdinal("submissionSetID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("entryUUID")))
                        objSubmissionSet.EntryUUID = dbReader.GetString(dbReader.GetOrdinal("entryUUID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("uniqueID")))
                        objSubmissionSet.UniqueID = dbReader.GetString(dbReader.GetOrdinal("uniqueID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("submissionSetXml")))
                        objSubmissionSet.SubmissionSetXml = dbReader.GetString(dbReader.GetOrdinal("submissionSetXml"));


                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("SubmissionSetDocumentFolderID")))
                        objSubmissionSetDocumentFolder.SubmissionSetDocumentFolderID = dbReader.GetInt32(dbReader.GetOrdinal("SubmissionSetDocumentFolderID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderID")))
                        objSubmissionSetDocumentFolder.FolderID = dbReader.GetInt32(dbReader.GetOrdinal("folderID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("documentEntryID")))
                        objSubmissionSetDocumentFolder.DocumentEntryID = dbReader.GetInt32(dbReader.GetOrdinal("documentEntryID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("sourceObject")))
                        objSubmissionSetDocumentFolder.SourceObject = dbReader.GetString(dbReader.GetOrdinal("sourceObject"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("targetObject")))
                        objSubmissionSetDocumentFolder.TargetObject = dbReader.GetString(dbReader.GetOrdinal("targetObject"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("associationXml")))
                        objSubmissionSetDocumentFolder.AssociationXml = dbReader.GetString(dbReader.GetOrdinal("associationXml"));


                    objSubmissionSetAssociation.SubmissionSets.Add(objSubmissionSet);
                    objSubmissionSetAssociation.SubmissionSetDocumentFolders.Add(objSubmissionSetDocumentFolder);
                }

            }

            return objSubmissionSetAssociation;
        }


        public List<Folder> GetFoldersByFolderIDs(string folderIDs)
        {
            IDataReader dbReader = null;
            List<Folder> lstFolders = new List<Folder>();
            Folder objFolder = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_get_Folders_By_FolderIDs");

            db.AddInParameter(dbCommand, "folderIDs", DbType.String, folderIDs);

            using (dbReader = db.ExecuteReader(dbCommand))
            {
                while (dbReader.Read())
                {
                    objFolder = new Folder();

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("availabilityStatus")))
                        objFolder.AvailabilityStatus = dbReader.GetString(dbReader.GetOrdinal("availabilityStatus"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("comments")))
                        objFolder.Comments = dbReader.GetString(dbReader.GetOrdinal("comments"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("entryUUID")))
                        objFolder.EntryUUID = dbReader.GetString(dbReader.GetOrdinal("entryUUID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("uniqueID")))
                        objFolder.UniqueID = dbReader.GetString(dbReader.GetOrdinal("uniqueID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("patientID")))
                    {
                        objFolder.FolderPatient = new Patient();
                        objFolder.FolderPatient.PatientID = dbReader.GetInt32(dbReader.GetOrdinal("patientID"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("title")))
                        objFolder.Title = dbReader.GetString(dbReader.GetOrdinal("title"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderXml")))
                        objFolder.FolderXml = dbReader.GetString(dbReader.GetOrdinal("folderXml"));

                    lstFolders.Add(objFolder);
                }
            }

            return lstFolders;
        }

        
        public List<DocumentEntry> GetDocumentEntriesByDocumentEntryIDs(string documentEntryIDs)
        {
            IDataReader dbReader = null;
            List<DocumentEntry> lstDocumentEntries = new List<DocumentEntry>();
            DocumentEntry objDocumentEntry = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_get_DocumentEntry_By_documentEntryIDs");

            db.AddInParameter(dbCommand, "documentEntryIDs", DbType.String, documentEntryIDs);

            dbReader = db.ExecuteReader(dbCommand);

            using (dbReader)
            {
                while (dbReader.Read())
                {
                    objDocumentEntry = new DocumentEntry();

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("documentEntryID")))
                        objDocumentEntry.ID = dbReader.GetInt32(dbReader.GetOrdinal("documentEntryID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("availabilityStatus")))
                        objDocumentEntry.AvailabilityStatus = dbReader.GetString(dbReader.GetOrdinal("availabilityStatus"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("classCodeValue")))
                    {
                        objDocumentEntry.ClassCode = new CodeValue();
                        objDocumentEntry.ClassCode.Value = dbReader.GetString(dbReader.GetOrdinal("classCodeValue"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("classCodeDisplayName")))
                    {
                        if (objDocumentEntry.ClassCode == null)
                            objDocumentEntry.ClassCode = new CodeValue();

                        objDocumentEntry.ClassCode.CodingScheme = dbReader.GetString(dbReader.GetOrdinal("classCodeDisplayName"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("confidentialityCodeValue")))
                    {
                        objDocumentEntry.ConfidentialityCode = new CodeValue();
                        objDocumentEntry.ConfidentialityCode.Value = dbReader.GetString(dbReader.GetOrdinal("confidentialityCodeValue"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("confidentialityCodeDisplayName")))
                    {
                        if (objDocumentEntry.ConfidentialityCode == null)
                            objDocumentEntry.ConfidentialityCode = new CodeValue();

                        objDocumentEntry.ConfidentialityCode.CodingScheme = dbReader.GetString(dbReader.GetOrdinal("confidentialityCodeDisplayName"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("formatCodeValue")))
                    {
                        objDocumentEntry.FormatCode = new CodeValue();
                        objDocumentEntry.FormatCode.Value = dbReader.GetString(dbReader.GetOrdinal("formatCodeValue"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("formatCodeDisplayName")))
                    {
                        if (objDocumentEntry.FormatCode == null)
                            objDocumentEntry.FormatCode = new CodeValue();

                        objDocumentEntry.FormatCode.CodingScheme = dbReader.GetString(dbReader.GetOrdinal("formatCodeDisplayName"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("healthcareFacilityTypeCodeValue")))
                    {
                        objDocumentEntry.HealthcareFacilityCode = new CodeValue();
                        objDocumentEntry.HealthcareFacilityCode.Value = dbReader.GetString(dbReader.GetOrdinal("healthcareFacilityTypeCodeValue"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("healthcareFacilityTypeCodeDisplayName")))
                    {
                        if (objDocumentEntry.HealthcareFacilityCode == null)
                            objDocumentEntry.HealthcareFacilityCode = new CodeValue();

                        objDocumentEntry.HealthcareFacilityCode.CodingScheme = dbReader.GetString(dbReader.GetOrdinal("healthcareFacilityTypeCodeDisplayName"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("languageCodeValue")))
                    {
                        objDocumentEntry.LanguageCode = new CodeValue();
                        objDocumentEntry.LanguageCode.Value = dbReader.GetString(dbReader.GetOrdinal("languageCodeValue"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("languageCodeDisplayName")))
                    {
                        if (objDocumentEntry.LanguageCode == null)
                            objDocumentEntry.LanguageCode = new CodeValue();

                        objDocumentEntry.LanguageCode.CodingScheme = dbReader.GetString(dbReader.GetOrdinal("languageCodeDisplayName"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("mimeType")))
                        objDocumentEntry.MimeType = dbReader.GetString(dbReader.GetOrdinal("mimeType"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("patientID")))
                    {
                        objDocumentEntry.DocumentPatient = new Patient();
                        objDocumentEntry.DocumentPatient.PatientID = dbReader.GetInt32(dbReader.GetOrdinal("patientID"));
                    }
                    
                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("entryUUID")))
                        objDocumentEntry.EntryUUID = dbReader.GetString(dbReader.GetOrdinal("entryUUID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("uniqueID")))
                        objDocumentEntry.UniqueID = dbReader.GetString(dbReader.GetOrdinal("uniqueID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("extrinsicObjectXML")))
                        objDocumentEntry.ExtrinsicObjectXML = dbReader.GetString(dbReader.GetOrdinal("extrinsicObjectXML"));

                    lstDocumentEntries.Add(objDocumentEntry);
                }
            }

            return lstDocumentEntries;
        }


        public List<Folder> GetFoldersByEntryUUIDOrUniqueID(string entryUUID, string uniqueID)
        {
            List<Folder> lstFolders = new List<Folder>();
            Folder objFolder = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_Folders_By_entryUUID_Or_uniqueID");

            if (string.IsNullOrEmpty(entryUUID))
                db.AddInParameter(selectCommand, "entryUUID", DbType.String, DBNull.Value);
            else
                db.AddInParameter(selectCommand, "entryUUID", DbType.String, entryUUID);

            if (string.IsNullOrEmpty(uniqueID))
                db.AddInParameter(selectCommand, "uniqueID", DbType.String, DBNull.Value);
            else
                db.AddInParameter(selectCommand, "uniqueID", DbType.String, uniqueID);


            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                while (dbReader.Read())
                {
                    
                    objFolder = new Folder();

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderID")))
                        objFolder.FolderID = dbReader.GetInt32(dbReader.GetOrdinal("folderID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("availabilityStatus")))
                        objFolder.AvailabilityStatus = dbReader.GetString(dbReader.GetOrdinal("availabilityStatus"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("entryUUID")))
                        objFolder.EntryUUID = dbReader.GetString(dbReader.GetOrdinal("entryUUID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("uniqueID")))
                        objFolder.UniqueID = dbReader.GetString(dbReader.GetOrdinal("uniqueID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderXml")))
                        objFolder.FolderXml = dbReader.GetString(dbReader.GetOrdinal("folderXml"));

                    lstFolders.Add(objFolder);
                }

            }

            return lstFolders;

        }

        public List<SubmissionSetDocumentFolder> GetAssociationsForFolderIDs(string folderIDs)
        {
            List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolders = new List<SubmissionSetDocumentFolder>();
            SubmissionSetDocumentFolder objSubmissionSetDocumentFolder = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_Association_For_folderIDs");

            db.AddInParameter(selectCommand, "folderIDs", DbType.String, folderIDs);

            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                while (dbReader.Read())
                {

                    objSubmissionSetDocumentFolder = new SubmissionSetDocumentFolder();


                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("SubmissionSetDocumentFolderID")))
                        objSubmissionSetDocumentFolder.SubmissionSetDocumentFolderID = dbReader.GetInt32(dbReader.GetOrdinal("SubmissionSetDocumentFolderID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("submissionSetID")))
                        objSubmissionSetDocumentFolder.SubmissionSetID = dbReader.GetInt32(dbReader.GetOrdinal("submissionSetID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderID")))
                        objSubmissionSetDocumentFolder.FolderID = dbReader.GetInt32(dbReader.GetOrdinal("folderID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("documentEntryID")))
                        objSubmissionSetDocumentFolder.DocumentEntryID = dbReader.GetInt32(dbReader.GetOrdinal("documentEntryID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("sourceObject")))
                        objSubmissionSetDocumentFolder.SourceObject = dbReader.GetString(dbReader.GetOrdinal("sourceObject"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("targetObject")))
                        objSubmissionSetDocumentFolder.TargetObject = dbReader.GetString(dbReader.GetOrdinal("targetObject"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("associationXml")))
                        objSubmissionSetDocumentFolder.AssociationXml = dbReader.GetString(dbReader.GetOrdinal("associationXml"));

                    lstSubmissionSetDocumentFolders.Add(objSubmissionSetDocumentFolder);
                }

            }


            return lstSubmissionSetDocumentFolders;
        }

        public List<SubmissionSetDocumentFolder> GetSubmissionSetDocumentFolderByEntryUUIDorUniqueID(string documentEntryUUID, string documentEntryUniqueID)
        {
            List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolder = new List<SubmissionSetDocumentFolder>();
            SubmissionSetDocumentFolder objSubmissionSetDocumentFolder = null;


            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_SubmissionSetDocumentFolder_By_EntryUUID_Or_UniqueID");

            if (string.IsNullOrEmpty(documentEntryUUID))
                db.AddInParameter(selectCommand, "documentEntryUUID", DbType.String, DBNull.Value);
            else
                db.AddInParameter(selectCommand, "documentEntryUUID", DbType.String, documentEntryUUID);

            if (string.IsNullOrEmpty(documentEntryUniqueID))
                db.AddInParameter(selectCommand, "documentEntryUniqueID", DbType.String, DBNull.Value);
            else
                db.AddInParameter(selectCommand, "documentEntryUniqueID", DbType.String, documentEntryUniqueID);


            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                while (dbReader.Read())
                {
                    objSubmissionSetDocumentFolder = new SubmissionSetDocumentFolder();

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("submissionSetID")))
                        objSubmissionSetDocumentFolder.SubmissionSetID = dbReader.GetInt32(dbReader.GetOrdinal("submissionSetID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderID")))
                        objSubmissionSetDocumentFolder.FolderID = dbReader.GetInt32(dbReader.GetOrdinal("folderID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("documentEntryID")))
                        objSubmissionSetDocumentFolder.DocumentEntryID = dbReader.GetInt32(dbReader.GetOrdinal("documentEntryID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("sourceObject")))
                        objSubmissionSetDocumentFolder.SourceObject = dbReader.GetString(dbReader.GetOrdinal("sourceObject"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("targetObject")))
                        objSubmissionSetDocumentFolder.TargetObject = dbReader.GetString(dbReader.GetOrdinal("targetObject"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("associationXml")))
                        objSubmissionSetDocumentFolder.AssociationXml = dbReader.GetString(dbReader.GetOrdinal("associationXml"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("associationType")))
                        objSubmissionSetDocumentFolder.AssociationType = dbReader.GetString(dbReader.GetOrdinal("associationType"));
                    
                    lstSubmissionSetDocumentFolder.Add(objSubmissionSetDocumentFolder);
                }

            }

            return lstSubmissionSetDocumentFolder;
        }

        public List<SubmissionSetDocumentFolder> GetSubmissionSetDocumentFolderByEntryUUIDorUniqueID(string documentEntryUUID, string documentEntryUniqueID, string associationTypes)
        {
            List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolder = new List<SubmissionSetDocumentFolder>();
            SubmissionSetDocumentFolder objSubmissionSetDocumentFolder = null;


            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_SubmissionSetDocumentFolder_By_ID_And_associationType");

            if (string.IsNullOrEmpty(documentEntryUUID))
                db.AddInParameter(selectCommand, "documentEntryUUID", DbType.String, DBNull.Value);
            else
                db.AddInParameter(selectCommand, "documentEntryUUID", DbType.String, documentEntryUUID);

            if (string.IsNullOrEmpty(documentEntryUniqueID))
                db.AddInParameter(selectCommand, "documentEntryUniqueID", DbType.String, DBNull.Value);
            else
                db.AddInParameter(selectCommand, "documentEntryUniqueID", DbType.String, documentEntryUniqueID);

            db.AddInParameter(selectCommand, "associationTypes", DbType.String, associationTypes);

            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                while (dbReader.Read())
                {
                    objSubmissionSetDocumentFolder = new SubmissionSetDocumentFolder();

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("submissionSetID")))
                        objSubmissionSetDocumentFolder.SubmissionSetID = dbReader.GetInt32(dbReader.GetOrdinal("submissionSetID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderID")))
                        objSubmissionSetDocumentFolder.FolderID = dbReader.GetInt32(dbReader.GetOrdinal("folderID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("documentEntryID")))
                        objSubmissionSetDocumentFolder.DocumentEntryID = dbReader.GetInt32(dbReader.GetOrdinal("documentEntryID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("sourceObject")))
                        objSubmissionSetDocumentFolder.SourceObject = dbReader.GetString(dbReader.GetOrdinal("sourceObject"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("targetObject")))
                        objSubmissionSetDocumentFolder.TargetObject = dbReader.GetString(dbReader.GetOrdinal("targetObject"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("associationXml")))
                        objSubmissionSetDocumentFolder.AssociationXml = dbReader.GetString(dbReader.GetOrdinal("associationXml"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("associationType")))
                        objSubmissionSetDocumentFolder.AssociationType = dbReader.GetString(dbReader.GetOrdinal("associationType"));

                    lstSubmissionSetDocumentFolder.Add(objSubmissionSetDocumentFolder);
                }

            }

            return lstSubmissionSetDocumentFolder;
        }

        public List<SubmissionSet> GetSubmissionSets(string availabilityStatus, string patientUID)
        {
            List<SubmissionSet> lstSubmissionSet = new List<SubmissionSet>();
            SubmissionSet objSubmissionSet = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_submissionSetDetails_SubmissionSet_Patient");

            db.AddInParameter(selectCommand, "availabilityStatus", DbType.String, availabilityStatus);
            db.AddInParameter(selectCommand, "patientUID", DbType.String, patientUID);
            
            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                while (dbReader.Read())
                {

                    objSubmissionSet = new SubmissionSet();


                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("submissionSetID")))
                        objSubmissionSet.ID = dbReader.GetInt32(dbReader.GetOrdinal("submissionSetID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("availabilityStatus")))
                        objSubmissionSet.AvailabilityStatus = dbReader.GetString(dbReader.GetOrdinal("availabilityStatus"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("comments")))
                        objSubmissionSet.Comments = dbReader.GetString(dbReader.GetOrdinal("comments"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("contentTypeCodeValue")))
                    {
                        objSubmissionSet.ContentType = new CodeValue();
                        objSubmissionSet.ContentType.Value = dbReader.GetString(dbReader.GetOrdinal("contentTypeCodeValue"));
                    }

                    if ((objSubmissionSet.ContentType != null) && (!dbReader.IsDBNull(dbReader.GetOrdinal("contentTypeCodeDisplayName"))))
                        objSubmissionSet.ContentType.CodingScheme = dbReader.GetString(dbReader.GetOrdinal("contentTypeCodeDisplayName"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("entryUUID")))
                        objSubmissionSet.EntryUUID = dbReader.GetString(dbReader.GetOrdinal("entryUUID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("patientID")))
                    {
                        objSubmissionSet.SubmissionPatient = new Patient();
                        objSubmissionSet.SubmissionPatient.PatientID = dbReader.GetInt32(dbReader.GetOrdinal("patientID"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("sourceID")))
                        objSubmissionSet.SourceID = dbReader.GetString(dbReader.GetOrdinal("sourceID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("submissionTime")))
                        objSubmissionSet.SubmissionTime = dbReader.GetDateTime(dbReader.GetOrdinal("submissionTime"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("title")))
                        objSubmissionSet.Title = dbReader.GetString(dbReader.GetOrdinal("title"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("uniqueID")))
                        objSubmissionSet.UniqueID = dbReader.GetString(dbReader.GetOrdinal("uniqueID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("submissionSetXml")))
                        objSubmissionSet.SubmissionSetXml = dbReader.GetString(dbReader.GetOrdinal("submissionSetXml"));

                    lstSubmissionSet.Add(objSubmissionSet);
                }

            }

            return lstSubmissionSet;

        }

        public List<Author> GetSubmissionSetAuthorDetails(int submissionSetID)
        {
            List<Author> lstAuthor = null;
            Author author = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand selectCommand = db.GetStoredProcCommand("usp_get_SubmissionSetAuthor_by_submissionSetID");

            db.AddInParameter(selectCommand, "submissionSetID", DbType.Int32, submissionSetID);

            lstAuthor = new List<Author>();

            using (IDataReader dbReader = db.ExecuteReader(selectCommand))
            {
                while (dbReader.Read())
                {

                    author = new Author();

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("authorID")))
                        author.AuthorID = dbReader.GetInt32(dbReader.GetOrdinal("authorID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("authorInstitution")))
                        author.Institution = dbReader.GetString(dbReader.GetOrdinal("authorInstitution"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("authorPerson")))
                        author.Person = dbReader.GetString(dbReader.GetOrdinal("authorPerson"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("authorRole")))
                        author.Role = dbReader.GetString(dbReader.GetOrdinal("authorRole"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("authorSpeciality")))
                        author.Specialty = dbReader.GetString(dbReader.GetOrdinal("authorSpeciality"));

                    lstAuthor.Add(author);
                }

            }

            return lstAuthor;

        }

        public List<DocumentEntry> GetDocumentEntries(string availabilityStatus, string patientUID)
        {
            IDataReader dbReader = null;
            List<DocumentEntry> lstDocumentEntries = new List<DocumentEntry>();
            DocumentEntry objDocumentEntry = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_get_documentEntryDetails_DocumentEntry_Patient");

            db.AddInParameter(dbCommand, "availabilityStatus", DbType.String, availabilityStatus);
            db.AddInParameter(dbCommand, "patientUID", DbType.String, patientUID);

            dbReader = db.ExecuteReader(dbCommand);

            using (dbReader)
            {
                while (dbReader.Read())
                {
                    objDocumentEntry = new DocumentEntry();

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("documentEntryID")))
                        objDocumentEntry.ID = dbReader.GetInt32(dbReader.GetOrdinal("documentEntryID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("availabilityStatus")))
                        objDocumentEntry.AvailabilityStatus = dbReader.GetString(dbReader.GetOrdinal("availabilityStatus"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("classCodeValue")))
                    {
                        objDocumentEntry.ClassCode = new CodeValue();
                        objDocumentEntry.ClassCode.Value = dbReader.GetString(dbReader.GetOrdinal("classCodeValue"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("classCodeDisplayName")))
                    {
                        if (objDocumentEntry.ClassCode == null)
                            objDocumentEntry.ClassCode = new CodeValue();

                        objDocumentEntry.ClassCode.CodingScheme = dbReader.GetString(dbReader.GetOrdinal("classCodeDisplayName"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("confidentialityCodeValue")))
                    {
                        objDocumentEntry.ConfidentialityCode = new CodeValue();
                        objDocumentEntry.ConfidentialityCode.Value = dbReader.GetString(dbReader.GetOrdinal("confidentialityCodeValue"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("confidentialityCodeDisplayName")))
                    {
                        if (objDocumentEntry.ConfidentialityCode == null)
                            objDocumentEntry.ConfidentialityCode = new CodeValue();

                        objDocumentEntry.ConfidentialityCode.CodingScheme = dbReader.GetString(dbReader.GetOrdinal("confidentialityCodeDisplayName"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("formatCodeValue")))
                    {
                        objDocumentEntry.FormatCode = new CodeValue();
                        objDocumentEntry.FormatCode.Value = dbReader.GetString(dbReader.GetOrdinal("formatCodeValue"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("formatCodeDisplayName")))
                    {
                        if (objDocumentEntry.FormatCode == null)
                            objDocumentEntry.FormatCode = new CodeValue();

                        objDocumentEntry.FormatCode.CodingScheme = dbReader.GetString(dbReader.GetOrdinal("formatCodeDisplayName"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("healthcareFacilityTypeCodeValue")))
                    {
                        objDocumentEntry.HealthcareFacilityCode = new CodeValue();
                        objDocumentEntry.HealthcareFacilityCode.Value = dbReader.GetString(dbReader.GetOrdinal("healthcareFacilityTypeCodeValue"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("healthcareFacilityTypeCodeDisplayName")))
                    {
                        if (objDocumentEntry.HealthcareFacilityCode == null)
                            objDocumentEntry.HealthcareFacilityCode = new CodeValue();

                        objDocumentEntry.HealthcareFacilityCode.CodingScheme = dbReader.GetString(dbReader.GetOrdinal("healthcareFacilityTypeCodeDisplayName"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("languageCodeValue")))
                    {
                        objDocumentEntry.LanguageCode = new CodeValue();
                        objDocumentEntry.LanguageCode.Value = dbReader.GetString(dbReader.GetOrdinal("languageCodeValue"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("languageCodeDisplayName")))
                    {
                        if (objDocumentEntry.LanguageCode == null)
                            objDocumentEntry.LanguageCode = new CodeValue();

                        objDocumentEntry.LanguageCode.CodingScheme = dbReader.GetString(dbReader.GetOrdinal("languageCodeDisplayName"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("practiceSettingCodeValue")))
                    {
                        objDocumentEntry.PracticeSettingsCode = new CodeValue();
                        objDocumentEntry.PracticeSettingsCode.Value = dbReader.GetString(dbReader.GetOrdinal("practiceSettingCodeValue"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("practiceSettingCodeDisplayName")))
                    {
                        if (objDocumentEntry.PracticeSettingsCode == null)
                            objDocumentEntry.PracticeSettingsCode = new CodeValue();

                        objDocumentEntry.PracticeSettingsCode.CodingScheme = dbReader.GetString(dbReader.GetOrdinal("practiceSettingCodeDisplayName"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("creationTime")))
                        objDocumentEntry.CreationTime = dbReader.GetDateTime(dbReader.GetOrdinal("creationTime"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("serviceStartTime")))
                        objDocumentEntry.ServiceStartTime = dbReader.GetDateTime(dbReader.GetOrdinal("serviceStartTime"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("serviceStopTime")))
                        objDocumentEntry.ServiceStopTime = dbReader.GetDateTime(dbReader.GetOrdinal("serviceStopTime"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("mimeType")))
                        objDocumentEntry.MimeType = dbReader.GetString(dbReader.GetOrdinal("mimeType"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("patientID")))
                    {
                        objDocumentEntry.DocumentPatient = new Patient();
                        objDocumentEntry.DocumentPatient.PatientID = dbReader.GetInt32(dbReader.GetOrdinal("patientID"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("entryUUID")))
                        objDocumentEntry.EntryUUID = dbReader.GetString(dbReader.GetOrdinal("entryUUID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("uniqueID")))
                        objDocumentEntry.UniqueID = dbReader.GetString(dbReader.GetOrdinal("uniqueID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("extrinsicObjectXML")))
                        objDocumentEntry.ExtrinsicObjectXML = dbReader.GetString(dbReader.GetOrdinal("extrinsicObjectXML"));

                    lstDocumentEntries.Add(objDocumentEntry);
                }
            }

            return lstDocumentEntries;
        }

        public List<CodeValue> GetDocumentEntryEventCodeList(int documentEntryID)
        {
            List<CodeValue> lstDocumentEntryEventCodeList = null;
            CodeValue objDocumentEntryEventCodeList = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_get_DocumentEntryEventCodeList_By_DocumentEntryID");

            db.AddInParameter(dbCommand, "documentEntryID", DbType.Int32, documentEntryID);

            using (IDataReader dbReader = db.ExecuteReader(dbCommand))
            {
                while (dbReader.Read())
                {
                    objDocumentEntryEventCodeList = new CodeValue();

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("eventCodeValue")))
                        objDocumentEntryEventCodeList.Value = dbReader.GetString(dbReader.GetOrdinal("eventCodeValue"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("eventCodeDisplayName")))
                        objDocumentEntryEventCodeList.CodingScheme = dbReader.GetString(dbReader.GetOrdinal("eventCodeDisplayName"));

                    lstDocumentEntryEventCodeList.Add(objDocumentEntryEventCodeList);
                }
            }

            return lstDocumentEntryEventCodeList;
        }

        public List<Folder> GetFolders(string availabilityStatus, string patientUID)
        {
            IDataReader dbReader = null;
            List<Folder> lstFolders = new List<Folder>();
            Folder objFolder = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_get_folderDetails_Folder_Patient");

            db.AddInParameter(dbCommand, "availabilityStatus", DbType.String, availabilityStatus);
            db.AddInParameter(dbCommand, "patientUID", DbType.String, patientUID);

            using (dbReader = db.ExecuteReader(dbCommand))
            {
                while (dbReader.Read())
                {
                    objFolder = new Folder();


                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderID")))
                        objFolder.FolderID = dbReader.GetInt32(dbReader.GetOrdinal("folderID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("availabilityStatus")))
                        objFolder.AvailabilityStatus = dbReader.GetString(dbReader.GetOrdinal("availabilityStatus"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("comments")))
                        objFolder.Comments = dbReader.GetString(dbReader.GetOrdinal("comments"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("entryUUID")))
                        objFolder.EntryUUID = dbReader.GetString(dbReader.GetOrdinal("entryUUID"));
                    
                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("lastUpdateTime")))
                        objFolder.LastUpdateTime = dbReader.GetDateTime(dbReader.GetOrdinal("lastUpdateTime"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("patientID")))
                    {
                        objFolder.FolderPatient = new Patient();
                        objFolder.FolderPatient.PatientID = dbReader.GetInt32(dbReader.GetOrdinal("patientID"));
                    }

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("title")))
                        objFolder.Title = dbReader.GetString(dbReader.GetOrdinal("title"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("uniqueID")))
                        objFolder.UniqueID = dbReader.GetString(dbReader.GetOrdinal("uniqueID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderXml")))
                        objFolder.FolderXml = dbReader.GetString(dbReader.GetOrdinal("folderXml"));

                    lstFolders.Add(objFolder);
                }
            }

            return lstFolders;
        }


        public List<FolderCodeList> GetFolderCodeListByFolderIDs(string folderIDs, string eventCodeValues, string eventCodeDisplayNames)
        {
            IDataReader dbReader = null;
            List<FolderCodeList> lstFolderCodeList = new List<FolderCodeList>();
            FolderCodeList folderCodeList = null;

            Database db = DatabaseFactory.CreateDatabase(CONST_CONNECTIONSTRING_NAME);
            DbCommand dbCommand = db.GetStoredProcCommand("usp_get_FolderCodeList_By_folderIDS");

            db.AddInParameter(dbCommand, "folderIDs", DbType.String, folderIDs);
            db.AddInParameter(dbCommand, "eventCodeValues", DbType.String, eventCodeValues);
            db.AddInParameter(dbCommand, "eventCodeDisplayNames", DbType.String, eventCodeDisplayNames);

            using (dbReader = db.ExecuteReader(dbCommand))
            {
                while (dbReader.Read())
                {
                    folderCodeList = new FolderCodeList();

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderCodeListID")))
                        folderCodeList.FolderCodeListID = dbReader.GetInt32(dbReader.GetOrdinal("folderCodeListID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("folderID")))
                        folderCodeList.FolderID = dbReader.GetInt32(dbReader.GetOrdinal("folderID"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("eventCodeValue")))
                        folderCodeList.EventCodeValue = dbReader.GetString(dbReader.GetOrdinal("eventCodeValue"));

                    if (!dbReader.IsDBNull(dbReader.GetOrdinal("eventCodeDisplayName")))
                        folderCodeList.EventCodeDisplayName = dbReader.GetString(dbReader.GetOrdinal("eventCodeDisplayName"));

                    lstFolderCodeList.Add(folderCodeList);
                }
            }

            return lstFolderCodeList;
        }
    }
}
