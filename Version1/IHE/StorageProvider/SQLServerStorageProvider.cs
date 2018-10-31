using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration.Provider;
using Microsoft.IHE.XDS.DataAccess;
namespace Microsoft.IHE.XDS.DocumentRepository.StorageProvider
{
    public class SQLServerStorageProvider : StorageProviderBase
    {
        #region IStorageProvider Members

        private string _applicationName;

        public string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (string.IsNullOrEmpty(name))
                name = "SQLServerStorageProvider";
            // attribute doesn't exist or is empty
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description",
                "SQLServer Storage provider");
            }
            // Call the base class's Initialize method
            base.Initialize(name, config);
            // Initialize _applicationName

            _applicationName = config["applicationName"];

            if (string.IsNullOrEmpty(_applicationName))
                _applicationName = "/";

            if (config.Count > 0)
            {
                string attr = config.GetKey(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized attribute: " + attr);
            }

        }
        public override string SaveDocument(System.IO.Stream content, string documentname)
        {
            string strStorageUniqueIdentofier = null;
            try
            {
                RepositoryDataAccess objDataBaseLogic = new RepositoryDataAccess();
                strStorageUniqueIdentofier = objDataBaseLogic.SaveDocument(content, documentname);
            }
            catch (Exception ex)
            {

                throw;
            }
            return strStorageUniqueIdentofier;
        }

        public override System.IO.Stream RetrieveDocument(string documentStorageUid)
        {
            System.IO.Stream stream = null;

            try
            {
                RepositoryDataAccess objDataBaseLogic = new RepositoryDataAccess();
                stream = objDataBaseLogic.RetireveDocument(documentStorageUid);
            }
            catch
            {

                throw;
            }
            return stream;
        }

        #endregion
    }
}
