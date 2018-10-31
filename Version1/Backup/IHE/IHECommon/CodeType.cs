using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class CodeType
    {
        private int codeTypeID;

        public int CodeTypeID
        {
            get { return codeTypeID; }
            set { codeTypeID = value; }
        }

        private string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private string classSchemeUUID;

        public string ClassSchemeUUID
        {
            get { return classSchemeUUID; }
            set { classSchemeUUID = value; }
        }

        private string codingScheme;

        public string CodingScheme
        {
            get { return codingScheme; }
            set { codingScheme = value; }
        }

        private List<CodeValue> lstCodeValue;

        public List<CodeValue> CodeValues
        {
            get { return lstCodeValue; }
            set { lstCodeValue = value; }
        }
	

    }
}
