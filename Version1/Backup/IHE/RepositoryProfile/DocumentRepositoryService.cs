using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;

using System.ServiceModel;
using System.ServiceModel.Channels;

using Microsoft.IHE.XDS.Common;
using System.Collections.Specialized;
using Microsoft.IHE.XDS.BusinessLogic;
using Microsoft.IHE.XDS.BusinessLogic.ATNA;
using Microsoft.IHE.XDS.DocumentRepository.StorageProvider;


namespace Microsoft.IHE.XDS.DocumentRepository
{
    [ServiceBehavior()]
    public class DocumentRepositoryService : IDocumentRepository
    {
        DocumentRepositoryLog objDocumentRepositoryLog = null;
        private static readonly string SOURCE_USERID = " ";

        Message IDocumentRepository.ProvideAndRegisterDocumentSet(Message msgRequest)
        {
            Message msgResponse = null;
            XmlDocument xmlDocRequest = null;
            XmlDocument xmlDocResponse = null;
            XmlNode nodeRegistryError = null;
            string errorCode = null;            
            RepositoryLogic repositoryLogic = null;
            StringDictionary stringDictionary = null;
            string eventOutcomeIndicator = "0";
            string submissionSetUniqueID = string.Empty;
            string sourceUserID = string.Empty;
            string destinationUserID = string.Empty;

            try
            {
                repositoryLogic = new RepositoryLogic();

                objDocumentRepositoryLog = new DocumentRepositoryLog();
                objDocumentRepositoryLog.StartTime = DateTime.Now;
                objDocumentRepositoryLog.RequesterIdentity = Environment.UserName;

                //ATNA Event Source & Destination UserID
                sourceUserID = GetSourceUserID();
                destinationUserID = GetDestinationUserID();

                //Request XmlDocument
                xmlDocRequest = new XmlDocument();
                xmlDocRequest.Load(msgRequest.GetReaderAtBodyContents());                              


                //Process Message will Construct Response for Register Transaction Set-B
                //Recieves a message from Provide and register document set B(With no errors from Repositrory)
                xmlDocResponse = ProcessProvideAndRegisterDocumentSet(xmlDocRequest, msgRequest.Headers.MessageVersion, out stringDictionary);

                //ATNA Event
                eventOutcomeIndicator = stringDictionary["$EventIdentification.EventOutcomeIndicator$"];
            }
            catch (ServerTooBusyException serverTooBusyException)
            {                
                //Construct Error Response
                xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, GlobalValues.CONST_ERROR_CODE_XDSRepositoryTooBusyException, GlobalValues.CONST_ERROR_CODE_XDSRepositoryTooBusyException, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);

                nodeRegistryError = xmlDocResponse.SelectSingleNode(@"//*[local-name()='RegistryError']");
                errorCode = nodeRegistryError.Attributes["errorCode"].Value;
                if (errorCode != null)
                {
                    repositoryLogic.LogRolledBackData(objDocumentRepositoryLog, errorCode);
                }

                //ATNA EVENT LOG
                eventOutcomeIndicator = "8";
            }
            catch (TimeoutException timeoutException)
            {
                //Construct Error Response
                xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, GlobalValues.CONST_ERROR_CODE_TimeOut, GlobalValues.CONST_ERROR_CODE_TimeOut, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);

                nodeRegistryError = xmlDocResponse.SelectSingleNode(@"//*[local-name()='RegistryError']");
                errorCode = nodeRegistryError.Attributes["errorCode"].Value;
                if (errorCode != null)
                {
                    repositoryLogic.LogRolledBackData(objDocumentRepositoryLog, errorCode);
                }

                //ATNA EVENT LOG
                eventOutcomeIndicator = "8";
            }
            catch (System.ServiceModel.Security.SecurityAccessDeniedException AuthorizationException)
            {
                //Construct Error Response
                xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, GlobalValues.CONST_ERROR_CODE_XDSAuthorizationException, GlobalValues.CONST_ERROR_CODE_XDSAuthorizationException, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);

                nodeRegistryError = xmlDocResponse.SelectSingleNode(@"//*[local-name()='RegistryError']");
                errorCode = nodeRegistryError.Attributes["errorCode"].Value;
                if (errorCode != null)
                {
                    repositoryLogic.LogRolledBackData(objDocumentRepositoryLog, errorCode);
                }

