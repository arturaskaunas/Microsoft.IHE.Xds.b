using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;

namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class GetAll : StoredQueryBase
    {

        public override System.Xml.XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest)
        {
            XmlDocument xmlDocResponse = null;
            XmlDocument xmlDocAdHocQueryResponse;
            List<SubmissionSet> lstSubmissionSet = null;
            List<DocumentEntry> lstDocumentEntry = null;
            List<Folder> lstFolder = null;
            List<Association> lstAssociation = null;
            string patientUID = null;
            string submissionSetStatus = null;
            string documentEntryStatus = null;
            string folderStatus = null;
            string documentEntryUUIDs = null;
            RegistryStoredQueryDataAccess objStoredQueryDAL = new RegistryStoredQueryDataAccess();
            
            patientUID = base.RemoveSingleQuotes(GetParameterValue(objStoredQueryRequest.ParameterList, "$patientId"));
            submissionSetStatus = base.PrepareForSqlInStatement(GetParameterValue(objStoredQueryRequest.ParameterList, "$XDSSubmissionSetStatus"));
            documentEntryStatus = base.PrepareForSqlInStatement(GetParameterValue(objStoredQueryRequest.ParameterList, "$XDSDocumentEntryStatus"));
            folderStatus = base.PrepareForSqlInStatement(GetParameterValue(objStoredQueryRequest.ParameterList, "$XDSFolderStatus"));

            //SubmissionSet
            lstSubmissionSet = objStoredQueryDAL.GetSubmissionSets(submissionSetStatus, patientUID);

            //DocumentEntry
            lstDocumentEntry = objStoredQueryDAL.GetDocumentEntries(documentEntryStatus, patientUID);
            lstDocumentEntry = GetDocumentEntries(lstDocumentEntry, objStoredQueryRequest);
            documentEntryUUIDs = GetDocumentEntryUUIDs(lstDocumentEntry);

            //Folders
            lstFolder = objStoredQueryDAL.GetFolders(folderStatus, patientUID);

            //Associations
            lstAssociation = objStoredQueryDAL.GetAssociations(documentEntryUUIDs, null);


            //Construct AdHocQueryResponse Element
            xmlDocAdHocQueryResponse = StoredQueryBase.ConstructAdHocQueryResponseElement(GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS);

            xmlDocResponse = ConstructRegistryObjectList(xmlDocAdHocQueryResponse, lstSubmissionSet, lstDocumentEntry, lstFolder, lstAssociation);

            return xmlDocResponse;              
        }


        private XmlDocument ConstructRegistryObjectList(XmlDocument xmlDocAdHocQueryResponse, List<SubmissionSet> lstSubmissionSet, List<DocumentEntry> lstDocumentEntry, List<Folder> lstFolder, List<Association> lstAssociation)
        {
            XmlElement xmlRoot = xmlDocAdHocQueryResponse.DocumentElement;

            //RegistryObjectList
            XmlElement eltRegistryObjectList = xmlDocAdHocQueryResponse.CreateElement("rim", "RegistryObjectList", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);
            xmlRoot.AppendChild(eltRegistryObjectList);

            //Submissions Sets
            if (lstSubmissionSet != null)
            {
                for (int count = 0; count < lstSubmissionSet.Count; count++)
                {
                    eltRegistryObjectList.InnerXml += lstSubmissionSet[count].SubmissionSetXml;
                }
            }

            //Document Entries
            if (lstDocumentEntry != null)
            {
                for (int count = 0; count < lstDocumentEntry.Count; count++)
                {
                    eltRegistryObjectList.InnerXml += lstDocumentEntry[count].ExtrinsicObjectXML;
                }
            }

            //Folders
            if (lstFolder != null)
            {
                for (int count = 0; count < lstFolder.Count; count++)
                {
                    eltRegistryObjectList.InnerXml += lstFolder[count].FolderXml;
                }
            }

            //Related Associations
            if (lstAssociation != null)
            {
                for (int count = 0; count < lstAssociation.Count; count++)
                {
                    eltRegistryObjectList.InnerXml += lstAssociation[count].AssociationXml;
                }
            }

            return xmlDocAdHocQueryResponse;
        }


        private string GetDocumentEntryUUIDs(List<DocumentEntry> lstDocumentEntry)
        {
            string documentEntryUUIDs = string.Empty;

            if(lstDocumentEntry == null)
                return documentEntryUUIDs;

            for (int count = 0; count < lstDocumentEntry.Count; count++)
            {
                if (count > 0)
                    documentEntryUUIDs += ",";

                documentEntryUUIDs += lstDocumentEntry[count].EntryUUID;
            }

            return documentEntryUUIDs;
        }

        protected List<DocumentEntry> GetDocumentEntries(List<DocumentEntry> lstDocumentEntries, StoredQuery objStoredQueryRequest)
        {
            List<DocumentEntry> lstFilteredDocumentEntryObjects = new List<DocumentEntry>();
            List<string> lstEntryUUIDs = new List<string>();
            DocumentEntry objDocumentEntry = null;
            string[] arrConfidentialityCodes = null;
            string[] arrFormatCodes = null;

            if (lstDocumentEntries == null)
                return lstDocumentEntries;

            if (objStoredQueryRequest == null)
                return lstDocumentEntries;
                        
            arrConfidentialityCodes = base.GetSQLINParameterValues(objStoredQueryRequest, "$XDSDocumentEntryConfidentialityCode");
            arrFormatCodes = base.GetSQLINParameterValues(objStoredQueryRequest, "$XDSDocumentEntryFormatCode");

            if ((arrConfidentialityCodes == null) && (arrFormatCodes == null))
                return lstDocumentEntries;

            //Add Matching Confidentiality Codes 
            if (arrConfidentialityCodes != null)
            {
                for (int count = 0; count < arrConfidentialityCodes.Length; count++)
                {
                    objDocumentEntry = lstDocumentEntries.Find(
                        delegate(DocumentEntry documentEntry)
                        {
                            if (documentEntry.ConfidentialityCode == null)
                                return false;

                            if (documentEntry.ConfidentialityCode.Value == arrConfidentialityCodes[count])
                                return true;

                            return false;
                        }
                    );

                    if (objDocumentEntry != null)
                        lstEntryUUIDs.Add(objDocumentEntry.EntryUUID);
                }
            }

            //Add Matching Format Codes
            if (arrFormatCodes != null)
            {
                for (int count = 0; count < arrFormatCodes.Length; count++)
                {
                    objDocumentEntry = lstDocumentEntries.Find(
                        delegate(DocumentEntry documentEntry)
                        {
                            if (documentEntry.FormatCode == null)
                                return false;

                            if (documentEntry.FormatCode.Value == arrFormatCodes[count])
                                return true;

                            return false;
                        }
                    );

                    if ((objDocumentEntry != null) && (!lstEntryUUIDs.Contains(objDocumentEntry.EntryUUID)))
                        lstEntryUUIDs.Add(objDocumentEntry.EntryUUID);
                }
            }

            //Merge Filtered DocumentEntry Objects
            for (int count = 0; count < lstEntryUUIDs.Count; count++)
            {
                objDocumentEntry = lstDocumentEntries.Find(
                    delegate(DocumentEntry documentEntry)
                    {

                        if (documentEntry.EntryUUID == lstEntryUUIDs[count])
                            return true;

                        return false;
                    }
                );

                if (objDocumentEntry != null)
                    lstFilteredDocumentEntryObjects.Add(objDocumentEntry);
            }

            return lstFilteredDocumentEntryObjects;
        }

    }
}
