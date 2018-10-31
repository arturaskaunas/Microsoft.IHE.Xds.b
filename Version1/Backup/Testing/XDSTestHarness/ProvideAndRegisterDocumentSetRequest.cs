using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;


namespace Microsoft.XDS.Test
{
    /// <summary>
    /// This class is an example on how to use custom serialisation in order
    /// to enable MTOM compression within a WCF Service.
    /// It is intended to be used with the XDS.b ProvideAndRegisterDocumentSet operation
    /// </summary>
    [XmlRoot(Namespace = "urn:ihe:iti:xds-b:2007")] // Notice that the code decoration is used to define the namespace.
    public class ProvideAndRegisterDocumentSetRequest : IXmlSerializable
    {
        private XmlNode documentMetadata;

        /// <summary>
        /// DocumentMetadata is an XMLNode that contains all the metadata related to a submission set
        /// In a real world example, this would be an object and serialisation would transform its
        /// properties into the right ebXML Registry schema
        /// </summary>
        public XmlNode DocumentMetadata
        {
            get { return documentMetadata; }
            set { documentMetadata = value; }
        }

        private List<IHEDocument> documents;

        /// <summary>
        /// Documents contains a list of Documents to be added to the request.
        /// In a real world example, validation to find out if the document ID is valid should be
        /// executed before any document would be added to the List.
        /// </summary>
        public List<IHEDocument> Documents
        {
            get { return documents; }
            set { documents = value; }
        }

        public ProvideAndRegisterDocumentSetRequest()
        {
            Documents = new List<IHEDocument>();
        }

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// This method gets a XMLRepresentation of this class and convert it to the document
        /// </summary>
        /// <param name="reader">an XMLRepresentation of this Class</param>
        public void ReadXml(XmlReader reader)
        {
            // The simplest way to make this work is to convert the reader into an XML Document
            // and parse its nodes.
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(reader.ReadOuterXml());
            DocumentMetadata = doc.SelectSingleNode(@".//*[local-name()='SubmitObjectsRequest']");
            XmlNodeList docs = doc.SelectNodes(@".//*[local-name()='Document']");
            foreach (XmlNode item in docs)
            {
                IHEDocument iheDoc = new IHEDocument();
                iheDoc.DocumentID = item.Attributes["id"].Value.ToString();
                iheDoc.Document = Convert.FromBase64String(item.InnerText);
                documents.Add(iheDoc);
            }
        }

        /// <summary>
        /// This method generates a XML Representation of this class from the existing data.
        /// </summary>
        /// <param name="writer">The location where the XML Representation will be generated.</param>
        public void WriteXml(XmlWriter writer)
        {
            //First we write the whole DocumentMetadata Node into the Root Element
            //Notice that the Root Element is already created internally by .NET
            //taking in consideration the code decoration used on the class definition.
            writer.WriteNode(DocumentMetadata.CreateNavigator(), true);
            //Afte the Document Metadata is created, each document is created in sequence.
            foreach (IHEDocument doc in Documents)
            {
                writer.WriteStartElement("Document");
                writer.WriteAttributeString("id", doc.DocumentID);
                writer.WriteBase64(doc.Document, 0, doc.Document.Length);
                writer.WriteEndElement();
            }
        }

        #endregion
    }
    /// <summary>
    /// This is a helper class to store information about IHE Documents.
    /// </summary>
    public class IHEDocument
    {
        private string documentID;

        public string DocumentID
        {
            get { return documentID; }
            set { documentID = value; }
        }
        private byte[] document;

        public byte[] Document
        {
            get { return document; }
            set { document = value; }
        }
    }
}
