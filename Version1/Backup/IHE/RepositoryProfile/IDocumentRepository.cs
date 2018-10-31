using System.Diagnostics;
using System.Web.Services;
using System.ComponentModel;
using System.Web.Services.Protocols;
using System;
using System.Xml.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.IHE.XDS.DocumentRepository
{
    [ServiceContract(Namespace = "urn:ihe:iti:xds-b:2007", Name = "XDSRepository")]
    public interface IDocumentRepository
    {
        [OperationContract(Action = "urn:ihe:iti:2007:ProvideAndRegisterDocumentSet-b",
                           ReplyAction = "urn:ihe:iti:2007:ProvideAndRegisterDocumentSet-bResponse")]
        Message ProvideAndRegisterDocumentSet(Message input);

        [OperationContract(Action = "urn:ihe:iti:2007:RetrieveDocumentSet",
                           ReplyAction = "urn:ihe:iti:2007:RetrieveDocumentSetResponse")]
        Message RetrieveDocumentSet(Message input);
    }

}
