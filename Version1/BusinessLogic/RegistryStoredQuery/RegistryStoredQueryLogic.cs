using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;
using Microsoft.IHE.XDS.BusinessLogic.ATNA;

namespace Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery
{
    public class RegistryStoredQueryLogic
    {
             

        public XmlDocument RegistryStoredQuery(XmlDocument xmlDocRequest, out StringDictionary atnaParameterValues)
        {                       
            XmlDocument xmlDocResponse = null;
            StoredQuery objStoredQueryRequest = null;
            StoredQuery objStoredQueryDB = null;
            StoredQueryBase objStoredQuery = null;
            string parameterName = null;
            string dependentParameterName = null;
            string eventOutcomeIndicator = "0";

            atnaParameterValues = new StringDictionary();

            try
            {

                //Construct the Request Stored Query Object
                objStoredQueryRequest = StoredQueryBase.ConstructStoredQuery(xmlDocRequest);

                //-------------------------------------------------------------------------------------
                //ATNA
                StoredQueryParameter queryParamPatientID = objStoredQueryRequest.ParameterList.Find(
                    delegate(StoredQueryParameter queryParameter)
                    {
                        if (queryParameter != null && queryParameter.ParameterName != null && queryParameter.ParameterName == "$XDSDocumentEntryPatientId")
                        {
                            return true;
                        }

                        return false;
                    }
                    );
                if (queryParamPatientID != null)
                    atnaParameterValues.Add("$XDSPatient$", queryParamPatientID.ParameterValue);

                XmlNode nodeAdhocQuery = xmlDocRequest.SelectSingleNode(@"//*[local-name()='AdhocQuery']");

                if(nodeAdhocQuery != null)
                    atnaParameterValues.Add("$AdhocQuery$", nodeAdhocQuery.OuterXml);

                //-------------------------------------------------------------------------------------
                
                objStoredQueryDB = GetStoredQueryDetails(objStoredQueryRequest.StoredQueryUniqueID, objStoredQueryRequest.ReturnType);


                #region "***********Validations****************"

                if (objStoredQueryDB == null)
                {
                    eventOutcomeIndicator = "8";
                    atnaParameterValues.Add("$EventIdentification.EventOutcomeIndicator$", eventOutcomeIndicator);
                    
                    //Invalid Query ID
                    xmlDocResponse = StoredQueryBase.ConstructStoredQueryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Invalid query id.", GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xmlDocResponse;
                }

                //Update the Request Parameters with Dependent Parameters
                UpdateDependentParameterName(objStoredQueryRequest.ParameterList, objStoredQueryDB.ParameterList);

                //Validate Repeating Parameter
                if (IsParameterRepeated(objStoredQueryRequest.ParameterList))
                {
                    eventOutcomeIndicator = "8";
                    atnaParameterValues.Add("$EventIdentification.EventOutcomeIndicator$", eventOutcomeIndicator);

                    //Parameter Repeating
                    xmlDocResponse = StoredQueryBase.ConstructStoredQueryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Repeating parameter.", GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xmlDocResponse;

                }


                //Parameter Validation
                if (!IsRequiredQueryParameterExists(objStoredQueryRequest.ParameterList, objStoredQueryDB.ParameterList))
                {
                    eventOutcomeIndicator = "8";
                    atnaParameterValues.Add("$EventIdentification.EventOutcomeIndicator$", eventOutcomeIndicator);

                    //Required Parameter Missing
                    xmlDocResponse = StoredQueryBase.ConstructStoredQueryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Required parameter missing.", GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xmlDocResponse;
                }

                //Validate Dependent Parameter
                if (!IsValidDependentParameterExists(objStoredQueryRequest.ParameterList, out parameterName, out dependentParameterName))
                {
                    eventOutcomeIndicator = "8";
                    atnaParameterValues.Add("$EventIdentification.EventOutcomeIndicator$", eventOutcomeIndicator);

                    //Required Parameter Missing
                    xmlDocResponse = StoredQueryBase.ConstructStoredQueryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, string.Format("{0} parameter is specified but corresponding {1} parameter is not specified.", parameterName, dependentParameterName) , GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xmlDocResponse;
                }

                //Validate Dependent Parameter Value Count
                if (!IsValidDependentParameterValueCount(objStoredQueryRequest.ParameterList, out parameterName, out dependentParameterName))
                {
                    eventOutcomeIndicator = "8";
                    atnaParameterValues.Add("$EventIdentification.EventOutcomeIndicator$", eventOutcomeIndicator);

                    //Required Parameter Missing
                    xmlDocResponse = StoredQueryBase.ConstructStoredQueryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, string.Format("{0} parameter values does not have equivalent number of values in {1} parameter.", parameterName, dependentParameterName), GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                    return xmlDocResponse;
                }

                #endregion

                //Merge the StoredQuery Request & DB details
                //we are left with only one StoredQuery Object with all the params & it's values
                objStoredQueryRequest = StoredQueryBase.UpdateParameterValues(objStoredQueryRequest, objStoredQueryDB);

                //Create concrete StoredQuery object based on query name
                objStoredQuery = StoredQueryBase.GetStoredQueryObject(objStoredQueryRequest.StoredQueryName);

                //Process Message & Construct Response Accordingly
                xmlDocResponse = objStoredQuery.ProcessQuery(objStoredQueryRequest);
            }
            catch
            {
                //Return Error Response
                xmlDocResponse = StoredQueryBase.ConstructStoredQueryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Unexpected Registry Error.", GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);

                eventOutcomeIndicator = "8";
            }

            atnaParameterValues.Add("$EventIdentification.EventOutcomeIndicator$", eventOutcomeIndicator);

            return xmlDocResponse;
        }

 
        private StoredQuery GetStoredQueryDetails(string queryUUID, string returnType)
        {
            StoredQuery objStoredQuery = null;

            //Call DAL & get all the query parameters for the given query uuid
            RegistryStoredQueryDataAccess objStoredQueryDAL = new RegistryStoredQueryDataAccess();
            objStoredQuery = objStoredQueryDAL.GetStoredQueryDetails(queryUUID, returnType);

            return objStoredQuery;
        }

