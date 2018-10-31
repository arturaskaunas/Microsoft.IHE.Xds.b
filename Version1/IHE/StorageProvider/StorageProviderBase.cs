using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration.Provider;

namespace Microsoft.IHE.XDS.DocumentRepository.StorageProvider
{
    /// <summary>
    /// This interface defines the basic operations that should be implemented by a class
    /// that works as a Storage Provider for an XDS Repository
    /// </summary>
    public abstract class StorageProviderBase:ProviderBase
    {
        /// <summary>
        /// Saves a document content withint the Storage Provider
        /// </summary>
        /// <param name="content">A binary stream representing the content of the document.</param>
        /// <param name="documentname">The name of the document</param>
        /// <returns>Returns an string representing the Unique Identifier of the document
        /// within the Storage Provider.</returns>
        public abstract string SaveDocument(System.IO.Stream content, string documentname);
        
        /// <summary>
        /// Retrieves a document from the Storage provider.
        /// </summary>
        /// <param name="documentstorageuid">
        /// A string representing the unique identifer of the document
        /// within the Storage Provider
        /// </param>
        /// <returns>A binary stream representing the content of the document.</returns>
        public abstract System.IO.Stream RetrieveDocument(string documentstorageuid);
    }
}
