using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;
using System.Xml.Schema;
using System.IO;
using System.Globalization;
using System.Transactions;

using Microsoft.IHE.XDS.BusinessLogic.ATNA;


namespace Microsoft.IHE.XDS.BusinessLogic
{
    public class RegistryLogic
    {

        public bool IsDocumentMetaDataExists(XmlNodeList extrinsicNodes)
        {
            bool IsDocMetaDataExist = true;
            if (extrinsicNodes != null)
            {
                if (extrinsicNodes.Count < 1)
                {
                    IsDocMetaDataExist = false;
                }
            }
            else
            {
                IsDocMetaDataExist = false;
            }
            return IsDocMetaDataExist;
        }
      

        /// <summary>
        /// This function checks if the patient id exists in the feed
        /// </summary>
        /// <param name="patientUID"></param>
        /// <param name="patientID"></param>
        /// <returns></returns>
        /// 
        public bool IsPatientIDExistInFeed(string patientUID, out int patientID)
        {
            bool IsExist = false;
            try
            {
                RegistryDataAccess objDB = new RegistryDataAccess();
                IsExist = objDB.IsPatientIdExistinFeed(patientUID, out patientID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsExist;
        }

        /// <summary>
        /// This function checks whether all the patient Ids are same through out the document. IF they differ, it returns failure.
        /// </summary>
        /// <param name="xmlDocRequest">Xml Document</param>
        /// <returns></returns>
        public bool IsPatientUIdUnique(XmlDocument xmlDocRequest, out string patientUID)
        {
            bool IsIDUnique = true;
            XmlElement rootElement = xmlDocRequest.DocumentElement;
            XmlNode Root = rootElement.SelectSingleNode(@"//*[local-name()='RegistryObjectList']");

            //Getting all the ExtrinsicObjects from the XML
            XmlNodeList ExternalIDNodes = Root.SelectNodes(@"//*[local-name()='ExternalIdentifier']");

            List<string> patientIDs = new List<string>();

            patientUID = string.Empty;
            foreach (XmlNode externalId in ExternalIDNodes)
            {
                switch (externalId.Attributes["identificationScheme"].Value.ToString())
                {
                    case GlobalValues.XDSDocumentEntry_patientIdUUID:
                        patientIDs.Add(externalId.Attributes["value"].Value.ToString());
                        break;
                    case GlobalValues.XDSFolder_patientIdUUID:
                        patientIDs.Add(externalId.Attributes["value"].Value.ToString());
                        break;
                    case GlobalValues.XDSSubmissionSet_patientIdUUID:
                        patientIDs.Add(externalId.Attributes["value"].Value.ToString());
                        break;
                }
            }

            string firstID = patientIDs[0];
            foreach (string id in patientIDs)
            {
                if (!id.Equals(firstID))
                    IsIDUnique = false;
            }

            if (IsIDUnique) patientUID = firstID;
            return IsIDUnique;
        }

        bool IsSchemaError = false;
        /// <summary>
        /// This function checks if the document adheres to the IHE XDS schema.
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public bool IsSchemaValid(XmlDocument xdoc)
        {
            bool IsSchemaValid = true;
            try
            {

                string xsdTNS = "urn:ihe:iti:xds-b:2007";
                string xsdPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Schemas\IHEXDS.xsd";

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationEventHandler += new ValidationEventHandler(settings_ValidationEventHandler);
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas.Add(xsdTNS, xsdPath);
                XmlReader reader = XmlReader.Create(new StringReader(xdoc.OuterXml), settings);
                while (reader.Read())
                {
                    //Let Reader read all contents
                }
                if (IsSchemaError)
                {
                    IsSchemaValid = false;
                }
            }
            catch (Exception ex)
            {
                IsSchemaValid = false;
                throw ex;
            }
            return IsSchemaValid;
        }
        void settings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            IsSchemaError = true;
        }


        /// <summary>
        /// This function checks whether all the unique Ids are unique through out the document. IF they are same, it returns failure.
        /// </summary>
        /// <param name="doc">THE XML DOCUMENT</param>
        /// <returns></returns>
        public bool IsDuplicateUniqueIDinMessage(XmlDocument doc, out string submissionSetAndfolderUniqueIds, out string documentUniqueIds)
        {
            bool IsIDDuplicate = false;
            string externalIdValue = string.Empty;
            string uniqueId = string.Empty;
            List<string> lstFindAllResult = null;

            submissionSetAndfolderUniqueIds = string.Empty;
            documentUniqueIds = string.Empty;

            try
            {
                XmlElement rootElement = doc.DocumentElement;
                XmlNode Root = rootElement.SelectSingleNode(@"//*[local-name()='RegistryObjectList']");

                //Getting all the ExtrinsicObjects from the XML
                XmlNodeList ExternalIDNodes = Root.SelectNodes(@"//*[local-name()='ExternalIdentifier']");

                List<string> lstSubmissionSetAndfolderUniqueIds = new List<string>();
                List<string> lstDocumentUniqueIds = new List<string>();

                foreach (XmlNode externalId in ExternalIDNodes)
                {
                    externalIdValue = externalId.Attributes["value"].Value;

                    switch (externalId.Attributes["identificationScheme"].Value)
                    {                            
                        case GlobalValues.XDSDocumentEntry_uniqueIdUUID:
                            lstDocumentUniqueIds.Add(externalIdValue);

                            if(documentUniqueIds == string.Empty)
                                documentUniqueIds = externalIdValue;
                            else
                                documentUniqueIds += "," + externalIdValue;

                            break;
                        case GlobalValues.XDSFolder_uniqueIdUUID:
                            lstSubmissionSetAndfolderUniqueIds.Add(externalIdValue);

                            if(submissionSetAndfolderUniqueIds == string.Empty)
                                submissionSetAndfolderUniqueIds = externalIdValue;
                            else
                                submissionSetAndfolderUniqueIds += "," + externalIdValue;

                            break;
                        case GlobalValues.XDSSubmissionSet_uniqueIdUUID:
                            lstSubmissionSetAndfolderUniqueIds.Add(externalIdValue);

                            if (submissionSetAndfolderUniqueIds == string.Empty)
                                submissionSetAndfolderUniqueIds = externalIdValue;
                            else
                                submissionSetAndfolderUniqueIds += "," + externalIdValue;

                            break;
                    }
                }


                for (int count = 0; count < lstSubmissionSetAndfolderUniqueIds.Count; count++)
                {
                    uniqueId = lstSubmissionSetAndfolderUniqueIds[count];

                    lstFindAllResult = lstSubmissionSetAndfolderUniqueIds.FindAll(
                        delegate(string item)
                        {
                            if (item == uniqueId)
                                return true;

                            return false;
                        }
                        );

                    if (lstFindAllResult != null && lstFindAllResult.Count > 1)
                    {
                        IsIDDuplicate = true;
                        break;
                    }

                }

                //string firstID = lstSubmissionSetAndfolderUniqueIds[0];
                //for (int index = 1; index < lstSubmissionSetAndfolderUniqueIds.Count; index++)
                //{
                //    if (firstID.Equals(lstSubmissionSetAndfolderUniqueIds[index].ToString()))
                //    { IsIDDuplicate = true; break; }
                //}
                //if (submissionSetAndfolderUniqueIds.Length > 1) submissionSetAndfolderUniqueIds = submissionSetAndfolderUniqueIds.Remove(submissionSetAndfolderUniqueIds.Length - 1, 1);

            }
            catch
            {
                throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError);
            }
            return IsIDDuplicate;
        }

        public bool IsDuplicateUniqueIDinRegistry(string uniqueIDs)
        {
            RegistryDataAccess objDAL = new RegistryDataAccess();
            return objDAL.IsDuplicateUniqueIDinRegistry(uniqueIDs);
        }

        private bool IsDuplicateDocumentUniqueIdWithDifferentHash(List<DocumentEntry> lstDocumentEntryRequest, List<DocumentEntry> lstMatchingUniqueIDsDB, out string duplicateUniqueId)
        {
            bool isDuplicateWithdifferentHash = false;
            DocumentEntry documentEntryMatching = null;
            string documentUniqueId = null;
            string hash = null;

            duplicateUniqueId = string.Empty;

            for (int requestCount = 0; requestCount < lstDocumentEntryRequest.Count; requestCount++)
            {
                documentUniqueId = lstDocumentEntryRequest[requestCount].UniqueID;
                hash = lstDocumentEntryRequest[requestCount].Hash;

                documentEntryMatching = lstMatchingUniqueIDsDB.Find(
                    delegate(DocumentEntry documentEntry)
                    {
                        if(documentEntry.UniqueID == documentUniqueId)
                        {
                            return true;
                        }

                        return false;
                    }
                    );

                if (documentEntryMatching != null && documentEntryMatching.Hash != hash)
                {
                    isDuplicateWithdifferentHash = true;
                    break;
                }

            }


            return isDuplicateWithdifferentHash;
        }

