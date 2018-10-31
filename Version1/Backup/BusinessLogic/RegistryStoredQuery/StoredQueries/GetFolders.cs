using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;

namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class GetFolders : StoredQueryBase
    {

        public override XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest)
        {
            XmlDocument xmlDocResponse = null;
            XmlDocument xmlDocAdHocQueryResponse;
            List<Folder> lstFolders = null;
            //string sSqlCode = string.Empty;
            //RegistryStoredQueryDataAccess objDAL = null;

            //Special Validations
            if (IsEmptyParametersExists(objStoredQueryRequest.ParameterList))
            {
                xmlDocResponse = StoredQueryBase.ConstructStoredQueryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "No parameters passed.", GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                return xmlDocResponse;
            }

            if (IsBothParametersPassed(objStoredQueryRequest.ParameterList))
            {
                xmlDocResponse = StoredQueryBase.ConstructStoredQueryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Both entryUUID and uniqueUUID parameters are passed. Only one parameter is allowed.", GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                return xmlDocResponse;
            }

            //Construct SQL Query
            //sSqlCode = base.ConstructDynamicSQLQuery(objStoredQueryRequest);

            //Get Folders
            lstFolders = GetFoldersByEntryUUIDOrUniqueID(objStoredQueryRequest.ParameterList);

            //Construct AdHocQueryResponse Element
            xmlDocAdHocQueryResponse = StoredQueryBase.ConstructAdHocQueryResponseElement(GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS);

            if (objStoredQueryRequest.ReturnType.ToLower() == "leafclass")
            {                
                //objDAL = new RegistryStoredQueryDataAccess();
                //lstFolders = objDAL.GetFoldersLeafClass(sSqlCode);

                // Construct LeafClass - RegistryObjectList Element
                xmlDocResponse = ConstructLeafClassRegistryObjectList(xmlDocAdHocQueryResponse, lstFolders);

            }
            else
            {
                //Default ObjectRef

                //List<string> lstEntryUUIds = null;
                //Execute SQL & Get Document IDs       
                //objDAL = new RegistryStoredQueryDataAccess();
                //lstEntryUUIds = objDAL.GetFoldersObjectRef(sSqlCode);

                // Construct ObjectRef - RegistryObjectList Element
                xmlDocResponse = ConstructObjectRefRegistryObjectList(xmlDocAdHocQueryResponse, lstFolders);
            }


            return xmlDocResponse;
        }


        private XmlDocument ConstructObjectRefRegistryObjectList(XmlDocument xmlDocAdHocQueryResponse, List<Folder> lstFolder)
        {
            XmlElement xmlRoot = xmlDocAdHocQueryResponse.DocumentElement;

            //Attribute - id
            XmlAttribute attribID = null;

            //ObjectRef
            XmlElement eltObjectRef = null;

            //RegistryObjectList
            XmlElement eltRegistryObjectList = xmlDocAdHocQueryResponse.CreateElement("rim", "RegistryObjectList", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);
            xmlRoot.AppendChild(eltRegistryObjectList);

            if (lstFolder != null)
            {
                for (int iCount = 0; iCount < lstFolder.Count; iCount++)
                {
                    //ObjectRef
                    eltObjectRef = xmlDocAdHocQueryResponse.CreateElement("rim", "ObjectRef", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);

                    attribID = xmlDocAdHocQueryResponse.CreateAttribute("id");
                    attribID.Value = lstFolder[iCount].EntryUUID;
                    eltObjectRef.Attributes.Append(attribID);

                    eltRegistryObjectList.AppendChild(eltObjectRef);
                }
            }


            return xmlDocAdHocQueryResponse;
        }


        private XmlDocument ConstructLeafClassRegistryObjectList(XmlDocument xmlDocAdHocQueryResponse, List<Folder> lstFolders)
        {
            XmlElement xmlRoot = xmlDocAdHocQueryResponse.DocumentElement;

            StringBuilder sbObjectRefXml = new StringBuilder();

            //Attribute - id
            XmlAttribute attribID = null;

            //ObjectRef
            XmlElement eltObjectRef = null;

            //RegistryObjectList
            XmlElement eltRegistryObjectList = xmlDocAdHocQueryResponse.CreateElement("rim", "RegistryObjectList", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);
            xmlRoot.AppendChild(eltRegistryObjectList);

            if (lstFolders != null)
            {
                for (int iCount = 0; iCount < lstFolders.Count; iCount++)
                {
                    //ExtrinsicObject
                    eltRegistryObjectList.InnerXml += lstFolders[iCount].FolderXml;

                    //ObjectRef
                    eltObjectRef = xmlDocAdHocQueryResponse.CreateElement("rim", "ObjectRef", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);

                    //id
                    attribID = xmlDocAdHocQueryResponse.CreateAttribute("id");
                    attribID.Value = lstFolders[iCount].EntryUUID;
                    eltObjectRef.Attributes.Append(attribID);
                    sbObjectRefXml.Append(eltObjectRef.OuterXml);

                    //sObjectRefXml += eltObjectRef.OuterXml;
                    //eltRegistryObjectList.AppendChild(eltObjectRef);
                }

                eltRegistryObjectList.InnerXml += sbObjectRefXml.ToString();
            }


            return xmlDocAdHocQueryResponse;
        }


    }
}
