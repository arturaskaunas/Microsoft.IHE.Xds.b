using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Schema;
using System.Configuration;
using System.Security.Cryptography;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;
using Microsoft.IHE.XDS.BusinessLogic.ATNA;


namespace Microsoft.IHE.XDS.BusinessLogic
{
    public class RepositoryLogic
    {
        bool IsSchemaError = false;
        //To create slots for meta data.
        public XmlDocument CreateRepositoryMetadata(XmlDocument xmlDocMsgBody, string slotName, string slotValue, string documentID)
        {
            XmlDocument xmlDocRepositoryMetadata = new XmlDocument();
            xmlDocRepositoryMetadata.LoadXml(xmlDocMsgBody.OuterXml);

            XmlElement rootElement = xmlDocRepositoryMetadata.DocumentElement;
            XmlNodeList extrinsicObjects = rootElement.SelectNodes(".//*[local-name()='ExtrinsicObject']");
            XmlNode submitObjReqNode = rootElement.SelectSingleNode(@"//*[local-name()='SubmitObjectsRequest']");

            for (int nodeCount = 0; nodeCount < extrinsicObjects.Count; nodeCount++)
            {

                XmlNode xn = extrinsicObjects[nodeCount];

                if (xn.Attributes["id"].Value == documentID)
                {
                    XmlElement eSlot = xmlDocRepositoryMetadata.CreateElement("Slot", "urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0");
                    XmlAttribute att = xmlDocRepositoryMetadata.CreateAttribute("name");
                    att.Value = slotName;
                    eSlot.Attributes.Append(att);

                    XmlElement eVal = xmlDocRepositoryMetadata.CreateElement("ValueList", "urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0");
                    eSlot.AppendChild(eVal);

                    XmlElement eValue = xmlDocRepositoryMetadata.CreateElement("Value", "urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0");
                    eValue.InnerText = slotValue;

                    eVal.AppendChild(eValue);

                    //xn.AppendChild(eSlot);
                    xn.InsertAfter(eSlot, null);

                }
            }
                
            return xmlDocRepositoryMetadata;
        }


        public XmlDocument CreateRepositorySlotElement(XmlDocument xmlDocRequest, string slotName, string slotValue, string documentEntryUUID)
        {
            XmlElement eltRoot = null;
            XmlNode nodeExtrinsicObject = null;
            XmlNode nodeExtrinsicObjectSlotValue = null;
            string xpathExtrinsicObject = @".//*[local-name()='ExtrinsicObject'][@id='$id$']";
            string xpathExtrinsicObjectSlotValue = @".//*[local-name()='ExtrinsicObject'][@id='$id$']/*[local-name()='Slot'][@name='$name$']/*[local-name()='ValueList']/*[local-name()='Value']";

            //Root Element
            eltRoot = xmlDocRequest.DocumentElement;

            //ExtrinsicObject element of particular id/entryUUID
            xpathExtrinsicObject = xpathExtrinsicObject.Replace("$id$", documentEntryUUID);
            nodeExtrinsicObject = eltRoot.SelectSingleNode(xpathExtrinsicObject);

            //ExtrinsicObject->Slot->ValueList->Value
            xpathExtrinsicObjectSlotValue = xpathExtrinsicObjectSlotValue.Replace("$id$", documentEntryUUID);
            xpathExtrinsicObjectSlotValue = xpathExtrinsicObjectSlotValue.Replace("$name$", slotName);
            nodeExtrinsicObjectSlotValue = eltRoot.SelectSingleNode(xpathExtrinsicObjectSlotValue);

            if (nodeExtrinsicObject == null)
                return xmlDocRequest;

            if (nodeExtrinsicObjectSlotValue != null)
            {
                nodeExtrinsicObjectSlotValue.InnerText = slotValue;
            }
            else
            {
                XmlElement eltSlot = xmlDocRequest.CreateElement("Slot", "urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0");
                XmlAttribute attribName = xmlDocRequest.CreateAttribute("name");
                attribName.Value = slotName;
                eltSlot.Attributes.Append(attribName);

                XmlElement eltValueList = xmlDocRequest.CreateElement("ValueList", "urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0");
                eltSlot.AppendChild(eltValueList);

                XmlElement eltValue = xmlDocRequest.CreateElement("Value", "urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0");
                eltValue.InnerText = slotValue;
                eltValueList.AppendChild(eltValue);

                nodeExtrinsicObject.InsertAfter(eltSlot, null);
            }

            return xmlDocRequest;
        }
        
