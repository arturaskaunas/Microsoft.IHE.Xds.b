using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;


namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class FindDocuments: StoredQueryBase
    {


        public override XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest)
        {
            XmlDocument xmlDocResponse = null;
            XmlDocument xmlDocAdHocQueryResponse;
            List<DocumentEntry> lstDocumentEntries = null;
            string patientUID = null;
            string availabilityStatus = null;
            List<string> lstAvailabilityStatus = null;

            patientUID = GetParameterValue(objStoredQueryRequest.ParameterList, "$XDSDocumentEntryPatientId");
            availabilityStatus = GetParameterValue(objStoredQueryRequest.ParameterList, "$XDSDocumentEntryStatus");
            lstAvailabilityStatus = GetSqlInStatementValues(availabilityStatus);

            //Get DocumentEntries
            lstDocumentEntries = GetDocumentEntries(patientUID, lstAvailabilityStatus);

            if (lstDocumentEntries != null && lstDocumentEntries.Count > 0)
            {
                //Filter By DocumentEntryEventCodeList
                lstDocumentEntries = FilterByDocumentEntryEventCodeList(lstDocumentEntries, objStoredQueryRequest.ParameterList);

                //Filter By Code & CodeScheme
                lstDocumentEntries = FilterByCode(lstDocumentEntries, objStoredQueryRequest.ParameterList, "$XDSDocumentEntryClassCode", "$XDSDocumentEntryClassCodeScheme");
                lstDocumentEntries = FilterByCode(lstDocumentEntries, objStoredQueryRequest.ParameterList, "$XDSDocumentEntryPracticeSettingCode", "$XDSDocumentEntryPracticeSettingCodeScheme");
                lstDocumentEntries = FilterByCode(lstDocumentEntries, objStoredQueryRequest.ParameterList, "$XDSDocumentEntryHealthcareFacilityTypeCode", "$XDSDocumentEntryHealthcareFacilityTypeCodeScheme");
                lstDocumentEntries = FilterByCode(lstDocumentEntries, objStoredQueryRequest.ParameterList, "$XDSDocumentEntryConfidentialityCode", "$XDSDocumentEntryConfidentialityCodeScheme");
                lstDocumentEntries = FilterByCode(lstDocumentEntries, objStoredQueryRequest.ParameterList, "$XDSDocumentEntryFormatCode", "$XDSDocumentEntryFormatCodeScheme");

                //Filter By Time
                lstDocumentEntries = FilterByTime(lstDocumentEntries, objStoredQueryRequest.ParameterList, "$XDSDocumentEntryCreationTimeFrom", "$XDSDocumentEntryCreationTimeTo");
                lstDocumentEntries = FilterByTime(lstDocumentEntries, objStoredQueryRequest.ParameterList, "$XDSDocumentEntryServiceStartTimeFrom", "$XDSDocumentEntryServiceStartTimeTo");
                lstDocumentEntries = FilterByTime(lstDocumentEntries, objStoredQueryRequest.ParameterList, "$XDSDocumentEntryServiceStopTimeFrom", "$XDSDocumentEntryServiceStopTimeTo");
            }

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

        private List<DocumentEntry> FilterByDocumentEntryEventCodeList(List<DocumentEntry> lstDocumentEntry, List<StoredQueryParameter> lstParameter)
        {
            List<DocumentEntry> lstFilteredDocumentEntry = null;
            string documentEntryEventCodeList = null;
            string documentEntryEventCodeListScheme = null;
            List<string> lstDocumentEntryEventCodeList = null;
            List<string> lstDocumentEntryEventCodeListScheme = null;

            documentEntryEventCodeList = GetParameterValue(lstParameter, "$XDSDocumentEntryEventCodeList");
            lstDocumentEntryEventCodeList = GetSqlInStatementValues(documentEntryEventCodeList);

            documentEntryEventCodeListScheme = GetParameterValue(lstParameter, "$XDSDocumentEntryEventCodeListScheme");
            lstDocumentEntryEventCodeListScheme = GetSqlInStatementValues(documentEntryEventCodeListScheme);
            
            if ((documentEntryEventCodeList == null) || (documentEntryEventCodeListScheme == null))
                return lstDocumentEntry;

            lstFilteredDocumentEntry = new List<DocumentEntry>();

            foreach (DocumentEntry documentEntry in lstDocumentEntry)
            {
                if (documentEntry.EventCodeList == null)
                    continue;

                foreach (CodeValue codeValue in documentEntry.EventCodeList)
                {
                    if (lstDocumentEntryEventCodeList.Contains(codeValue.Value)
                        && lstDocumentEntryEventCodeListScheme.Contains(codeValue.CodingScheme))
                    {
                        lstFilteredDocumentEntry.Add(documentEntry);
                        break;
                    }
                }
            }


            return lstFilteredDocumentEntry;
        }


        private List<DocumentEntry> FilterByCode(List<DocumentEntry> lstDocumentEntry, List<StoredQueryParameter> lstParameter, string codeParameterName, string codeSchemeParameterName)
        {
            List<DocumentEntry> lstFilteredDocumentEntry = null;
            string codeValues = null;
            string codeSchemes = null;
            List<string> lstCodeValue = null;
            List<string> lstCodeScheme = null;

            codeValues = GetParameterValue(lstParameter, codeParameterName);
            lstCodeValue = GetSqlInStatementValues(codeValues);

            codeSchemes = GetParameterValue(lstParameter, codeSchemeParameterName);
            lstCodeScheme = GetSqlInStatementValues(codeSchemes);

            if ((codeValues == null) || (codeSchemes == null))
                return lstDocumentEntry;

            lstFilteredDocumentEntry = new List<DocumentEntry>();

            foreach (DocumentEntry documentEntry in lstDocumentEntry)
            {
                   
                switch(codeParameterName)
                {
                    case "$XDSDocumentEntryClassCode":

                        if (lstCodeValue.Contains(documentEntry.ClassCode.Value)
                            && lstCodeScheme.Contains(documentEntry.ClassCode.CodingScheme))
                        {
                            lstFilteredDocumentEntry.Add(documentEntry);
                            break;
                        }
                        break;

                    case "$XDSDocumentEntryPracticeSettingCode":

                        if (lstCodeValue.Contains(documentEntry.PracticeSettingsCode.Value)
                            && lstCodeScheme.Contains(documentEntry.PracticeSettingsCode.CodingScheme))
                        {
                            lstFilteredDocumentEntry.Add(documentEntry);
                            break;
                        }
                        break;

                    case "$XDSDocumentEntryHealthcareFacilityTypeCode":

                        if (lstCodeValue.Contains(documentEntry.HealthcareFacilityCode.Value)
                            && lstCodeScheme.Contains(documentEntry.HealthcareFacilityCode.CodingScheme))
                        {
                            lstFilteredDocumentEntry.Add(documentEntry);
                            break;
                        }
                        break;

                    case "$XDSDocumentEntryConfidentialityCode":

                        if (lstCodeValue.Contains(documentEntry.ConfidentialityCode.Value)
                            && lstCodeScheme.Contains(documentEntry.ConfidentialityCode.CodingScheme))
                        {
                            lstFilteredDocumentEntry.Add(documentEntry);
                            break;
                        }
                        break;

                    case "$XDSDocumentEntryFormatCode":

                        if (lstCodeValue.Contains(documentEntry.FormatCode.Value)
                            && lstCodeScheme.Contains(documentEntry.FormatCode.CodingScheme))
                        {
                            lstFilteredDocumentEntry.Add(documentEntry);
                            break;
                        }
                        break;


                }

            }


            return lstFilteredDocumentEntry;
        }


        private List<DocumentEntry> FilterByTime(List<DocumentEntry> lstDocumentEntry, List<StoredQueryParameter> lstParameter, string parameterNameFrom, string parameterNameTo)
        {
            List<DocumentEntry> lstFilteredDocumentEntry = null;
            string timeFrom = null;
            string timeTo = null;
            DateTime dtTimeFrom = DateTime.Today;
            DateTime dtTimeTo = DateTime.Today;

            timeFrom = GetParameterValue(lstParameter, parameterNameFrom);
            timeTo = GetParameterValue(lstParameter, parameterNameTo);

            if (string.IsNullOrEmpty(timeFrom) || string.IsNullOrEmpty(timeTo))
                return lstDocumentEntry;

            if( (!DateTime.TryParse(timeFrom, out dtTimeFrom))
                || !(DateTime.TryParse(timeTo, out dtTimeTo)) )
                return lstDocumentEntry;

            lstFilteredDocumentEntry = new List<DocumentEntry>();

            foreach (DocumentEntry documentEntry in lstDocumentEntry)
            {
                switch(parameterNameFrom)
                {
                    case "$XDSDocumentEntryCreationTimeFrom":
                        if (documentEntry.CreationTime >= dtTimeFrom
                            && documentEntry.CreationTime < dtTimeTo)
                        {
                            lstFilteredDocumentEntry.Add(documentEntry);
                            break;
                        }
                        break;

                    case "$XDSDocumentEntryServiceStartTimeFrom":
                        if (documentEntry.ServiceStartTime >= dtTimeFrom
                            && documentEntry.ServiceStartTime < dtTimeTo)
                        {
                            lstFilteredDocumentEntry.Add(documentEntry);
                            break;
                        }
                        break;

                    case "$XDSDocumentEntryServiceStopTimeFrom":
                        if (documentEntry.ServiceStopTime >= dtTimeFrom
                            && documentEntry.ServiceStopTime < dtTimeTo)
                        {
                            lstFilteredDocumentEntry.Add(documentEntry);
                            break;
                        }
                        break;
                }
            }


            return lstFilteredDocumentEntry;
        }

        private XmlDocument ConstructObjectRefRegistryObjectList(XmlDocument xmlDocAdHocQueryResponse, List<DocumentEntry> lstDocumentEntry)
        {
            XmlElement xmlRoot = xmlDocAdHocQueryResponse.DocumentElement;

            //Attribute - id
            XmlAttribute attribID = null;

            //ObjectRef
            XmlElement eltObjectRef = null;

            //RegistryObjectList
            XmlElement eltRegistryObjectList = xmlDocAdHocQueryResponse.CreateElement("rim", "RegistryObjectList", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);
            xmlRoot.AppendChild(eltRegistryObjectList);

            if (lstDocumentEntry != null)
            {
                for (int iCount = 0; iCount < lstDocumentEntry.Count; iCount++)
                {
                    //ObjectRef
                    eltObjectRef = xmlDocAdHocQueryResponse.CreateElement("rim", "ObjectRef", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);

                    attribID = xmlDocAdHocQueryResponse.CreateAttribute("id");
                    attribID.Value = lstDocumentEntry[iCount].EntryUUID;
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
