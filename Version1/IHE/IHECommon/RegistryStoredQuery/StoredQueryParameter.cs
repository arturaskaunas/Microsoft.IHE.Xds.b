using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IHE.XDS.Common
{
    public class StoredQueryParameter
    {
        int storedQueryParameterID;
        public int StoredQueryParameterID
        {
            get
            {
                return storedQueryParameterID;
            }
            set
            {
                storedQueryParameterID = value;
            }
        }

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



        string parameterName = string.Empty;
        public string ParameterName
        {
            get
            {
                return parameterName;
            }
            set
            {
                parameterName = value;
            }
        }



        string parameterValue = string.Empty;
        public string ParameterValue
        {
            get
            {
                return parameterValue;
            }
            set
            {
                parameterValue = value;
            }
        }

        string attribute = null;
        public string Attribute
        {
            get
            {
                return attribute;
            }
            set
            {
                attribute = value;
            }
        }

        bool isMandatory;
        public bool IsMandatory
        {
            get
            {
                return isMandatory;
            }
            set
            {
                isMandatory = value;
            }
        }


        bool isMultiple;
        public bool IsMultiple
        {
            get
            {
                return isMultiple;
            }
            set
            {
                isMultiple = value;
            }
        }

        string dependentParameterName = string.Empty;
        public string DependentParameterName
        {
            get
            {
                return dependentParameterName;
            }
            set
            {
                dependentParameterName = value;
            }
        }

        string tableName = null;
        public string TableName
        {
            get
            {
                return tableName;
            }
            set
            {
                tableName = value;
            }
        }

        string joinConditionSQLCode = null;
        public string JoinConditionSQLCode
        {
            get
            {
                return joinConditionSQLCode;
            }
            set
            {
                joinConditionSQLCode = value;
            }
        }

        string whereConditionSQLCode = null;
        public string WhereConditionSQLCode
        {
            get
            {
                return whereConditionSQLCode;
            }
            set
            {
                whereConditionSQLCode = value;
            }
        }

        string sqlCode = string.Empty;
        public string SQLCode
        {
            get
            {
                return sqlCode;
            }
            set
            {
                sqlCode = value;
            }
        }

        int sequenceNumber;
        public int SequenceNumber
        {
            get
            {
                return sequenceNumber;
            }
            set
            {
                sequenceNumber = value;
            }
        }


        bool isParameterSupplied = false;
        public bool IsParameterSupplied
        {
            get
            {
                return isParameterSupplied;
            }
            set
            {
                isParameterSupplied = value;
            }
        }

    }
}
