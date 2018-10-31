using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace Microsoft.IHE.XDS.DocumentRegistry.XDSDocumentRegistryServiceHost
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Microsoft.IHE.XDS.ScriptedInstaller
    {
        public ProjectInstaller()
        {
            InitializeService("XDSDocumentRegistryServiceHost", "XDS.b Document Registry Service", "XDS.b Document Registry Service");
        }
    }
}