using System;
using System.Xml;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Microsoft.IHE.XDS.Common
{
    public class XDSResponse
    {

        List<XDSError> lstXDSError = null;
        public List<XDSError> XDSErrorList
        {
            get { return lstXDSError; }
            set { lstXDSError = value; }
        }

        StringDictionary sdAtnaParameters = null;
        public StringDictionary AtnaParameters
        {
            get
            {
                if (sdAtnaParameters == null)
                    sdAtnaParameters = new StringDictionary();

                return sdAtnaParameters;
            }

            set
            {
                sdAtnaParameters = value;
            }
        }

        XmlDocument xmlDocXDSResponse = null;
        public XmlDocument XDSResponseDocument
        {
            get { return xmlDocXDSResponse; }
            set { xmlDocXDSResponse = value; }
        }
    }
}