        //To get the binary content from the docuement as byte[]
        public byte[] GetDocumentContent(XmlDocument xdsDoc, string xdsUniqueId)
        {
            byte[] documentContent = null;
            try
            {
                XmlElement rootElement = xdsDoc.DocumentElement;
                XmlNodeList xdsDocuments = rootElement.SelectNodes(@"//*[local-name()='Document']");
                foreach (XmlNode xdsDocument in xdsDocuments)
                {
                    if (xdsDocument.Attributes["id"].Value.ToString() == xdsUniqueId)
                    {
                        string strContent = xdsDocument.InnerText.ToString();
                        documentContent = System.Text.ASCIIEncoding.ASCII.GetBytes(strContent);
                    }

                }
            }
            catch
            {
                throw;
            }
            return documentContent;

        }
        
        //public bool IsDocumentIdAvailable(XmlDocument xDoc, XmlNamespaceManager xmlnsMgr)
        //{
        //    bool IsDocumentAvailable = false;
        //    try
        //    {
        //        XmlElement rootElement = xDoc.DocumentElement;
        //        XmlNodeList xdsDocuments = rootElement.SelectNodes("//tns:SubmitObjectsRequest/x:RegistryObjectList/x:ExtrinsicObject", xmlnsMgr);
        //        foreach (XmlNode xdsDocument in xdsDocuments)
        //        {
        //            if (xdsDocument.Attributes["id"] != null && string.IsNullOrEmpty(xdsDocument.Attributes["id"].Value.ToString()))
        //            {
        //                IsDocumentAvailable = true;
        //                break;
        //            }
        //            else
        //            {
        //                IsDocumentAvailable= false;
        //            }

        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return IsDocumentAvailable;
        //}
        //TO get the Binary part of Document

        public Stream GetDocumentContentStream(XmlDocument xmlDocRequest, string documentUniqueId)
        {
            Stream documentContentStream = null;
            string documentData = null;

            XmlElement eltRoot = null;
            XmlNodeList nodeListDocument = null;

            try
            {
                eltRoot = xmlDocRequest.DocumentElement;
                nodeListDocument = eltRoot.SelectNodes(@"//*[local-name()='Document']");

                foreach (XmlNode nodeDocument in nodeListDocument)
                {
                    if (nodeDocument.Attributes["id"].Value == documentUniqueId)
                    {
                        documentData = nodeDocument.InnerText;
                        documentContentStream = new MemoryStream(Convert.FromBase64String(documentData));
                        break;
                    }

                }
            }
            catch
            {
                throw;
            }

            return documentContentStream;
        }
        
        
        public bool IsHashMatching(string documentEntryUUID)
        {
            //Was implemented in Service Layer,Need to Paste it here
            bool IsHashMatching = false;
            try
            {
                RepositoryDataAccess objRepositoryDataAccess = new RepositoryDataAccess();
                IsHashMatching = objRepositoryDataAccess.IsHashMatching(documentEntryUUID);
            }
            catch 
            {

                throw;
            }
            return IsHashMatching;
        }
        
        public bool IsValidUniqueID(string xdsUniqueId)
        {
            bool IsValidUniqueID = true;
            try
            {
                RepositoryDataAccess objRepositoryDataAccess = new RepositoryDataAccess();
                IsValidUniqueID = objRepositoryDataAccess.IsValidUniqueID(xdsUniqueId);
            }
            catch 
            {

                throw;
            }
            return IsValidUniqueID;
        }
        
        public void SaveMetaData(DocumentEntry documentEntry)
        {
            //Code to insert the content into the database.

            try
            {
                //Call DAL's Save Metadata
                RepositoryDataAccess objRepositoryDataAccess = new RepositoryDataAccess();
                objRepositoryDataAccess.SaveMetaData(documentEntry);
            }
            catch
            {

                throw;
            }

        }
        
        public int LogDocumentEntry()
        {
            int noOfRows = 0;
            try
            {
                RepositoryDataAccess objRepositoryDataAccess = new RepositoryDataAccess();
                noOfRows = objRepositoryDataAccess.LogDocumentEntry();
            }
            catch
            {

                throw;
            }
            return noOfRows;
        }
        