        public bool IsMimeTypeBelongstoAffinityDomain(string mimeType)
        {
            string configurationValue = string.Empty;
            RegistryStoredQueryDataAccess objStoredQueryDAL = new RegistryStoredQueryDataAccess();

            //Check the mimetype configuration
            configurationValue = objStoredQueryDAL.GetConfigurationValue("validateMimeType");

            //if validateMimeType is not true then return true
            if (string.Compare(configurationValue, "true", true) != 0)
                return true;

            CodeType objCodeType = objStoredQueryDAL.GetCodeDetails("mimeType");

            if (objCodeType == null)
                return false;

            if (objCodeType.CodeValues == null)
                return false;

            for (int count = 0; count < objCodeType.CodeValues.Count; count++)
            {
                if (objCodeType.CodeValues[count].Value == mimeType)
                    return true;
            }

            return false;
        }


        public bool IsDuplicateClassification(XmlDocument doc)
        {
            bool IsIDDuplicate = false;
            try
            {
                XmlElement rootElement = doc.DocumentElement;
                XmlNode Root = rootElement.SelectSingleNode(@"//*[local-name()='RegistryObjectList']");

                //Getting all the Classificatio from the XML
                XmlNodeList rootClassifications = Root.SelectNodes(@"//*[local-name()='Classification']");

                List<string> uniqueIDs = new List<string>();

                foreach (XmlNode rootClassification in rootClassifications)
                {
                    uniqueIDs.Add(rootClassification.Attributes["id"].Value.ToString());
                }
                string firstID = uniqueIDs[0];
                for (int index = 1; index < uniqueIDs.Count; index++)
                {
                    if (firstID.Equals(uniqueIDs[index].ToString()))
                    { IsIDDuplicate = true; break; }
                }
            }

            catch
            {
                throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError);
            }
            return IsIDDuplicate;
        }


