using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Collections.Specialized;
using System.ServiceProcess;
using Microsoft.Win32;

namespace Microsoft.IHE.XDS.DocumentRepository.XDSDocumentRepositoryServiceHost
{
    [RunInstaller(true)]
    public class ProjectInstaller : Microsoft.IHE.XDS.ScriptedInstaller
    {
        public ProjectInstaller()
        {
            InitializeService("XDSDocumentRepositoryServiceHost","XDS.b Document Repository Service", "XDS.b Document Repository Service");
        }
    }
}