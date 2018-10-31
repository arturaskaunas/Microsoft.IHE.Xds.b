using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;

namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class FindFolders: StoredQueryBase
    {

        public override System.Xml.XmlDocument ProcessQuery(StoredQuery objStoredQueryRequest)
        {
            XmlDocument xmlDocResponse = null;            
            XmlDocument xmlDocAdHocQueryResponse;
            List<Folder> lstFolder = null;
       

            //Get folders
            lstFolder = GetFolders(objStoredQueryRequest.ParameterList);
            lstFolder = FilterResults(objStoredQueryRequest.ParameterList, lstFolder);

            //Construct AdHocQueryResponse Element
            xmlDocAdHocQueryResponse = StoredQueryBase.ConstructAdHocQueryResponseElement(GlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS);

            if (objStoredQueryRequest.ReturnType.ToLower() == "leafclass")
            {
                // Construct LeafClass - RegistryObjectList Element
                xmlDocResponse = ConstructLeafClassFolder(xmlDocAdHocQueryResponse, lstFolder);
            }
            else
            {
                //Default ObjectRef

                // Construct ObjectRef - RegistryObjectList Element
                xmlDocResponse = ConstructObjectRefFolder(xmlDocAdHocQueryResponse, lstFolder);
            }


            return xmlDocResponse;           
        }


        private List<Folder> GetFolders(List<StoredQueryParameter> lstStoredQueryParameter)
        {
            List<Folder> lstFolder = null;
            RegistryStoredQueryDataAccess objRegistryStoredQueryDAL = null;
            string availabilityStatusList = null;
            string patientUID = null;

            for (int count = 0; count < lstStoredQueryParameter.Count; count++)
            {
                if (lstStoredQueryParameter[count].ParameterName == "$XDSFolderStatus")
                {
                    availabilityStatusList = PrepareForSqlInStatement(lstStoredQueryParameter[count].ParameterValue);
                }
                else if (lstStoredQueryParameter[count].ParameterName == "$XDSFolderPatientId")
                {
                    patientUID = lstStoredQueryParameter[count].ParameterValue;
                }
            }

            objRegistryStoredQueryDAL = new RegistryStoredQueryDataAccess();
            lstFolder = objRegistryStoredQueryDAL.GetFolders(availabilityStatusList, patientUID);
            objRegistryStoredQueryDAL = null;

            return lstFolder;
        }

        private List<Folder> FilterResults(List<StoredQueryParameter> lstStoredQueryParameter, List<Folder> lstFolder)
        {
            RegistryStoredQueryDataAccess registryStoredQueryDAL = new RegistryStoredQueryDataAccess();
            List<Folder> lstFolderFiltered = new List<Folder>();
            string lastUpdateTimeFrom = null;
            string lastUpdateTimeTo = null;
            string folderCodeList = null;
            string folderCodeListScheme = null;

            //If no filter is passed 
            lstFolderFiltered = lstFolder;

            lastUpdateTimeFrom = GetParameterValue(lstStoredQueryParameter, "$XDSFolderLastUpdateTimeFrom");
            lastUpdateTimeTo = GetParameterValue(lstStoredQueryParameter, "$XDSFolderLastUpdateTimeTo");
            folderCodeList = GetParameterValue(lstStoredQueryParameter, "$XDSFolderCodeList");
            folderCodeListScheme = GetParameterValue(lstStoredQueryParameter, "$XDSFolderCodeListScheme");



            if ((folderCodeList != null) && (folderCodeListScheme != null))
            {
                List<FolderCodeList> lstFolderCodeList = null;
                List<string> lstFolderIDs = null;
                string folderIDs = null;

                folderCodeList = PrepareForSqlInStatement(folderCodeList);
                folderCodeListScheme = PrepareForSqlInStatement(folderCodeListScheme);

                lstFolderIDs = GetFolderIDs(lstFolderFiltered);
                folderIDs = PrepareForSqlInStatement(lstFolderIDs);

                lstFolderCodeList = registryStoredQueryDAL.GetFolderCodeListByFolderIDs(folderIDs, folderCodeList, folderCodeListScheme);

                lstFolderIDs = GetFolderIDs(lstFolderCodeList);
                folderIDs = PrepareForSqlInStatement(lstFolderIDs);

                //Find & Assign matching codelist's
                if (lstFolderCodeList != null)
                {
                    lstFolderFiltered = lstFolderFiltered.FindAll(
                        delegate(Folder folder)
                        {
                            if(folderIDs.Contains(folder.FolderID.ToString()))
                                return true;

                            return false;
                        }
                        );
                }
            }


            if( (lastUpdateTimeFrom != null) && (lastUpdateTimeTo != null) )
            {
                lstFolderFiltered = lstFolderFiltered.FindAll(
                    delegate(Folder folder)
                    {
                        if( (folder.LastUpdateTime >= DateTime.Parse(lastUpdateTimeFrom)) 
                            && (folder.LastUpdateTime < DateTime.Parse(lastUpdateTimeTo)) )
                        {
                            return true;
                        }

                        return false;
                    }
                    );


            }
            else if( (lastUpdateTimeFrom != null) && (lastUpdateTimeTo == null) )
            {
                lstFolderFiltered = lstFolderFiltered.FindAll(
                    delegate(Folder folder)
                    {
                        if(folder.LastUpdateTime >= DateTime.Parse(lastUpdateTimeFrom))
                            return true;

                        return false;
                    }
                    );

            }
            else if ((lastUpdateTimeFrom == null) && (lastUpdateTimeTo != null))
            {
                lstFolderFiltered = lstFolderFiltered.FindAll(
                    delegate(Folder folder)
                    {
                        if (folder.LastUpdateTime < DateTime.Parse(lastUpdateTimeTo))
                            return true;

                        return false;
                    }
                    );

            }


            return lstFolderFiltered;
        }

        private XmlDocument ConstructLeafClassFolder(XmlDocument xmlDocAdHocQueryResponse, List<Folder> lstFolder)
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

            if (lstFolder != null)
            {
                for (int iCount = 0; iCount < lstFolder.Count; iCount++)
                {
                    //ExtrinsicObject
                    eltRegistryObjectList.InnerXml += lstFolder[iCount].FolderXml;

                    //ObjectRef
                    eltObjectRef = xmlDocAdHocQueryResponse.CreateElement("rim", "ObjectRef", GlobalValues.CONST_XML_NAMESPACE_QUERY_rim);

                    //id
                    attribID = xmlDocAdHocQueryResponse.CreateAttribute("id");
                    attribID.Value = lstFolder[iCount].EntryUUID;
                    eltObjectRef.Attributes.Append(attribID);
                    sbObjectRefXml.Append(eltObjectRef.OuterXml);

                }

                eltRegistryObjectList.InnerXml += sbObjectRefXml.ToString();
            }


            return xmlDocAdHocQueryResponse;
        }


        private XmlDocument ConstructObjectRefFolder(XmlDocument xmlDocAdHocQueryResponse, List<Folder> lstFolder)
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

        
    }
}
