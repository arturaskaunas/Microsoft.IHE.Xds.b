
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;

namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class GetDocumentsAndAssociations : StoredQueryBase
    {

        public override System.Xml.XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest)
        {            
            XmlDocument xmlDocResponse = null;
            string sSqlCode = string.Empty;
            XmlDocument xmlDocAdHocQueryResponse;
            List<DocumentEntry> lstDocumentEntries = null;
            List<Association> lstAssociations = null;

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

            //Construct AdHocQueryResponse Element
            xmlDocAdHocQueryResponse = StoredQueryBase.ConstructAdHocQueryResponseElement(GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS);

            lstDocumentEntries = GetDocuments(objStoredQueryRequest.ParameterList);
            lstAssociations = GetAssociations(objStoredQueryRequest.ParameterList);

            xmlDocResponse = ConstructRegistryObjectList(xmlDocAdHocQueryResponse, lstDocumentEntries, lstAssociations);

            return xmlDocResponse;
        }

        private XmlDocument ConstructRegistryObjectList(XmlDocument xmlDocAdHocQueryResponse, List<DocumentEntry> lstDocumentEntries, List<Association> lstAssociations)
        {
            XmlElement xmlRoot = xmlDocAdHocQueryResponse.DocumentElement;

            StringBuilder sbObjectRefXml = new StringBuilder();

            //Attribute - id
            //XmlAttribute attribID = null;

            //ObjectRef
            //XmlElement eltObjectRef = null;

            //RegistryObjectList
            XmlElement eltRegistryObjectList = xmlDocAdHocQueryResponse.CreateElement("rim", "RegistryObjectList", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);
            xmlRoot.AppendChild(eltRegistryObjectList);

            if (lstDocumentEntries != null)
            {
                for (int iCount = 0; iCount < lstDocumentEntries.Count; iCount++)
                {
                    //ExtrinsicObject
                    eltRegistryObjectList.InnerXml += lstDocumentEntries[iCount].ExtrinsicObjectXML;

                    ////ObjectRef
                    //eltObjectRef = xmlDocAdHocQueryResponse.CreateElement("rim", "ObjectRef", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);

                    ////id
                    //attribID = xmlDocAdHocQueryResponse.CreateAttribute("id");
                    //attribID.Value = lstDocumentEntries[iCount].SourceObject;
                    //eltObjectRef.Attributes.Append(attribID);
                    //sbObjectRefXml.Append(eltObjectRef.OuterXml);

                }

                //eltRegistryObjectList.InnerXml += sbObjectRefXml.ToString();
            }

            if (lstAssociations != null)
            {
                for (int iCount = 0; iCount < lstAssociations.Count; iCount++)
                {
                    //ExtrinsicObject
                    eltRegistryObjectList.InnerXml += lstAssociations[iCount].AssociationXml;

                    ////ObjectRef
                    //eltObjectRef = xmlDocAdHocQueryResponse.CreateElement("rim", "ObjectRef", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);

                    ////id
                    //attribID = xmlDocAdHocQueryResponse.CreateAttribute("id");
                    //attribID.Value = lstDocumentEntries[iCount].SourceObject;
                    //eltObjectRef.Attributes.Append(attribID);
                    //sbObjectRefXml.Append(eltObjectRef.OuterXml);

                }

                //eltRegistryObjectList.InnerXml += sbObjectRefXml.ToString();
            }

            return xmlDocAdHocQueryResponse;
        }


        private List<DocumentEntry> GetDocuments(List<StoredQueryParameter> lstStoredQueryParameter)
        {
            List<DocumentEntry> lstDocumentEntries = null;
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
            lstDocumentEntries = objStoredQueryDAL.GetDocuments(entryUUID, uniqueID);

            return lstDocumentEntries;
        }

        private List<Association> GetAssociations(List<StoredQueryParameter> lstStoredQueryParameter)
        {
            List<Association> lstAssociations = null;
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
            lstAssociations = objStoredQueryDAL.GetAssociations(entryUUID, uniqueID);

            return lstAssociations;
        }

    }
}
