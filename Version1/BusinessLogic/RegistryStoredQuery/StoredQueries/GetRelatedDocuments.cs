
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;

namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class GetRelatedDocuments : StoredQueryBase
    {

        public override System.Xml.XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest)
        {
            XmlDocument xmlDocResponse = null;
            XmlDocument xmlDocAdHocQueryResponse;
            List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolders = null;
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

            //SubmissionSetDocumentFolder/Associations
            lstSubmissionSetDocumentFolders = GetSubmissionSetDocumentFolderDetails(objStoredQueryRequest.ParameterList);

            //Document Entry
            lstDocumentEntryIDs = base.GetDocumentEntryIDs(lstSubmissionSetDocumentFolders);
            documentEntryIDs = base.PrepareForSqlInStatement(lstDocumentEntryIDs);
            RegistryStoredQueryDataAccess objRegistryStoredQueryDAL = new RegistryStoredQueryDataAccess();
            lstDocumentEntry = objRegistryStoredQueryDAL.GetDocumentEntriesByDocumentEntryIDs(documentEntryIDs);


            //Construct AdHocQueryResponse Element
            xmlDocAdHocQueryResponse = StoredQueryBase.ConstructAdHocQueryResponseElement(GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS);

            xmlDocResponse = ConstructRegistryObjectList(xmlDocAdHocQueryResponse, lstDocumentEntry, lstSubmissionSetDocumentFolders);

            return xmlDocResponse;  
        }

        private XmlDocument ConstructRegistryObjectList(XmlDocument xmlDocAdHocQueryResponse, List<DocumentEntry> lstDocumentEntry, List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolder)
        {
            XmlElement xmlRoot = xmlDocAdHocQueryResponse.DocumentElement;

            //RegistryObjectList
            XmlElement eltRegistryObjectList = xmlDocAdHocQueryResponse.CreateElement("rim", "RegistryObjectList", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);
            xmlRoot.AppendChild(eltRegistryObjectList);

            //Document Entries
            if (lstDocumentEntry != null)
            {
                for (int count = 0; count < lstDocumentEntry.Count; count++)
                {
                    eltRegistryObjectList.InnerXml += lstDocumentEntry[count].ExtrinsicObjectXML;
                }
            }

            //Related Associations
            if (lstSubmissionSetDocumentFolder != null)
            {
                for (int count = 0; count < lstSubmissionSetDocumentFolder.Count; count++)
                {
                    eltRegistryObjectList.InnerXml += lstSubmissionSetDocumentFolder[count].AssociationXml;
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
            string associationTypes = null;

            for (int count = 0; count < lstStoredQueryParameter.Count; count++)
            {
                //Prepare SQL IN Param Value
                paramValue = PrepareForSqlInStatement(lstStoredQueryParameter[count].ParameterValue);

                if (lstStoredQueryParameter[count].ParameterName == "$XDSDocumentEntryEntryUUID")
                {
                    entryUUID = paramValue;
                }
                else if(lstStoredQueryParameter[count].ParameterName == "$XDSDocumentEntryUniqueId")
                {
                    uniqueID = paramValue;
                }
                else if (lstStoredQueryParameter[count].ParameterName == "$AssociationTypes")
                {
                    associationTypes = PrepareForSqlInStatement(paramValue);
                }
            }

            objStoredQueryDAL = new RegistryStoredQueryDataAccess();
            lstSubmissionSetDocumentFolder = objStoredQueryDAL.GetSubmissionSetDocumentFolderByEntryUUIDorUniqueID(entryUUID, uniqueID, associationTypes);

            return lstSubmissionSetDocumentFolder;
        }


    }
}
