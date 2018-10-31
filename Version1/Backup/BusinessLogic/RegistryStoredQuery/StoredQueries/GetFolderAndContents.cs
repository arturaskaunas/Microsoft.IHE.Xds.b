using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;

namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class GetFolderAndContents : StoredQueryBase
    {

        public override System.Xml.XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest)
        {
            XmlDocument xmlDocResponse = null;
            XmlDocument xmlDocAdHocQueryResponse;            
            List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolders = null;
            List<Folder> lstFolders = null;
            List<DocumentEntry> lstDocumentEntry = null;
            List<string> lstDocumentEntryIDs = null;
            string documentEntryIDs = null;


            //Validate Parameters (Either EntryUUID or UniqueID has to be passed)
            if (base.IsEmptyParametersExists(objStoredQueryRequest.ParameterList))
            {
                xmlDocResponse = StoredQueryBase.ConstructStoredQueryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "No parameters passed.", GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                return xmlDocResponse;
            }

            if (base.IsBothParametersPassed(objStoredQueryRequest.ParameterList))
            {
                xmlDocResponse = StoredQueryBase.ConstructStoredQueryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Both entryUUID and uniqueUUID parameters are passed. Only one parameter is allowed.", GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                return xmlDocResponse;
            }

            //Folders
            lstFolders = GetFoldersByEntryUUIDOrUniqueID(objStoredQueryRequest.ParameterList);

            //Associations
            lstSubmissionSetDocumentFolders = GetAssociationsForFolderIDs(lstFolders);

            //Document Entries
            lstDocumentEntryIDs = base.GetDocumentEntryIDs(lstSubmissionSetDocumentFolders);
            documentEntryIDs = base.PrepareForSqlInStatement(lstDocumentEntryIDs);

            if (!string.IsNullOrEmpty(documentEntryIDs))
            {
                lstDocumentEntry = GetDocumentEntriesByIds(documentEntryIDs, objStoredQueryRequest);
                lstSubmissionSetDocumentFolders = FilterAssociationBasedonDocuments(lstSubmissionSetDocumentFolders, lstDocumentEntry);
            }

            //Construct AdHocQueryResponse Element
            xmlDocAdHocQueryResponse = StoredQueryBase.ConstructAdHocQueryResponseElement(GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS);

            xmlDocResponse = ConstructRegistryObjectList(xmlDocAdHocQueryResponse, lstFolders, lstSubmissionSetDocumentFolders, lstDocumentEntry);

            return xmlDocResponse;      
        }


        private XmlDocument ConstructRegistryObjectList(XmlDocument xmlDocAdHocQueryResponse, List<Folder> lstFolders, List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolders, List<DocumentEntry> lstDocumentEntries)
        {
            XmlElement xmlRoot = xmlDocAdHocQueryResponse.DocumentElement;

            StringBuilder sbObjectRefXml = new StringBuilder();

            //RegistryObjectList
            XmlElement eltRegistryObjectList = xmlDocAdHocQueryResponse.CreateElement("rim", "RegistryObjectList", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);
            xmlRoot.AppendChild(eltRegistryObjectList);

            //Folders
            if (lstFolders != null)
            {
                for (int iCount = 0; iCount < lstFolders.Count; iCount++)
                {
                    eltRegistryObjectList.InnerXml += lstFolders[iCount].FolderXml;
                }
            }

            //AssociationXml
            if (lstSubmissionSetDocumentFolders != null)
            {
                for (int iCount = 0; iCount < lstSubmissionSetDocumentFolders.Count; iCount++)
                {
                    eltRegistryObjectList.InnerXml += lstSubmissionSetDocumentFolders[iCount].AssociationXml;
                }
            }

            //Document Entries
            if (lstDocumentEntries != null)
            {
                for (int iCount = 0; iCount < lstDocumentEntries.Count; iCount++)
                {
                    eltRegistryObjectList.InnerXml += lstDocumentEntries[iCount].ExtrinsicObjectXML;
                }
            }

            return xmlDocAdHocQueryResponse;
        }


        private List<SubmissionSetDocumentFolder> GetAssociationsForFolderIDs(List<Folder> lstFolders)
        {
            string folderIDs = null;
            List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolders = null;
            RegistryStoredQueryDataAccess objRegistryStoredQueryDAL = null;

            for (int count = 0; count < lstFolders.Count; count++)
            {
                if (count > 0)
                {
                    folderIDs += ",";
                }
                    
                folderIDs = lstFolders[count].FolderID.ToString();
            }

            objRegistryStoredQueryDAL = new RegistryStoredQueryDataAccess();
            lstSubmissionSetDocumentFolders = objRegistryStoredQueryDAL.GetAssociationsForFolderIDs(folderIDs);
            objRegistryStoredQueryDAL = null;

            return lstSubmissionSetDocumentFolders;
        }


        private List<SubmissionSetDocumentFolder> FilterAssociationBasedonDocuments(List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolders, List<DocumentEntry> lstDocumentEntry)
        {
            string targetObject = null;
            List<SubmissionSetDocumentFolder> lstFilteredSSDF = new List<SubmissionSetDocumentFolder>();

            for (int count = 0; count < lstSubmissionSetDocumentFolders.Count; count++)
            {
                targetObject = lstSubmissionSetDocumentFolders[count].TargetObject;

                if (lstDocumentEntry.Exists(
                    delegate(DocumentEntry documentEntry)
                    {
                        if (documentEntry.EntryUUID == targetObject)
                            return true;

                        return false;
                    }
                    ))
                {
                    lstFilteredSSDF.Add(lstSubmissionSetDocumentFolders[count]);
                }

            }

            return lstFilteredSSDF;
        }
        

        
    }
}
