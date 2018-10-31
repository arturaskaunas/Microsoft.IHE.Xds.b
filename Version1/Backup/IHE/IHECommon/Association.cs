using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class Association
    {

        private string uniqueID;
        public string UniqueID
        {
            get { return uniqueID; }
            set { uniqueID = value; }
        }

        private string associationType;
        public string AssociationType
        {
            get { return associationType; }
            set { associationType = value; }
        }

        private string sourceObject;
        public string SourceObject
        {
            get { return sourceObject; }
            set { sourceObject = value; }
        }

        private string targetObject;
        public string TargetObject
        {
            get { return targetObject; }
            set { targetObject = value; }
        }

        private string associationXml;
        public string AssociationXml
        {
            get { return associationXml; }
            set { associationXml = value; }
        }
        

    }
}
