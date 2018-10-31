using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class StoredQuery
    {
        int storedQueryID;
        public int StoredQueryID
        {
            get
            {
                return storedQueryID;
            }
            set
            {
                storedQueryID = value;
            }
        }

        string storedQueryName;
        public string StoredQueryName
        {
            get
            {
                return storedQueryName;
            }
            set
            {
                storedQueryName = value;
            }
        }


        string storedQueryUniqueID;
        public string StoredQueryUniqueID
        {
            get
            {
                return storedQueryUniqueID;
            }
            set
            {
                storedQueryUniqueID = value;
            }
        }

        string storedQuerySQLCode;
        public string StoredQuerySQLCode
        {
            get
            {
                return storedQuerySQLCode;
            }
            set
            {
                storedQuerySQLCode = value;
            }
        }

        int storedQuerySequence;
        public int StoredQuerySequence
        {
            get
            {
                return storedQuerySequence;
            }
            set
            {
                storedQuerySequence = value;
            }
        }

        bool returnComposedObjects;
        public bool ReturnComposedObjects
        {
            get
            {
                return returnComposedObjects;
            }
            set
            {
                returnComposedObjects = value;
            }
        }


        string returnType;
        public string ReturnType
        {
            get
            {
                return returnType;
            }
            set
            {
                returnType = value;
            }
        }

        List<StoredQueryParameter> parameterList = new List<StoredQueryParameter>();
        public List<StoredQueryParameter> ParameterList
        {
            get
            {
                return parameterList;
            }
            set
            {
                parameterList = value;
            }
        }



    }

}
