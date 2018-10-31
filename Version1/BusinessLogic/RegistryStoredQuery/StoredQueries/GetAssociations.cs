using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;

namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class GetAssociations : StoredQueryBase
    {

        public override System.Xml.XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest)
        {
            XmlDocument xmlDocResponse = null;
            XmlDocument xmlDocAdHocQueryResponse;
            List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolder = null;

            //Get Associations
            lstSubmissionSetDocumentFolder = GetAssociationsBySourceObjectOrTargetObject(objStoredQueryRequest.ParameterList);

            //Construct AdHocQueryResponse Element
            xmlDocAdHocQueryResponse = StoredQueryBase.ConstructAdHocQueryResponseElement(GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS);

            xmlDocResponse = ConstructRegistryObjectList(xmlDocAdHocQueryResponse, lstSubmissionSetDocumentFolder);

            return xmlDocResponse;
        }

        protected List<SubmissionSetDocumentFolder> GetAssociationsBySourceObjectOrTargetObject(List<StoredQueryParameter> lstStoredQueryParams)
        {
            List<SubmissionSetDocumentFolder> lstAssociations = null;
            RegistryStoredQueryDataAccess objRegistryStoredQueryDAL = null;
            string uuids = null;

            for (int count = 0; count < lstStoredQueryParams.Count; count++)
            {
                if (lstStoredQueryParams[count].ParameterName == "$uuid")
                {
                    uuids = PrepareForSqlInStatement(lstStoredQueryParams[count].ParameterValue);
                }
            }

            objRegistryStoredQueryDAL = new RegistryStoredQueryDataAccess();
            lstAssociations = objRegistryStoredQueryDAL.GetAssociations(uuids);
            objRegistryStoredQueryDAL = null;

            return lstAssociations;
        }


        private XmlDocument ConstructRegistryObjectList(XmlDocument xmlDocAdHocQueryResponse, List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolder)
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

            if (lstSubmissionSetDocumentFolder != null)
            {
                for (int iCount = 0; iCount < lstSubmissionSetDocumentFolder.Count; iCount++)
                {
                    //ExtrinsicObject
                    eltRegistryObjectList.InnerXml += lstSubmissionSetDocumentFolder[iCount].AssociationXml;

                    //ObjectRef
                    eltObjectRef = xmlDocAdHocQueryResponse.CreateElement("rim", "ObjectRef", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);

                    //id
                    attribID = xmlDocAdHocQueryResponse.CreateAttribute("id");
                    attribID.Value = lstSubmissionSetDocumentFolder[iCount].SourceObject;
                    eltObjectRef.Attributes.Append(attribID);
                    sbObjectRefXml.Append(eltObjectRef.OuterXml);

                }

                eltRegistryObjectList.InnerXml += sbObjectRefXml.ToString();
            }


            return xmlDocAdHocQueryResponse;
        }


    }
}
