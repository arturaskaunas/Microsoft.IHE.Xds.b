using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Transactions;

using Microsoft.IHE.XDS.Common;
using Microsoft.IHE.XDS.DataAccess;
using Microsoft.IHE.XDS.BusinessLogic.ATNA;

namespace Microsoft.IHE.XDS.BusinessLogic
{
    public class PatientIdentityFeedLogic
    {
        public static readonly string PATIENT_ROOT_EXTENSION_SEPARATOR = "^";
        public static readonly string DATETIME_FORMAT = "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz";
        public static readonly string PATIENT_ACK_DATETIME_FORMAT = "yyyymmddhhmm";
        

        static PatientIdentityFeedLogic()
        {
            if(!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PATIENT_ROOT_EXTENSION_SEPARATOR"]))
                PATIENT_ROOT_EXTENSION_SEPARATOR = ConfigurationManager.AppSettings["PATIENT_ROOT_EXTENSION_SEPARATOR"];
        }

        public XDSResponse PatientRegistryRecordAdded(XmlDocument xmlDocPatientRecordAddedRequest)
        {
            int patientID = 0;
            string resultCode = "CA";
            string eventOutcomeIndicator = "0";
            XDSResponse xdsResponse = null;
            XmlDocument xmlDocResponse = null;
            PatientIdentityFeedRecord patientIdentityFeedRecord = null;
            PatientIdentityFeedDataAccess patientDataAccess = new PatientIdentityFeedDataAccess();

            try
            {
                xdsResponse = new XDSResponse();
                xdsResponse.AtnaParameters = new StringDictionary();

                //Parse Xml & get Patient UID
                patientIdentityFeedRecord = GetPatient(xmlDocPatientRecordAddedRequest);

                //Insert/Feed Patient UID in the DB
                //First validates whether the patient uid already exists in the DB
                if (IsPatientUIDExists(patientIdentityFeedRecord.PatientUID, out patientID))
                {
                    patientIdentityFeedRecord.ResultCode = PatientIdentityFeedResultCode.PATIENT_ALREADY_EXISTS;
                    resultCode = "CE";
                }
                else
                {
                    patientIdentityFeedRecord = patientDataAccess.PatientRegistryRecordAdded(patientIdentityFeedRecord);
                }

            }
            catch (Exception ex)
            {
                patientIdentityFeedRecord.ResultCode = PatientIdentityFeedResultCode.FAILURE;
                resultCode = "CE";
                eventOutcomeIndicator = "8";
            }

            //Generate Response Message
            //xmlDocResponse = ConstructPatientRegistryRecordAddedResponse(patientIdentityFeedRecord);
            if (patientIdentityFeedRecord.ResultCode != PatientIdentityFeedResultCode.SUCCESS)
            {
                resultCode = "CE";
                eventOutcomeIndicator = "8";
            }

            xmlDocResponse = ConstructPatientAcknowledgementMessage(xmlDocPatientRecordAddedRequest, resultCode, "PATIENT-ADD-ACK");

            xdsResponse.XDSResponseDocument = xmlDocResponse;
            xdsResponse.AtnaParameters.Add("$EventIdentification.EventOutcomeIndicator$", eventOutcomeIndicator);

            return xdsResponse;
        }

        public PatientIdentityFeedRecord GetPatient(XmlDocument xmlDocPatientRecordAddedRequest)
        {
            PatientIdentityFeedRecord patientIdentityFeedRecord = null;
            XmlElement root = null;
            XmlNode patientIdNode = null;


            root = xmlDocPatientRecordAddedRequest.DocumentElement;
            patientIdNode = root.SelectSingleNode(".//*[local-name()='controlActProcess']/*[local-name()='subject']/*[local-name()='registrationEvent']/*[local-name()='subject1']/*[local-name()='patient']/*[local-name()=\"id\"]");

            if (patientIdNode == null)
                throw new Exception("Element 'id' is missing in the request xml.");

            if (patientIdNode != null)
            {
                patientIdentityFeedRecord = new PatientIdentityFeedRecord();
                patientIdentityFeedRecord.Root = patientIdNode.Attributes["root"].Value;
                patientIdentityFeedRecord.Extension = patientIdNode.Attributes["extension"].Value;                
            }

            return patientIdentityFeedRecord;
        }

        public bool IsPatientUIDExists(string patientUID, out int patientID)
        {
            patientID = 0;
            RegistryDataAccess registryDataAccess = new RegistryDataAccess();
            return registryDataAccess.IsPatientIdExistinFeed(patientUID, out patientID);

        }


        
        public XDSResponse PatientRegistryRecordRevised(XmlDocument xmlDocRequest)
        {
            XDSResponse xdsResponse = null;
            XmlDocument xmlDocResponse = null;
            PatientIdentityFeedRecord patientIdentityFeedRecord = null;
            string eventOutcomeIndicator = "0";
            string resultCode = "CA";
            int patientID = 0;

            xdsResponse = new XDSResponse();
            xdsResponse.AtnaParameters = new StringDictionary();

            patientIdentityFeedRecord = GetPatient(xmlDocRequest);

            if (patientIdentityFeedRecord != null)
            {
                if (IsPatientUIDExists(patientIdentityFeedRecord.PatientUID, out patientID))
                {
                    resultCode = "CA";
                    eventOutcomeIndicator = "0";
                }
                else
                {
                    resultCode = "CE";
                    eventOutcomeIndicator = "8";
                }
                
            }

            xmlDocResponse = ConstructPatientAcknowledgementMessage(xmlDocRequest, resultCode, "PATIENT-REVISED-ACK");
            xdsResponse.XDSResponseDocument = xmlDocResponse;

            xdsResponse.AtnaParameters.Add("$EventIdentification.EventOutcomeIndicator$", eventOutcomeIndicator);

            return xdsResponse;
        }


        public XDSResponse PatientRegistryDuplicatesResolved(XmlDocument xmlDocRequest, out PatientDuplicateEntry oldPatientDuplicateEntry)
        {
            XDSResponse xdsResponse = null;
            XmlDocument xmlDocResponse = null;            
            PatientDuplicateEntry patientDuplicateEntry = null;
            PatientIdentityFeedDataAccess patientDataAccess = null;
            List<RegistryXmlEntries> registryXmlEntriesList = null;
            RegistryXmlEntries registryXmlEntry = null;
            int newPatientID = 0;
            int oldPatientID = 0;
            string resultCode = "CA";
            string eventOutcomeIndicator = "0";

            oldPatientDuplicateEntry = new PatientDuplicateEntry();

            try
            {
                xdsResponse = new XDSResponse();
                xdsResponse.AtnaParameters = new StringDictionary();
                patientDataAccess = new PatientIdentityFeedDataAccess();
                               
                //Gets the Primary & Replacement Of Patient Entries from the xml
                patientDuplicateEntry = GetPatientDuplicateEntry(xmlDocRequest);
                oldPatientDuplicateEntry = patientDuplicateEntry;

                //New Patient ID
                IsPatientUIDExists(patientDuplicateEntry.NewPatient.PatientUID, out newPatientID);

                //PatientUID does not exist
                if (newPatientID == 0)
                {
                    //Generate Response Message            
                    xmlDocResponse = ConstructPatientAcknowledgementMessage(xmlDocRequest, "CE", "PATIENT-DUPLICATES-RESOLVED-ACK");
                    xdsResponse.XDSResponseDocument = xmlDocResponse;
                    
                    //ATNA
                    eventOutcomeIndicator = "8";
                    xdsResponse.AtnaParameters.Add("$EventIdentification.EventOutcomeIndicator$", eventOutcomeIndicator);

                    return xdsResponse;
                }
                else
                {
                    patientDuplicateEntry.NewPatient.PatientID = newPatientID;
                }

                //Old Patient IDs
                registryXmlEntriesList = new List<RegistryXmlEntries>();

                for (int oldPatientCount = 0; oldPatientCount < patientDuplicateEntry.OldPatientList.Count; oldPatientCount++)
                {
                    IsPatientUIDExists(patientDuplicateEntry.OldPatientList[oldPatientCount].PatientUID, out oldPatientID);

                    //Patient Does Not Exists, Stop processing the request & respond with a failure message
                    if (oldPatientID <= 0)
                    {
                        patientDuplicateEntry.OldPatientList[oldPatientCount].ResultCode = PatientIdentityFeedResultCode.PATIENT_NOT_FOUND;

                        //Generate Response Message            
                        xmlDocResponse = ConstructPatientAcknowledgementMessage(xmlDocRequest, "CE", "PATIENT-DUPLICATES-RESOLVED-ACK");
                        xdsResponse.XDSResponseDocument = xmlDocResponse;

                        //ATNA
                        eventOutcomeIndicator = "8";
                        xdsResponse.AtnaParameters.Add("$EventIdentification.EventOutcomeIndicator$", eventOutcomeIndicator);

                        return xdsResponse;
                    }

                    patientDuplicateEntry.OldPatientList[oldPatientCount].PatientID = oldPatientID;

                    //Get xml entries from registry tables
                    registryXmlEntry = new RegistryXmlEntries();
                    registryXmlEntry = patientDataAccess.GetRegistryXmlEntries(patientDuplicateEntry.OldPatientList[oldPatientCount].PatientID);
                    registryXmlEntriesList.Add(registryXmlEntry);
                }


                if (registryXmlEntriesList != null && registryXmlEntriesList.Count != 0)
                {
                    for (int count = 0; count < registryXmlEntriesList.Count; count++)
                    {
                        registryXmlEntriesList[count].PatientUID = patientDuplicateEntry.NewPatient.PatientUID;
                        registryXmlEntriesList[count] = UpdatePatientUIDInRegistryXmlEntries(registryXmlEntriesList[count]);
                    }
                }

                //Transaction Scope - Start
                using (TransactionScope ts = new TransactionScope())
                {
                    for (int patientCount = 0; patientCount < patientDuplicateEntry.OldPatientList.Count; patientCount++)
                    {
                        //Update the Patient IDs in all the referecing tables
                        patientDataAccess.UpdateRegistryPatientID(patientDuplicateEntry.OldPatientList[patientCount].PatientUID, patientDuplicateEntry.NewPatient.PatientUID);

                        //Delete Patient
                        patientDataAccess.DeleteRegistryPatientUID(patientDuplicateEntry.OldPatientList[patientCount].PatientUID);
                    }

                    for (int registryXmlCount = 0; registryXmlCount < registryXmlEntriesList.Count; registryXmlCount++)
                    {
                        //Update Xml Data stored in tables with new PatientUIDs
                        //TABLES:
                        //TABLE::DocumentEntry
                        //  --  sourcePatientId
                        //  --  extrinsicObjectXML
                        //  ---->   sourcePatientId
                        //  ---->   value (Example: <ExternalIdentifier id="urn:uuid:9afb1c3f-942c-6676-77e2-38fdc3f32a47" registryObject="theDocument" identificationScheme="urn:uuid:58a6f841-87b3-4a3e-92fd-a8ffeff98427" value="$PatientId">)
                        patientDataAccess.UpdateDocumentEntryPatientUID(registryXmlEntriesList[registryXmlCount].PatientUID, registryXmlEntriesList[registryXmlCount].DocumentEntryList);

                        //TABLE::Folder
                        //  --  folderXml
                        //  ---->   value (Example: <ExternalIdentifier id="urn:uuid:2876acb1-e84c-9fe4-d356-f98d6e8afd82" registryObject="Folder" identificationScheme="urn:uuid:f64ffdf0-4b97-4e06-b79f-a52b38ec2f8a" value="$PatientId">)
                        patientDataAccess.UpdateFolderPatientUID(registryXmlEntriesList[registryXmlCount].FolderList);

                        //TABLE::SubmissionSet
                        //  --  submissionSet
                        //  ---->   value (Example: <ExternalIdentifier id="urn:uuid:6e11c871-91c3-0206-9df2-0cb245d2e888" registryObject="SubmissionSet06" identificationScheme="urn:uuid:6b5aea1a-874d-4603-a4bc-96a0a7b38446" value="$PatientId">)
                        patientDataAccess.UpdateSubmissionSetPatientUID(registryXmlEntriesList[registryXmlCount].SubmissionSetList);
                    }

                    //Commit
                    ts.Complete();
                }
                //Transaction Scope - End
                
            }
            catch
            {
                resultCode = "CE";
                eventOutcomeIndicator = "8";
            }

            //ATNA            
            xdsResponse.AtnaParameters.Add("$EventIdentification.EventOutcomeIndicator$", eventOutcomeIndicator);


            //Generate Response Message            
            xmlDocResponse = ConstructPatientAcknowledgementMessage(xmlDocRequest, resultCode, "PATIENT-DUPLICATES-RESOLVED-ACK");
            xdsResponse.XDSResponseDocument = xmlDocResponse;

            return xdsResponse;
        }


        private PatientDuplicateEntry GetPatientDuplicateEntry(XmlDocument xmlDocRequest)
        {
            XmlElement root = null;
            XmlNodeList nodeListOldPatient = null;
            XmlNode nodeNewPatient = null;
            PatientIdentityFeedRecord oldPatientFeedRecord = null;
            PatientDuplicateEntry patientDuplicateEntry = new PatientDuplicateEntry();

            root = xmlDocRequest.DocumentElement;

            //New Patient UID
            nodeNewPatient = root.SelectSingleNode(".//*[local-name()='controlActProcess']/*[local-name()='subject']/*[local-name()='registrationEvent']/*[local-name()='subject1']/*[local-name()='patient']/*[local-name()=\"id\"]");

            if (nodeNewPatient == null)
                throw new Exception("Node registrationEvent does not exists in the request xml.");

            //New Patient
            patientDuplicateEntry.NewPatient = new PatientIdentityFeedRecord();
            patientDuplicateEntry.NewPatient.Root = nodeNewPatient.Attributes["root"].Value;
            patientDuplicateEntry.NewPatient.Extension = nodeNewPatient.Attributes["extension"].Value;

            //Old Patient UIDs
            //Commented below line of code during Europe Connect-a-thon
            //Bug Fixed: Sample xml used during development was incorrect, the new xpath expression gets the patient id
            //nodeListOldPatient = root.SelectNodes(".//*[local-name()=\"replacementOf\"]/*[local-name()=\"priorRegistration\"]/*[local-name()=\"subject2\"]/*[local-name()=\"priorRegisteredRole\"]/*[local-name()=\"id\"]");
            nodeListOldPatient = root.SelectNodes(".//*[local-name()=\"replacementOf\"]/*[local-name()=\"priorRegistration\"]/*[local-name()=\"id\"]");

            if (nodeListOldPatient == null)
                throw new Exception("Node replacementOf or one it's child nodes does not exists in the request xml.");

            //Old Patients
            patientDuplicateEntry.OldPatientList = new List<PatientIdentityFeedRecord>();

            for (int oldPatientCount = 0; oldPatientCount < nodeListOldPatient.Count; oldPatientCount++)
            {
                oldPatientFeedRecord = new PatientIdentityFeedRecord();
                oldPatientFeedRecord.Root = nodeListOldPatient[oldPatientCount].Attributes["root"].Value;
                oldPatientFeedRecord.Extension = nodeListOldPatient[oldPatientCount].Attributes["extension"].Value;

                patientDuplicateEntry.OldPatientList.Add(oldPatientFeedRecord);
            }
                        

            return patientDuplicateEntry;
        }


        private RegistryXmlEntries UpdatePatientUIDInRegistryXmlEntries(RegistryXmlEntries registryXmlEntries)
        {
            XmlDocument xmlDocDocumentEntry = null;
            XmlDocument xmlDocFolder = null;
            XmlDocument xmlDocSubmissionSet = null;
            XmlElement root = null;
            XmlNode node = null;

            if (registryXmlEntries == null)
                throw new ArgumentNullException("registryXmlEntries");

            if (string.IsNullOrEmpty(registryXmlEntries.PatientUID))
                throw new Exception("registryXmlEntries.PatientUID cannot be null or empty.");


            //TABLE::DocumentEntry

            for (int documentEntryCount = 0; documentEntryCount < registryXmlEntries.DocumentEntryList.Count; documentEntryCount++)
            {
                xmlDocDocumentEntry = new XmlDocument();
                xmlDocDocumentEntry.LoadXml(registryXmlEntries.DocumentEntryList[documentEntryCount].ExtrinsicObjectXML);
                root = xmlDocDocumentEntry.DocumentElement;

                //ExtrinsicObject/Slot(With name attribute equal to 'sourcePatientId')/ValueList/Value
                node = root.SelectSingleNode("//*[local-name()='ExtrinsicObject']/*[local-name()='Slot'][@name=\"sourcePatientId\"]/*[local-name()='ValueList']/*[local-name()='Value']");

                //Updating sourcePatientId value
                if (node != null)
                {
                    node.InnerText = registryXmlEntries.PatientUID;
                }

                //ExtrinsicObject/ExternalIdentifier(With identificationScheme='urn:uuid:58a6f841-87b3-4a3e-92fd-a8ffeff98427')/value=**PATIENTUID**
                node = root.SelectSingleNode("//*[local-name()='ExtrinsicObject']/*[local-name()='ExternalIdentifier'][@identificationScheme=\"urn:uuid:58a6f841-87b3-4a3e-92fd-a8ffeff98427\"]");

                if (node != null)
                {
                    node.Attributes["value"].Value = registryXmlEntries.PatientUID;

                    node = root.SelectSingleNode("//*[local-name()='ExtrinsicObject']");
                    registryXmlEntries.DocumentEntryList[documentEntryCount].ExtrinsicObjectXML = node.OuterXml;
                }
            }

            //TABLE::DocumentEntry

            //TABLE::Folder

            for (int folderCount = 0; folderCount < registryXmlEntries.FolderList.Count; folderCount++)
            {
                xmlDocFolder = new XmlDocument();
                xmlDocFolder.LoadXml(registryXmlEntries.FolderList[folderCount].FolderXml);
                root = xmlDocFolder.DocumentElement;

                //RegistryPackage/ExternalIdentifier(With identificationScheme='urn:uuid:f64ffdf0-4b97-4e06-b79f-a52b38ec2f8a')/value=**PATIENTUID**
                node = root.SelectSingleNode("//*[local-name()='RegistryPackage']/*[local-name()='ExternalIdentifier'][@identificationScheme=\"urn:uuid:f64ffdf0-4b97-4e06-b79f-a52b38ec2f8a\"]");

                if (node != null)
                {
                    node.Attributes["value"].Value = registryXmlEntries.PatientUID;

                    node = root.SelectSingleNode("//*[local-name()='RegistryPackage']");
                    registryXmlEntries.FolderList[folderCount].FolderXml = node.OuterXml;
                }
            }

            //TABLE::Folder

            //TABLE::SubmissionSet

            for (int submissionSetCount = 0; submissionSetCount < registryXmlEntries.SubmissionSetList.Count; submissionSetCount++)
            {
                xmlDocSubmissionSet = new XmlDocument();
                xmlDocSubmissionSet.LoadXml(registryXmlEntries.SubmissionSetList[submissionSetCount].SubmissionSetXml);
                root = xmlDocSubmissionSet.DocumentElement;

                //RegistryPackage/ExternalIdentifier(With identificationScheme='urn:uuid:6b5aea1a-874d-4603-a4bc-96a0a7b38446')/value=**PATIENTUID**
                node = root.SelectSingleNode("//*[local-name()='RegistryPackage']/*[local-name()='ExternalIdentifier'][@identificationScheme=\"urn:uuid:6b5aea1a-874d-4603-a4bc-96a0a7b38446\"]");

                if (node != null)
                {
                    node.Attributes["value"].Value = registryXmlEntries.PatientUID;

                    node = root.SelectSingleNode("//*[local-name()='RegistryPackage']");
                    registryXmlEntries.SubmissionSetList[submissionSetCount].SubmissionSetXml = node.OuterXml;

                }
            }

            //TABLE::SubmissionSet


            return registryXmlEntries;
        }

        
        public XmlDocument ConstructPatientAcknowledgementMessage(XmlDocument requestMessage, string resultCode, string patientMessageKey)
        {
            XmlDocument xmlDocResponse = null;
            PatientIdentityFeedDataAccess patientDataAccess = null;
            PatientMessageConfiguration patientMsgConfig = null;
            XmlElement root = null;
            XmlNode node = null;
            string originalMessageID = null;
            string interactionIDRoot = null;
            string receiverRoot = null;
            string senderRoot = null;


            //Get Patient Record Added Acknowledgement Message
            patientDataAccess = new PatientIdentityFeedDataAccess();
            patientMsgConfig = patientDataAccess.GetPatientMessageConfiguration(patientMessageKey);

            root = requestMessage.DocumentElement;
            
            //id
            node = root.SelectSingleNode(".//*[local-name()='id']");
            originalMessageID = node.Attributes["root"].Value;

            //interactionId\root
            node = root.SelectSingleNode(".//*[local-name()='interactionId']");
            interactionIDRoot = node.Attributes["root"].Value;

            //receiver
            node = root.SelectSingleNode(".//*[local-name()='receiver']/*[local-name()='device']/*[local-name()='id']");
            receiverRoot = node.Attributes["root"].Value;

            //sender
            node = root.SelectSingleNode(".//*[local-name()='sender']/*[local-name()='device']/*[local-name()='id']");
            senderRoot = node.Attributes["root"].Value;

            //Replace Values
            //$NEW.GUID$
            patientMsgConfig.PatientMessageValue = patientMsgConfig.PatientMessageValue.Replace("$NEW.GUID$", Guid.NewGuid().ToString());

            //$creationTime$
            patientMsgConfig.PatientMessageValue = patientMsgConfig.PatientMessageValue.Replace("$creationTime$", DateTime.Now.ToString(PATIENT_ACK_DATETIME_FORMAT));

            //$INTERACTION.ID$
            patientMsgConfig.PatientMessageValue = patientMsgConfig.PatientMessageValue.Replace("$INTERACTION.ID$", interactionIDRoot);

            //$RECEIVER.ROOT$ - Swap Sender & Receiver fromthe original message
            patientMsgConfig.PatientMessageValue = patientMsgConfig.PatientMessageValue.Replace("$RECEIVER.ROOT$", senderRoot);

            //$SENDER.ROOT$ - Swap Sender & Receiver fromthe original message
            patientMsgConfig.PatientMessageValue = patientMsgConfig.PatientMessageValue.Replace("$SENDER.ROOT$", receiverRoot);

            //$RESULT.CODE$
            patientMsgConfig.PatientMessageValue = patientMsgConfig.PatientMessageValue.Replace("$RESULT.CODE$", resultCode);

            //$ORIGINAL.MESSAGE.ID$
            patientMsgConfig.PatientMessageValue = patientMsgConfig.PatientMessageValue.Replace("$ORIGINAL.MESSAGE.ID$", originalMessageID);

            xmlDocResponse = new XmlDocument();
            xmlDocResponse.LoadXml(patientMsgConfig.PatientMessageValue);

            return xmlDocResponse;
        }


        




        public void ProcessPatientRecordAddATNAEvent(string xdsPatientUID, string sourceUserID, string destinationUserID, string eventOutcomeIndicator)
        {
            try
            {
                //Log ATNA - Repository Event
                ATNALogic atnaLogic = new ATNALogic();
                AuditMessageConfiguration auditMsgConfig = null;

                auditMsgConfig = atnaLogic.GetAuditMessageConfigurationDetails("REGISTRY-PATIENT-RECORD-ADD-ITI-44");

                if (auditMsgConfig != null)
                {

                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$XDSPatientID$", xdsPatientUID);

                    //$ActiveParticipant.UserID.Source$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Source$", sourceUserID);

                    //$ActiveParticipant.UserID.Destination$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Destination$", destinationUserID);

                    atnaLogic.ProcessEvent(auditMsgConfig, ATNAEvent.XDSREGISTRY_TYPE, eventOutcomeIndicator, ATNAEvent.UDP_TAG_APPNAME_REGISTRY);
                }
            }
            catch
            {
                //Oops...ATNA event failed...probably UDP failure....can't afford to stop XDSRepository service sorry :(
            }
        }

        public void ProcessPatientRecordRevisedATNAEvent(string xdsPatientUID, string sourceUserID, string destinationUserID, string eventOutcomeIndicator)
        {
            try
            {
                //Log ATNA - Repository Event
                ATNALogic atnaLogic = new ATNALogic();
                AuditMessageConfiguration auditMsgConfig = null;

                auditMsgConfig = atnaLogic.GetAuditMessageConfigurationDetails("REGISTRY-PATIENT-RECORD-REVISED-ITI-44");

                if (auditMsgConfig != null)
                {

                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$XDSPatientID$", xdsPatientUID);

                    //$ActiveParticipant.UserID.Source$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Source$", sourceUserID);

                    //$ActiveParticipant.UserID.Destination$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Destination$", destinationUserID);

                    atnaLogic.ProcessEvent(auditMsgConfig, ATNAEvent.XDSREGISTRY_TYPE, eventOutcomeIndicator, ATNAEvent.UDP_TAG_APPNAME_REGISTRY);
                }
            }
            catch
            {
                //Oops...ATNA event failed...probably UDP failure....can't afford to stop XDSRepository service sorry :(
            }
        }


        public void ProcessPatientDuplicatesResolvedATNAEvent(PatientDuplicateEntry patientDuplicateEntry, string sourceUserID, string destinationUserID, string eventOutcomeIndicator)
        {
            try
            {
                //Log ATNA - Repository Event
                ATNALogic atnaLogic = new ATNALogic();
                AuditMessageConfiguration auditMsgConfig = null;
                PatientIdentityFeedRecord patientNotFound = null;

                if (patientDuplicateEntry != null)
                {
                    patientNotFound = patientDuplicateEntry.OldPatientList.Find(
                        delegate(PatientIdentityFeedRecord patFeedRecord)
                        {
                            if (patFeedRecord.ResultCode == PatientIdentityFeedResultCode.PATIENT_NOT_FOUND)
                                return true;

                            return false;
                        }
                        );

                    if (patientNotFound != null)
                        eventOutcomeIndicator = "8";
                }

                //Patient Add/Update ATNA Event
                auditMsgConfig = atnaLogic.GetAuditMessageConfigurationDetails("REGISTRY-PATIENT-RECORD-DUPLICATES-RESOLVED-UPDATE-ITI-44");

                if (auditMsgConfig != null)
                {
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$XDSPatientID$", patientDuplicateEntry.NewPatient.PatientUID);

                    //$ActiveParticipant.UserID.Source$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Source$", sourceUserID);

                    //$ActiveParticipant.UserID.Destination$
                    auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Destination$", destinationUserID);

                    //New Patient Add Event
                    atnaLogic.ProcessEvent(auditMsgConfig, ATNAEvent.XDSREGISTRY_TYPE, eventOutcomeIndicator, ATNAEvent.UDP_TAG_APPNAME_REGISTRY);
                }

                //Patient Delete ATNA Event
                auditMsgConfig = atnaLogic.GetAuditMessageConfigurationDetails("REGISTRY-PATIENT-RECORD-DUPLICATES-RESOLVED-DELETE-ITI-44");

                if ((auditMsgConfig != null) && (patientNotFound == null))
                {
                    //ATNA Log for all the deleted patient uids
                    for (int oldPatientCount = 0; oldPatientCount < patientDuplicateEntry.OldPatientList.Count; oldPatientCount++)
                    {
                        auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$XDSPatientID$", patientDuplicateEntry.OldPatientList[oldPatientCount].PatientUID);

                        //$ActiveParticipant.UserID.Destination$
                        auditMsgConfig = ATNALogic.SetParameterValue(auditMsgConfig, "$ActiveParticipant.UserID.Destination$", ATNAEvent.XDSREGISTRY_SERVICE_ADDRESS);

                        //Old Patient Delete Event
                        atnaLogic.ProcessEvent(auditMsgConfig, ATNAEvent.XDSREGISTRY_TYPE, "0", ATNAEvent.UDP_TAG_APPNAME_REGISTRY);
                    }
                }

            }
            catch
            {
                //Oops...ATNA event failed...probably UDP failure....can't afford to stop XDSRepository service sorry :(                
            }
        }

    }
}
