using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration.Provider;
using System.Configuration;

namespace Microsoft.IHE.XDS.DocumentRepository.StorageProvider
{
    public class SQLServerProviderCollection:ProviderCollection
    {
        public new SQLServerStorageProvider this[string name]
        {
            get
            {
                return (SQLServerStorageProvider)base[name];
            }
        }
        public override void Add(System.Configuration.Provider.ProviderBase provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            //If Not TypeOf provider Is MyProviderBase Then Throw New ArgumentException("Invalid provider type", "provider")

            //' add
            //MyBase.Add(provider)
            if (!(provider is SQLServerStorageProvider))
                throw new ArgumentException("Invalid provider type", "provider");
            base.Add(provider);
        }
    }
}