        private bool IsMetadataValid(XmlNodeList xdsExtrinsicDocuments, XmlElement rootElement)
        {
            bool IsValid = true;
            try
            {
                foreach (XmlNode xdsExtrinsicDocument in xdsExtrinsicDocuments)
                {
                    if (xdsExtrinsicDocument.Attributes["id"].Equals(null))
                    {
                        IsValid = false;
                    }
                    if (xdsExtrinsicDocument.Attributes["mimeType"].Equals(null))
                    {
                        IsValid = false;
                    }
                }
            }
            catch
            {
                throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError);
            }
            return IsValid;
        }


        private bool IsAppendRequestForDeprecatedDocument(XmlDocument xmlDocRequest)
        {
            bool isAppendRequestForDeprecatedDocument = false;
            RegistryStoredQueryDataAccess registryQueryDataAccess = null;
            List<DocumentEntry> lstDocumentEntry = null;
            XmlElement eltRoot = null;
            XmlNodeList nodeListAssociation = null;

            string entryUUID = null;
            string xpath = null;
            string xpathAssociation_APND = ".//*[local-name()='RegistryObjectList']/*[local-name()='Association'][@associationType='$associationType$']";
            string xpathAssociation_XFRM = ".//*[local-name()='RegistryObjectList']/*[local-name()='Association'][@associationType='$associationType$']";

            registryQueryDataAccess = new RegistryStoredQueryDataAccess();

            eltRoot = xmlDocRequest.DocumentElement;

            xpath = xpathAssociation_APND.Replace("$associationType$", GlobalValues.CONST_ASSOCIATION_TYPE_APND);
            nodeListAssociation = eltRoot.SelectNodes(xpath);

            if (nodeListAssociation != null && nodeListAssociation.Count > 0)
            {

                for (int count = 0; count < nodeListAssociation.Count; count++)
                {
                    entryUUID = nodeListAssociation[count].Attributes["targetObject"].Value;

                    lstDocumentEntry = registryQueryDataAccess.GetDocuments(entryUUID, null);

                    if ((lstDocumentEntry != null) && (lstDocumentEntry.Count > 0))
                    {
                        for (int documentCount = 0; documentCount < lstDocumentEntry.Count; documentCount++)
                        {
                            if (lstDocumentEntry[documentCount].AvailabilityStatus == GlobalValues.CONST_AVAILABILITYSTATUS_DEPRECATED)
                            {
                                isAppendRequestForDeprecatedDocument = true;
                                break;
                            }
                        }
                    }

                }

            }

            if (isAppendRequestForDeprecatedDocument)
                return isAppendRequestForDeprecatedDocument;

            xpath = xpathAssociation_XFRM.Replace("$associationType$", GlobalValues.CONST_ASSOCIATION_TYPE_XFRM);
            nodeListAssociation = eltRoot.SelectNodes(xpath);

            if (nodeListAssociation != null && nodeListAssociation.Count > 0)
            {

                for (int count = 0; count < nodeListAssociation.Count; count++)
                {
                    entryUUID = nodeListAssociation[count].Attributes["targetObject"].Value;

                    lstDocumentEntry = registryQueryDataAccess.GetDocuments(entryUUID, null);

                    if ((lstDocumentEntry != null) && (lstDocumentEntry.Count > 0))
                    {
                        for (int documentCount = 0; documentCount < lstDocumentEntry.Count; documentCount++)
                        {
                            if (lstDocumentEntry[documentCount].AvailabilityStatus == GlobalValues.CONST_AVAILABILITYSTATUS_DEPRECATED)
                            {
                                isAppendRequestForDeprecatedDocument = true;
                                break;
                            }
                        }
                    }

                }

            }



            return isAppendRequestForDeprecatedDocument;
        }



        private bool IsMissingRepositoryUniqueId(XmlDocument xmlDocRequest)
        {
            bool isMissingRepositoryUniqueId = false;
            XmlElement eltRoot = null;
            XmlNodeList nodeListExtrinsicObject = null;
            XmlNodeList nodeListSlotRepositoryUniqueId = null;
            string xpathExtrinsicObject = ".//*[local-name()='RegistryObjectList']/*[local-name()='ExtrinsicObject']";
            string xpathSlotRepositoryUniqueId = ".//*[local-name()='RegistryObjectList']/*[local-name()='ExtrinsicObject'][@id='$id$']/*[local-name()='Slot'][@name='repositoryUniqueId']";
            string xpath = null;
            string documentEntryUUID = null;

            eltRoot = xmlDocRequest.DocumentElement;

            nodeListExtrinsicObject = eltRoot.SelectNodes(xpathExtrinsicObject);


            for (int count = 0; count < nodeListExtrinsicObject.Count; count++)
            {
                documentEntryUUID = nodeListExtrinsicObject[count].Attributes["id"].Value;

                xpath = xpathSlotRepositoryUniqueId.Replace("$id$", documentEntryUUID);
                nodeListSlotRepositoryUniqueId = eltRoot.SelectNodes(xpath);

                if ((nodeListSlotRepositoryUniqueId == null) || (nodeListSlotRepositoryUniqueId.Count == 0))
                {
                    isMissingRepositoryUniqueId = true;
                    break;
                }                
            }

            return isMissingRepositoryUniqueId;
        }


        public XDSResponse RegisterDocumentSet(XmlDocument xmlDocRequest)
        {
            XDSResponse xdsResponse = new XDSResponse();
            RegistryDataAccess objRegistryDataAccess = null;
            List<DocumentEntry> lstDocumentEntryMatchingUniqueId = null;
            SubmissionSet objSubmissionSet = null;
            List<Folder> lstFolder = null;
            List<DocumentEntry> lstDocumentEntry = null;
            List<DocumentEntry> lstReplacedDocumentEntry = null;
            List<Association> lstAssociation = null;
            List<Association> lstReplaceAssociation = null;
            XmlElement eltRoot = null;
            XmlNode nodeSubmissionSet = null;
            XmlNode nodeRegistryObjectList = null;
            XmlNodeList nodeListExtrinsicObject = null;            
            string patientUID = string.Empty;
            string submissionSetAndfolderUniqueIds = string.Empty;
            string documentUniqueIds = string.Empty;
            int patientID = 0;
            DateTime dtStartTime = DateTime.Now;
            string submissionSetUniqueID = string.Empty;
            string errorMessage = string.Empty;

            try
            {

                //SubmitObjectsRequest
                eltRoot = xmlDocRequest.DocumentElement;

                //RegistryObjectList
                nodeRegistryObjectList = eltRoot.SelectSingleNode(@"//*[local-name()='RegistryObjectList']");

                //ExtrinsicObject
                nodeListExtrinsicObject = nodeRegistryObjectList.SelectNodes(@"//*[local-name()='ExtrinsicObject']");

                //RegistryPackage = SubmissionSet
                nodeSubmissionSet = eltRoot.SelectSingleNode("//*[local-name()='SubmitObjectsRequest']/*[local-name()=\"RegistryObjectList\"]/*[local-name()='RegistryPackage']/*[local-name()='ExternalIdentifier'][@identificationScheme='urn:uuid:96fdda7c-d067-4183-912e-bf5ee74998a8']/@value");

                if (nodeSubmissionSet != null)
                {
                    //ATNA
                    xdsResponse.AtnaParameters.Add("$SubmissionSet.UniqueID$", nodeSubmissionSet.Value);
                }


                #region Validations

                if (IsDuplicateUniqueIDinMessage(xmlDocRequest, out submissionSetAndfolderUniqueIds, out documentUniqueIds))
                {
                    errorMessage = "A UniqueId \"{0}\" value was found to be used more than once within the submission.";
                    errorMessage = string.Format(errorMessage, submissionSetAndfolderUniqueIds);
                    xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, errorMessage, GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryDuplicateUniqueIdInMessage, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xdsResponse;
                    //throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryDuplicateUniqueIdInMessage);
                }
                if (IsDuplicateUniqueIDinRegistry(submissionSetAndfolderUniqueIds))
                {
                    errorMessage = "UniqueId \"{0}\" received was not unique within the Registry. UniqueId could have been attached to XDSSubmissionSet or XDSFolder.";
                    errorMessage = string.Format(errorMessage, submissionSetAndfolderUniqueIds);
                    xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, errorMessage, GlobalValues.CONST_REGISTRYERROR_CODE_XDSDuplicateUniqueIdInRegistry, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xdsResponse;
                    //throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_XDSDuplicateUniqueIdInRegistry);
                }

                //Get DcoumentEntry matching document UniqueIDs, will be used to validate uniqueId & hash
                if (!string.IsNullOrEmpty(documentUniqueIds))
                {
                    RegistryStoredQueryDataAccess storedQueryDataAccess = new RegistryStoredQueryDataAccess();
                    lstDocumentEntryMatchingUniqueId = storedQueryDataAccess.GetDocuments(null, documentUniqueIds);
                }

                //Validation not required as the request can be without a document(Just Create a folder)
                //if (!IsDocumentMetaDataExists(nodeListExtrinsicObject))
                //{
                //    xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryMetadataError, GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryMetadataError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                //    return xdsResponse;
                //    //throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryMetadataError);
                //}

                if (!IsMetadataValid(nodeListExtrinsicObject, eltRoot))
                {
                    xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Indicates that this registry did not support the capability required to service the request.", GlobalValues.CONST_REGISTRYERROR_CODE_UnsupportedCapabilityException, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xdsResponse;                    
                    //throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_UnsupportedCapabilityException);
                }

                if (!IsPatientUIdUnique(xmlDocRequest, out patientUID))
                {
                    xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "XDS specifies where patient IDs must match between documents, submission sets, and folders. This error is thrown when the patient ID is required to match and does not.  The codeContext shall indicate the value of the Patient Id and the nature of the conflict.", GlobalValues.CONST_REGISTRYERROR_CODE_XDSPatientIdDoesNotMatch, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xdsResponse;
                    //throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_XDSPatientIdDoesNotMatch);
                }

                if (!IsPatientIDExistInFeed(patientUID, out patientID))
                {
                    xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Patient ID referenced in metadata is not known to the Registry actor via the Patient Identity Feed or is unknown because of patient identifier merge.  The codeContext shall include the value of patient ID in question.", GlobalValues.CONST_REGISTRYERROR_CODE_XDSUnknownPatientId, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xdsResponse;
                    //throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_XDSUnknownPatientId);
                }
                if (IsDuplicateClassification(xmlDocRequest))
                {
                    xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Indicates that this registry did not support the capability required to service the request.", GlobalValues.CONST_REGISTRYERROR_CODE_UnsupportedCapabilityException, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xdsResponse;
                    //throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_UnsupportedCapabilityException);
                }

                //repositoryUniqueId - Validation
                if (IsMissingRepositoryUniqueId(xmlDocRequest))
                {
                    xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "repositoryUniqueId is missing for one or more documents.", GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryMetadataError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xdsResponse;
                }

                //Validation Append request for deprecated document 
                //Associations of type Append (APND & XFRM)
                if (IsAppendRequestForDeprecatedDocument(xmlDocRequest))
                {
                    xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Cannot perform APND or XFRM on deprecated document.", GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryMetadataError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xdsResponse;
                }



#if debug 
                if (!IsSchemaValid(doc))
                {                    
                    xdsResponse.XDSResponseDocument = ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Indicates that the requestor attempted to perform an operation that was semantically invalid.", GlobalValues.CONST_ERROR_CODE_XDSInvalidRequest, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xdsResponse;
                }
#endif

                #endregion

                //SubmissionSet
                objSubmissionSet = GetSubmissionSet(xmlDocRequest);

                if (objSubmissionSet == null)
                {
                    xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Indicates that this registry did not support the capability required to service the request.", GlobalValues.CONST_REGISTRYERROR_CODE_UnsupportedCapabilityException, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xdsResponse;

                    //throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError);
                }

                //Folder
                lstFolder = GetFolders(xmlDocRequest);

                if (lstFolder == null)
                    lstFolder = new List<Folder>();

                //DocumentEntry
                lstDocumentEntry = GetDocumentEntries(xmlDocRequest);

                if (lstDocumentEntry == null)
                    throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError);

                //Validation : Document UniqueIds & hash
                if ((lstDocumentEntryMatchingUniqueId != null) && (lstDocumentEntryMatchingUniqueId.Count > 0))
                {
                    string duplicateUniqueId = null;
                    errorMessage = "Document being registered was a duplicate ({0} already in registry) but hash does not match.";

                    if (IsDuplicateDocumentUniqueIdWithDifferentHash(lstDocumentEntry, lstDocumentEntryMatchingUniqueId, out duplicateUniqueId))
                    {
                        errorMessage = string.Format(errorMessage, duplicateUniqueId);

                        xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, errorMessage, GlobalValues.CONST_REGISTRYERROR_CODE_XDSNonIdenticalHash, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                        return xdsResponse;
                    }
                }

                //Association
                lstAssociation = GetAssociations(xmlDocRequest);

                if (lstAssociation == null)
                    throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError);                

                //Associations of type Replace (RPLC & XFRM_RPLC)
                lstReplaceAssociation = GetReplaceAssociation(lstAssociation);

                //Get & Update status of all the DocumentEntry/ExtrinsicObject
                lstReplacedDocumentEntry = GetDocumentEntryByEntryUUIDs(lstReplaceAssociation);
                lstReplacedDocumentEntry = UpdateExtrinsicObjectDeprecateStatus(lstReplacedDocumentEntry);

                //Associates Objects (SubmissionSet - Folder - DocumentEntry)
                AssociateObjects(xmlDocRequest, objSubmissionSet, lstFolder, lstDocumentEntry, lstAssociation);
                               
                try
                {
                    objRegistryDataAccess = new RegistryDataAccess();

                    using (TransactionScope registryTransaction = new System.Transactions.TransactionScope())
                    {

                        objRegistryDataAccess.RegisterDocumentSet(lstDocumentEntry, lstFolder, objSubmissionSet, lstReplacedDocumentEntry, patientID, xmlDocRequest.InnerText, dtStartTime);

                        registryTransaction.Complete();
                    }

                }
                catch
                {
                    RegistryLog nlog = new RegistryLog();
                    nlog.RequesterIdentity = System.Environment.UserName;
                    nlog.RequestMetadata = xmlDocRequest.InnerXml;
                    nlog.Result = GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE;
                    nlog.TransactionName = "RegisterDocumentSet-b";
                    nlog.StartTime = dtStartTime;
                    nlog.FinishTime = DateTime.Now;
                    RegistryDataAccess obj = new RegistryDataAccess();
                    obj.CreateRegistryLogEntry(nlog);

                    xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "XDSRegistryError", GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xdsResponse;
                }

            }
            catch
            {
                xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "XDSRegistryError", GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                return xdsResponse;
            }

            //Construct Success Response
            xdsResponse.XDSResponseDocument = CommonUtility.ConstructRegistrySuccessResponse();

            return xdsResponse;
        }
             

        public void CreateRegistryLogEntry(string metaData, string result, DateTime start)
        {
            RegistryLog nlog = new RegistryLog();
            nlog.RequesterIdentity = System.Environment.UserName;
            nlog.RequestMetadata = metaData;
            nlog.Result = result;
            nlog.TransactionName = "RegisterDocumentSet-b";
            nlog.StartTime = start;
            nlog.FinishTime = DateTime.Now;
            RegistryDataAccess objDAL = new RegistryDataAccess();
            objDAL.CreateRegistryLogEntry(nlog);
        }

        public DateTime ParseStringToDateTime(string datetimeString, string nameOftheField)
        {
            DateTime dt = DateTime.MinValue;
            try
            {
                if (datetimeString != null)
                {
                    if (datetimeString.Length == 14)
                    {
                        dt = DateTime.ParseExact(datetimeString, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                    }
                    else if (datetimeString.Length == 12)
                    {
                        dt = DateTime.ParseExact(datetimeString, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                    }
                    else if (datetimeString.Length == 10)
                    {
                        dt = DateTime.ParseExact(datetimeString, "yyyyMMddHH", CultureInfo.InvariantCulture);
                    }

                    else if (datetimeString.Length == 8)
                    {
                        dt = DateTime.ParseExact(datetimeString, "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        throw new Exception("Message contains Invalid DateTime in " + nameOftheField);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt;
        }


        private List<DocumentEntry> GetDocumentEntries(XmlDocument xmlDocRequest)
        {
            if (xmlDocRequest == null)
                throw new ArgumentNullException("xmlDocRequest");

            XmlElement eltRoot = null;
            XmlNodeList nodeListExtrinsicObject = null;
            XmlNodeList nodeListExternalIdentifier = null;
            XmlNodeList nodeListSlot = null;
            XmlNode node = null;

            List<DocumentEntry> lstDocumentEntry = new List<DocumentEntry>();
            DocumentEntry objDocumentEntry = null;
            
            string xpathExternalIdentifier = @".//*[local-name()='RegistryObjectList']/*[local-name()='ExtrinsicObject'][@id='$id$']/*[local-name()='ExternalIdentifier']";
            string xpathSlot = @".//*[local-name()='RegistryObjectList']/*[local-name()='ExtrinsicObject'][@id='$id$']/*[local-name()='Slot']";
            string xpathSlotValue = @".//*[local-name()='RegistryObjectList']/*[local-name()='ExtrinsicObject'][@id='$id$']/*[local-name()='Slot'][@name='$name$']/*[local-name()='ValueList']/*[local-name()='Value']";
            string xpathSlotValueCurrent = null;
            string identificationScheme = null;
            string externalIdentifierValue = null;
            string slotName = null;
            string slotValue = null;
            int size = 0;

            //SubmitObjectsRequest
            eltRoot = xmlDocRequest.DocumentElement; ;

            //ExtrinsicObject
            nodeListExtrinsicObject = eltRoot.SelectNodes(@".//*[local-name()='RegistryObjectList']/*[local-name()='ExtrinsicObject']");

            for (int nodeCountExtrinsicObject = 0; nodeCountExtrinsicObject < nodeListExtrinsicObject.Count; nodeCountExtrinsicObject++)
            {
                objDocumentEntry = new DocumentEntry();
                objDocumentEntry.AvailabilityStatus = GlobalValues.CONST_AVAILABILITYSTATUS_APPROVED;

                //Add/Update the status attribute of ExtrinsicObject
                if (nodeListExtrinsicObject[nodeCountExtrinsicObject].Attributes["status"] == null)
                {
                    XmlAttribute attribStatus = xmlDocRequest.CreateAttribute("status");
                    attribStatus.Value = GlobalValues.CONST_AVAILABILITYSTATUS_APPROVED;
                    nodeListExtrinsicObject[nodeCountExtrinsicObject].Attributes.Append(attribStatus);
                }
                else
                {
                    nodeListExtrinsicObject[nodeCountExtrinsicObject].Attributes["status"].Value = objDocumentEntry.AvailabilityStatus;
                }

                //mimeType
                objDocumentEntry.MimeType = nodeListExtrinsicObject[nodeCountExtrinsicObject].Attributes["mimeType"].Value;

                //ExtrinsicObject Xml
                objDocumentEntry.ExtrinsicObjectXML = nodeListExtrinsicObject[nodeCountExtrinsicObject].OuterXml;

                //Mime Type Validation
                if (!IsMimeTypeBelongstoAffinityDomain(objDocumentEntry.MimeType))
                {
                    throw new Exception(GlobalValues.CONST_REGISTRYERROR_CODE_UnsupportedCapabilityException);
                }

                //id/entryUUID
                objDocumentEntry.EntryUUID = nodeListExtrinsicObject[nodeCountExtrinsicObject].Attributes["id"].Value;

                //ExtrinsicObject/Name/LocalizedString
                node = nodeListExtrinsicObject[nodeCountExtrinsicObject].SelectSingleNode(@".//*[local-name()='Name']/*[local-name()='LocalizedString']");

                //value
                if (node != null)
                    objDocumentEntry.Title = node.Attributes["value"].Value;

                //ExtrinsicObject/Description
                node = nodeListExtrinsicObject[nodeCountExtrinsicObject].SelectSingleNode(@".//*[local-name()='Description']");

                //Comments
                if ((node != null) && (node.InnerText != null))
                    objDocumentEntry.Comments = node.InnerText;

                #region ExternalIdentifier

                //ExternalIdentifier
                nodeListExternalIdentifier = eltRoot.SelectNodes(xpathExternalIdentifier.Replace("$id$", objDocumentEntry.EntryUUID));

                for (int nodeCountExtId = 0; nodeCountExtId < nodeListExternalIdentifier.Count; nodeCountExtId++)
                {
                    identificationScheme = nodeListExternalIdentifier[nodeCountExtId].Attributes["identificationScheme"].Value;
                    externalIdentifierValue = nodeListExternalIdentifier[nodeCountExtId].Attributes["value"].Value;


                    if (identificationScheme == GlobalValues.XDSDocumentEntry_patientIdUUID) //PatientUID
                    {
                        objDocumentEntry.DocumentPatient = new Patient();
                        objDocumentEntry.DocumentPatient.PatientUID = externalIdentifierValue;
                    }
                    else if (identificationScheme == GlobalValues.XDSDocumentEntry_uniqueIdUUID) //UniqueID
                    {
                        objDocumentEntry.UniqueID = externalIdentifierValue;
                    }
                }

                #endregion

                #region Slot

                //Slots
                nodeListSlot = eltRoot.SelectNodes(xpathSlot.Replace("$id$", objDocumentEntry.EntryUUID));

                for (int nodeCountSlot = 0; nodeCountSlot < nodeListSlot.Count; nodeCountSlot++)
                {
                    slotName = nodeListSlot[nodeCountSlot].Attributes["name"].Value;

                    xpathSlotValueCurrent = xpathSlotValue;
                    xpathSlotValueCurrent = xpathSlotValueCurrent.Replace("$id$", objDocumentEntry.EntryUUID);
                    xpathSlotValueCurrent = xpathSlotValueCurrent.Replace("$name$", slotName);

                    node = eltRoot.SelectSingleNode(xpathSlotValueCurrent);

                    if(node != null)
                        slotValue = node.InnerText;

                    switch (slotName)
                    {
                        case "creationTime":                            
                            objDocumentEntry.CreationTime = ParseStringToDateTime(slotValue, "ExtrinsicObject->Slot->creationTime");
                            break;

                        case "languageCode":
                            objDocumentEntry.LanguageCode = new CodeValue();
                            objDocumentEntry.LanguageCode.Value = slotValue;
                            break;

                        case "serviceStartTime":
                            objDocumentEntry.ServiceStartTime = ParseStringToDateTime(slotValue, "ExtrinsicObject->Slot->serviceStartTime");
                            break;

                        case "serviceStopTime":
                            objDocumentEntry.ServiceStopTime = ParseStringToDateTime(slotValue, "ExtrinsicObject->Slot->serviceStopTime");
                            break;

                        case "sourcePatientId":
                            objDocumentEntry.SourcePatientID = slotValue;
                            break;

                        case "sourcePatientInfo":
                            objDocumentEntry.SourcePatientInfo = node.ParentNode.OuterXml;
                            break;

                        case "repositoryUniqueId":
                            objDocumentEntry.RepositoryUniqueID = slotValue;
                            break;

                        case "hash":
                            objDocumentEntry.Hash = slotValue;
                            break;

                        case "size":

                            if (int.TryParse(slotValue, out size))
                                objDocumentEntry.Size = size;

                            break;

                        case "URI":
                            objDocumentEntry.URI = slotValue;
                            break;

                        case "legalAuthenticator":
                            objDocumentEntry.LegalAuthenticator = slotValue;
                            break;
                    }

                }

                #endregion

                //Update Document Classifications
                UpdateDocumentClassification(xmlDocRequest, ref objDocumentEntry);

                //Add Document Entry to the collection
                lstDocumentEntry.Add(objDocumentEntry);
            }

            return lstDocumentEntry;
        }

        private void UpdateDocumentClassification(XmlDocument xmlDocRequest, ref DocumentEntry objDocumentEntry)
        {
            string xpathClassification = @".//*[local-name()='RegistryObjectList']/*[local-name()='ExtrinsicObject'][@id='$id$']/*[local-name()='Classification']";
            string xpathSlot = @".//*[local-name()='RegistryObjectList']/*[local-name()='ExtrinsicObject'][@id='$id$']/*[local-name()='Classification'][@id='$classificationid$']/*[local-name()='Slot']";
            string xpathSlotValue = @".//*[local-name()='RegistryObjectList']/*[local-name()='ExtrinsicObject'][@id='$id$']/*[local-name()='Classification'][@id='$classificationid$']/*[local-name()='Slot']/*[local-name()='ValueList']/*[local-name()='Value']";
            //string xpathLocalizedString = @".//*[local-name()='RegistryObjectList']/*[local-name()='ExtrinsicObject'][@id='$id$']/*[local-name()='Classification'][@id='$classificationid$']/*[local-name()='Name']/*[local-name()='LocalizedString']";
            string xpathSlotCodingSchemeValue = @".//*[local-name()='RegistryObjectList']/*[local-name()='ExtrinsicObject'][@id='$id$']/*[local-name()='Classification'][@id='$classificationid$']/*[local-name()='Slot'][@name='codingScheme']/*[local-name()='ValueList']/*[local-name()='Value']";

            string classificationId = null;
            string classificationScheme = null;
            string slotValue = null;
            //string localizedStringValue = null;
            string nodeRepresentation = null;
            string codingScheme = null;

            XmlElement eltRoot = null;
            XmlNodeList nodeListClassification = null;
            XmlNodeList nodeListSlot = null;
            XmlNodeList nodeListSlotValue = null;
            XmlNode nodeSlotValue = null;
            //XmlNode nodeLocalizedString = null;
            XmlNode nodeSlotCodingSchemeValue = null;


            //SubmitObjectsRequest
            eltRoot = xmlDocRequest.DocumentElement;

            //ExtrinsicObject
            nodeListClassification = eltRoot.SelectNodes(xpathClassification.Replace("$id$", objDocumentEntry.EntryUUID));
            xpathSlot = xpathSlot.Replace("$id$", objDocumentEntry.EntryUUID);
            xpathSlotValue = xpathSlotValue.Replace("$id$", objDocumentEntry.EntryUUID);
            //xpathLocalizedString = xpathLocalizedString.Replace("$id$", objDocumentEntry.EntryUUID);
            xpathSlotCodingSchemeValue = xpathSlotCodingSchemeValue.Replace("$id$", objDocumentEntry.EntryUUID);


            if (nodeListClassification == null)
                return;

            for (int nodeCount = 0; nodeCount < nodeListClassification.Count; nodeCount++)
            {
                classificationId = nodeListClassification[nodeCount].Attributes["id"].Value;
                classificationScheme = nodeListClassification[nodeCount].Attributes["classificationScheme"].Value;

                if (nodeListClassification[nodeCount].Attributes["nodeRepresentation"] != null)
                    nodeRepresentation = nodeListClassification[nodeCount].Attributes["nodeRepresentation"].Value;

                //Slot
                nodeListSlot = eltRoot.SelectNodes(xpathSlot.Replace("$classificationid$", classificationId));

                //Value - Nodes
                nodeListSlotValue = eltRoot.SelectNodes(xpathSlotValue.Replace("$classificationid$", classificationId));

                //Value - Node
                nodeSlotValue = eltRoot.SelectSingleNode(xpathSlotValue.Replace("$classificationid$", classificationId));
                if(nodeSlotValue != null)
                    slotValue = nodeSlotValue.InnerText;

                //LocalizedString - Node
                //nodeLocalizedString = eltRoot.SelectSingleNode(xpathLocalizedString.Replace("$classificationid$", classificationId));
                //if(nodeLocalizedString != null)
                //    localizedStringValue = nodeLocalizedString.Attributes["value"].Value;

                //<Slot name="codingScheme"> -> <ValueList> -> <Value>Connect-a-thon confidentialityCodes</Value>
                nodeSlotCodingSchemeValue = eltRoot.SelectSingleNode(xpathSlotCodingSchemeValue.Replace("$classificationid$", classificationId));
                if (nodeSlotCodingSchemeValue != null)
                    codingScheme = nodeSlotCodingSchemeValue.InnerText;

                switch (classificationScheme)
                {
                    case GlobalValues.XDSDocumentEntry_classCodeUUID:
                        objDocumentEntry.ClassCode = new CodeValue();
                        objDocumentEntry.ClassCode.Value = nodeRepresentation;
                        objDocumentEntry.ClassCode.CodingScheme = slotValue;
                        break;

                    case GlobalValues.XDSDocumentEntry_confidentialityCodeUUID:
                        objDocumentEntry.ConfidentialityCode = new CodeValue();
                        objDocumentEntry.ConfidentialityCode.Value = nodeRepresentation;
                        objDocumentEntry.ConfidentialityCode.CodingScheme = slotValue;
                        break;

                    case GlobalValues.XDSDocumentEntry_formatCodeUUID:
                        objDocumentEntry.FormatCode = new CodeValue();
                        objDocumentEntry.FormatCode.Value = nodeRepresentation;
                        objDocumentEntry.FormatCode.CodingScheme = slotValue;
                        break;

                    case GlobalValues.XDSDocumentEntry_healthCareFacilityTypeCodeUUID:
                        objDocumentEntry.HealthcareFacilityCode = new CodeValue();
                        objDocumentEntry.HealthcareFacilityCode.Value = nodeRepresentation;
                        objDocumentEntry.HealthcareFacilityCode.CodingScheme = slotValue;
                        break;

                    case GlobalValues.XDSDocumentEntry_practiceSettingCodeUUID:
                        objDocumentEntry.PracticeSettingsCode = new CodeValue();
                        objDocumentEntry.PracticeSettingsCode.Value = nodeRepresentation;
                        objDocumentEntry.PracticeSettingsCode.CodingScheme = slotValue;
                        break;

                    case GlobalValues.XDSDocumentEntry_typeCodeUUID:
                        objDocumentEntry.TypeCode = new CodeValue();
                        objDocumentEntry.TypeCode.Value = nodeRepresentation;
                        objDocumentEntry.TypeCode.CodingScheme = slotValue;
                        break;

                    case GlobalValues.XDSDocumentEntry_eventCodeListUUID:
                        
                        List<CodeValue> objEvents = new List<CodeValue>();

                        foreach (XmlNode nodeValue in nodeListSlotValue)
                        {
                            CodeValue eventCode = new CodeValue();
                            eventCode.Value = nodeRepresentation;
                            eventCode.CodingScheme = nodeValue.InnerText; 
                            objEvents.Add(eventCode);
                        }

                        objDocumentEntry.EventCodeList = objEvents;
                        break;

                    case GlobalValues.XDSDocumentEntry_authorDescriptionUUID:

                        Author objAuthor = new Author();

                        if (objDocumentEntry.DocumentAuthor == null)
                            objDocumentEntry.DocumentAuthor = new List<Author>();

                        foreach (XmlNode slot in nodeListSlot)
                        {                            
                            if (slot.Attributes["name"].Value.Equals("authorPerson"))
                                objAuthor.Person = slot.InnerXml;
                            else if (slot.Attributes["name"].Value.Equals("authorInstitution"))
                                objAuthor.Institution = slot.InnerXml;
                            else if (slot.Attributes["name"].Value.Equals("authorRole"))
                                objAuthor.Role = slot.InnerXml;
                            else if (slot.Attributes["name"].Value.Equals("authorSpecialty"))
                                objAuthor.Specialty = slot.InnerXml;                            
                        }
                        
                        objDocumentEntry.DocumentAuthor.Add(objAuthor);

                        break;
                }
            }
        }

        private SubmissionSet GetSubmissionSet(XmlDocument xmlDocRequest)
        {
            string xpathClassificationSubmissionSet = @".//*[local-name()='RegistryObjectList']/*[local-name()='Classification'][@classificationNode='$classificationNode$']";
            string xpathRegistryPackage = @".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']";            
            string xpathNameLocalizedString = @".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='Name']/*[local-name()='LocalizedString']";
            string xpathDescriptionLocalizedString = @".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='Description']/*[local-name()='LocalizedString']";
            string xpathSubmissionTimeValue = @".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='Slot'][@name='submissionTime']/*[local-name()='ValueList']/*[local-name()='Value']";
            string xpathExternalIdentifier = @".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='ExternalIdentifier']"; 

            string xpath = null;
            string classifiedObject = null;
            string identificationScheme = null;

            SubmissionSet objSubmissionSet = null;

            XmlElement eltRoot = null;
            XmlNodeList nodeListClassification = null;
            XmlNodeList nodeListExternalIdentifier = null;
            XmlNode nodeRegistryPackage = null;
            XmlNode node = null;



            //SubmitObjectsRequest
            eltRoot = xmlDocRequest.DocumentElement;

            //Classification
            xpath = xpathClassificationSubmissionSet.Replace("$classificationNode$", GlobalValues.XDSSubmissionSetUUID);
            nodeListClassification = eltRoot.SelectNodes(xpath);

            if (nodeListClassification == null)
                return null;

            for (int classificationCount = 0; classificationCount < nodeListClassification.Count; classificationCount++)
            {
                objSubmissionSet = new SubmissionSet();

                classifiedObject = nodeListClassification[classificationCount].Attributes["classifiedObject"].Value;

                if (string.IsNullOrEmpty(classifiedObject))
                    continue;

                //RegistryPackage
                xpath = xpathRegistryPackage.Replace("$id$", classifiedObject);
                nodeRegistryPackage = eltRoot.SelectSingleNode(xpath);

                if (nodeRegistryPackage == null)
                    continue;

                //Set the SubmissionSet Status to Approved
                if (nodeRegistryPackage.Attributes["status"] == null)
                {
                    XmlAttribute attribStatus = xmlDocRequest.CreateAttribute("status");
                    attribStatus.Value = GlobalValues.CONST_AVAILABILITYSTATUS_APPROVED;
                    nodeRegistryPackage.Attributes.Append(attribStatus);
                }
                else
                {
                    nodeRegistryPackage.Attributes["status"].Value = GlobalValues.CONST_AVAILABILITYSTATUS_APPROVED;
                }

                //SubmissionSet/RegistryPackage Xml
                objSubmissionSet.SubmissionSetXml = nodeRegistryPackage.OuterXml + nodeListClassification[classificationCount].OuterXml;

                //AvailabilityStatus
                objSubmissionSet.AvailabilityStatus = GlobalValues.CONST_AVAILABILITYSTATUS_APPROVED;

                //EntryUUID/classifiedObject
                objSubmissionSet.EntryUUID = classifiedObject;

                //submissionTime
                xpath = xpathSubmissionTimeValue.Replace("$id$", classifiedObject);
                node = eltRoot.SelectSingleNode(xpath);

                if (node != null)
                    objSubmissionSet.SubmissionTime = ParseStringToDateTime(node.InnerText, "SubmissionSet->Slot->submissionTime");

                //Name->LocalizedString
                xpath = xpathNameLocalizedString.Replace("$id$", classifiedObject);
                node = eltRoot.SelectSingleNode(xpath);

                if (node != null)
                    objSubmissionSet.Title = node.Attributes["value"].Value;

                //Description->LocalizedString
                xpath = xpathDescriptionLocalizedString.Replace("$id$", classifiedObject);
                node = eltRoot.SelectSingleNode(xpath);

                if (node != null)
                    objSubmissionSet.Comments = node.Attributes["value"].Value;


                #region ExternalIdentifier
                //ExternalIdentifier
                xpath = xpathExternalIdentifier.Replace("$id$", classifiedObject);
                nodeListExternalIdentifier = eltRoot.SelectNodes(xpath);

                if (nodeListExternalIdentifier != null)
                {
                    foreach (XmlNode nodeExternalIdentifier in nodeListExternalIdentifier)
                    {
                        identificationScheme = nodeExternalIdentifier.Attributes["identificationScheme"].Value;

                        switch (identificationScheme)
                        {
                            case GlobalValues.XDSSubmissionSet_patientIdUUID:
                                objSubmissionSet.SubmissionPatient = new Patient();
                                objSubmissionSet.SubmissionPatient.PatientUID = nodeExternalIdentifier.Attributes["value"].Value;
                                break;

                            case GlobalValues.XDSSubmissionSet_sourceIdUUID:
                                objSubmissionSet.SourceID = nodeExternalIdentifier.Attributes["value"].Value;
                                break;

                            case GlobalValues.XDSSubmissionSet_uniqueIdUUID:
                                objSubmissionSet.UniqueID = nodeExternalIdentifier.Attributes["value"].Value;
                                break;
                        }
                    }
                }

                #endregion ExternalIdentifier

                //Classifications
                UpdateSubmissionSetClassification(xmlDocRequest, ref objSubmissionSet);
            }


            return objSubmissionSet;
        }

        private void UpdateSubmissionSetClassification(XmlDocument xmlDocRequest, ref SubmissionSet objSubmissionSet)
        {
            string xpathClassification = ".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='Classification']";
            string xpathSlots = ".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='Classification'][@id='$classificationid$']/*[local-name()='Slot']";
            string xpathSlotValue = ".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='Classification'][@id='$classificationid$']/*[local-name()='Slot']/*[local-name()='ValueList']/*[local-name()='Value']";
            string xpathLocalizedString = ".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='Classification'][@id='$classificationid$']/*[local-name()='Name']/*[local-name()='LocalizedString']";
            string xpath = null;
            string classificationId = null;
            string classificationScheme = null;

            XmlElement eltRoot = null;
            XmlNodeList nodeListClassification = null;
            XmlNode node = null;

            //SubmitObjectsRequest
            eltRoot = xmlDocRequest.DocumentElement;

            //Classification
            xpath = xpathClassification.Replace("$id$", objSubmissionSet.EntryUUID);
            nodeListClassification = eltRoot.SelectNodes(xpath);

            if (nodeListClassification == null)
                return;

            for (int nodeCount = 0; nodeCount < nodeListClassification.Count; nodeCount++)
            {
                classificationId = nodeListClassification[nodeCount].Attributes["id"].Value;
                classificationScheme = nodeListClassification[nodeCount].Attributes["classificationScheme"].Value;

                switch (classificationScheme)
                {
                    case GlobalValues.XDSSubmissionSet_contentTypeCodeUUID:
                        objSubmissionSet.ContentType = new CodeValue();

                        xpath = xpathSlotValue.Replace("$id$", objSubmissionSet.EntryUUID);
                        xpath = xpath.Replace("$classificationid$", classificationId);
                        node = eltRoot.SelectSingleNode(xpath);

                        if (node != null)
                            objSubmissionSet.ContentType.CodingScheme = node.InnerText;

                        xpath = xpathLocalizedString.Replace("$id$", objSubmissionSet.EntryUUID);
                        xpath = xpath.Replace("$classificationid$", classificationId);
                        node = eltRoot.SelectSingleNode(xpath);
                        
                        if (node != null)
                            objSubmissionSet.ContentType.Value = node.Attributes["value"].Value;

                        break;

                    case GlobalValues.XDSSubmissionSet_authorDescriptionUUID:
                                                
                        Author objAuthor = null;

                        if (objSubmissionSet.SubmissionAuthor == null)
                            objSubmissionSet.SubmissionAuthor = new List<Author>();
                        
                        xpath = xpathSlots.Replace("$id$", objSubmissionSet.EntryUUID);
                        xpath = xpath.Replace("$classificationid$", classificationId);

                        objAuthor = GetAuthor(xmlDocRequest, xpath);

                        if(objAuthor != null)
                            objSubmissionSet.SubmissionAuthor.Add(objAuthor);

                        break;

                }
            }

        }

        private List<Folder> GetFolders(XmlDocument xmlDocRequest)
        {
            string xpathClassificationFolder = @".//*[local-name()='RegistryObjectList']/*[local-name()='Classification'][@classificationNode='$classificationNode$']";
            string xpathRegistryPackage = @".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']";
            string xpathNameLocalizedString = @".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='Name']/*[local-name()='LocalizedString']";
            string xpathDescriptionLocalizedString = @".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='Description']/*[local-name()='LocalizedString']";
            string xpathExternalIdentifier = @".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='ExternalIdentifier']";
            
            string xpath = null;
            string classifiedObject = null;
            string identificationScheme = null;

            List<Folder> lstFolder = new List<Folder>();
            Folder objFolder = null;

            XmlElement eltRoot = null;
            XmlNodeList nodeListClassification = null;
            XmlNodeList nodeListExternalIdentifier = null;
            XmlNode nodeRegistryPackage = null;
            XmlNode node = null;

            //SubmitObjectsRequest
            eltRoot = xmlDocRequest.DocumentElement;

            //Classification
            xpath = xpathClassificationFolder.Replace("$classificationNode$", GlobalValues.XDSFolderUUID);
            nodeListClassification = eltRoot.SelectNodes(xpath);

            if (nodeListClassification == null)
                return null;

            for (int classificationCount = 0; classificationCount < nodeListClassification.Count; classificationCount++)
            {
                objFolder = new Folder();

                classifiedObject = nodeListClassification[classificationCount].Attributes["classifiedObject"].Value;

                if (string.IsNullOrEmpty(classifiedObject))
                    continue;

                //RegistryPackage
                xpath = xpathRegistryPackage.Replace("$id$", classifiedObject);
                nodeRegistryPackage = eltRoot.SelectSingleNode(xpath);

                if (nodeRegistryPackage == null)
                    continue;

                //Folder Xml
                objFolder.FolderXml = nodeRegistryPackage.OuterXml + nodeListClassification[classificationCount].OuterXml;

                //AvailabilityStatus
                objFolder.AvailabilityStatus = GlobalValues.CONST_AVAILABILITYSTATUS_APPROVED;

                //EntryUUID / classifiedObject
                objFolder.EntryUUID = classifiedObject;

                //Name->LocalizedString
                xpath = xpathNameLocalizedString.Replace("$id$", classifiedObject);
                node = eltRoot.SelectSingleNode(xpath);

                if (node != null)
                    objFolder.Title = node.Attributes["value"].Value;

                //Description->LocalizedString
                xpath = xpathDescriptionLocalizedString.Replace("$id$", classifiedObject);
                node = eltRoot.SelectSingleNode(xpath);

                if (node != null)
                    objFolder.Comments = node.Attributes["value"].Value;

                #region ExternalIdentifier
                //ExternalIdentifier
                xpath = xpathExternalIdentifier.Replace("$id$", classifiedObject);
                nodeListExternalIdentifier = eltRoot.SelectNodes(xpath);

                if (nodeListExternalIdentifier != null)
                {
                    foreach (XmlNode nodeExternalIdentifier in nodeListExternalIdentifier)
                    {
                        identificationScheme = nodeExternalIdentifier.Attributes["identificationScheme"].Value;

                        switch (identificationScheme)
                        {
                            case GlobalValues.XDSFolder_patientIdUUID:
                                objFolder.FolderPatient = new Patient();
                                objFolder.FolderPatient.PatientUID = nodeExternalIdentifier.Attributes["value"].Value;
                                break;

                            case GlobalValues.XDSFolder_uniqueIdUUID:
                                objFolder.UniqueID = nodeExternalIdentifier.Attributes["value"].Value;
                                break;
                        }
                    }
                }

                #endregion ExternalIdentifier

                
                //Classification
                UpdateFolderClassification(xmlDocRequest, ref objFolder);

                lstFolder.Add(objFolder);
            }

            return lstFolder;
        }

        private void UpdateFolderClassification(XmlDocument xmlDocRequest, ref Folder objFolder)
        {
            string xpathClassification = ".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='Classification']";
            //string xpathSlots = ".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='Classification'][@id='$classificationid$']/*[local-name()='Slot']";
            string xpathSlotValue = ".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='Classification'][@id='$classificationid$']/*[local-name()='Slot']/*[local-name()='ValueList']/*[local-name()='Value']";
            string xpathLocalizedString = ".//*[local-name()='RegistryObjectList']/*[local-name()='RegistryPackage'][@id='$id$']/*[local-name()='Classification'][@id='$classificationid$']/*[local-name()='Name']/*[local-name()='LocalizedString']";
            string xpath = null;
            string classificationId = null;
            string classificationScheme = null;
            string localizedStringValue = null;

            XmlElement eltRoot = null;
            XmlNodeList nodeListClassification = null;
            XmlNodeList nodeListSlotValue = null;
            XmlNode node = null;

            //SubmitObjectsRequest
            eltRoot = xmlDocRequest.DocumentElement;

            //Classification
            xpath = xpathClassification.Replace("$id$", objFolder.EntryUUID);
            nodeListClassification = eltRoot.SelectNodes(xpath);

            if (nodeListClassification == null)
                return;

            for (int nodeCount = 0; nodeCount < nodeListClassification.Count; nodeCount++)
            {
                classificationId = nodeListClassification[nodeCount].Attributes["id"].Value;
                classificationScheme = nodeListClassification[nodeCount].Attributes["classificationScheme"].Value;

                if(classificationScheme.Equals(GlobalValues.XDSFolder_codeListUUID))
                {
                    objFolder.CodeList = new List<CodeValue>();

                    xpath = xpathLocalizedString.Replace("$id$", objFolder.EntryUUID);
                    xpath = xpath.Replace("$classificationid$", classificationId);
                    node = eltRoot.SelectSingleNode(xpath);

                    if (node != null)
                        localizedStringValue = node.Attributes["value"].Value;

                    xpath = xpathSlotValue.Replace("$id$", objFolder.EntryUUID);
                    xpath = xpath.Replace("$classificationid$", classificationId);
                    nodeListSlotValue = eltRoot.SelectNodes(xpath);

                    if (nodeListSlotValue != null)
                    {
                        CodeValue objCodeValue = null;

                        foreach (XmlNode nodeValue in nodeListSlotValue)
                        {
                            objCodeValue = new CodeValue();
                            objCodeValue.Value = localizedStringValue;
                            objCodeValue.CodingScheme = nodeValue.Attributes["value"].Value;
                            objFolder.CodeList.Add(objCodeValue);
                        }

                    }
                }
            }
        }


        private List<Association> GetAssociations(XmlDocument xmlDocRequest)
        {            
            List<Association> lstAssociation = new List<Association>();
            Association objAssociation = null;
            XmlElement eltRoot = null;
            XmlNodeList nodeListAssociation = null;

            string xpathAssociation = ".//*[local-name()='RegistryObjectList']/*[local-name()='Association']";

            //SubmitObjectsRequest
            eltRoot = xmlDocRequest.DocumentElement;

            //Association
            nodeListAssociation = eltRoot.SelectNodes(xpathAssociation);

            if (nodeListAssociation == null)
                return null;

            foreach (XmlNode association in nodeListAssociation)
            {
                objAssociation = new Association();
                objAssociation.AssociationType = association.Attributes["associationType"].Value;
                objAssociation.SourceObject = association.Attributes["sourceObject"].Value;
                objAssociation.TargetObject = association.Attributes["targetObject"].Value;
                objAssociation.AssociationXml = association.OuterXml;

                lstAssociation.Add(objAssociation);
            }

            return lstAssociation;
        }

        private List<Association> GetReplaceAssociation(List<Association> lstAssociation)
        {
            List<Association> lstFilteredAssociation = null;

            lstFilteredAssociation = lstAssociation.FindAll(
                delegate(Association association)
                {
                    if ((association.AssociationType == GlobalValues.CONST_ASSOCIATION_TYPE_RPLC) || (association.AssociationType == GlobalValues.CONST_ASSOCIATION_TYPE_XFRM_RPLC))
                    {
                        return true;
                    }

                    return false;
                }
                );

            return lstFilteredAssociation;
        }

        private void AssociateObjects(XmlDocument xmlDocRequest, SubmissionSet objSubmissionSet, List<Folder> lstFolder, List<DocumentEntry> lstDocumentEntry, List<Association> lstAssociation)
        {
            string sourceObject = null;
            string targetObject = null;
            Folder sourceFolder = null;
            DocumentEntry sourceDocumentEntry = null;
                        

            //Associate SubmissionSet/Folder/Document
            for (int associationCount = 0; associationCount < lstAssociation.Count; associationCount++)
            {
                sourceObject = lstAssociation[associationCount].SourceObject;
                targetObject = lstAssociation[associationCount].TargetObject;

                //folder
                sourceFolder = GetFolderByEntryUUID(lstFolder, sourceObject);

                //DocumentEntry
                sourceDocumentEntry = GetDocumentEntryByEntryUUID(lstDocumentEntry, sourceObject);

#region "Source SubmissionSet"

                if (objSubmissionSet.EntryUUID == sourceObject)
                {
                    //objSubmissionSet.SourceObjectID = sourceObject;
                    //objSubmissionSet.TargetObjectID = targetObject;

                    Folder objFolder = GetFolderByEntryUUID(lstFolder, targetObject);

                    if (objFolder != null)
                    {
                        if (objSubmissionSet.FolderList == null)
                            objSubmissionSet.FolderList = new List<Folder>();

                        objFolder.AssociationType = lstAssociation[associationCount].AssociationType;
                        objFolder.AssociationXml = lstAssociation[associationCount].AssociationXml;
                        objSubmissionSet.FolderList.Add(objFolder);                        
                    }
                    else
                    {
                        DocumentEntry documentEntry = GetDocumentEntryByEntryUUID(lstDocumentEntry, targetObject);

                        if (documentEntry != null)
                        {
                            if (objSubmissionSet.DocumentList == null)
                                objSubmissionSet.DocumentList = new List<DocumentEntry>();

                            //Update ExtrinsicObject/DocumentEntry Status
                            //UpdateExtrinsicObjectStatus(xmlDocRequest, documentEntry.EntryUUID, 

                            documentEntry.AssociationType = lstAssociation[associationCount].AssociationType;
                            documentEntry.AssociationXml = lstAssociation[associationCount].AssociationXml;
                            //objSubmissionSet.DocumentEntryEntryUUID = documentEntry.EntryUUID;
                            //objSubmissionSet.DocumentEntryUniqueID = documentEntry.UniqueID;
                            objSubmissionSet.DocumentList.Add(documentEntry);
                        }
                    }
                }

#endregion "Source SubmissionSet"

                else if (sourceFolder != null)
                {
                    DocumentEntry targetDocumentEntry = null;

                    if (sourceFolder.DocumentList == null)
                        sourceFolder.DocumentList = new List<DocumentEntry>();

                    targetDocumentEntry = GetDocumentEntryByEntryUUID(lstDocumentEntry, targetObject);

                    if (targetDocumentEntry != null)
                    {
                        targetDocumentEntry.AssociationType = lstAssociation[associationCount].AssociationType;
                        targetDocumentEntry.AssociationXml = lstAssociation[associationCount].AssociationXml;
                        sourceFolder.DocumentList.Add(targetDocumentEntry);
                        //sourceFolder.DocumentEntryEntryUUID = targetDocumentEntry.EntryUUID;
                        //sourceFolder.DocumentEntryUniqueID = targetDocumentEntry.UniqueID;                        
                    }

                }
               
            }

        }

        private List<DocumentEntry> UpdateExtrinsicObjectDeprecateStatus(List<DocumentEntry> lstReplacedDocumentEntry) 
        {
            XmlDocument xmlDocExtrinsicObject = null;
            XmlElement eltRoot = null;

            for (int count = 0; count < lstReplacedDocumentEntry.Count; count++)
            {
                xmlDocExtrinsicObject = new XmlDocument();
                xmlDocExtrinsicObject.LoadXml(lstReplacedDocumentEntry[count].ExtrinsicObjectXML);

                eltRoot = xmlDocExtrinsicObject.DocumentElement;

                if (eltRoot.Attributes["status"] == null)
                {
                    XmlAttribute attribStatus = xmlDocExtrinsicObject.CreateAttribute("status");
                    attribStatus.Value = GlobalValues.CONST_AVAILABILITYSTATUS_DEPRECATED;
                    eltRoot.Attributes.Append(attribStatus);
                }
                else
                {
                    eltRoot.Attributes["status"].Value = GlobalValues.CONST_AVAILABILITYSTATUS_DEPRECATED;
                }

                lstReplacedDocumentEntry[count].AvailabilityStatus = GlobalValues.CONST_AVAILABILITYSTATUS_DEPRECATED;
                lstReplacedDocumentEntry[count].ExtrinsicObjectXML = xmlDocExtrinsicObject.OuterXml;
            }

            return lstReplacedDocumentEntry;
        }

        private Author GetAuthor(XmlDocument xmlDocRequest, string xpathExpression)
        {
            Author objAuthor = null;
            XmlElement eltRoot = null;
            XmlNodeList nodeListSlot = null;

            //SubmitObjectsRequest
            eltRoot = xmlDocRequest.DocumentElement;

            nodeListSlot = eltRoot.SelectNodes(xpathExpression);

            if (nodeListSlot == null)
                return null;

            objAuthor = new Author();

            foreach (XmlNode slot in nodeListSlot)
            {
                if (slot.Attributes["name"].Value.Equals("authorPerson"))
                    objAuthor.Person = slot.InnerXml;
                else if (slot.Attributes["name"].Value.Equals("authorInstitution"))
                    objAuthor.Institution = slot.InnerXml;
                else if (slot.Attributes["name"].Value.Equals("authorRole"))
                    objAuthor.Role = slot.InnerXml;
                else if (slot.Attributes["name"].Value.Equals("authorSpecialty"))
                    objAuthor.Specialty = slot.InnerXml;
            }


            return objAuthor;
        }

        private Folder GetFolderByEntryUUID(List<Folder> lstFolder, string entryUUID)
        {
            Folder objFolder = lstFolder.Find(
            delegate(Folder folder)
            {
                if (folder.EntryUUID == entryUUID)
                    return true;

                return false;
            }

            );

            return objFolder;
        }

        private DocumentEntry GetDocumentEntryByEntryUUID(List<DocumentEntry> lstDocumentEntry, string entryUUID)
        {
            DocumentEntry objDocumentEntry = lstDocumentEntry.Find(
            delegate(DocumentEntry docEntry)
            {
                if (docEntry.EntryUUID == entryUUID)
                    return true;

                return false;
            }

            );

            return objDocumentEntry;
        }


        private List<DocumentEntry> GetDocumentEntryByEntryUUIDs(List<Association> lstReplaceAssociation)
        {
            List<DocumentEntry> lstDocumentEntry = null;
            StringBuilder sbEntryUUID = new StringBuilder();
            string entryUUIDs = null;
            RegistryStoredQueryDataAccess storedQueryDataAccess = null; 

            for (int count = 0; count < lstReplaceAssociation.Count; count++)
            {
                if (count > 0)
                    sbEntryUUID.Append(",");
                  
                sbEntryUUID.Append(lstReplaceAssociation[count].TargetObject);
            }

            entryUUIDs = sbEntryUUID.ToString();

            storedQueryDataAccess = new RegistryStoredQueryDataAccess();
            lstDocumentEntry = storedQueryDataAccess.GetDocuments(entryUUIDs, null);

            return lstDocumentEntry;
        }

        public void ProcessRegisterDocumentSetATNAEvent(string submissionSetUniqueID, string sourceUserID, string destinationUserID, string eventOutcomeIndicator)
        {
            try
            {
                //Log ATNA - Repository Event
                ATNALogic atnaLogic = new ATNALogic();
                AuditMessageConfiguration auditMsgConfig = null;

                auditMsgConfig = atnaLogic.GetAuditMessageConfigurationDetails("REGISTRY-RDS-IMPORT-ITI-42");

                if (auditMsgConfig != null)
                {

                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$SubmissionSet.UniqueID$", submissionSetUniqueID);
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$SubmissionSet.ClassificationNode.UUID$", ATNAEvent.XDSREPOSITORY_SUBMISSIONSET_CLASSIFICATIONNODE_UUID);

                    //$ActiveParticipant.UserID.Source$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Source$", sourceUserID);

                    //$ActiveParticipant.UserID.Destination$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Destination$", destinationUserID);

                    atnaLogic.ProcessEvent(auditMsgConfig, ATNAEvent.XDSREGISTRY_TYPE, eventOutcomeIndicator, ATNAEvent.UDP_TAG_APPNAME_REGISTRY);
                }
            }
            catch
            {
                //Oops...ATNA event failed...probably UDP failure....can't afford to stop XDSRepository service sorry :(
            }
        }



    }
}
