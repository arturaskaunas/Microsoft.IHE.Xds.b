using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;

namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class GetDocuments : StoredQueryBase
    {

        public override System.Xml.XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest)
        {
            XmlDocument xmlDocResponse = null;            
            XmlDocument xmlDocAdHocQueryResponse;
            List<DocumentEntry> lstDocumentEntries = null;

            //Special Validations
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

            //Get documents
            lstDocumentEntries = GetDocumentsByEntryUUIDOrUniqueID(objStoredQueryRequest.ParameterList);

            //Construct AdHocQueryResponse Element
            xmlDocAdHocQueryResponse = StoredQueryBase.ConstructAdHocQueryResponseElement(GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS);

            if (objStoredQueryRequest.ReturnType.ToLower() == "leafclass")
            {
                // Construct LeafClass - RegistryObjectList Element
                xmlDocResponse = ConstructLeafClassRegistryObjectList(xmlDocAdHocQueryResponse, lstDocumentEntries);
            }
            else
            {
                //Default ObjectRef

                // Construct ObjectRef - RegistryObjectList Element
                xmlDocResponse = ConstructObjectRefRegistryObjectList(xmlDocAdHocQueryResponse, lstDocumentEntries);
            }


            return xmlDocResponse;
        }

        private XmlDocument ConstructObjectRefRegistryObjectList(XmlDocument xmlDocAdHocQueryResponse, List<DocumentEntry> lstDocumentEntries)
        {
            XmlElement xmlRoot = xmlDocAdHocQueryResponse.DocumentElement;

            //Attribute - id
            XmlAttribute attribID = null;

            //ObjectRef
            XmlElement eltObjectRef = null;

            //RegistryObjectList
            XmlElement eltRegistryObjectList = xmlDocAdHocQueryResponse.CreateElement("rim", "RegistryObjectList", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);
            xmlRoot.AppendChild(eltRegistryObjectList);

            if (lstDocumentEntries != null)
            {
                for (int iCount = 0; iCount < lstDocumentEntries.Count; iCount++)
                {
                    //ObjectRef
                    eltObjectRef = xmlDocAdHocQueryResponse.CreateElement("rim", "ObjectRef", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);

                    attribID = xmlDocAdHocQueryResponse.CreateAttribute("id");
                    attribID.Value = lstDocumentEntries[iCount].EntryUUID;
                    eltObjectRef.Attributes.Append(attribID);

                    eltRegistryObjectList.AppendChild(eltObjectRef);
                }
            }


            return xmlDocAdHocQueryResponse;
        }


        private XmlDocument ConstructLeafClassRegistryObjectList(XmlDocument xmlDocAdHocQueryResponse, List<DocumentEntry> lstDocumentEntries)
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

            if (lstDocumentEntries != null)
            {
                for (int iCount = 0; iCount < lstDocumentEntries.Count; iCount++)
                {
                    //ExtrinsicObject
                    eltRegistryObjectList.InnerXml += lstDocumentEntries[iCount].ExtrinsicObjectXML;

                    //ObjectRef
                    eltObjectRef = xmlDocAdHocQueryResponse.CreateElement("rim", "ObjectRef", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);

                    //id
                    attribID = xmlDocAdHocQueryResponse.CreateAttribute("id");
                    attribID.Value = lstDocumentEntries[iCount].EntryUUID;
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