        public int LogRepositoryData(DocumentRepositoryLog objDocumentRepositoryLog)
        {
            //Was implemented in Service Layer,Need to Paste it here
            int noOfRows = 0;
            try
            {
                RepositoryDataAccess objRepositoryDataAccess = new RepositoryDataAccess();
                noOfRows = objRepositoryDataAccess.LogRepositoryData(objDocumentRepositoryLog);
            }
            catch 
            {

                throw;
            }
            return noOfRows;
        }
        
        
        public string GetRepositoryUniqueID(string strRepositoryKey)
        {
            string strRepositoryUniqueID = string.Empty;
            try
            {
                RepositoryDataAccess objRepositoryDataAccess = new RepositoryDataAccess();
                strRepositoryUniqueID = objRepositoryDataAccess.GetRepositoryUniqueID(strRepositoryKey);
            }
            catch 
            {

                throw;
            }
            return strRepositoryUniqueID;
        }
        
        
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
                /* Original was if (!IsSchemaError) 
                 * then IsSchemaValid = false - 
                 * this is wrong, as IsSchemaError is only true when a schema error is throw. */
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
            //TODO::Event Log for this schema
            IsSchemaError = true;

        }
        
        public RetrieveDocumentSet GetDocumentRepositoryMetaData(string documentID)
        {
            RetrieveDocumentSet objRetrieveDocumentSet = null;

            try
            {
                RepositoryDataAccess obj = new RepositoryDataAccess();
                objRetrieveDocumentSet = obj.GetDocumentRepositoryMetaData(documentID);
            }
            catch 
            {

                throw;
            }
            return objRetrieveDocumentSet;
        }
        
        public bool IsRepositoryIDExists(string repositoryUniqueID)
        {            
            bool IsRepositoryIDExists = false;
            try
            {
                RepositoryDataAccess repositoryDataAccess = new RepositoryDataAccess();
                IsRepositoryIDExists = repositoryDataAccess.IsRepositoryIDExists(repositoryUniqueID);
            }
            catch 
            {
                throw;
            }
            return IsRepositoryIDExists;
        }


        public XmlDocument GenerateContentDocument(XmlDocument objTotalDocument, RetrieveDocumentSet objRetrieveDocumentSet)
        {
            XmlElement retirieveDocumentSetElement = objTotalDocument.DocumentElement;
            XmlElement documentResponseElement = null;

            documentResponseElement = objTotalDocument.CreateElement("DocumentResponse", "urn:ihe:iti:xds-b:2007");
            retirieveDocumentSetElement.AppendChild(documentResponseElement);

            XmlElement homeCommunityElement = objTotalDocument.CreateElement("HomeCommunityID", "urn:ihe:iti:xds-b:2007");
            homeCommunityElement.InnerText = objRetrieveDocumentSet.HomeCommunityID;

            XmlElement repositoryuniqueIdElement = objTotalDocument.CreateElement("RepositoryUniqueId", "urn:ihe:iti:xds-b:2007");
            repositoryuniqueIdElement.InnerText = objRetrieveDocumentSet.RepositoryUniqueID;

            XmlElement docUniqueId = objTotalDocument.CreateElement("DocumentUniqueId", "urn:ihe:iti:xds-b:2007");
            docUniqueId.InnerText = objRetrieveDocumentSet.DocumentID;

            XmlElement eltMimeType = objTotalDocument.CreateElement("mimeType", "urn:ihe:iti:xds-b:2007");
            eltMimeType.InnerText = objRetrieveDocumentSet.MimeType;

            XmlElement documentContentElement = objTotalDocument.CreateElement("Document", "urn:ihe:iti:xds-b:2007");

            byte[] bytArrDocument = new byte[objRetrieveDocumentSet.Content.Length];
            objRetrieveDocumentSet.Content.Read(bytArrDocument, 0, bytArrDocument.Length);
            
            //documentContentElement.InnerText = System.Text.ASCIIEncoding.ASCII.GetString(bytArrDocument);            
            documentContentElement.InnerText = Convert.ToBase64String(bytArrDocument);
            
            if (!string.IsNullOrEmpty(objRetrieveDocumentSet.HomeCommunityID))
            {
                documentResponseElement.AppendChild(homeCommunityElement);
            }
            documentResponseElement.AppendChild(repositoryuniqueIdElement);
            documentResponseElement.AppendChild(docUniqueId);
            documentResponseElement.AppendChild(eltMimeType);
            documentResponseElement.AppendChild(documentContentElement);

            return objTotalDocument;
        }


