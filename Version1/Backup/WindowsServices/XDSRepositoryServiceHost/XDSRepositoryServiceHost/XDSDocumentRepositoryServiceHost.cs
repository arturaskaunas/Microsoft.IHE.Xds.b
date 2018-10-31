using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

using System.ServiceModel;
using Microsoft.IHE.XDS.DocumentRepository;

using Microsoft.IHE.XDS.BusinessLogic.ATNA;

namespace Microsoft.IHE.XDS.DocumentRepository.XDSDocumentRepositoryServiceHost
{
    
    partial class XDSDocumentRepositoryServiceHost : ServiceBase
    {
        ServiceHost _serviceHost = null;

        public XDSDocumentRepositoryServiceHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string eventOutcomeIndicator = "0";

            try
            {
                _serviceHost = new ServiceHost(typeof(DocumentRepositoryService));

                _serviceHost.Open();
            }
            catch
            {
                //Failure
                eventOutcomeIndicator = "8";
            }

            try
            {
                //Log ATNA - Repository Application Start Event
                ATNALogic atnaLogic = new ATNALogic();
                atnaLogic.ProcessEvent("REPOSITORY-APP-START-ITI-20", ATNAEvent.XDSREPOSITORY_TYPE, eventOutcomeIndicator, ATNAEvent.UDP_TAG_APPNAME_REPOSITORY);
            }
            catch
            {
                //Oops...ATNA event failed...probably UDP failure....can't afford to stop XDSRepository service sorry :(
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
                //Log ATNA - Repository Application Start Event
                ATNALogic atnaLogic = new ATNALogic();
                atnaLogic.ProcessEvent("REPOSITORY-APP-STOP-ITI-20", ATNAEvent.XDSREPOSITORY_TYPE, eventOutcomeIndicator, ATNAEvent.UDP_TAG_APPNAME_REPOSITORY);
            }
            catch
            {
                //Oops...ATNA event failed...probably UDP failure....can't afford to stop XDSRepository service sorry :(
            }

        }
    }
}
