using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Configuration;
using System.IO;
using System.Web;
using System.Configuration;
using System.Configuration.Provider;


namespace Microsoft.IHE.XDS.DocumentRepository.StorageProvider
{
   public  class SQLServerStorageService
    {
        private static SQLServerStorageProvider _provider = null;
        private static SQLServerProviderCollection _providers = null;
        private static object _lock = new object();
        public SQLServerStorageProvider Provider
        {
            get
            {
                return _provider;
            }
        }
        public SQLServerProviderCollection Providers
        {
            get
            {
                return _providers;
            }
        }
        public static string SaveDocument(Stream content, string documentname)
        {
            // Make sure a provider is loaded
            LoadProviders();
            // Delegate to the provider
            return _provider.SaveDocument(content, documentname);

        }
        public static Stream RetreiveDocument(string documentstorageid)
        {
            // Make sure a provider is loaded
            LoadProviders();
            // Delegate to the provider
            return _provider.RetrieveDocument(documentstorageid);

        }
        private static void LoadProviders()
        {
            if (_provider == null)
            {
                lock (_lock)
                {
                    if (_provider == null)
                    {
                        SQLProviderSection section = (SQLProviderSection)ConfigurationManager.GetSection("storageprovider");
                        _providers = new SQLServerProviderCollection();
                        ProvidersHelper.InstantiateProviders(section.Providers, _providers, typeof(SQLServerStorageProvider));
                        _provider = _providers[section.DefaultProvider];
                        if (_provider == null)
                        {
                            throw new ProviderException("Unable to load default ImageProvider");
                        }
                    }
                }
            }
        }
    }
}