        public bool IsMissingDocumentAttachment(XmlElement eltProvideAndRegDocSet)
        {
            bool isMissingDocumentAttachment = false;
            XmlNodeList nodeListExtrinsicObject = null;
            XmlNodeList nodeListDocument = null;
            List<string> lstExtrinsicObjectId = new List<string>();
            List<string> lstDocumentId = new List<string>();

            nodeListExtrinsicObject = eltProvideAndRegDocSet.SelectNodes(".//*[local-name()='ExtrinsicObject']");

            nodeListDocument = eltProvideAndRegDocSet.SelectNodes(".//*[local-name()='Document']");

            foreach (XmlNode node in nodeListExtrinsicObject)
            {
                lstExtrinsicObjectId.Add(node.Attributes["id"].Value);
            }

            foreach (XmlNode node in nodeListDocument)
            {
                lstDocumentId.Add(node.Attributes["id"].Value);
            }

            if (lstDocumentId.Count < lstExtrinsicObjectId.Count)
            {
                isMissingDocumentAttachment = true;
                return isMissingDocumentAttachment;
            }

            for (int count = 0; count < lstExtrinsicObjectId.Count; count++)
            {
                if (!lstDocumentId.Contains(lstExtrinsicObjectId[count]))
                {
                    isMissingDocumentAttachment = true;
                    break;
                }
            }

            return isMissingDocumentAttachment;
        }

        //public bool IsMissingDocumentAttachment(XmlNodeList xdsExtrinsicDocuments, XmlElement rootElement)
        //{
        //    ArrayList objDocAttachmentIDs = null;
        //    ArrayList objMetadatadIDs = null;
        //    bool IsMissingDocumentAttachment = true;
        //    try
        //    {
        //        objDocAttachmentIDs = new ArrayList();
        //        objMetadatadIDs = new ArrayList();

        //        XmlNodeList xnlDocumentNode = rootElement.SelectNodes(@"//*[local-name()='Document']");
        //        foreach (XmlNode node in xnlDocumentNode)
        //        {
        //            objDocAttachmentIDs.Add(node.Attributes["id"].Value);
        //        }
        //        foreach (XmlNode xdsExtrinsicDocument in xdsExtrinsicDocuments)
        //        {

        //            if (xdsExtrinsicDocument.Attributes["id"] != null)
        //            {
        //                objMetadatadIDs.Add(xdsExtrinsicDocument.Attributes["id"].Value);
        //            }
        //        }
        //        for (int docID = 0; docID < objMetadatadIDs.Count; docID++)
        //        {
        //            if (objDocAttachmentIDs.Contains(objMetadatadIDs[docID].ToString()))
        //            {
        //                IsMissingDocumentAttachment = false;
        //            }
        //            else
        //            {
        //                IsMissingDocumentAttachment = true;
        //                break;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        IsMissingDocumentAttachment = true;
        //        throw;
        //    }


        //    return IsMissingDocumentAttachment;

        //}


        public bool IsDuplicateUniqueID(XmlNode rootElement)
        {
            bool IsTrue = false;
            try
            {
                XmlNodeList nodeListSubmitObjectsRequest = rootElement.SelectNodes(".//*[local-name()='SubmitObjectsRequest']//@id");
                List<string> lstSubmitObjectsRequestValue = new List<string>();

                foreach (XmlNode n in nodeListSubmitObjectsRequest)
                {
                    lstSubmitObjectsRequestValue.Add(n.Value);
                }
                for (int count = 0; count < lstSubmitObjectsRequestValue.Count; count++)
                {

                    int index = lstSubmitObjectsRequestValue.IndexOf(lstSubmitObjectsRequestValue[count], count + 1);
                    if (index != -1)
                    {
                        IsTrue = true;
                        break;
                    }

                }

            }
            catch
            {
                throw;
            }


            return IsTrue;
        }


