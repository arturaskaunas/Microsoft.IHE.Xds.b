using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Microsoft.IHE.XDS.Common
{
    public class DocumentEntry : SubmitObjectBase
    {
        private List<Author> documentAuthor;

        public List<Author> DocumentAuthor
        {
            get { return documentAuthor; }
            set { documentAuthor = value; }
        }

        private CodeValue classCode;

        public CodeValue ClassCode
        {
            get { return classCode; }
            set { classCode = value; }
        }

        private CodeValue confidentialityCode;

        public CodeValue ConfidentialityCode
        {
            get { return confidentialityCode; }
            set { confidentialityCode = value; }
        }

        private DateTime creationTime;

        public DateTime CreationTime
        {
            get { return creationTime; }
            set { creationTime = value; }
        }

        private CodeValue formatCode;

        public CodeValue FormatCode
        {
            get { return formatCode; }
            set { formatCode = value; }
        }

        private string hash;

        public string Hash
        {
            get { return hash; }
            set { hash = value; }
        }

        private CodeValue healthcareFacilityCode;

        public CodeValue HealthcareFacilityCode
        {
            get { return healthcareFacilityCode; }
            set { healthcareFacilityCode = value; }
        }

        private CodeValue languageCode;

        public CodeValue LanguageCode
        {
            get { return languageCode; }
            set { languageCode = value; }
        }

        private string legalAuthenticator;

        public string LegalAuthenticator
        {
            get { return legalAuthenticator; }
            set { legalAuthenticator = value; }
        }

        private string mimeType;

        public string MimeType
        {
            get { return mimeType; }
            set { mimeType = value; }
        }

        private string parentDocumentID;

        public string ParentDocumentID
        {
            get { return parentDocumentID; }
            set { parentDocumentID = value; }
        }

        private string parentDocumentRelationship;

        public string ParentDocumentRelationship
        {
            get { return parentDocumentRelationship; }
            set { parentDocumentRelationship = value; }
        }

        private Patient documentPatient;

        public Patient DocumentPatient
        {
            get { return documentPatient; }
            set { documentPatient = value; }
        }

        private CodeValue practiceSettingsCode;

        public CodeValue PracticeSettingsCode
        {
            get { return practiceSettingsCode; }
            set { practiceSettingsCode = value; }
        }

        private DateTime serviceStartTime = DateTime.Now;

        public DateTime ServiceStartTime
        {
            get { return serviceStartTime; }
            set { serviceStartTime = value; }
        }

        private DateTime serviceStopTime = DateTime.MaxValue;

        public DateTime ServiceStopTime
        {
            get { return serviceStopTime; }
            set { serviceStopTime = value; }
        }

        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        private string sourcePatientID;

        public string SourcePatientID
        {
            get { return sourcePatientID; }
            set { sourcePatientID = value; }
        }

        private string sourcePatientInfo;

        public string SourcePatientInfo
        {
            get { return sourcePatientInfo; }
            set { sourcePatientInfo = value; }
        }

        private CodeValue typeCode;

        public CodeValue TypeCode
        {
            get { return typeCode; }
            set { typeCode = value; }
        }

        private string uri;

        public string URI
        {
            get { return uri; }
            set { uri = value; }
        }

        private Stream content;

        public Stream Content
        {
            get { return content; }
            set { content = value; }
        }

        private List<CodeValue> eventCodeList;

        public List<CodeValue> EventCodeList
        {
            get { return eventCodeList; }
            set { eventCodeList = value; }
        }

        private string repositoryUniqueID;

        public string RepositoryUniqueID
        {
            get
            {
                return repositoryUniqueID;
            }
            set
            {
                repositoryUniqueID = value;
            }
        }


        string extrinsicObjectXML;
        public string ExtrinsicObjectXML
        {
            get
            {
                return extrinsicObjectXML;
            }
            set
            {
                extrinsicObjectXML = value;
            }
        }

        private string associationXml;
        public string AssociationXml
        {
            get { return associationXml; }
            set { associationXml = value; }
        }

        //private string sourceObjectID;
        //public string SourceObjectID
        //{
        //    get { return sourceObjectID; }
        //    set { sourceObjectID = value; }
        //}

        //private string targetObjectID;
        //public string TargetObjectID
        //{
        //    get { return targetObjectID; }
        //    set { targetObjectID = value; }
        //}

    }
}
