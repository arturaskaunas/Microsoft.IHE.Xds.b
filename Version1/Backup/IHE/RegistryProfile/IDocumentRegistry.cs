using System.Diagnostics;
using System.Web.Services;
using System.ComponentModel;
using System.Web.Services.Protocols;
using System;
using System.Xml.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.IHE.XDS.DocumentRegistry
{
    [ServiceContract(Namespace = "urn:ihe:iti:xds-b:2007", Name = "XDSRegistry")]
    public interface IDocumentRegistry
    {
        [OperationContract(Action = "urn:ihe:iti:2007:RegistryStoredQuery",
                           ReplyAction = "urn:ihe:iti:2007:RegistryStoredQueryResponse")]        
        Message RegistryStoredQuery(Message input);

        [OperationContract(Action = "urn:ihe:iti:2007:RegisterDocumentSet-b",
                           ReplyAction = "urn:ihe:iti:2007:RegisterDocumentSet-bResponse")]
        Message RegisterDocumentSet(Message input);

        #region Patient Identity Feed HL7v3
        [OperationContract(Action = "urn:hl7-org:v3:PRPA_IN201301UV",
                           ReplyAction = "urn:hl7-org:v3:MCCI_IN000002UV01")]
        Message PatientRegistryRecordAdded(Message input);

        [OperationContract(Action = "urn:hl7-org:v3:PRPA_IN201302UV",
                          ReplyAction = "urn:hl7-org:v3:MCCI_IN000002UV01")]
        Message PatientRegistryRecordRevised(Message input);

        [OperationContract(Action = "urn:hl7-org:v3:PRPA_IN201304UV",
                          ReplyAction = "urn:hl7-org:v3:MCCI_IN000002UV01")]
        Message PatientRegistryDuplicatesResolved(Message input);
        #endregion
    }
}