        public bool IsMissingDocumentMetadata(XmlElement eltProvideAndRegDocSet)
        {
            bool isMissingDocumentMetadata = false;
            XmlNodeList nodeListExtrinsicObject = null;
            XmlNodeList nodeListDocument = null;
            List<string> lstExtrinsicObjectId = new List<string>();
            List<string> lstDocumentId = new List<string>();

            nodeListExtrinsicObject = eltProvideAndRegDocSet.SelectNodes(".//*[local-name()='ExtrinsicObject']");

            nodeListDocument = eltProvideAndRegDocSet.SelectNodes(".//*[local-name()='Document']");

            foreach (XmlNode node in nodeListExtrinsicObject)
            {
                lstExtrinsicObjectId.Add(node.Attributes["id"].Value);
            }

            foreach (XmlNode node in nodeListDocument)
            {
                lstDocumentId.Add(node.Attributes["id"].Value);
            }

            if (lstExtrinsicObjectId.Count < lstDocumentId.Count)
            {
                isMissingDocumentMetadata = true;
                return isMissingDocumentMetadata;
            }

            for (int count = 0; count < lstDocumentId.Count; count++)
            {
                if (!lstExtrinsicObjectId.Contains(lstDocumentId[count]))
                {
                    isMissingDocumentMetadata = true;
                    break;
                }
            }

            return isMissingDocumentMetadata;
        }

        ///// <summary>
        ///// It identifies whether any Extrinsic object is exists,else simply pass the Metadata
        ///// </summary>
        ///// <param name="nodeListExtrinsicDocuments"></param>
        ///// <param name="rootElement"></param>
        ///// <returns></returns>       
        //public bool IsMissingDocumentMetadata(XmlNodeList nodeListExtrinsicDocuments, XmlElement rootElement)
        //{

        //    List<string> lstDocAttachmentIds = new List<string>();
        //    List<string> lstMetadatadIds = new List<string>();
        //    bool IsMissingDocumentMetadata = true;

        //    try
        //    {
        //        XmlNodeList xnlDocumentNode = rootElement.SelectNodes(@"//*[local-name()='Document']");

        //        foreach (XmlNode node in xnlDocumentNode)
        //        {
        //            lstDocAttachmentIds.Add(node.Attributes["id"].Value);
        //        }

        //        foreach (XmlNode nodeExtrinsicDocument in nodeListExtrinsicDocuments)
        //        {

        //            if (nodeExtrinsicDocument.Attributes["id"] != null)
        //            {
        //                lstMetadatadIds.Add(nodeExtrinsicDocument.Attributes["id"].Value);
        //            }
        //        }

        //        for (int attachmentCount = 0; attachmentCount < lstDocAttachmentIds.Count; attachmentCount++)
        //        {
        //            if (lstMetadatadIds.Contains(lstMetadatadIds[attachmentCount]))
        //            {
        //                IsMissingDocumentMetadata = false;
        //            }
        //            else
        //            {
        //                IsMissingDocumentMetadata = true;

        //                break;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        IsMissingDocumentMetadata = true;
        //        //throw;
        //    }


        //    return IsMissingDocumentMetadata;
        //}


        public void LogRolledBackData(DocumentRepositoryLog objDocumentRepositoryLog, string errorCode)
        {
            try
            {
                //objDocumentRepositoryLog.Result = "ErrorCode";
                objDocumentRepositoryLog.Result = errorCode;
                objDocumentRepositoryLog.EndTime = DateTime.Now;
                int noOfRows = LogRepositoryData(objDocumentRepositoryLog);
            }
            catch
            {
                throw;
            }
        }


        public string GetHashCode(XmlDocument xmlDocRequest, string documentUniqueId)
        {

            string documentData = null;
            string hash = null;
            byte[] auditMessageStream = null;
            byte[] hashValue = null;
            string hashAlgorithmName = "SHA1";
            HashAlgorithm hashAlgorithm = null;
            StringBuilder sbHashValue = null;

            XmlElement eltRoot = null;
            XmlNodeList nodeListDocument = null;

            try
            {
                sbHashValue = new StringBuilder();


                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["HASH_ALGORITHM"]))
                    hashAlgorithmName = ConfigurationManager.AppSettings["HASH_ALGORITHM"];

                eltRoot = xmlDocRequest.DocumentElement;
                nodeListDocument = eltRoot.SelectNodes(@"//*[local-name()='Document']");

                foreach (XmlNode nodeDocument in nodeListDocument)
                {
                    if (nodeDocument.Attributes["id"].Value == documentUniqueId)
                    {
                        documentData = nodeDocument.InnerText;
                        break;
                    }

                }

                //Convert Document Data to Byte Array
                auditMessageStream = Convert.FromBase64String(documentData);
                hashAlgorithm = HashAlgorithm.Create(hashAlgorithmName);
                hashValue = hashAlgorithm.ComputeHash(auditMessageStream);

