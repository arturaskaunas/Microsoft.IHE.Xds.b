using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;


using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;


namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public abstract class StoredQueryBase
    {

        public static StoredQueryBase GetStoredQueryObject(string queryName)
        {
            StoredQueryBase objStoredQuery = null;
            string queryNameLower = queryName.ToLower();

            switch (queryNameLower)
            {
                case "finddocuments":
                    objStoredQuery = new FindDocuments();
                    break;

                case "findsubmissionsets":
                    objStoredQuery = new FindSubmissionSets();
                    break;

                case "findfolders":
                    objStoredQuery = new FindFolders();
                    break;

                case "getall":
                    objStoredQuery = new GetAll();
                    break;

                case "getdocuments":
                    objStoredQuery = new GetDocuments();
                    break;

                case "getfolders":
                    objStoredQuery = new GetFolders();
                    break;

                case "getassociations":
                    objStoredQuery = new GetAssociations();
                    break;

                case "getdocumentsandassociations":
                    objStoredQuery = new GetDocumentsAndAssociations();
                    break;

                case "getsubmissionsets":
                    objStoredQuery = new GetSubmissionSets();
                    break;

                case "getsubmissionsetandcontents":
                    objStoredQuery = new GetSubmissionSetAndContents();
                    break;

                case "getfolderandcontents":
                    objStoredQuery = new GetFolderAndContents();
                    break;

                case "getfoldersfordocument":
                    objStoredQuery = new GetFoldersForDocument();
                    break;

                case "getrelateddocuments":
                    objStoredQuery = new GetRelatedDocuments();
                    break;

            }

            return objStoredQuery;
        }

        public static StoredQuery ConstructStoredQuery(XmlDocument xmlDoc)
        {
            StoredQuery objStoredQuery = new StoredQuery();
            StoredQueryParameter objParameter = null;
            string returnComposedObjects = null;

            //ResponseOption
            XmlNode xmlNodeResponseOption = xmlDoc.SelectSingleNode(@"//*[local-name()='ResponseOption']");

            //returnComposedObjects
            if(xmlNodeResponseOption.Attributes["returnComposedObjects"] != null)
                returnComposedObjects = xmlNodeResponseOption.Attributes["returnComposedObjects"].Value;

            if ((!string.IsNullOrEmpty(returnComposedObjects)) && returnComposedObjects == "true")
            {
                objStoredQuery.ReturnComposedObjects = true;
            }

            //returnType
            objStoredQuery.ReturnType = xmlNodeResponseOption.Attributes["returnType"].Value;

            //AdhocQuery
            XmlNode xmlNodeAdhocQuery = xmlDoc.SelectSingleNode(@"//*[local-name()='AdhocQuery']");
            objStoredQuery.StoredQueryUniqueID = xmlNodeAdhocQuery.Attributes["id"].Value;

            //Slot
            XmlNodeList xmlNodeListSlot = xmlDoc.SelectNodes(@"//*[local-name()='Slot']");

            //Looping through the Slots
            for (int iSlotCount = 0; iSlotCount < xmlNodeListSlot.Count; iSlotCount++)
            {
                objParameter = new StoredQueryParameter();

                //name
                objParameter.ParameterName = xmlNodeListSlot[iSlotCount].Attributes["name"].Value;

                //ValueList
                if (xmlNodeListSlot[iSlotCount].HasChildNodes)
                {
                    foreach (XmlNode node in xmlNodeListSlot[iSlotCount].ChildNodes)
                    {
                        if ((node.NodeType == XmlNodeType.Element) && (node.LocalName == "ValueList"))
                        {
                            //Value
                            //Only 1 Value will exist at any given point in time
                            //Although the node is ValueList with multiple possible Value node
                            //But the request will contain ('some_value','some_value') for multiple values
                            if(node.ChildNodes.Count > 0)
                                objParameter.ParameterValue = node.ChildNodes[0].InnerText;
                        }
                    }

                }

                //Adding Parameter to the collection
                objStoredQuery.ParameterList.Add(objParameter);
            }


            return objStoredQuery;
        }


        public static StoredQuery UpdateParameterValues(StoredQuery objQueryRequest, StoredQuery objQueryDB)
        {
            string sParameterValue = null;

            objQueryRequest.StoredQueryID = objQueryDB.StoredQueryID;
            objQueryRequest.StoredQueryName = objQueryDB.StoredQueryName;
            objQueryRequest.StoredQuerySequence = objQueryDB.StoredQuerySequence;
            objQueryRequest.StoredQuerySQLCode = objQueryDB.StoredQuerySQLCode;
            objQueryRequest.StoredQueryUniqueID = objQueryDB.StoredQueryUniqueID;


            for (int requestCount = 0; requestCount < objQueryRequest.ParameterList.Count; requestCount++)
            {

                for (int dbQueryCount = 0; dbQueryCount < objQueryDB.ParameterList.Count; dbQueryCount++)
                {
                    if (objQueryRequest.ParameterList[requestCount].ParameterName == objQueryDB.ParameterList[dbQueryCount].ParameterName)
                    {
                        sParameterValue = objQueryRequest.ParameterList[requestCount].ParameterValue;

                        sParameterValue = ResolveDateTimeFormat(objQueryRequest.ParameterList[requestCount].ParameterName, sParameterValue);

                        objQueryRequest.ParameterList[requestCount] = objQueryDB.ParameterList[dbQueryCount];
                        objQueryRequest.ParameterList[requestCount].ParameterValue = sParameterValue;
                        break;
                    }
                }

            }

            return objQueryRequest;
        }

        private static string ResolveDateTimeFormat(string parameterName, string parameterValue)
        {
            string newParameterValue = null;
            List<string> lstParameterWithDateTime = null;

            if (string.IsNullOrEmpty(parameterName) || string.IsNullOrEmpty(parameterValue))
                return parameterValue;

            newParameterValue = parameterValue;

            lstParameterWithDateTime = new List<string>();
            lstParameterWithDateTime.Add("$XDSSubmissionSetSubmissionTimeFrom");
            lstParameterWithDateTime.Add("$XDSSubmissionSetSubmissionTimeTo");
            lstParameterWithDateTime.Add("$XDSDocumentEntryCreationTimeFrom");
            lstParameterWithDateTime.Add("$XDSDocumentEntryCreationTimeTo");
            lstParameterWithDateTime.Add("$XDSDocumentEntryServiceStartTimeFrom");
            lstParameterWithDateTime.Add("$XDSDocumentEntryServiceStartTimeTo");
            lstParameterWithDateTime.Add("$XDSDocumentEntryServiceStopTimeFrom");
            lstParameterWithDateTime.Add("$XDSDocumentEntryServiceStopTimeTo");
            lstParameterWithDateTime.Add("$XDSFolderLastUpdateTimeFrom");
            lstParameterWithDateTime.Add("$XDSFolderLastUpdateTimeTo");

            if (lstParameterWithDateTime.Contains(parameterName))
            {

                if (parameterValue.Length == 4)
                {
                    newParameterValue = parameterValue + "0101";
                }
                else if (parameterValue.Length == 6)
                {
                    newParameterValue = parameterValue + "01";
                }

            }
            


            return newParameterValue;
        }

        
        protected virtual string PrepareForSqlInStatement(string paramValue)
        {
            string[] arrValues;

            if (string.IsNullOrEmpty(paramValue))
                return paramValue;

            paramValue = paramValue.Trim();

            if (paramValue.StartsWith("("))
                paramValue = paramValue.Remove(0, 1);

            if (paramValue.EndsWith(")"))
                paramValue = paramValue.Remove(paramValue.Length - 1, 1);

            arrValues = paramValue.Split(',');


            for (int count = 0; count < arrValues.Length; count++)
            {
                arrValues[count] = arrValues[count].Trim();

                if(arrValues[count].StartsWith("'"))
                    arrValues[count] = arrValues[count].Remove(0, 1);

                if (arrValues[count].EndsWith("'"))
                    arrValues[count] = arrValues[count].Remove(arrValues[count].Length - 1, 1);
            }

            //string array values might repeat, remove repeating value to avoid multiple records
            arrValues = RemoveDuplicateValues(arrValues);

            paramValue = string.Join(",", arrValues);

            return paramValue;
        }

        protected virtual string PrepareForSqlInStatement(List<string> paramValues)
        {
            string paramValue;
            string[] arrValues = null;

            if (paramValues == null)
                return string.Empty;

            arrValues = paramValues.ToArray();

            paramValue = string.Join(",", arrValues);

            return paramValue;
        }

        protected string[] SplitSqlInStatement(string paramValue)
        {
            string[] arrValues = null;

            if (string.IsNullOrEmpty(paramValue))
                return arrValues;

            paramValue = PrepareForSqlInStatement(paramValue);

            arrValues = paramValue.Split(',');

            return arrValues;
        }

        protected List<string> GetSqlInStatementValues(string paramValue)
        {
            List<string> lstValue = null;
            string[] arrValues = null;

            if (string.IsNullOrEmpty(paramValue))
                return null;

            lstValue = new List<string>();
            arrValues = SplitSqlInStatement(paramValue);
            
            foreach (string val in arrValues)
            {
                lstValue.Add(val);
            }

            return lstValue;
        }

        protected string RemoveSingleQuotes(string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
                return paramValue;

            paramValue = paramValue.Trim();

            if (paramValue.StartsWith("'"))
                paramValue = paramValue.Remove(0, 1);

            if (paramValue.EndsWith("'"))
                paramValue = paramValue.Remove(paramValue.Length - 1, 1);

            return paramValue;
         }

        protected string[] RemoveDuplicateValues(string[] arrValues)
        {
            List<string> lstDistinctValues = new List<string>();            

            for (int count = 0; count < arrValues.Length; count++)
            {
                if (!lstDistinctValues.Contains(arrValues[count]))
                    lstDistinctValues.Add(arrValues[count]);
            }

            return lstDistinctValues.ToArray();
        }

        public static XmlDocument ConstructAdHocQueryResponseElement(string status)
        {
            //<tns:AdhocQueryResponse status="" xmlns:tns="urn:oasis:names:tc:ebxml-regrep:xsd:query:3.0" xmlns:rim="urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="urn:oasis:names:tc:ebxml-regrep:xsd:query:3.0">
            //</tns:AdhocQueryResponse>
            XmlElement eltAdhocQueryResponse;
            XmlAttribute attribAdhocQueryResponse;
            XmlDocument xmlDocAdhocQueryResponse = new XmlDocument();

            //AdhocQueryResponse
            eltAdhocQueryResponse = xmlDocAdhocQueryResponse.CreateElement("query", "AdhocQueryResponse", GlobalValues.CONST_XML_NAMESPACE_QUERY);

            //status
            attribAdhocQueryResponse = xmlDocAdhocQueryResponse.CreateAttribute("status");
            attribAdhocQueryResponse.Value = status;
            eltAdhocQueryResponse.Attributes.Append(attribAdhocQueryResponse);

            //xmlns:tns="urn:oasis:names:tc:ebxml-regrep:xsd:query:3.0"
            attribAdhocQueryResponse = xmlDocAdhocQueryResponse.CreateAttribute("xmlns:query");
            attribAdhocQueryResponse.Value = GlobalValues.CONST_XML_NAMESPACE_QUERY;
            eltAdhocQueryResponse.Attributes.Append(attribAdhocQueryResponse);

            //xmlns:rim="urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0"
            attribAdhocQueryResponse = xmlDocAdhocQueryResponse.CreateAttribute("xmlns:rim");
            attribAdhocQueryResponse.Value = GlobalValues.CONST_XML_NAMESPACE_QUERY_rim;
            eltAdhocQueryResponse.Attributes.Append(attribAdhocQueryResponse);

            //xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
            //attribAdhocQueryResponse = xmlDocAdhocQueryResponse.CreateAttribute("xmlns:xsi");
            //attribAdhocQueryResponse.Value = GlobalValues.CONST_XML_NAMESPACE_QUERY_xsi;
            //eltAdhocQueryResponse.Attributes.Append(attribAdhocQueryResponse);

            ////xsi:schemaLocation="urn:oasis:names:tc:ebxml-regrep:xsd:query:3.0"
            //attribAdhocQueryResponse = xmlDocAdhocQueryResponse.CreateAttribute("xsi:schemaLocation");
            //attribAdhocQueryResponse.Value = GlobalValues.CONST_XML_NAMESPACE_QUERY_xsi_schemaLocation;
            //eltAdhocQueryResponse.Attributes.Append(attribAdhocQueryResponse);

            //Append Root Element
            xmlDocAdhocQueryResponse.AppendChild(eltAdhocQueryResponse);


            return xmlDocAdhocQueryResponse;
        }

        public static XmlDocument ConstructStoredQueryErrorResponse(string status, string requestId, string codeContext, string errorCode, string severity, string location)
        {
            XmlDocument xmlDocAdHocQueryResponse = null;
            XmlElement eltRoot = null;
            XmlElement eltRegistryObjectList = null;
            XmlElement eltRegistryErrorList = null;
            XmlElement eltRegistryError = null;
            XmlAttribute attrib = null;

            xmlDocAdHocQueryResponse = ConstructAdHocQueryResponseElement(status);
            eltRoot = xmlDocAdHocQueryResponse.DocumentElement;

            //RegistryErrorList
            eltRegistryErrorList = xmlDocAdHocQueryResponse.CreateElement("tns:RegistryErrorList", "urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");
            eltRoot.AppendChild(eltRegistryErrorList);

            //highestSeverity
            attrib = xmlDocAdHocQueryResponse.CreateAttribute("highestSeverity");
            attrib.Value = string.Empty;
            eltRegistryErrorList.Attributes.Append(attrib);


            //RegistryError
            eltRegistryError = xmlDocAdHocQueryResponse.CreateElement("tns:RegistryError", "urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");
            
            //Append RegistryError
            eltRegistryErrorList.AppendChild(eltRegistryError);

            //codeContext
            attrib = xmlDocAdHocQueryResponse.CreateAttribute("codeContext");
            attrib.Value = codeContext;
            eltRegistryError.Attributes.Append(attrib);

            //errorCode
            attrib = xmlDocAdHocQueryResponse.CreateAttribute("errorCode");
            attrib.Value = errorCode;
            eltRegistryError.Attributes.Append(attrib);

            //severity
            attrib = xmlDocAdHocQueryResponse.CreateAttribute("severity");
            attrib.Value = severity;
            eltRegistryError.Attributes.Append(attrib);

            if (!string.IsNullOrEmpty(location))
            {
                //location
                attrib = xmlDocAdHocQueryResponse.CreateAttribute("location");
                attrib.Value = location;
                eltRegistryError.Attributes.Append(attrib);
            }

            //RegistryObjectList
            eltRegistryObjectList = xmlDocAdHocQueryResponse.CreateElement("rim:RegistryObjectList", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);
            eltRoot.AppendChild(eltRegistryObjectList);

            return xmlDocAdHocQueryResponse;
        }

        protected bool IsEmptyParametersExists(List<StoredQueryParameter> lstStoredQueryParameter)
        {
            bool bResult = false;

            if ((lstStoredQueryParameter == null) || (lstStoredQueryParameter.Count == 0))
                bResult = true;

            return bResult;
        }

        protected bool IsBothParametersPassed(List<StoredQueryParameter> lstStoredQueryParameter)
        {
            //Note: Only 1 parameter is allowed at any given point in time
            //Either entryUUID or uniqueID

            bool bResult = false;

            if ((lstStoredQueryParameter != null) && (lstStoredQueryParameter.Count > 1))
                bResult = true;

            return bResult;
        }

        protected List<string> GetDocumentEntryIDs(List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolder)
        {
            List<string> lstDocumentEntryIDs = new List<string>();

            if (lstSubmissionSetDocumentFolder == null)
                return lstDocumentEntryIDs;

            for (int count = 0; count < lstSubmissionSetDocumentFolder.Count; count++)
            {
                if (lstSubmissionSetDocumentFolder[count].DocumentEntryID > 0)
                    lstDocumentEntryIDs.Add(lstSubmissionSetDocumentFolder[count].DocumentEntryID.ToString());
            }

            return lstDocumentEntryIDs;
        }

        protected List<string> GetFolderIDs(List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolder)
        {
            List<string> lstFolderIDs = new List<string>();

            for (int count = 0; count < lstSubmissionSetDocumentFolder.Count; count++)
            {
                if (lstSubmissionSetDocumentFolder[count].FolderID > 0)
                    lstFolderIDs.Add(lstSubmissionSetDocumentFolder[count].FolderID.ToString());
            }

            return lstFolderIDs;
        }

        protected List<string> GetFolderIDs(List<Folder> lstFolder)
        {
            List<string> lstFolderIDs = new List<string>();

            for (int count = 0; count < lstFolder.Count; count++)
            {
                if (lstFolder[count].FolderID > 0)
                    lstFolderIDs.Add(lstFolder[count].FolderID.ToString());
            }

            return lstFolderIDs;
        }

        protected List<string> GetFolderIDs(List<FolderCodeList> lstFolderCodeList)
        {
            List<string> lstFolderIDs = new List<string>();

            for (int count = 0; count < lstFolderCodeList.Count; count++)
            {
                if (lstFolderCodeList[count].FolderID > 0)
                    lstFolderIDs.Add(lstFolderCodeList[count].FolderID.ToString());
            }

            return lstFolderIDs;
        }

        protected List<DocumentEntry> GetDocumentEntries(string patientUID, List<string> lstAvailabilityStatus)
        {
            List<DocumentEntry> lstDocumentEntry = null;
            RegistryStoredQueryDataAccess storedQueryDAL = null;
            string availabilityStatus = null;

            storedQueryDAL = new RegistryStoredQueryDataAccess();

            availabilityStatus = PrepareForSqlInStatement(lstAvailabilityStatus);
            lstDocumentEntry = storedQueryDAL.GetDocumentEntries(availabilityStatus, patientUID);

            if ((lstDocumentEntry == null) || (lstDocumentEntry.Count == 0))
                return lstDocumentEntry;

            foreach (DocumentEntry documentEntry in lstDocumentEntry)
            {
                documentEntry.EventCodeList = storedQueryDAL.GetDocumentEntryEventCodeList(documentEntry.ID);
            }

            return lstDocumentEntry;
        }

        protected List<DocumentEntry> GetDocumentEntriesByIds(string documentEntryIDs, StoredQuery objStoredQueryRequest)
        {
            List<DocumentEntry> lstFilteredDocumentEntryObjects = new List<DocumentEntry>();
            List<DocumentEntry> lstDocumentEntries = null;
            List<string> lstEntryUUIDs = new List<string>();
            DocumentEntry objDocumentEntry = null;
            string[] arrConfidentialityCodes = null;
            string[] arrFormatCodes = null;

            RegistryStoredQueryDataAccess objStoredQueryDAL = new RegistryStoredQueryDataAccess();

            lstDocumentEntries = objStoredQueryDAL.GetDocumentEntriesByDocumentEntryIDs(documentEntryIDs);

            lstDocumentEntries = RemoveDuplicateDocumentEntry(lstDocumentEntries);

            arrConfidentialityCodes = GetSQLINParameterValues(objStoredQueryRequest, "$XDSDocumentEntryConfidentialityCode");
            arrFormatCodes = GetSQLINParameterValues(objStoredQueryRequest, "$XDSDocumentEntryFormatCode");

            //No filter condition specified - just return all the document entries
            if ((arrConfidentialityCodes == null) && (arrFormatCodes == null))
            {
                return lstDocumentEntries;
            }

            //Add Matching Confidentiality Codes 
            if (arrConfidentialityCodes != null)
            {
                for (int count = 0; count < arrConfidentialityCodes.Length; count++)
                {
                    objDocumentEntry = lstDocumentEntries.Find(
                        delegate(DocumentEntry documentEntry)
                        {
                            if (documentEntry.ConfidentialityCode == null)
                                return false;

                            if (documentEntry.ConfidentialityCode.Value == arrConfidentialityCodes[count])
                                return true;

                            return false;
                        }
                    );

                    if (objDocumentEntry != null)
                        lstEntryUUIDs.Add(objDocumentEntry.EntryUUID);
                }
            }

            //Add Matching Format Codes
            if (arrFormatCodes != null)
            {
                for (int count = 0; count < arrFormatCodes.Length; count++)
                {
                    objDocumentEntry = lstDocumentEntries.Find(
                        delegate(DocumentEntry documentEntry)
                        {
                            if (documentEntry.FormatCode == null)
                                return false;

                            if (documentEntry.FormatCode.Value == arrFormatCodes[count])
                                return true;

                            return false;
                        }
                    );

                    if ((objDocumentEntry != null) && (!lstEntryUUIDs.Contains(objDocumentEntry.EntryUUID)))
                        lstEntryUUIDs.Add(objDocumentEntry.EntryUUID);
                }
            }

            //Merge Filtered DocumentEntry Objects
            for (int count = 0; count < lstEntryUUIDs.Count; count++)
            {
                objDocumentEntry = lstDocumentEntries.Find(
                    delegate(DocumentEntry documentEntry)
                    {

                        if (documentEntry.EntryUUID == lstEntryUUIDs[count])
                            return true;

                        return false;
                    }
                );

                if (objDocumentEntry != null)
                    lstFilteredDocumentEntryObjects.Add(objDocumentEntry);
            }

            return lstFilteredDocumentEntryObjects;
        }

        protected string[] GetSQLINParameterValues(StoredQuery objStoredQueryRequest, string parameterName)
        {
            string[] arrValues = null;
            string paramValue = null;

            for (int count = 0; count < objStoredQueryRequest.ParameterList.Count; count++)
            {
                if (objStoredQueryRequest.ParameterList[count].ParameterName == parameterName)
                {
                    paramValue = objStoredQueryRequest.ParameterList[count].ParameterValue;
                    break;
                }
            }

            arrValues = SplitSqlInStatement(paramValue);

            return arrValues;
        }

        protected virtual string GetParameterValue(List<StoredQueryParameter> lstStoredQueryParameter, string parameterName)
        {
            string paramValue = null;
            StoredQueryParameter objQueryParameter = null;

            if (lstStoredQueryParameter != null)
            {

                objQueryParameter = lstStoredQueryParameter.Find(
                    delegate(StoredQueryParameter objStoredQueryParameter)
                    {

                        if (objStoredQueryParameter.ParameterName == parameterName)
                            return true;

                        return false;
                    }
                );

                if (objQueryParameter != null)
                    paramValue = objQueryParameter.ParameterValue;
            }

            return paramValue;
        }

        protected List<SubmissionSet> RemoveDuplicateSubmissionSet(List<SubmissionSet> lstSubmissionSets)
        {
            List<SubmissionSet> lstDistinctSubmissionSets = null;

            if (lstSubmissionSets == null)
                return lstSubmissionSets;

            lstDistinctSubmissionSets = new List<SubmissionSet>();

            for (int count = 0; count < lstSubmissionSets.Count; count++)
            {
                if (!lstDistinctSubmissionSets.Exists(
                    delegate(SubmissionSet submissionSet)
                    {
                        if (submissionSet.ID == lstSubmissionSets[count].ID)
                            return true;

                        return false;
                    }
                    ))
                {
                    lstDistinctSubmissionSets.Add(lstSubmissionSets[count]);
                }

            }


            return lstDistinctSubmissionSets;
        }

        protected List<DocumentEntry> RemoveDuplicateDocumentEntry(List<DocumentEntry> lstDocumentEntry)
        {
            List<DocumentEntry> lstDistinctDocumentEntry = null;

            if (lstDocumentEntry == null)
                return lstDocumentEntry;

            lstDistinctDocumentEntry = new List<DocumentEntry>();

            for (int count = 0; count < lstDocumentEntry.Count; count++)
            {
                if (!lstDistinctDocumentEntry.Exists(
                    delegate(DocumentEntry documentEntry)
                    {
                        if (documentEntry.ID == lstDocumentEntry[count].ID)
                            return true;

                        return false;
                    }
                    ))
                {
                    lstDistinctDocumentEntry.Add(lstDocumentEntry[count]);
                }

            }


            return lstDistinctDocumentEntry;
        }

        protected List<SubmissionSetDocumentFolder> RemoveDuplicateSubmissionSetDocumentFolder(List<SubmissionSetDocumentFolder> lstSubmissionSetDocumentFolder)
        {
            List<SubmissionSetDocumentFolder> lstDistinctSubmissionSetDocumentFolder = null;

            if (lstSubmissionSetDocumentFolder == null)
                return lstSubmissionSetDocumentFolder;

            lstDistinctSubmissionSetDocumentFolder = new List<SubmissionSetDocumentFolder>();

            for (int count = 0; count < lstSubmissionSetDocumentFolder.Count; count++)
            {
                if (!lstDistinctSubmissionSetDocumentFolder.Exists(
                    delegate(SubmissionSetDocumentFolder submissionSetDocFolder)
                    {
                        if (submissionSetDocFolder.SubmissionSetDocumentFolderID == lstSubmissionSetDocumentFolder[count].SubmissionSetDocumentFolderID)
                            return true;

                        return false;
                    }
                    ))
                {
                    lstDistinctSubmissionSetDocumentFolder.Add(lstSubmissionSetDocumentFolder[count]);
                }

            }


            return lstDistinctSubmissionSetDocumentFolder;
        }

        protected List<Folder> GetFoldersByEntryUUIDOrUniqueID(List<StoredQueryParameter> lstStoredQueryParams)
        {
            List<Folder> lstFolders = null;
            RegistryStoredQueryDataAccess objRegistryStoredQueryDAL = null;
            string entryUUID = null;
            string uniqueID = null;

            for (int count = 0; count < lstStoredQueryParams.Count; count++)
            {
                if (lstStoredQueryParams[count].ParameterName == "$XDSFolderEntryUUID")
                {
                    entryUUID = PrepareForSqlInStatement(lstStoredQueryParams[count].ParameterValue);
                }
                else if (lstStoredQueryParams[count].ParameterName == "$XDSFolderUniqueId")
                {
                    uniqueID = PrepareForSqlInStatement(lstStoredQueryParams[count].ParameterValue);
                }
            }

            objRegistryStoredQueryDAL = new RegistryStoredQueryDataAccess();
            lstFolders = objRegistryStoredQueryDAL.GetFoldersByEntryUUIDOrUniqueID(entryUUID, uniqueID);
            objRegistryStoredQueryDAL = null;

            return lstFolders;
        }

        protected List<DocumentEntry> GetDocumentsByEntryUUIDOrUniqueID(List<StoredQueryParameter> lstStoredQueryParams)
        {
            List<DocumentEntry> lstDocumentEntry = null;
            RegistryStoredQueryDataAccess objRegistryStoredQueryDAL = null;
            string entryUUIDs = null;
            string uniqueIDs = null;

            for (int count = 0; count < lstStoredQueryParams.Count; count++)
            {
                if (lstStoredQueryParams[count].ParameterName == "$XDSDocumentEntryEntryUUID")
                {
                    entryUUIDs = PrepareForSqlInStatement(lstStoredQueryParams[count].ParameterValue);
                }
                else if (lstStoredQueryParams[count].ParameterName == "$XDSDocumentEntryUniqueId")
                {
                    uniqueIDs = PrepareForSqlInStatement(lstStoredQueryParams[count].ParameterValue);
                }
            }

            objRegistryStoredQueryDAL = new RegistryStoredQueryDataAccess();
            lstDocumentEntry = objRegistryStoredQueryDAL.GetDocuments(entryUUIDs, uniqueIDs);
            objRegistryStoredQueryDAL = null;

            return lstDocumentEntry;
        }


        public abstract XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest);

    
    }
}
