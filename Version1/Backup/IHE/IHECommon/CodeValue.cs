using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class CodeValue
    {
        private int codeValueID;

        public int CodeValueID
        {
            get { return codeValueID; }
            set { codeValueID = value; }
        }


        private int codeTypeID;

        public int CodeTypeID
        {
            get { return codeTypeID; }
            set { codeTypeID = value; }
        }

        private string codeValue;

        public string Value
        {
            get { return codeValue; }
            set { codeValue = value; }
        }

        private string codingScheme;

        public string CodingScheme
        {
            get { return codingScheme; }
            set { codingScheme = value; }
        }
	
	
    }
}
