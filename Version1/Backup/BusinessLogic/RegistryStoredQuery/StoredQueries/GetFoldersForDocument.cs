using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;

namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class GetFoldersForDocument : StoredQueryBase
    {

        public override System.Xml.XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest)
        {
            XmlDocument xmlDocResponse = null;
            XmlDocument xmlDocAdHocQueryResponse;            
            List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolders = null;
            List<Folder> lstFolders = null;
            List<string> lstFolderIDs = null;

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

            //SubmissionSetDocumentFolder/Associations
            lstSubmissionSetDocumentFolders = GetSubmissionSetDocumentFolderDetails(objStoredQueryRequest.ParameterList);

            //Folders
            lstFolderIDs = base.GetFolderIDs(lstSubmissionSetDocumentFolders);
            lstFolders = GetFoldersByFolderIDs(lstFolderIDs);

            //Construct AdHocQueryResponse Element
            xmlDocAdHocQueryResponse = StoredQueryBase.ConstructAdHocQueryResponseElement(GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS);

            xmlDocResponse = ConstructRegistryObjectList(xmlDocAdHocQueryResponse, lstFolders);

            return xmlDocResponse;  
        }

        private XmlDocument ConstructRegistryObjectList(XmlDocument xmlDocAdHocQueryResponse, List<Folder> lstFolders)
        {
            XmlElement xmlRoot = xmlDocAdHocQueryResponse.DocumentElement;

            //RegistryObjectList
            XmlElement eltRegistryObjectList = xmlDocAdHocQueryResponse.CreateElement("rim", "RegistryObjectList", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);
            xmlRoot.AppendChild(eltRegistryObjectList);

            //Folders
            if (lstFolders != null)
            {
                for (int count = 0; count < lstFolders.Count; count++)
                {
                    eltRegistryObjectList.InnerXml += lstFolders[count].FolderXml;
                }
            }

            return xmlDocAdHocQueryResponse;
        }


        private List<SubmissionSetDocumentFolder> GetSubmissionSetDocumentFolderDetails(List<StoredQueryParameter> lstStoredQueryParameter)
        {
            List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolder = null;
            RegistryStoredQueryDataAccess objStoredQueryDAL = null;
            string paramValue = null;
            string entryUUID = null;
            string uniqueID = null;
            
            //Prepare SQL IN Param Value
            paramValue = PrepareForSqlInStatement(lstStoredQueryParameter[0].ParameterValue);

            if (lstStoredQueryParameter[0].ParameterName == "$XDSDocumentEntryEntryUUID")
            {
                entryUUID = paramValue;
            }
            else
            {
                uniqueID = paramValue;
            }

            objStoredQueryDAL = new RegistryStoredQueryDataAccess();
            lstSubmissionSetDocumentFolder = objStoredQueryDAL.GetSubmissionSetDocumentFolderByEntryUUIDorUniqueID(entryUUID, uniqueID);

            return lstSubmissionSetDocumentFolder;
        }

        private List<Folder> GetFoldersByFolderIDs(List<string> lstFolderIDs)
        {
            string[] arrFolderIDs = null;
            string folderIDs = null;
            List<Folder> lstFolders = null;
            RegistryStoredQueryDataAccess objRegistryStoredQueryDAL = null;

            arrFolderIDs = lstFolderIDs.ToArray();
            folderIDs = string.Join(",", arrFolderIDs);

            objRegistryStoredQueryDAL = new RegistryStoredQueryDataAccess();
            lstFolders = objRegistryStoredQueryDAL.GetFoldersByFolderIDs(folderIDs);
            objRegistryStoredQueryDAL = null;

            return lstFolders;
        }



    }
}
