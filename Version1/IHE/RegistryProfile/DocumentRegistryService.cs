using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.BusinessLogic;
using Microsoft.IHE.XDS.BusinessLogic.ATNA;
using Microsoft.IHE.XDS.BusinessLogic.RegistryStoredQuery;


namespace Microsoft.IHE.XDS.DocumentRegistry
{


    [ServiceBehavior()]
    public class DocumentRegistryService : IDocumentRegistry
    {
        private static readonly string SOURCE_USERID = " ";
           
        Message IDocumentRegistry.RegisterDocumentSet(Message input)
        {
            XDSResponse xdsResponse = null;
            Message msgResponse = null;
            XmlDocument xmlDocRequest = null; ;
            XmlDocument xmlDocResponse = null;
            XmlNode nodeRegistryError = null;
            RegistryLogic objRegistryLogic = null;

            DateTime dtStart = DateTime.Now;
            string eventOutcomeIndicator = "0";
            string submissionSetUniqueID = string.Empty;
            string sourceUserID = string.Empty;
            string destinationUserID = string.Empty;

            try
            {
                //ATNA Event - Active Participant Source UserID
                sourceUserID = GetSourceUserID();

                //ATNA Event - Active Participant Destination UserID
                destinationUserID = GetDestinationUserID();

                //Request XmlDocument
                xmlDocRequest = new XmlDocument();
                xmlDocRequest.Load(input.GetReaderAtBodyContents());                              

                //
                objRegistryLogic = new RegistryLogic();
                xdsResponse = objRegistryLogic.RegisterDocumentSet(xmlDocRequest);

                xmlDocResponse = xdsResponse.XDSResponseDocument;

                //Failure
                nodeRegistryError = xmlDocResponse.SelectSingleNode(@"//*[local-name()='RegistryError']");

                if (nodeRegistryError != null)
                {
                        eventOutcomeIndicator = "8";

                        objRegistryLogic.CreateRegistryLogEntry(xmlDocRequest.InnerXml, GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, dtStart);
                }

                ////Failure
                //if ((xdsResponse.XDSErrorList != null) && (xdsResponse.XDSErrorList.Count > 0))
                //{
                //    eventOutcomeIndicator = "8";
                //}
                
                //if ((xdsResponse.XDSErrorList == null) || (xdsResponse.XDSErrorList.Count == 0))
                //{
                    
                //}
                //else
                //{
                //    xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, "Internal Registry Error", "RegistryInternalError", GlobalValues.CONST_SEVERITY_TYPE_ERROR, "Registry");
                    
                //    objRegistryLogic.CreateRegistryLogEntry(xmlDocRequest.InnerXml, GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, dtStart);

                //    //ATNA Event Outcome Indicator
                //    eventOutcomeIndicator = "8";

                //}

            }
            catch (System.ServiceModel.ServerTooBusyException serverTooBusyException)
            {

                xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, serverTooBusyException.Message, GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryTooBusyException, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);
                
                objRegistryLogic.CreateRegistryLogEntry(xmlDocRequest.InnerXml, GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, dtStart);

                //ATNA Event Outcome Indicator
                eventOutcomeIndicator = "8";

            }
            catch (TimeoutException timeoutException)
            {
                xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, timeoutException.Message, GlobalValues.CONST_ERROR_CODE_TimeOut, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);

                objRegistryLogic.CreateRegistryLogEntry(xmlDocRequest.InnerXml, GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, dtStart);

                //ATNA Event Outcome Indicator
                eventOutcomeIndicator = "8";

            }
            catch (System.ServiceModel.Security.SecurityAccessDeniedException AuthorizationException)
            {
                xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, AuthorizationException.Message, GlobalValues.CONST_ERROR_CODE_XDSAuthorizationException, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);

                objRegistryLogic.CreateRegistryLogEntry(xmlDocRequest.InnerXml, GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, dtStart);

