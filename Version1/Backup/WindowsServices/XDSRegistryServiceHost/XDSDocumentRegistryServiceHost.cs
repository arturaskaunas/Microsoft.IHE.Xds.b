using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using Microsoft.IHE.XDS.DocumentRegistry;
using Microsoft.IHE.XDS.BusinessLogic.ATNA;

namespace Microsoft.IHE.XDS.DocumentRegistry.XDSDocumentRegistryServiceHost
{
    
    partial class XDSDocumentRegistryServiceHost : ServiceBase
    {
        ServiceHost _serviceHost = null;

        public XDSDocumentRegistryServiceHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string eventOutcomeIndicator = "0";

            try
            {
                _serviceHost = new ServiceHost(typeof(DocumentRegistryService));

                _serviceHost.Open();
            }
            catch
            {
                //Failure
                eventOutcomeIndicator = "8";
            }

            try
            {
                //Log ATNA - Registry Application Start Event
                ATNALogic atnaLogic = new ATNALogic();
                atnaLogic.ProcessEvent("REGISTRY-APP-START-ITI-20", ATNAEvent.XDSREGISTRY_TYPE, eventOutcomeIndicator, ATNAEvent.UDP_TAG_APPNAME_REGISTRY);
            }
            catch
            {
                //Oops...ATNA event failed...probably UDP failure....can't afford to mess up with XDSRegistry service sorry :(
            }
        }

        protected override void OnStop()
        {
            string eventOutcomeIndicator = "0";

            try
            {
                if (_serviceHost != null && _serviceHost.State != CommunicationState.Closed)
                    _serviceHost.Close();
            }
            catch
            {
                //Failure
                eventOutcomeIndicator = "8";
            }

            try
            {
                //Log ATNA - Registry Application Stop Event
                ATNALogic atnaLogic = new ATNALogic();
                atnaLogic.ProcessEvent("REGISTRY-APP-STOP-ITI-20", ATNAEvent.XDSREGISTRY_TYPE, eventOutcomeIndicator, ATNAEvent.UDP_TAG_APPNAME_REGISTRY);
            }
            catch
            {
                //Oops...ATNA event failed...probably UDP failure....can't afford to mess up with XDSRegistry service sorry :(
            }
        }
    }
}