        private bool IsParameterRepeated(List<StoredQueryParameter> queryParameterRequestList)
        {
            bool isRepeated = false;
            string parameterName = null;
            List<StoredQueryParameter> lstParameter = null;

            for (int count = 0; count < queryParameterRequestList.Count; count++)
            {
                parameterName = queryParameterRequestList[count].ParameterName;

                lstParameter = queryParameterRequestList.FindAll(
                    delegate(StoredQueryParameter parameter)
                    {
                        if (parameter.ParameterName == parameterName)
                            return true;

                        return false;
                    }
                    );

                if ((lstParameter != null) && (lstParameter.Count > 1))
                {
                    isRepeated = true;
                    break;
                }

            }

            return isRepeated;
        }

        private bool IsRequiredQueryParameterExists(List<StoredQueryParameter> queryParameterRequestList, List<StoredQueryParameter> queryParameterDBList)
        {
            bool bResult = true;
            //bool bFlag = false;
            bool bRequiredParamEsists = false;

            //Loop through required DB Parameter list and check for missing parameters from request

            List<StoredQueryParameter> objRequiredParams = queryParameterDBList.FindAll(
                delegate(StoredQueryParameter sq) 
                {
                    if (sq.IsMandatory)
                        return true;

                    return false; 
                }
            );

            //Validate Required Parameters
            for (int iRequiredParamCount = 0; iRequiredParamCount < objRequiredParams.Count; iRequiredParamCount++)
            {
                bRequiredParamEsists = false;

                for (int iRequestParamCount = 0; iRequestParamCount < queryParameterRequestList.Count; iRequestParamCount++)
                {
                    if (objRequiredParams[iRequiredParamCount].ParameterName == queryParameterRequestList[iRequestParamCount].ParameterName)
                    {
                        bRequiredParamEsists = true;
                        continue;
                    }
                }

                if (!bRequiredParamEsists)
                {
                    bResult = false;
                    break;
                }
            }

            return bResult;
        }