                //ATNA EVENT LOG
                eventOutcomeIndicator = "8";
            }
            catch (OutOfMemoryException RepositoryOutOfResources)
            {
                //Construct Error Response
                xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, GlobalValues.CONST_ERROR_CODE_XDSRepositoryOutOfResources, GlobalValues.CONST_ERROR_CODE_XDSRepositoryOutOfResources, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);

                nodeRegistryError = xmlDocResponse.SelectSingleNode(@"//*[local-name()='RegistryError']");
                errorCode = nodeRegistryError.Attributes["errorCode"].Value;
                if (errorCode != null)
                {
                    repositoryLogic.LogRolledBackData(objDocumentRepositoryLog, errorCode);
                }

                //ATNA EVENT LOG
                eventOutcomeIndicator = "8";
            }
            catch (Exception ex)
            {

                if (ex.Message == GlobalValues.CONST_ERROR_CODE_XDSMissingDocumentMetadata)
                {
                    //Construct Error Response
                    xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "XDSDocumentEntry document exists in metadata with no corresponding metatdata", GlobalValues.CONST_ERROR_CODE_XDSMissingDocumentMetadata, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);

                }
                else if (ex.Message == GlobalValues.CONST_ERROR_CODE_XDSMissingDocumentAttachment)
                {
                    //Construct Error Response
                    xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "XDSDocumentEntry exists in metadata with no corresponding attached document", GlobalValues.CONST_ERROR_CODE_XDSMissingDocumentAttachment, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                }
                else if (ex.Message == GlobalValues.CONST_ERROR_CODE_XDSInvalidRequest)
                {
                    //Construct Error Response
                    xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "XDSInvalidRequest - DcoumentId is not unique.", GlobalValues.CONST_ERROR_CODE_XDSInvalidRequest, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                }
                else if (ex.Message == GlobalValues.CONST_ERROR_CODE_XDSRepositoryMetadataError)
                {
                    //Construct Error Response
                    xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Error occurred in Parsing the Metadata.", GlobalValues.CONST_ERROR_CODE_XDSRepositoryMetadataError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                }
                else if (ex.Message == GlobalValues.CONST_ERROR_CODE_XDSRegistryNotAvailable)
                {
                    //Construct Error Response
                    xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Repository was unable to access the Registry.", GlobalValues.CONST_ERROR_CODE_XDSRegistryNotAvailable, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                }
                else
                {
                    //Construct Error Response
                    xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, GlobalValues.CONST_ERROR_CODE_XDSRepositoryError, GlobalValues.CONST_ERROR_CODE_XDSRepositoryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                }

                nodeRegistryError = xmlDocResponse.SelectSingleNode(@"//*[local-name()='RegistryError']");

                errorCode = nodeRegistryError.Attributes["errorCode"].Value;
                if (errorCode != null)
                {
                    repositoryLogic.LogRolledBackData(objDocumentRepositoryLog, errorCode);
                }

                //ATNA EVENT LOG
                eventOutcomeIndicator = "8";
            }


            //ATNA EVENT LOG
            submissionSetUniqueID = stringDictionary["$SubmissionSet.UniqueID$"];            

            //REPOSITORY-RDS-EXPORT-ITI-42
            repositoryLogic.ProcessRegisterDocumentSetATNAEvent(submissionSetUniqueID, sourceUserID, destinationUserID, eventOutcomeIndicator);
            //REPOSITORY-P-AND-R-IMPORT-ITI-41
            repositoryLogic.ProcessProvideAndRegisterATNAEvent(submissionSetUniqueID, sourceUserID, destinationUserID, eventOutcomeIndicator);

            msgResponse = Message.CreateMessage(msgRequest.Headers.MessageVersion, GlobalValues.CONST_ACTION_ProvideAndRegisterDocumentSet_bResponse, new XmlNodeReader(xmlDocResponse));

            return msgResponse;

        }


        Message IDocumentRepository.RetrieveDocumentSet(Message msgRequest)
        {
            Message msgResponse = null;
            XmlDocument xmlDocRequest = null;
            XmlDocument xmlDocResponse = null;
            RepositoryLogic repositoryLogic = null;

            string eventOutcomeIndicator = "0";
            string documentEntryUUID = string.Empty;
            StringDictionary atnaParameterValues = null;
            string sourceUserID = string.Empty;
            string destinationUserID = string.Empty;


            try
            {
                repositoryLogic = new RepositoryLogic();

                //ATNA Event Source & Destination UserID
                sourceUserID = GetSourceUserID();
                destinationUserID = GetDestinationUserID();

                //Request XmlDocument
                xmlDocRequest = new XmlDocument();
                xmlDocRequest.Load(msgRequest.GetReaderAtBodyContents());

                //Process Retrieve Document Set Response
                xmlDocResponse = ProcessRetrieveDocumentSetResponse(xmlDocRequest, out atnaParameterValues);

                //ATNA
                documentEntryUUID = atnaParameterValues["$DocumentEntry.UUID$"];
                eventOutcomeIndicator = atnaParameterValues["$EventIdentification.EventOutcomeIndicator$"];


            }
            catch (System.ServiceModel.ServerTooBusyException serverTooBusyException)
            {
                //ATNA Event Failure
                eventOutcomeIndicator = "8";

                xmlDocResponse = GenerateRetrieveDocumentSetErrorMessage(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, GlobalValues.CONST_ERROR_CODE_XDSRepositoryTooBusyException, serverTooBusyException.Message, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
            }
            catch (TimeoutException timeoutException)
            {
                //ATNA Event Failure
                eventOutcomeIndicator = "8";

                xmlDocResponse = GenerateRetrieveDocumentSetErrorMessage(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, GlobalValues.CONST_ERROR_CODE_TimeOut, timeoutException.Message, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
            }
            catch (System.ServiceModel.Security.SecurityAccessDeniedException AuthorizationException)
            {
                //ATNA Event Failure
                eventOutcomeIndicator = "8";

                xmlDocResponse = GenerateRetrieveDocumentSetErrorMessage(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, GlobalValues.CONST_ERROR_CODE_XDSAuthorizationException, AuthorizationException.Message, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
            }
            catch (Exception ex)
            {
                //ATNA Event Failure
                eventOutcomeIndicator = "8";

                xmlDocResponse = GenerateRetrieveDocumentSetErrorMessage(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, ex.Message, GlobalValues.CONST_ERROR_CODE_XDSRepositoryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
            }

            //REPOSITORY-RDS-EXPORT-ITI-43
            repositoryLogic.ProcessRetrieveDocumentSetATNAEvent(documentEntryUUID, sourceUserID, destinationUserID, eventOutcomeIndicator);

            msgResponse = Message.CreateMessage(msgRequest.Headers.MessageVersion, GlobalValues.CONST_ACTION_RETRIEVEDOCSETRESPONSE, new XmlNodeReader(xmlDocResponse)); 

            return msgResponse;
        }
        

        private XmlDocument ProcessProvideAndRegisterDocumentSet(XmlDocument xmlDocRequest, MessageVersion msgVersion, out StringDictionary atnaParameterValues)
        {
            XmlDocument xmlDocRegistryRequest = null;
            XmlDocument xmlDocResponse = null;
            Message registryMessage = null;
            XmlDocument xmlDocRegistryResponse = null;
            RepositoryLogic repositoryLogic = null;
            XmlElement eltProvideAndRegDocSet = null;
            XmlNode nodeSubmitObjectsRequest = null;
            XmlNodeList nodeListExtrinsicObject = null;
            XmlNodeList nodeListDocument = null;
            XmlNode nodeSubmissionSet = null;
            XmlNode nodeExternalIdentifier = null;
            List<DocumentEntry> lstDocumentEntry = null;
            StringBuilder sbMetaData = null;
            string xpathExternalIdentifierDocument = @".//*[local-name()='ExtrinsicObject'][@id='$id$']/*[local-name()='ExternalIdentifier'][@identificationScheme='$identificationScheme$']";            
            string xpath = null;
            string entryUUID = null;
            string uniqueID = null;
            string eventOutcomeIndicator = "0";

            atnaParameterValues = new StringDictionary();

            try
            {
                repositoryLogic = new RepositoryLogic();
                xpathExternalIdentifierDocument = xpathExternalIdentifierDocument.Replace("$identificationScheme$", GlobalValues.XDSDocumentEntry_uniqueIdUUID);

                eltProvideAndRegDocSet = xmlDocRequest.DocumentElement;
                
                nodeListExtrinsicObject = eltProvideAndRegDocSet.SelectNodes(".//*[local-name()='ExtrinsicObject']");

                nodeListDocument = eltProvideAndRegDocSet.SelectNodes(".//*[local-name()='Document']");

                nodeSubmissionSet = eltProvideAndRegDocSet.SelectSingleNode("//*[local-name()='SubmitObjectsRequest']/*[local-name()=\"RegistryObjectList\"]/*[local-name()='RegistryPackage']/*[local-name()='ExternalIdentifier'][@identificationScheme='urn:uuid:96fdda7c-d067-4183-912e-bf5ee74998a8']/@value");

                if (nodeSubmissionSet != null)
                {
                    atnaParameterValues.Add("$SubmissionSet.UniqueID$", nodeSubmissionSet.Value);
                }

                //Proceed further only if any ExtrinsicObject Exists(with attachments)
                if (nodeListExtrinsicObject == null)
                {
                    throw new Exception();
                }

                //if (repositoryLogic.IsMissingDocumentAttachment(nodeListDocument, eltProvideAndRegDocSet))
                if (repositoryLogic.IsMissingDocumentAttachment(eltProvideAndRegDocSet))    
                {
                    throw new Exception(GlobalValues.CONST_ERROR_CODE_XDSMissingDocumentAttachment);
                }

                //if (repositoryLogic.IsMissingDocumentMetadata(nodeListExtrinsicObject, eltProvideAndRegDocSet))
                if (repositoryLogic.IsMissingDocumentMetadata(eltProvideAndRegDocSet))
                {
                    throw new Exception(GlobalValues.CONST_ERROR_CODE_XDSMissingDocumentMetadata);
                }

#if debug
                if (!repositoryLogic.IsSchemaValid(xmlDocMsgBody))
                {
                    throw new Exception(GlobalValues.CONST_ERROR_CODE_XDSInvalidRequest);
                }
#endif

                if (repositoryLogic.IsDuplicateUniqueID(eltProvideAndRegDocSet))
                {
                    throw new Exception(GlobalValues.CONST_ERROR_CODE_XDSRepositoryDuplicateUniqueIdInMessage);
                }

                if (nodeListExtrinsicObject.Count > 0 && eltProvideAndRegDocSet.SelectNodes(@"//*[local-name()='Document']").Count > 0)
                {

                    //No document OR Metadata is Missing

                    lstDocumentEntry = new List<DocumentEntry>();                                        
                    sbMetaData = new StringBuilder();

                    foreach (XmlNode node in nodeListExtrinsicObject)
                    {
                        DocumentEntry objDocumentEntry = new DocumentEntry();                        

                        if (node.Attributes["mimeType"].Value == string.Empty)
                            throw new Exception(GlobalValues.CONST_ERROR_CODE_XDSRepositoryMetadataError);

                        objDocumentEntry.MimeType = node.Attributes["mimeType"].Value;

                        entryUUID = node.Attributes["id"].Value;
                        xpath = xpathExternalIdentifierDocument.Replace("$id$", node.Attributes["id"].Value);
                        nodeExternalIdentifier = eltProvideAndRegDocSet.SelectSingleNode(xpath);
                        uniqueID = nodeExternalIdentifier.Attributes["value"].Value;

                        if (string.IsNullOrEmpty(uniqueID))
                            throw new Exception(GlobalValues.CONST_ERROR_CODE_XDSRepositoryMetadataError);

                        if (!repositoryLogic.IsValidUniqueID(uniqueID))
                        {
                            throw new Exception(GlobalValues.CONST_ERROR_CODE_XDSInvalidRequest);
                        }

                        objDocumentEntry.EntryUUID = entryUUID;
                        objDocumentEntry.UniqueID = uniqueID;
                        objDocumentEntry.Hash = repositoryLogic.GetHashCode(xmlDocRequest, entryUUID);
                        objDocumentEntry.Content = repositoryLogic.GetDocumentContentStream(xmlDocRequest, entryUUID);
                        objDocumentEntry.Size = (int)objDocumentEntry.Content.Length;

                        lstDocumentEntry.Add(objDocumentEntry);

                        sbMetaData.Append(node.OuterXml);
                    }

                    //Logging the Repository Data
                    objDocumentRepositoryLog.RequestMetadata = sbMetaData.ToString();

                    using (TransactionScope provideAndRegSetBScope = new TransactionScope())
                    {
                        //Information Logged in the Document Repository Log
                        objDocumentRepositoryLog.EndTime = DateTime.Now;
                        objDocumentRepositoryLog.Result = "OK";
                        int noOfRows = repositoryLogic.LogRepositoryData(objDocumentRepositoryLog);
                        for (int docElement = 0; docElement < lstDocumentEntry.Count; docElement++)
                        {
                            string sStorageUniqueIdentifier = string.Empty;

                            DocumentEntry objDocEntry = lstDocumentEntry[docElement];

                            sStorageUniqueIdentifier = SQLServerStorageService.SaveDocument(objDocEntry.Content, objDocEntry.UniqueID);

                            //Save Metadata
                            repositoryLogic.SaveMetaData(objDocEntry);
                            
                            //Insert into Document Entry Log
                            repositoryLogic.LogDocumentEntry();

                        }
                        //Create SLots for Repository ID,,Hash and Size and send those to 
                        //Register document Set B
                        //this Array list already contains all ID's
                        if (lstDocumentEntry.Count > 0)
                        {
                            string repositoryUniqueId = repositoryLogic.GetRepositoryUniqueID("repositoryUniqueID");
                            string repositoryURI = string.Empty;

                            for (int docID = 0; docID < lstDocumentEntry.Count; docID++)
                            {
                                DocumentEntry objDocumentEntry = lstDocumentEntry[docID];

                                //Creates Slots to add Medatada repositoryUniqueId
                                xmlDocRequest = repositoryLogic.CreateRepositorySlotElement(xmlDocRequest, "repositoryUniqueId", repositoryUniqueId, objDocumentEntry.EntryUUID);

                                //Creates Slots to add Medatada hash
                                xmlDocRequest = repositoryLogic.CreateRepositorySlotElement(xmlDocRequest, "hash", objDocumentEntry.Hash, objDocumentEntry.EntryUUID);

                                //Creates Slots to add Medatada size
                                xmlDocRequest = repositoryLogic.CreateRepositorySlotElement(xmlDocRequest, "size", objDocumentEntry.Content.Length.ToString(), objDocumentEntry.EntryUUID);

                                //Creates Slots to add Medatada URI
                                xmlDocRequest = repositoryLogic.CreateRepositorySlotElement(xmlDocRequest, "URI", repositoryURI, objDocumentEntry.EntryUUID);

                            }

                        }
                        //Message to be sent to Registry
                        try
                        {
                            //Sample RegistryResponse  - <RegistryResponse xmlns="urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0" />;                        
                            //Call the XDSRegistry Service
                            Message provideAndResgiterSetInput = null;
                            string xdsRegistryEndpointName = CommonUtility.GetXDSRegistryEndpointName();
                            xmlDocRegistryRequest = new XmlDocument();
                            nodeSubmitObjectsRequest = xmlDocRequest.SelectSingleNode(@"//*[local-name()='SubmitObjectsRequest']");
                            xmlDocRegistryRequest.LoadXml(nodeSubmitObjectsRequest.OuterXml);
                            XDSRegistry.XDSRegistryClient objRegsitryClient = new Microsoft.IHE.XDS.DocumentRepository.XDSRegistry.XDSRegistryClient(xdsRegistryEndpointName);
                            provideAndResgiterSetInput = Message.CreateMessage(msgVersion, GlobalValues.CONST_ACTION_REGISTERDOCUMENTSETB, new XmlNodeReader(xmlDocRegistryRequest));
                            registryMessage = objRegsitryClient.RegisterDocumentSet(provideAndResgiterSetInput);
                            
                        }
                        catch
                        {
                            throw new Exception("XDSRegistryNotAvailable");
                        }

                        
                        xmlDocRegistryResponse = new XmlDocument();
                        xmlDocRegistryResponse.Load(registryMessage.GetReaderAtBodyContents());

                        XmlNode errorList = xmlDocRegistryResponse.SelectSingleNode(@"//*[local-name()='RegistryError']");
                        string errorCode = null;

                        if (errorList != null)
                            errorCode = errorList.Attributes["errorCode"].Value;

                        //Move this outside TransactionScope...the log entry is rolled back
                        //Code block within TransactionScope should be
                        //if (errorCode == null)
                        //{
                        //    provideAndRegSetBScope.Complete();
                        //}
                        //Code block outside TransactionScope should be
                        //if (errorCode != null)
                        //{
                        //    repositoryLogic.LogRolledBackData(objDocumentRepositoryLog, errorCode);

                        //    //ATNA Failure Code
                        //    eventOutcomeIndicator = "8";
                        //}


                        if (errorCode != null)
                        {
                            repositoryLogic.LogRolledBackData(objDocumentRepositoryLog, errorCode);

                            //ATNA Failure Code
                            eventOutcomeIndicator = "8";
                        }
                        else
                        {
                            provideAndRegSetBScope.Complete();
                        }
                        
                        //Assign Registry Response to Repository Response
                        xmlDocResponse = xmlDocRegistryResponse;

                        //Transaction is completed
                    }
                }
                else
                {
                    //Get Message from Registry
                    try
                    {

                        //Sample RegistryResponse - <RegistryResponse xmlns="urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0" />;                        
                        //Call the XDSRegistry Service
                        Message msgProvideAndResgiterSetInput = null;
                        string xdsRegistryEndpointName = CommonUtility.GetXDSRegistryEndpointName();
                        xmlDocRegistryRequest = new XmlDocument();
                        nodeSubmitObjectsRequest = xmlDocRequest.SelectSingleNode(@"//*[local-name()='SubmitObjectsRequest']");
                        xmlDocRegistryRequest.LoadXml(nodeSubmitObjectsRequest.OuterXml);
                        XDSRegistry.XDSRegistryClient objRegsitryClient = new Microsoft.IHE.XDS.DocumentRepository.XDSRegistry.XDSRegistryClient(xdsRegistryEndpointName);
                        msgProvideAndResgiterSetInput = Message.CreateMessage(msgVersion, GlobalValues.CONST_ACTION_REGISTERDOCUMENTSETB, new XmlNodeReader(xmlDocRegistryRequest));
                        registryMessage = objRegsitryClient.RegisterDocumentSet(msgProvideAndResgiterSetInput);

                    }
                    catch
                    {
                        throw new Exception("XDSRegistryNotAvailable");
                    }

                    xmlDocRegistryResponse = new XmlDocument();
                    xmlDocRegistryResponse.Load(registryMessage.GetReaderAtBodyContents());

                    XmlNode errorList = xmlDocRegistryResponse.SelectSingleNode(@"//*[local-name()='RegistryError']");
                    string errorCode = null;

                    if (errorList != null)
                        errorCode = errorList.Attributes["errorCode"].Value;


                    if (errorCode != null)
                    {
                        repositoryLogic.LogRolledBackData(objDocumentRepositoryLog, errorCode);

                        //ATNA Failure Code
                        eventOutcomeIndicator = "8";

                    }

                    //Assign Registry Response to Repository Response
                    xmlDocResponse = xmlDocRegistryResponse;

                }
            }
            catch
            {
                throw;
            }

            atnaParameterValues.Add("$EventIdentification.EventOutcomeIndicator$", eventOutcomeIndicator);

            return xmlDocResponse;
            
        }


 
        






  

        

 


        



        private XmlDocument ProcessRetrieveDocumentSetResponse(XmlDocument xmlDocRequest, out StringDictionary atnaParametersValue)
        {
            XmlDocument xmlDocResponse = null;
            RetrieveDocumentSet objDocumentMetadata = null;
            RepositoryLogic repositoryLogic = null;
            List<RetrieveDocumentSet> lstRetrieveDocumentSet = null;
            RetrieveDocumentSet objRetrieveDocumentSet = null;
            XmlElement eltRoot = null;
            XmlNodeList nodeListDocumentRequest = null;
            XmlNode node = null;

            string documentEntryUUIDs = string.Empty;
            string eventOutomeIndicator = "0";

            atnaParametersValue = new StringDictionary();

            try
            {
                
                repositoryLogic = new RepositoryLogic();
                lstRetrieveDocumentSet = new List<RetrieveDocumentSet>();

                //Root Element
                eltRoot = xmlDocRequest.DocumentElement;
                                
                nodeListDocumentRequest = eltRoot.SelectNodes(".//*[local-name()='DocumentRequest']");                
                
                foreach (XmlNode nodeDocumentRequest in nodeListDocumentRequest)
                {
                    objRetrieveDocumentSet = new RetrieveDocumentSet();

                    if (nodeDocumentRequest.HasChildNodes)
                    {
                        node = nodeDocumentRequest.SelectSingleNode(".//*[local-name()='HomeCommunityId']");
                        if(node != null)
                            objRetrieveDocumentSet.HomeCommunityID = node.InnerText;

                        node = nodeDocumentRequest.SelectSingleNode(".//*[local-name()='RepositoryUniqueId']");
                        if(node != null)
                            objRetrieveDocumentSet.RepositoryUniqueID = node.InnerText;

                        node = nodeDocumentRequest.SelectSingleNode(".//*[local-name()='DocumentUniqueId']");
                        if(node != null)
                            objRetrieveDocumentSet.DocumentID = node.InnerText;

                        //Used for ATNA Logging
                        if(string.IsNullOrEmpty(documentEntryUUIDs))
                        {
                            documentEntryUUIDs = objRetrieveDocumentSet.DocumentID;
                        }
                        else
                        {
                            documentEntryUUIDs += ", " + objRetrieveDocumentSet.DocumentID;
                        }
                        

                        //CP - No Validation required for RepositoryUniqueID
                        //objRetrieveDocumentSet.IsRepositoryUniqueIDExsists = IsRepositoryIDExists(objRetrieveDocumentSet.RepositoryUniqueID);
                        
                        objDocumentMetadata = GetDocumentRepositoryMetaData(objRetrieveDocumentSet.DocumentID);

                        objRetrieveDocumentSet.ContentID = objDocumentMetadata.ContentID;
                        objRetrieveDocumentSet.MimeType = objDocumentMetadata.MimeType;

                        if (objRetrieveDocumentSet.ContentID > 0)
                        {
                            objRetrieveDocumentSet.Content = SQLServerStorageService.RetreiveDocument(objRetrieveDocumentSet.ContentID.ToString());
                        }
                       
                        lstRetrieveDocumentSet.Add(objRetrieveDocumentSet);
                    }
                }

                //ATNA
                atnaParametersValue.Add("$DocumentEntry.UUID$", documentEntryUUIDs);

                xmlDocResponse = GenerateTotalRetrieveDocument();
                
                int contentCount = 0;
                
                for (int i = 0; i < lstRetrieveDocumentSet.Count; i++)
                {
                    if (lstRetrieveDocumentSet[i].ContentID > 0)
                    {
                        contentCount++;
                    }
                }
                
                if (contentCount == 0)
                {
                    XmlElement docReosponseRootElement = xmlDocResponse.DocumentElement;
                    XmlElement xEleRegErrList = xmlDocResponse.CreateElement("tns:RegistryResponse", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");

                    XmlAttribute xAttStatus = xmlDocResponse.CreateAttribute("status");
                    xAttStatus.Value = GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE;
                    xEleRegErrList.Attributes.Append(xAttStatus);

                    XmlAttribute xAttXmlNSrs = xmlDocResponse.CreateAttribute("xmlns:tns");
                    xAttXmlNSrs.Value = "urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0";
                    xEleRegErrList.Attributes.Append(xAttXmlNSrs);


                    XmlAttribute xAttXmlNS = xmlDocResponse.CreateAttribute("xmlns:rim");
                    xAttXmlNS.Value = GlobalValues.CONST_XML_NAMESPACE_x;
                    xEleRegErrList.Attributes.Append(xAttXmlNS);

                    XmlElement xEleRegErrList1 = xmlDocResponse.CreateElement("tns:RegistryErrorList", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");

                    XmlAttribute attribhighestSeverity = xmlDocResponse.CreateAttribute("highestSeverity");
                    attribhighestSeverity.Value = "Highest SeverityURI";
                    xEleRegErrList1.Attributes.Append(attribhighestSeverity);

                    for (int loop = 0; loop < lstRetrieveDocumentSet.Count; loop++)
                    {
                        XmlElement xEleRegErr = xmlDocResponse.CreateElement("tns:RegistryError", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");

                        XmlAttribute xAttcodeContext = xmlDocResponse.CreateAttribute("codeContext");
                        xAttcodeContext.Value = GlobalValues.CONST_ERROR_CODE_XDSUnknownRepositoryUniqueID;
                        xEleRegErr.Attributes.Append(xAttcodeContext);

                        XmlAttribute xAttErrorCode = xmlDocResponse.CreateAttribute("errorCode");
                        xAttErrorCode.Value = string.Format("XDSDocumentEntry {0} exists in metadata with no corresponding attached document", lstRetrieveDocumentSet[loop].DocumentID);
                        xEleRegErr.Attributes.Append(xAttErrorCode);

                        XmlAttribute xAttSeverity = xmlDocResponse.CreateAttribute("severity");
                        xAttSeverity.Value = "Error";
                        xEleRegErr.Attributes.Append(xAttSeverity);

                        XmlAttribute xAttLocation = xmlDocResponse.CreateAttribute("location");
                        xAttLocation.Value = lstRetrieveDocumentSet[loop].DocumentID;
                        xEleRegErr.Attributes.Append(xAttLocation);

                        xEleRegErrList1.AppendChild(xEleRegErr);
                    }

                    xEleRegErrList.AppendChild(xEleRegErrList1);

                    docReosponseRootElement.AppendChild(xEleRegErrList);

                    //ATNA Event Outcome Indicator
                    eventOutomeIndicator = "8"; //Failure

                }
                if (contentCount < lstRetrieveDocumentSet.Count && contentCount > 0)
                {
                    //Partial Error
                    XmlElement rootElement = xmlDocResponse.DocumentElement;
                    XmlElement xEleRegErrList = xmlDocResponse.CreateElement("tns:RegistryResponse", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");

                    XmlAttribute xAttStatus = xmlDocResponse.CreateAttribute("status");
                    xAttStatus.Value = GlobalValues.CONST_RESPONSE_STATUS_TYPE_PARTIALSUCCESS;
                    xEleRegErrList.Attributes.Append(xAttStatus);

                    XmlAttribute xAttXmlNSrs = xmlDocResponse.CreateAttribute("xmlns:tns");
                    xAttXmlNSrs.Value = "urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0";
                    xEleRegErrList.Attributes.Append(xAttXmlNSrs);


                    XmlAttribute xAttXmlNS = xmlDocResponse.CreateAttribute("xmlns:rim");
                    xAttXmlNS.Value = GlobalValues.CONST_XML_NAMESPACE_x;
                    xEleRegErrList.Attributes.Append(xAttXmlNS);

                    XmlElement xEleRegErrList1 = xmlDocResponse.CreateElement("tns:RegistryErrorList", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");


                    XmlAttribute attribhighestSeverity = xmlDocResponse.CreateAttribute("highestSeverity");
                    attribhighestSeverity.Value = "Highest SeverityURI";
                    xEleRegErrList1.Attributes.Append(attribhighestSeverity);

                    for (int loop = 0; loop < lstRetrieveDocumentSet.Count; loop++)
                    {
                        //Rep False || ContentID < 1
                        //if(Rep == false)
                        //
                        //else
                        //
                        ////CP - No Validation required for RepositoryUniqueId
                        #region "CP - No Validation required for RepositoryUniqueId"
                        //if (lstRetrieveDocumentSet[loop].IsRepositoryUniqueIDExsists == false)
                            //{
                            //    //RepositoryUniqueID not available

                            //    XmlElement xEleRegErr = xmlDocTotalRetrieveDocumentSet.CreateElement("tns:RegistryError", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");

                            //    XmlAttribute xAttcodeContext = xmlDocTotalRetrieveDocumentSet.CreateAttribute("codeContext");
                            //    xAttcodeContext.Value = CONST_ERROR_CODE_XDSUnknownRepositoryUniqueID;
                            //    xEleRegErr.Attributes.Append(xAttcodeContext);

                            //    XmlAttribute xAttErrorCode = xmlDocTotalRetrieveDocumentSet.CreateAttribute("errorCode");
                            //    xAttErrorCode.Value = "The repositoryUniqueId value could not be resolved to a valid document repository or the value does not match the repositoryUniqueId of the Document Repository";
                            //    xEleRegErr.Attributes.Append(xAttErrorCode);

                            //    XmlAttribute xAttSeverity = xmlDocTotalRetrieveDocumentSet.CreateAttribute("severity");
                            //    xAttSeverity.Value = "PartialSuccess";
                            //    xEleRegErr.Attributes.Append(xAttSeverity);

                            //    XmlAttribute xAttLocation = xmlDocTotalRetrieveDocumentSet.CreateAttribute("location");
                            //    xAttLocation.Value = lstRetrieveDocumentSet[loop].DocumentID;
                            //    xEleRegErr.Attributes.Append(xAttLocation);

                            //    xEleRegErrList1.AppendChild(xEleRegErr);
                            //    xEleRegErrList.AppendChild(xEleRegErrList1);
                        //}
                        #endregion
                        
                        if (lstRetrieveDocumentSet[loop].ContentID < 1)
                        {
                            //RepositoryUniqueID is available.....Content ID not available

                            XmlElement xEleRegErr = xmlDocResponse.CreateElement("tns:RegistryError", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");

                            XmlAttribute xAttcodeContext = xmlDocResponse.CreateAttribute("codeContext");
                            xAttcodeContext.Value = GlobalValues.CONST_ERROR_CODE_XDSRepositoryError;
                            xEleRegErr.Attributes.Append(xAttcodeContext);

                            XmlAttribute xAttErrorCode = xmlDocResponse.CreateAttribute("errorCode");
                            xAttErrorCode.Value = string.Format("XDSDocumentEntry {0} does not exists.", lstRetrieveDocumentSet[loop].DocumentID);
                            xEleRegErr.Attributes.Append(xAttErrorCode);

                            XmlAttribute xAttSeverity = xmlDocResponse.CreateAttribute("severity");
                            xAttSeverity.Value = "Error";
                            xEleRegErr.Attributes.Append(xAttSeverity);

                            XmlAttribute xAttLocation = xmlDocResponse.CreateAttribute("location");
                            xAttLocation.Value = lstRetrieveDocumentSet[loop].DocumentID;
                            xEleRegErr.Attributes.Append(xAttLocation);

                            xEleRegErrList1.AppendChild(xEleRegErr);
                            xEleRegErrList.AppendChild(xEleRegErrList1);
                        }
                        else
                        {
                            //Success
                            xmlDocResponse = repositoryLogic.GenerateContentDocument(xmlDocResponse, lstRetrieveDocumentSet[loop]);
                        }
                    }


                    rootElement.AppendChild(xEleRegErrList);

                    //ATNA Event Outcome Indicator
                    eventOutomeIndicator = "4"; //Partial Success

                }
                else if (contentCount == lstRetrieveDocumentSet.Count)
                {
                    //Success
                    XmlElement rootElement = xmlDocResponse.DocumentElement;
                    XmlElement registryResponseElement = xmlDocResponse.CreateElement("tns:RegistryResponse", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");

                    //status
                    registryResponseElement.Attributes.Append(xmlDocResponse.CreateAttribute("status"));
                    registryResponseElement.Attributes["status"].Value = GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS;

                    //xmlns:tns
                    registryResponseElement.Attributes.Append(xmlDocResponse.CreateAttribute("xmlns:tns"));
                    registryResponseElement.Attributes["xmlns:tns"].Value = "urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0";

                    //xmlns:rim
                    registryResponseElement.Attributes.Append(xmlDocResponse.CreateAttribute("xmlns:rim"));
                    registryResponseElement.Attributes["xmlns:rim"].Value = GlobalValues.CONST_XML_NAMESPACE_x;

                    //Appending Registry Response Element
                    rootElement.AppendChild(registryResponseElement);
                    for (int i = 0; i < lstRetrieveDocumentSet.Count; i++)
                    {
                        xmlDocResponse = repositoryLogic.GenerateContentDocument(xmlDocResponse, lstRetrieveDocumentSet[i]);
                    }

                    //ATNA Event Outcome Indicator
                    eventOutomeIndicator = "0"; //Success

                }
                //xmlDocTotalRetrieveDocumentSet = GenerateContentDocument(xmlDocTotalRetrieveDocumentSet, lstDocEntries);

            }
            catch (Exception ex)
            {                
                throw ex;
            }

            //msgRetrieveDocumentSetResponse = Message.CreateMessage(input.Headers.MessageVersion, CONST_ACTION_RETRIEVEDOCSETRESPONSE, new XmlNodeReader(xmlDocTotalRetrieveDocumentSet));
            
            //ATNA
            atnaParametersValue.Add("$EventIdentification.EventOutcomeIndicator$", eventOutomeIndicator);

            //return msgRetrieveDocumentSetResponse;

            return xmlDocResponse;
        }


        private RetrieveDocumentSet GetDocumentRepositoryMetaData(string documentIUD)
        {
            RetrieveDocumentSet objRetrieveDocumentSet = null;

            try
            {
                RepositoryLogic objRepositoryLogic = new RepositoryLogic();
                objRetrieveDocumentSet = objRepositoryLogic.GetDocumentRepositoryMetaData(documentIUD);
            }
            catch
            {
                throw;
            }

            return objRetrieveDocumentSet;
        }


        private XmlDocument GenerateTotalRetrieveDocument()
        {
            XmlDocument xmlDocRetireveDocument = null;

            try
            {
                xmlDocRetireveDocument = new XmlDocument();

                XmlElement rootElement = xmlDocRetireveDocument.CreateElement("RetrieveDocumentSetResponse", @"urn:ihe:iti:xds-b:2007");
                XmlAttribute nameSpaceAttribute = xmlDocRetireveDocument.CreateAttribute("xmlns");
                nameSpaceAttribute.Value = "urn:ihe:iti:xds-b:2007";
                rootElement.Attributes.Append(nameSpaceAttribute);
                xmlDocRetireveDocument.AppendChild(rootElement);
            }
            catch
            {
                throw;
            }

            return xmlDocRetireveDocument;
        }




        public XmlDocument GenerateRetrieveDocumentSetErrorMessage(string status, string requestId, string codeContext, string errorCode, string severity, string location)
        {
            
            XmlDocument xmlDocErrorMessage = null;

            try
            {
                xmlDocErrorMessage = new XmlDocument();
                XmlElement rootElement = xmlDocErrorMessage.DocumentElement;

                XmlElement eltRegErrList = xmlDocErrorMessage.CreateElement("tns:RegistryResponse", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");

                XmlAttribute attribStatus = xmlDocErrorMessage.CreateAttribute("status");
                attribStatus.Value = status;
                eltRegErrList.Attributes.Append(attribStatus);

                if (requestId != string.Empty)
                {
                    XmlAttribute attribRequestId = xmlDocErrorMessage.CreateAttribute("requestId");
                    attribRequestId.Value = requestId;
                    eltRegErrList.Attributes.Append(attribRequestId);
                }

                XmlAttribute attribXmlNsTns = xmlDocErrorMessage.CreateAttribute("xmlns:tns");
                attribXmlNsTns.Value = "urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0";
                eltRegErrList.Attributes.Append(attribXmlNsTns);


                XmlAttribute attribXmlNsRim = xmlDocErrorMessage.CreateAttribute("xmlns:rim");
                attribXmlNsRim.Value = GlobalValues.CONST_XML_NAMESPACE_x;
                eltRegErrList.Attributes.Append(attribXmlNsRim);

                XmlElement eltRegistryErrorList = xmlDocErrorMessage.CreateElement("tns:RegistryErrorList", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");


                XmlAttribute attribHighestError = xmlDocErrorMessage.CreateAttribute("highestSeverity");
                attribHighestError.Value = "Highest SeverityURI";
                eltRegistryErrorList.Attributes.Append(attribHighestError);

                XmlElement eltRegistryError = xmlDocErrorMessage.CreateElement("tns:RegistryError", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");

                XmlAttribute attribCodeContext = xmlDocErrorMessage.CreateAttribute("codeContext");
                attribCodeContext.Value = codeContext;
                eltRegistryError.Attributes.Append(attribCodeContext);

                XmlAttribute attribErrorCode = xmlDocErrorMessage.CreateAttribute("errorCode");
                attribErrorCode.Value = errorCode;
                eltRegistryError.Attributes.Append(attribErrorCode);

                XmlAttribute attribSeverity = xmlDocErrorMessage.CreateAttribute("severity");
                attribSeverity.Value = severity;
                eltRegistryError.Attributes.Append(attribSeverity);

                if (location != string.Empty)
                {
                    XmlAttribute attribLocation = xmlDocErrorMessage.CreateAttribute("location");
                    attribLocation.Value = location;
                    eltRegistryError.Attributes.Append(attribLocation);
                }

                eltRegistryErrorList.AppendChild(eltRegistryError);
                eltRegErrList.AppendChild(eltRegistryErrorList);

                xmlDocErrorMessage.AppendChild(eltRegErrList);
                
            }

            catch
            {
                throw;
            }

            return xmlDocErrorMessage;
        }


        private string GetSourceUserID()
        {
            string sourceUserID = SOURCE_USERID;

            if (OperationContext.Current.Channel.RemoteAddress != null && OperationContext.Current.Channel.RemoteAddress.Uri != null)
                sourceUserID = OperationContext.Current.Channel.RemoteAddress.Uri.OriginalString;
            else if (OperationContext.Current.ServiceSecurityContext != null && OperationContext.Current.ServiceSecurityContext.PrimaryIdentity != null)
                sourceUserID = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;

            return sourceUserID;
        }

        private string GetDestinationUserID()
        {
            string destinationUserID = string.Empty;

            if (OperationContext.Current.Channel.LocalAddress != null && OperationContext.Current.Channel.LocalAddress.Uri != null)
                destinationUserID = OperationContext.Current.Channel.LocalAddress.Uri.OriginalString;
            else
                destinationUserID = ATNAEvent.XDSREGISTRY_SERVICE_ADDRESS;

            return destinationUserID;
        }

    }
}