                foreach (byte byt in hashValue)
                    sbHashValue.Append(byt.ToString("X2"));

                hash = sbHashValue.ToString();
            }
            catch
            {
                throw;
            }           

            return hash;
        }



        public void ProcessProvideAndRegisterATNAEvent(string submissionSetUniqueID, string sourceUserID, string destinationUserID, string eventOutcomeIndicator)
        {
            try
            {
                //Log ATNA - Repository Event
                ATNALogic atnaLogic = new ATNALogic();
                AuditMessageConfiguration auditMsgConfig = null;

                auditMsgConfig = atnaLogic.GetAuditMessageConfigurationDetails("REPOSITORY-P-AND-R-IMPORT-ITI-41");

                if (auditMsgConfig != null)
                {

                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$SubmissionSet.UniqueID$", submissionSetUniqueID);
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$SubmissionSet.ClassificationNode.UUID$", ATNAEvent.XDSREPOSITORY_SUBMISSIONSET_CLASSIFICATIONNODE_UUID);

                    //$ActiveParticipant.UserID.Source$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Source$", sourceUserID);

                    //$ActiveParticipant.UserID.Destination$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Destination$", destinationUserID);

                    atnaLogic.ProcessEvent(auditMsgConfig, ATNAEvent.XDSREPOSITORY_TYPE, eventOutcomeIndicator, ATNAEvent.UDP_TAG_APPNAME_REPOSITORY);
                }
            }
            catch
            {
                //Oops...ATNA event failed...probably UDP failure....can't afford to stop XDSRepository service sorry :(
            }
        }


        public void ProcessRegisterDocumentSetATNAEvent(string submissionSetUniqueID, string sourceUserID, string destinationUserID, string eventOutcomeIndicator)
        {
            try
            {
                //Log ATNA - Repository Event
                ATNALogic atnaLogic = new ATNALogic();
                AuditMessageConfiguration auditMsgConfig = null;

                auditMsgConfig = atnaLogic.GetAuditMessageConfigurationDetails("REPOSITORY-RDS-EXPORT-ITI-42");

                if (auditMsgConfig != null)
                {

                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$SubmissionSet.UniqueID$", submissionSetUniqueID);
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$SubmissionSet.ClassificationNode.UUID$", ATNAEvent.XDSREPOSITORY_SUBMISSIONSET_CLASSIFICATIONNODE_UUID);

                    //$ActiveParticipant.UserID.Source$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Source$", sourceUserID);

                    //$ActiveParticipant.UserID.Destination$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Destination$", destinationUserID);

                    atnaLogic.ProcessEvent(auditMsgConfig, ATNAEvent.XDSREPOSITORY_TYPE, eventOutcomeIndicator, ATNAEvent.UDP_TAG_APPNAME_REPOSITORY);
                }
            }
            catch
            {
                //Oops...ATNA event failed...probably UDP failure....can't afford to stop XDSRepository service sorry :(
            }
        }

        public void ProcessRetrieveDocumentSetATNAEvent(string documentEntryUUID, string sourceUserID, string destinationUserID, string eventOutcomeIndicator)
        {
            try
            {
                //Log ATNA - Repository Event
                ATNALogic atnaLogic = new ATNALogic();
                AuditMessageConfiguration auditMsgConfig = null;

                auditMsgConfig = atnaLogic.GetAuditMessageConfigurationDetails("REPOSITORY-RDS-EXPORT-ITI-43");

                if (auditMsgConfig != null)
                {

                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$DocumentEntry.UUID$", documentEntryUUID);
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$Document.UUID$", ATNAEvent.XDSREPOSITORY_DOCUMENT_CLASSIFICATIONNODE_UUID);

                    //$ActiveParticipant.UserID.Source$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Source$", sourceUserID);

                    //$ActiveParticipant.UserID.Destination$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Destination$", destinationUserID);

                    atnaLogic.ProcessEvent(auditMsgConfig, ATNAEvent.XDSREPOSITORY_TYPE, eventOutcomeIndicator, ATNAEvent.UDP_TAG_APPNAME_REPOSITORY);
                }
            }
            catch
            {
                //Oops...ATNA event failed...probably UDP failure....can't afford to stop XDSRepository service sorry :(
            }
        }


    }
}