        private bool IsValidDependentParameterExists(List<StoredQueryParameter> lstStoredQueryParameterRequest, out string parameterName, out string dependentParameterName)
        {
            //Validate whether dependent parameter exists in the request
            bool isValid = true;
            List<StoredQueryParameter> lstParamWithDependencyRequest = null;
            StoredQueryParameter parameterMatchingRequest = null;

            parameterName = string.Empty;
            dependentParameterName = string.Empty;

            lstParamWithDependencyRequest = GetAllParametersWithDependency(lstStoredQueryParameterRequest);

            if ((lstParamWithDependencyRequest != null) && (lstParamWithDependencyRequest.Count > 0))
            {

                for (int count = 0; count < lstParamWithDependencyRequest.Count; count++)
                {
                    if (!lstStoredQueryParameterRequest.Exists(
                        delegate(StoredQueryParameter param)
                        {
                            if (param.ParameterName == lstParamWithDependencyRequest[count].DependentParameterName)
                                return true;

                            return false;
                        }
                        ))
                    {
                        isValid = false;
                        parameterName = lstParamWithDependencyRequest[count].ParameterName;
                        dependentParameterName = lstParamWithDependencyRequest[count].DependentParameterName;
                        break;
                    }

                }

            }

            return isValid;
        }

        private bool IsValidDependentParameterValueCount(List<StoredQueryParameter> lstStoredQueryParameterRequest, out string parameterName, out string dependentParameterName)
        {
            //Validate whether dependent parameter's value count is equal
            bool isValid = true;
            List<StoredQueryParameter> lstParamWithDependencyRequest = null;
            StoredQueryParameter parameterMatchingRequest = null;
            string[] arrParamValues = null;
            string[] arrDependentParamValues = null;

            parameterName = string.Empty;
            dependentParameterName = string.Empty;

            lstParamWithDependencyRequest = GetAllParametersWithDependency(lstStoredQueryParameterRequest);

            if ((lstParamWithDependencyRequest != null) && (lstParamWithDependencyRequest.Count > 0))
            {

                for (int count = 0; count < lstParamWithDependencyRequest.Count; count++)
                {
                    parameterMatchingRequest = lstStoredQueryParameterRequest.Find(
                        delegate(StoredQueryParameter param)
                        {
                            if (param.ParameterName == lstParamWithDependencyRequest[count].DependentParameterName)
                                return true;

                            return false;
                        }
                        );

                    if(parameterMatchingRequest != null)
                    {
                        arrParamValues = lstParamWithDependencyRequest[count].ParameterValue.Split(',');
                        arrDependentParamValues = parameterMatchingRequest.ParameterValue.Split(',');

                        if (arrParamValues.Length != arrDependentParamValues.Length)
                        {
                            isValid = false;
                            parameterName = lstParamWithDependencyRequest[count].ParameterName;
                            dependentParameterName = parameterMatchingRequest.ParameterName;
                            break;
                        }
                    }

                }

            }

            return isValid;
        }
        

