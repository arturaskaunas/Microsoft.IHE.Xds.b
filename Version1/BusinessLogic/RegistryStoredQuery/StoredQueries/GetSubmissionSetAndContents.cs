using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;

namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class GetSubmissionSetAndContents : StoredQueryBase
    {

        public override System.Xml.XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest)
        {
            XmlDocument xmlDocResponse = null;
            XmlDocument xmlDocAdHocQueryResponse;
            RegistryStoredQueryDataAccess objStoredQueryDAL = null;
            SubmissionSetAssociation objSubmissionSetAssociation = null;
            List<Folder> lstSubmissionSetFolders = null;
            List<DocumentEntry> lstSubmissionSetDocumentEntry = null;
            List<string> lstSubmissionSetDocumentEntryIDs = null;
            List<string> lstSubmissionSetFolderIDs = null;
            List<string> lstFolderDocumentEntryIds = null;
            List<DocumentEntry> lstFolderDocumentEntry = null;
            string documentEntryIDs = null;
            string folderIDs = null;

            objStoredQueryDAL = new RegistryStoredQueryDataAccess();

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

            //SubmissionSet & Association
            objSubmissionSetAssociation = GetSubmissionSetAndAssociation(objStoredQueryRequest);
            
            lstSubmissionSetDocumentEntryIDs = GetDocumentEntryIDs(objSubmissionSetAssociation.SubmissionSetDocumentFolders);
            lstSubmissionSetFolderIDs = GetFolderIDs(objSubmissionSetAssociation.SubmissionSetDocumentFolders);
            
            //Documents directly associated to SubmissionSet
            documentEntryIDs = base.PrepareForSqlInStatement(lstSubmissionSetDocumentEntryIDs);

            if (!string.IsNullOrEmpty(documentEntryIDs))
            {
                lstSubmissionSetDocumentEntry = base.GetDocumentEntriesByIds(documentEntryIDs, objStoredQueryRequest);
            }

            //Folders directly associated to SubmissionSet
            folderIDs = base.PrepareForSqlInStatement(lstSubmissionSetFolderIDs);

            if (!string.IsNullOrEmpty(folderIDs))
            {                
                lstSubmissionSetFolders = objStoredQueryDAL.GetFoldersByFolderIDs(folderIDs);

                //=========================================================================
                //Only association of Sub-Doc & Sub-Folder needs to returned
                //Folder-Doc should not be returned
                //Refer: http://groups.google.com/group/ihe-xds-implementors/browse_thread/thread/b9771865e01a674c/d3fef7b63d6a184c?lnk=gst&q=GetSubmissionSetAndContents#d3fef7b63d6a184c
                //List<SubmissionSetDocumentFolder> lstSSTDF = objStoredQueryDAL.GetAssociationsForFolderIDs(folderIDs);

                //if ((lstSSTDF != null) && (lstSSTDF.Count > 0))
                //{
                //    objSubmissionSetAssociation.SubmissionSetDocumentFolders.AddRange(lstSSTDF);

                //    lstFolderDocumentEntryIds = base.GetDocumentEntryIDs(lstSSTDF);
                //    documentEntryIDs = base.PrepareForSqlInStatement(lstFolderDocumentEntryIds);

                //    if (!string.IsNullOrEmpty(documentEntryIDs))
                //    {
                //        lstFolderDocumentEntry = base.GetDocumentEntries(documentEntryIDs, objStoredQueryRequest);

                //        if ((lstFolderDocumentEntry != null) && (lstFolderDocumentEntry.Count > 0))
                //            lstSubmissionSetDocumentEntry.AddRange(lstFolderDocumentEntry);
                //    }
                //}
                //=========================================================================
            }

            //Remove Duplicate Associations
            objSubmissionSetAssociation.SubmissionSetDocumentFolders = base.RemoveDuplicateSubmissionSetDocumentFolder(objSubmissionSetAssociation.SubmissionSetDocumentFolders);

            //Remove Duplicate DocumentEntries
            lstSubmissionSetDocumentEntry = base.RemoveDuplicateDocumentEntry(lstSubmissionSetDocumentEntry);

            //Filter Documents with Association
            objSubmissionSetAssociation.SubmissionSetDocumentFolders = FilterSubmissionSetDocumentFolders(objSubmissionSetAssociation.SubmissionSetDocumentFolders, lstSubmissionSetDocumentEntry);

            //Construct AdHocQueryResponse Element
            xmlDocAdHocQueryResponse = StoredQueryBase.ConstructAdHocQueryResponseElement(GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS);

            xmlDocResponse = ConstructRegistryObjectList(xmlDocAdHocQueryResponse, objSubmissionSetAssociation.SubmissionSets, objSubmissionSetAssociation.SubmissionSetDocumentFolders, lstSubmissionSetDocumentEntry, lstSubmissionSetFolders);

            return xmlDocResponse;            
        }

        private XmlDocument ConstructRegistryObjectList(XmlDocument xmlDocAdHocQueryResponse, List<SubmissionSet> lstSubmissionSets, List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolders, List<DocumentEntry> lstDocumentEntries, List<Folder> lstFolders)
        {
            XmlElement xmlRoot = xmlDocAdHocQueryResponse.DocumentElement;

            StringBuilder sbObjectRefXml = new StringBuilder();

            //RegistryObjectList
            XmlElement eltRegistryObjectList = xmlDocAdHocQueryResponse.CreateElement("rim", "RegistryObjectList", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);
            xmlRoot.AppendChild(eltRegistryObjectList);

            //SubmissionSetXml
            if (lstSubmissionSets != null)
            {
                for (int iCount = 0; iCount < lstSubmissionSets.Count; iCount++)
                {
                    eltRegistryObjectList.InnerXml += lstSubmissionSets[iCount].SubmissionSetXml;
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

            //Folders
            if (lstFolders != null)
            {
                for (int iCount = 0; iCount < lstFolders.Count; iCount++)
                {
                    eltRegistryObjectList.InnerXml += lstFolders[iCount].FolderXml;
                }
            }

            return xmlDocAdHocQueryResponse;
        }

        private SubmissionSetAssociation GetSubmissionSetAndAssociation(StoredQuery objStoredQueryRequest)
        {
            SubmissionSetAssociation objSubmissionSetAssociation = null;
            RegistryStoredQueryDataAccess objDAL = null;
            string entryUUID = null;
            string uniqueID = null;

            for (int count = 0; count < objStoredQueryRequest.ParameterList.Count; count++)
            {
                if (objStoredQueryRequest.ParameterList[count].ParameterName == "$XDSSubmissionSetEntryUUID")
                {
                    entryUUID = base.RemoveSingleQuotes(objStoredQueryRequest.ParameterList[count].ParameterValue);
                }
                else if (objStoredQueryRequest.ParameterList[count].ParameterName == "$XDSSubmissionSetUniqueId")
                {
                    uniqueID = base.RemoveSingleQuotes(objStoredQueryRequest.ParameterList[count].ParameterValue);
                }
            }

            objDAL = new RegistryStoredQueryDataAccess();
            objSubmissionSetAssociation = objDAL.GetSubmissionSetAndAssociation(entryUUID, uniqueID);

            if ((objSubmissionSetAssociation != null) && (objSubmissionSetAssociation.SubmissionSets != null))
                objSubmissionSetAssociation.SubmissionSets = base.RemoveDuplicateSubmissionSet(objSubmissionSetAssociation.SubmissionSets);

            return objSubmissionSetAssociation;
        }

        private List<SubmissionSetDocumentFolder> FilterSubmissionSetDocumentFolders(List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolder, List<DocumentEntry> lstDocumentEntry)
        {
            List<SubmissionSetDocumentFolder> lstFilteredSSDF = new List<SubmissionSetDocumentFolder>();

            for (int count = 0; count < lstSubmissionSetDocumentFolder.Count; count++)
            {
                if (lstSubmissionSetDocumentFolder[count].DocumentEntryID > 0)
                {
                    if (lstDocumentEntry.Exists(
                        delegate(DocumentEntry documentEntry)
                        {

                            if (documentEntry.ID == lstSubmissionSetDocumentFolder[count].DocumentEntryID)
                                return true;

                            return false;
                        }
                        ))
                    {
                        lstFilteredSSDF.Add(lstSubmissionSetDocumentFolder[count]);
                    }
                }
                else
                {
                    lstFilteredSSDF.Add(lstSubmissionSetDocumentFolder[count]);
                }
            }

            return lstFilteredSSDF;
        }

    }



}
