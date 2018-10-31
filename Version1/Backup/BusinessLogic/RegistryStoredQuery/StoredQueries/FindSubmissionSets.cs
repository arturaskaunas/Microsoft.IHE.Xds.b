using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;

namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class FindSubmissionSets : StoredQueryBase   
    {

        public override XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest)
        {
            XmlDocument xmlDocResponse = null;
            List<SubmissionSet> lstSubmissionSet = null;
            string patientUID = null;
            string submissionSetStatus = null;
            XmlDocument xmlDocAdHocQueryResponse;
            RegistryStoredQueryDataAccess objDAL = new RegistryStoredQueryDataAccess();
            //string sSqlCode = string.Empty;
            //List<string> lstEntryUUIDs = null;


            //Construct SQL Query
            //sSqlCode = base.ConstructDynamicSQLQuery(objStoredQueryRequest);

            patientUID = base.RemoveSingleQuotes(GetParameterValue(objStoredQueryRequest.ParameterList, "$XDSSubmissionSetPatientId"));
            submissionSetStatus = base.PrepareForSqlInStatement(GetParameterValue(objStoredQueryRequest.ParameterList, "$XDSSubmissionSetStatus"));

            //Get SubmissionSets
            lstSubmissionSet = objDAL.GetSubmissionSets(submissionSetStatus, patientUID);

            //Get & Update SubmissionSet Author
            if (lstSubmissionSet != null && lstSubmissionSet.Count > 0)
            {
                lstSubmissionSet = UpdateSubmissionSetAuthor(lstSubmissionSet);

                //Filter based on SubmissionSet Author
                lstSubmissionSet = FilterByAuthor(lstSubmissionSet, objStoredQueryRequest.ParameterList);

                //Filter by SourceId
                lstSubmissionSet = FilterBySourceId(lstSubmissionSet, objStoredQueryRequest.ParameterList);

                //FilterByContentTypeCode
                lstSubmissionSet = FilterByContentTypeCode(lstSubmissionSet, objStoredQueryRequest.ParameterList);

                //FilterByContentSubmissionTime
                lstSubmissionSet = FilterByContentSubmissionTime(lstSubmissionSet, objStoredQueryRequest.ParameterList);

            }


            //Construct AdHocQueryResponse Element
            xmlDocAdHocQueryResponse = StoredQueryBase.ConstructAdHocQueryResponseElement(GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS);

            if (objStoredQueryRequest.ReturnType.ToLower() == "leafclass")
            {
                //List<SubmissionSet> lstSubmissionSet = null;

                //lstSubmissionSet = objDAL.FindSubmissionSetLeafClass(sSqlCode);

                // Construct LeafClass - RegistryObjectList Element
                xmlDocResponse = ConstructLeafClassSubmissionSet(xmlDocAdHocQueryResponse, lstSubmissionSet);

            }
            else
            {
                //Default ObjectRef

                //Execute SQL & Get Document IDs       
                //lstEntryUUIDs = objDAL.FindSubmissionSetObjectRef(sSqlCode);

                // Construct ObjectRef - RegistryObjectList Element
                xmlDocResponse = ConstructObjectRefSubmissionSet(xmlDocAdHocQueryResponse, lstSubmissionSet);
            }


            return xmlDocResponse;
        }

        private List<SubmissionSet> UpdateSubmissionSetAuthor(List<SubmissionSet> lstSubmissionSet)
        {
            RegistryStoredQueryDataAccess storedQueryDAL = null;

            if (lstSubmissionSet == null)
                return lstSubmissionSet;

            storedQueryDAL = new RegistryStoredQueryDataAccess();

            foreach (SubmissionSet submissionSet in lstSubmissionSet)
            {
                submissionSet.SubmissionAuthor = storedQueryDAL.GetSubmissionSetAuthorDetails(submissionSet.ID);
            }


            return lstSubmissionSet;
        }

        private List<SubmissionSet> FilterByAuthor(List<SubmissionSet> lstSubmissionSet, List<StoredQueryParameter> lstParameter)
        {
            List<SubmissionSet> lstSubmissionSetFiltered = new List<SubmissionSet>();
            List<string> lstAuthorPerson = null;
            string authorPersonValue = null;

            if (lstSubmissionSet == null)
                return lstSubmissionSet;

            authorPersonValue = GetParameterValue(lstParameter, "$XDSSubmissionSetAuthorPerson");

            if (authorPersonValue == null)
                return lstSubmissionSet;

            lstAuthorPerson = GetSqlInStatementValues(authorPersonValue);
            
            foreach (SubmissionSet submissionSet in lstSubmissionSet)
            {
                foreach (Author author in submissionSet.SubmissionAuthor)
                {
                    if (ValueExists(author.Person, lstAuthorPerson))
                    {
                        lstSubmissionSetFiltered.Add(submissionSet);
                        break;
                    }
                }
            }

            return lstSubmissionSetFiltered;
        }


        private List<SubmissionSet> FilterBySourceId(List<SubmissionSet> lstSubmissionSet, List<StoredQueryParameter> lstParameter)
        {
            List<SubmissionSet> lstSubmissionSetFiltered = new List<SubmissionSet>();
            string sourceIdValue = null;
            List<string> lstSourceId = null;

            if (lstSubmissionSet == null)
                return lstSubmissionSet;

            sourceIdValue = GetParameterValue(lstParameter, "$XDSSubmissionSetSourceId");

            if (sourceIdValue == null)
                return lstSubmissionSet;

            lstSourceId = GetSqlInStatementValues(sourceIdValue);

            foreach (SubmissionSet submissionSet in lstSubmissionSet)
            {
                if (submissionSet.SourceID == null)
                    continue;

                if (lstSourceId.Contains(submissionSet.SourceID))
                {
                    lstSubmissionSetFiltered.Add(submissionSet);
                }
            }

            return lstSubmissionSetFiltered;
        }


        private List<SubmissionSet> FilterByContentTypeCode(List<SubmissionSet> lstSubmissionSet, List<StoredQueryParameter> lstParameter)
        {
            List<SubmissionSet> lstSubmissionSetFiltered = new List<SubmissionSet>();
            string contentTypeCodeValue = null;
            List<string> lstContentTypeCode = null;

            if (lstSubmissionSet == null)
                return lstSubmissionSet;

            contentTypeCodeValue = GetParameterValue(lstParameter, "$XDSSubmissionSetContentType");

            if (contentTypeCodeValue == null)
                return lstSubmissionSet;

            lstContentTypeCode = GetSqlInStatementValues(contentTypeCodeValue);

            foreach (SubmissionSet submissionSet in lstSubmissionSet)
            {
                if (submissionSet.ContentType == null && submissionSet.ContentType.Value == null)
                    continue;

                if (lstContentTypeCode.Contains(submissionSet.ContentType.Value))
                {
                    lstSubmissionSetFiltered.Add(submissionSet);
                }
            }

            return lstSubmissionSetFiltered;
        }


        private List<SubmissionSet> FilterByContentSubmissionTime(List<SubmissionSet> lstSubmissionSet, List<StoredQueryParameter> lstParameter)
        {
            List<SubmissionSet> lstSubmissionSetFiltered = new List<SubmissionSet>();
            string submissionTimeFrom = null;
            string submissionTimeTo = null;
            DateTime dtSubmissionTimeFrom = DateTime.Today;
            DateTime dtSubmissionTimeTo = DateTime.Today;

            if (lstSubmissionSet == null)
                return lstSubmissionSet;

            submissionTimeFrom = GetParameterValue(lstParameter, "$XDSSubmissionSetSubmissionTimeFrom");
            submissionTimeTo = GetParameterValue(lstParameter, "$XDSSubmissionSetSubmissionTimeTo");

            if (submissionTimeFrom == null && submissionTimeTo == null)
                return lstSubmissionSet;

            if ((!DateTime.TryParse(submissionTimeFrom, out dtSubmissionTimeFrom))
                || (!DateTime.TryParse(submissionTimeTo, out dtSubmissionTimeTo)))
                return lstSubmissionSet;


            foreach (SubmissionSet submissionSet in lstSubmissionSet)
            {
                if (submissionSet.SubmissionTime >= dtSubmissionTimeFrom 
                    && submissionSet.SubmissionTime < dtSubmissionTimeTo)
                {
                    lstSubmissionSetFiltered.Add(submissionSet);
                }
            }

            return lstSubmissionSetFiltered;
        }


        private bool ValueExists(string authorValueXml, List<string> lstValue)
        {
            bool isExists = false;
            XmlDocument xmlDocValueList = null;
            XmlNodeList nodeListValue = null;
            string xpathValue = @".//*[local-name()='ValueList']/*[local-name()='Value']";

            if (authorValueXml == null || lstValue == null)
                return isExists;

            xmlDocValueList = new XmlDocument();
            xmlDocValueList.LoadXml(authorValueXml);
            nodeListValue = xmlDocValueList.SelectNodes(xpathValue);

            if (nodeListValue == null)
                return isExists;

                foreach (XmlNode node in nodeListValue)
                {
                    if(lstValue.Contains(node.InnerText))
                    {
                        isExists = true;
                        break;
                    }
                }

            return isExists;
        }

        private XmlDocument ConstructLeafClassSubmissionSet(XmlDocument xmlDocAdHocQueryResponse, List<SubmissionSet> lstSubmissionSet)
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

            if (lstSubmissionSet != null)
            {
                for (int iCount = 0; iCount < lstSubmissionSet.Count; iCount++)
                {
                    //ExtrinsicObject
                    eltRegistryObjectList.InnerXml += lstSubmissionSet[iCount].SubmissionSetXml;

                    //ObjectRef
                    eltObjectRef = xmlDocAdHocQueryResponse.CreateElement("rim", "ObjectRef", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);

                    //id
                    attribID = xmlDocAdHocQueryResponse.CreateAttribute("id");
                    attribID.Value = lstSubmissionSet[iCount].EntryUUID;
                    eltObjectRef.Attributes.Append(attribID);
                    sbObjectRefXml.Append(eltObjectRef.OuterXml);

                }

                eltRegistryObjectList.InnerXml += sbObjectRefXml.ToString();
            }


            return xmlDocAdHocQueryResponse;
        }

        private XmlDocument ConstructObjectRefSubmissionSet(XmlDocument xmlDocAdHocQueryResponse, List<SubmissionSet> lstSubmissionSet)
        {
            XmlElement xmlRoot = xmlDocAdHocQueryResponse.DocumentElement;

            //Attribute - id
            XmlAttribute attribID = null;

            //ObjectRef
            XmlElement eltObjectRef = null;

            //RegistryObjectList
            XmlElement eltRegistryObjectList = xmlDocAdHocQueryResponse.CreateElement("rim", "RegistryObjectList", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);
            xmlRoot.AppendChild(eltRegistryObjectList);

            if (lstSubmissionSet != null)
            {
                for (int iCount = 0; iCount < lstSubmissionSet.Count; iCount++)
                {
                    //ObjectRef
                    eltObjectRef = xmlDocAdHocQueryResponse.CreateElement("rim", "ObjectRef", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);

                    attribID = xmlDocAdHocQueryResponse.CreateAttribute("id");
                    attribID.Value = lstSubmissionSet[iCount].EntryUUID;
                    eltObjectRef.Attributes.Append(attribID);

                    eltRegistryObjectList.AppendChild(eltObjectRef);
                }
            }


            return xmlDocAdHocQueryResponse;
        }

    }
}
