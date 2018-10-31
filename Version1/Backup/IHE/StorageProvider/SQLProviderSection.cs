using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;


namespace Microsoft.IHE.XDS.DocumentRepository.StorageProvider
{
    public class SQLProviderSection:ConfigurationSection
    {
        
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get
            {
                return (ProviderSettingsCollection)this["providers"];
            }
        }
        
        [StringValidator(MinLength = 1), ConfigurationProperty("defaultProvider", DefaultValue = "SQLServerStorageProvider")]
        public string DefaultProvider
        {
            get { return (string)this["defaultProvider"]; }
            set { this["defaultProvider"] = value; }
        }
    }
}