                //ATNA Event Outcome Indicator
                eventOutcomeIndicator = "8";

            }
            catch (Exception)
            {
                //ATNA Event Outcome Indicator
                eventOutcomeIndicator = "8";

                xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);

                objRegistryLogic.CreateRegistryLogEntry(xmlDocRequest.InnerXml, GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, dtStart);
            }

            //------------------------------------------------------------------------------------
            //ATNA Event
            if(xdsResponse.AtnaParameters.ContainsKey("$SubmissionSet.UniqueID$"))
                submissionSetUniqueID = xdsResponse.AtnaParameters["$SubmissionSet.UniqueID$"];

            objRegistryLogic.ProcessRegisterDocumentSetATNAEvent(submissionSetUniqueID, sourceUserID, destinationUserID, eventOutcomeIndicator);
            //------------------------------------------------------------------------------------

            msgResponse = Message.CreateMessage(input.Headers.MessageVersion, GlobalValues.CONST_ACTION_RegisterDocumentSetResponse, new XmlNodeReader(xmlDocResponse));
            
            return msgResponse;
        }

        Message IDocumentRegistry.RegistryStoredQuery(System.ServiceModel.Channels.Message input)
        {
            Message msgResult = null;
            XmlDocument xmlDocRequest = null;
            XmlDocument xmlDocResponse = new XmlDocument();
            XmlElement rootElement = xmlDocResponse.DocumentElement;
            RegistryStoredQueryLogic objStoredQueryLogic = null;
            RegistryLogic objRegistryLogic = null;
            StringDictionary atnaParameterValues = null;
            string eventOutcomeIndicator = "0";
            string sourceUserID = string.Empty;
            string destinationUserID = string.Empty;
            string xdsPatientID = string.Empty;
            string adHocQueryElementXml = string.Empty;
            
            try
            {
                objRegistryLogic = new RegistryLogic();
                objStoredQueryLogic = new RegistryStoredQueryLogic();

                //ATNA Event - Active Participant Source UserID
                sourceUserID = GetSourceUserID();

                //ATNA Event - Active Participant Destination UserID
                destinationUserID = GetDestinationUserID();


                xmlDocRequest = new XmlDocument();
                xmlDocRequest.Load(input.GetReaderAtBodyContents());

                xmlDocResponse = objStoredQueryLogic.RegistryStoredQuery(xmlDocRequest, out atnaParameterValues);

                //ATNA
                eventOutcomeIndicator = atnaParameterValues["$EventIdentification.EventOutcomeIndicator$"];
                xdsPatientID = atnaParameterValues["$XDSPatient$"];
                adHocQueryElementXml = atnaParameterValues["$AdhocQuery$"];
                
            }
            catch (System.ServiceModel.ServerTooBusyException serverTooBusyException)
            {
                objRegistryLogic.CreateRegistryLogEntry(xmlDocRequest.InnerXml, GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, DateTime.Now);

                xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, serverTooBusyException.Message, GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryTooBusyException, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);

                //ATNA Event Failure Indicator
                eventOutcomeIndicator = "8";
            }
            catch (TimeoutException timeoutException)
            {
                objRegistryLogic.CreateRegistryLogEntry(xmlDocRequest.InnerXml, GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, DateTime.Now);

                xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, timeoutException.Message, GlobalValues.CONST_ERROR_CODE_TimeOut, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);

                //ATNA Event Failure Indicator
                eventOutcomeIndicator = "8";
            }
            catch (System.ServiceModel.Security.SecurityAccessDeniedException AuthorizationException)
            {
                objRegistryLogic.CreateRegistryLogEntry(xmlDocRequest.InnerXml, GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, DateTime.Now);

                xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, AuthorizationException.Message, GlobalValues.CONST_ERROR_CODE_XDSAuthorizationException, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);

                //ATNA Event Failure Indicator
                eventOutcomeIndicator = "8";
            }
            catch (Exception ex)
            {
                //Log Error Message
                objRegistryLogic.CreateRegistryLogEntry(xmlDocRequest.InnerXml, GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, DateTime.Now);

                //Generate Error Message
                xmlDocResponse = CommonUtility.ConstructRegistryErrorResponse(GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, string.Empty, ex.Message, GlobalValues.CONST_REGISTRYERROR_CODE_XDSRegistryError, GlobalValues.CONST_SEVERITY_TYPE_ERROR, string.Empty);

                //ATNA Event Failure Indicator
                eventOutcomeIndicator = "8";
            }

            //Record ATNA Event
            objStoredQueryLogic.ProcessRegistryStoredQueryATNAEvent(sourceUserID, destinationUserID, xdsPatientID, adHocQueryElementXml, eventOutcomeIndicator);

            //Create Soap Message
            msgResult = Message.CreateMessage(input.Headers.MessageVersion, GlobalValues.CONST_ACTION_RegistryStoredQueryResponse, new XmlNodeReader(xmlDocResponse));

            return msgResult;
        }

        Message IDocumentRegistry.PatientRegistryRecordAdded(System.ServiceModel.Channels.Message input)
        {
            Message msgResult = null;
            XmlDocument xmlDocRequest = null;
            XmlDocument xmlDocResponse = null;
            XDSResponse xdsResponse = null;
            PatientIdentityFeedLogic patientFeedLogic = null;
            PatientIdentityFeedRecord patientIdentityFeedRecord = null;
            RegistryLogic objRegistryLogic = null;
            string eventOutcomeIndicator = "0";
            string sourceUserID = string.Empty;
            string destinationUserID = string.Empty;

            try
            {
                //ATNA Event - Active Participant Source UserID
                sourceUserID = GetSourceUserID();

                //ATNA Event - Active Participant Destination UserID
                destinationUserID = GetDestinationUserID();

                xmlDocRequest = new XmlDocument();
                xmlDocRequest.Load(input.GetReaderAtBodyContents());

                objRegistryLogic = new RegistryLogic();
                patientFeedLogic = new PatientIdentityFeedLogic();
                xdsResponse = patientFeedLogic.PatientRegistryRecordAdded(xmlDocRequest);

                xmlDocResponse = xdsResponse.XDSResponseDocument;
                eventOutcomeIndicator = xdsResponse.AtnaParameters["$EventIdentification.EventOutcomeIndicator$"];

                //ATNA
                patientIdentityFeedRecord = patientFeedLogic.GetPatient(xmlDocRequest);

            }
            catch (Exception)
            {
                //Log Error
                objRegistryLogic.CreateRegistryLogEntry(xmlDocRequest.InnerXml, GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, DateTime.Now);

                //ATNA Event Failure Indicator
                eventOutcomeIndicator = "8";
            }

            //Log ATNA Event
            if (patientIdentityFeedRecord != null)
            {
                patientFeedLogic.ProcessPatientRecordAddATNAEvent(patientIdentityFeedRecord.PatientUID, sourceUserID, destinationUserID, eventOutcomeIndicator);
            }

            //Create Soap Message
            msgResult = Message.CreateMessage(input.Headers.MessageVersion, GlobalValues.CONST_ACTION_PatientIdentityFeedResponse, new XmlNodeReader(xmlDocResponse));


            return msgResult;
        }

        Message IDocumentRegistry.PatientRegistryRecordRevised(System.ServiceModel.Channels.Message input)
        {
            Message msgResult = null;
            XmlDocument xmlDocRequest = null;
            XmlDocument xmlDocResponse = null;
            XDSResponse xdsResponse = null;
            PatientIdentityFeedLogic patientFeedLogic = null;
            PatientIdentityFeedRecord patientIdentityFeedRecord = null;
            RegistryLogic objRegistryLogic = null;
            string eventOutcomeIndicator = "0";
            string sourceUserID = string.Empty;
            string destinationUserID = string.Empty;

            try
            {
                objRegistryLogic = new RegistryLogic();
                patientFeedLogic = new PatientIdentityFeedLogic();

                //ATNA Event - Active Participant Source UserID
                sourceUserID = GetSourceUserID();

                //ATNA Event - Active Participant Destination UserID
                destinationUserID = GetDestinationUserID();

                xmlDocRequest = new XmlDocument();
                xmlDocRequest.Load(input.GetReaderAtBodyContents());

                xdsResponse = patientFeedLogic.PatientRegistryRecordRevised(xmlDocRequest);
                xmlDocResponse = xdsResponse.XDSResponseDocument;

                //ATNA
                patientIdentityFeedRecord = patientFeedLogic.GetPatient(xmlDocRequest);
                eventOutcomeIndicator = xdsResponse.AtnaParameters["$EventIdentification.EventOutcomeIndicator$"];
            }
            catch
            {
                //Log Error
                objRegistryLogic.CreateRegistryLogEntry(xmlDocRequest.InnerXml, GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, DateTime.Now);

                eventOutcomeIndicator = "8";
            }

            //Log ATNA Event
            if (patientIdentityFeedRecord != null)
            {
                patientFeedLogic.ProcessPatientRecordRevisedATNAEvent(patientIdentityFeedRecord.PatientUID, sourceUserID, destinationUserID, eventOutcomeIndicator);
            }

            //Create Soap Message
            msgResult = Message.CreateMessage(input.Headers.MessageVersion, GlobalValues.CONST_ACTION_PatientIdentityFeedResponse, new XmlNodeReader(xmlDocResponse));


            return msgResult;
        }

        Message IDocumentRegistry.PatientRegistryDuplicatesResolved(System.ServiceModel.Channels.Message input)
        {
            Message msgResult = null;
            XmlDocument xmlDocRequest = null;
            XmlDocument xmlDocResponse = null;
            XDSResponse xdsResponse = null;
            PatientDuplicateEntry patientDuplicateEntry = null;
            PatientIdentityFeedLogic patientFeedLogic = null;
            RegistryLogic objRegistryLogic = null;
            string eventOutcomeIndicator = "0";
            string sourceUserID = string.Empty;
            string destinationUserID = string.Empty;

            try
            {
                //ATNA Event - Active Participant Source UserID
                sourceUserID = GetSourceUserID();

                //ATNA Event - Active Participant Destination UserID
                destinationUserID = GetDestinationUserID();

                xdsResponse = new XDSResponse();                
                xmlDocRequest = new XmlDocument();
                xmlDocRequest.Load(input.GetReaderAtBodyContents());

                objRegistryLogic = new RegistryLogic();
                patientFeedLogic = new PatientIdentityFeedLogic();
                xdsResponse = patientFeedLogic.PatientRegistryDuplicatesResolved(xmlDocRequest, out patientDuplicateEntry);
                xmlDocResponse = xdsResponse.XDSResponseDocument;

                //ATNA
                eventOutcomeIndicator = xdsResponse.AtnaParameters["$EventIdentification.EventOutcomeIndicator$"];

            }
            catch (Exception)
            {
                //Log Error
                objRegistryLogic.CreateRegistryLogEntry(xmlDocRequest.InnerXml, GlobalValues.CONST_RESPONSE_STATUS_TYPE_FAILURE, DateTime.Now);

                eventOutcomeIndicator = "8";
            }

            //Log ATNA Event
            if (patientDuplicateEntry != null)
            {
                patientFeedLogic.ProcessPatientDuplicatesResolvedATNAEvent(patientDuplicateEntry, sourceUserID, destinationUserID, eventOutcomeIndicator);
            }

            //Create Soap Message
            msgResult = Message.CreateMessage(input.Headers.MessageVersion, GlobalValues.CONST_ACTION_PatientIdentityFeedResponse, new XmlNodeReader(xmlDocResponse));


            return msgResult;
        }

        private string GetSourceUserID()
        {
            string sourceUserID = SOURCE_USERID;

            if (OperationContext.Current.Channel.RemoteAddress != null && OperationContext.Current.Channel.RemoteAddress.Uri != null)
                sourceUserID = OperationContext.Current.Channel.RemoteAddress.Uri.OriginalString;
            else if(OperationContext.Current.ServiceSecurityContext != null && OperationContext.Current.ServiceSecurityContext.PrimaryIdentity != null)
                sourceUserID = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;

            return sourceUserID;
        }

        private string GetDestinationUserID()
        {
            string destinationUserID = string.Empty;

            if (OperationContext.Current.Channel.LocalAddress != null && OperationContext.Current.Channel.LocalAddress.Uri != null)
                destinationUserID = OperationContext.Current.Channel.LocalAddress.Uri.OriginalString;
            else
                destinationUserID = ATNAEvent.XDSREGISTRY_SERVICE_ADDRESS;

            return destinationUserID;
        }

    }
}
