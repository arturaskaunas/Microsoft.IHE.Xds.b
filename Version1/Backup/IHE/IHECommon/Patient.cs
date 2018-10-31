using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class Patient
    {
        private string uniqueID;

        public string PatientUID
        {
            get { return uniqueID; }
            set { uniqueID = value; }
        }

        private int id;

        public int PatientID
        {
            get { return id; }
            set { id = value; }
        }

    }
}
