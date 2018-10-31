using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;

namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class GetSubmissionSets : StoredQueryBase
    {

        public override System.Xml.XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest)
        {
            XmlDocument xmlDocResponse = null;
            string sSqlCode = string.Empty;
            XmlDocument xmlDocAdHocQueryResponse;
            List<GetSubmissionSetsRequest> lstGetSubmissionSetsRequest = null;
            List<GetSubmissionSetsRequest> lstSubmissionSetXml = null;
            List<GetSubmissionSetsRequest> lstAssociationXml = null;

            //Construct AdHocQueryResponse Element
            xmlDocAdHocQueryResponse = StoredQueryBase.ConstructAdHocQueryResponseElement(GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS);

            lstGetSubmissionSetsRequest = GetSubmissionSetsByTargetObjects(objStoredQueryRequest.ParameterList);

            lstSubmissionSetXml = RemoveDuplicateGetSubmissionSets(lstGetSubmissionSetsRequest);

            lstAssociationXml = RemoveDuplicateAssociations(lstGetSubmissionSetsRequest);

            xmlDocResponse = ConstructRegistryObjectList(xmlDocAdHocQueryResponse, lstSubmissionSetXml, lstAssociationXml);

            return xmlDocResponse;
        }

        private XmlDocument ConstructRegistryObjectList(XmlDocument xmlDocAdHocQueryResponse, List<GetSubmissionSetsRequest> lstSubmissionSetXml, List<GetSubmissionSetsRequest> lstAssociationXml)
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

            if (lstSubmissionSetXml != null)
            {
                for (int iCount = 0; iCount < lstSubmissionSetXml.Count; iCount++)
                {
                    //ExtrinsicObject
                    eltRegistryObjectList.InnerXml += lstSubmissionSetXml[iCount].SubmissionSetXml;

                    ////ObjectRef
                    //eltObjectRef = xmlDocAdHocQueryResponse.CreateElement("rim", "ObjectRef", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);

                    ////id
                    //attribID = xmlDocAdHocQueryResponse.CreateAttribute("id");
                    //attribID.Value = lstDocumentEntries[iCount].SourceObject;
                    //eltObjectRef.Attributes.Append(attribID);
                    //sbObjectRefXml.Append(eltObjectRef.OuterXml);

                }
            }


            if (lstAssociationXml != null)
            {
                for (int iCount = 0; iCount < lstAssociationXml.Count; iCount++)
                {
                    //ExtrinsicObject
                    eltRegistryObjectList.InnerXml += lstAssociationXml[iCount].AssociationXml;

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

        private List<GetSubmissionSetsRequest> GetSubmissionSetsByTargetObjects(List<StoredQueryParameter> lstStoredQueryParameter)
        {
            List<GetSubmissionSetsRequest> lstGetSubmissionSetsRequest = null;
            List<GetSubmissionSetsRequest> lstDistinctGetSubmissionSetsRequest = null;
            RegistryStoredQueryDataAccess objStoredQueryDAL = null;
            string entryUUIDs = null;

            lstDistinctGetSubmissionSetsRequest = new List<GetSubmissionSetsRequest>();

            if (lstStoredQueryParameter[0].ParameterName == "$uuid")
            {
                //Prepare SQL IN Param Value
                entryUUIDs = PrepareForSqlInStatement(lstStoredQueryParameter[0].ParameterValue);
            }


            objStoredQueryDAL = new RegistryStoredQueryDataAccess();
            lstGetSubmissionSetsRequest = objStoredQueryDAL.GetSubmissionSetsByTargetObjects(entryUUIDs);

            if(lstGetSubmissionSetsRequest == null)
                return lstGetSubmissionSetsRequest;
            
            return lstGetSubmissionSetsRequest;
        }


        private List<GetSubmissionSetsRequest> RemoveDuplicateGetSubmissionSets(List<GetSubmissionSetsRequest> lstGetSubmissionSetsRequest)
        {
            List<GetSubmissionSetsRequest> lstDistinctGetSubmissionSetsRequest = null;

            if (lstGetSubmissionSetsRequest == null)
                return lstGetSubmissionSetsRequest;

            lstDistinctGetSubmissionSetsRequest = new List<GetSubmissionSetsRequest>();

            for (int count = 0; count < lstGetSubmissionSetsRequest.Count; count++)
            {
                if (!lstDistinctGetSubmissionSetsRequest.Exists(
                    delegate(GetSubmissionSetsRequest getSubmissionSets)
                    {
                        if (getSubmissionSets.SubmissionSetID == lstGetSubmissionSetsRequest[count].SubmissionSetID)
                            return true;

                        return false;
                    }
                    ))
                {
                    lstDistinctGetSubmissionSetsRequest.Add(lstGetSubmissionSetsRequest[count]);
                }

            }


            return lstDistinctGetSubmissionSetsRequest;
        }


        private List<GetSubmissionSetsRequest> RemoveDuplicateAssociations(List<GetSubmissionSetsRequest> lstGetSubmissionSetsRequest)
        {
            List<GetSubmissionSetsRequest> lstDistinctGetSubmissionSetsRequest = null;

            if (lstGetSubmissionSetsRequest == null)
                return lstGetSubmissionSetsRequest;

            lstDistinctGetSubmissionSetsRequest = new List<GetSubmissionSetsRequest>();

            for (int count = 0; count < lstGetSubmissionSetsRequest.Count; count++)
            {
                if (!lstDistinctGetSubmissionSetsRequest.Exists(
                    delegate(GetSubmissionSetsRequest getSubmissionSets)
                    {
                        if (getSubmissionSets.SubmissionSetDocumentFolderID == lstGetSubmissionSetsRequest[count].SubmissionSetDocumentFolderID)
                            return true;

                        return false;
                    }
                    ))
                {
                    lstDistinctGetSubmissionSetsRequest.Add(lstGetSubmissionSetsRequest[count]);
                }

            }


            return lstDistinctGetSubmissionSetsRequest;
        }
    }
}