        private void UpdateDependentParameterName(List<StoredQueryParameter> lstStoredQueryParameterRequest, List<StoredQueryParameter> lstStoredQueryParameterDB)
        {
            List<StoredQueryParameter> lstParamWithDependencyDB = null;
            StoredQueryParameter parameterMatchingRequest = null;

            lstParamWithDependencyDB = GetAllParametersWithDependency(lstStoredQueryParameterDB);

            if ((lstParamWithDependencyDB != null) && (lstParamWithDependencyDB.Count > 0))
            {

                for (int count = 0; count < lstParamWithDependencyDB.Count; count++)
                {
                    parameterMatchingRequest = lstStoredQueryParameterRequest.Find(
                        delegate(StoredQueryParameter param)
                        {
                            if (param.ParameterName == lstParamWithDependencyDB[count].ParameterName)
                                return true;

                            return false;
                        }
                        );

                    if (parameterMatchingRequest != null)
                        parameterMatchingRequest.DependentParameterName = lstParamWithDependencyDB[count].DependentParameterName;

                }

            }
        }

        private List<StoredQueryParameter> GetAllParametersWithDependency(List<StoredQueryParameter> lstStoredQueryParameter)
        {
            List<StoredQueryParameter> paramWithDependency = null;

            paramWithDependency = lstStoredQueryParameter.FindAll(
                delegate(StoredQueryParameter parameter)
                {
                    if (!string.IsNullOrEmpty(parameter.DependentParameterName))
                        return true;

                    return false;
                }
                );

            return paramWithDependency;
        }

        private string GetDependentParameterName(List<StoredQueryParameter> lstStoredQueryParameter, string paramName)
        {
            string dependentParameterName = null;
            StoredQueryParameter storedQueryParam = null;

            storedQueryParam = lstStoredQueryParameter.Find(
                delegate(StoredQueryParameter parameter)
                {
                    if (parameter.ParameterName == paramName)
                        return true;

                    return false;
                }
                );

            if (storedQueryParam != null)
                dependentParameterName = storedQueryParam.DependentParameterName;

            return dependentParameterName;
        }

        public void ProcessRegistryStoredQueryATNAEvent(string sourceUserID, string destinationUserID, string xdsPatientID, string adhocQueryElementXml, string eventOutcomeIndicator)
        {
            //adHocQueryElementXml = atnaParameterValues["$AdhocQuery$"];

            try
            {
                //Log ATNA - Repository Event
                ATNALogic atnaLogic = new ATNALogic();
                AuditMessageConfiguration auditMsgConfig = null;

                auditMsgConfig = atnaLogic.GetAuditMessageConfigurationDetails("REGISTRY-QUERY-ITI-18");

                if (auditMsgConfig != null)
                {

                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$XDSPatient$", xdsPatientID);
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$SubmissionSet.ClassificationNode.UUID$", ATNAEvent.XDSREPOSITORY_SUBMISSIONSET_CLASSIFICATIONNODE_UUID);

                    //$ActiveParticipant.UserID.Source$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Source$", sourceUserID);

                    //$ActiveParticipant.UserID.Destination$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Destination$", destinationUserID);

                    //Assigns attribute value for $AdhocQuery$ parameter
                    //<ParticipantObjectIdentification ParticipantObjectID="$AdhocQuery$" ParticipantObjectTypeCode="2" ParticipantObjectTypeCodeRole="24">
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(auditMsgConfig.MessageValue);
                    XmlNode node = xmlDoc.SelectSingleNode(@"//*[local-name()='ParticipantObjectIdentification'][@ParticipantObjectID='$AdhocQuery$']");
                    
                    if (node != null)
                    {
                        //Encoding encoding = Encoding.GetEncoding(adhocQueryElementXml);

                        //node.Attributes["ParticipantObjectID"].Value = adhocQueryElementXml;
                        node.Attributes["ParticipantObjectID"].Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(adhocQueryElementXml));
                        auditMsgConfig.MessageValue = xmlDoc.OuterXml;
                    }

                    atnaLogic.ProcessEvent(auditMsgConfig, ATNAEvent.XDSREGISTRY_TYPE, eventOutcomeIndicator, ATNAEvent.UDP_TAG_APPNAME_REGISTRY);
                }
            }
            catch
            {
                //Oops...ATNA event failed...probably UDP failure....can't afford to stop XDSRepository service sorry :(
            }
        }

   
    
    }
}
