using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Collections.Specialized;
using System.ServiceProcess;
using Microsoft.Win32;

namespace Microsoft.IHE.XDS
{
    [RunInstaller(true)]
    public partial class ScriptedInstaller : Installer
    {
        public ScriptedInstaller()
        {
            InitializeComponent();
        }

        public void InitializeService(string servicename, string displayname, string servicedescription)
        {
            serviceInstaller.ServiceName = servicename;
            serviceInstaller.DisplayName = displayname;
            serviceInstaller.Description = servicedescription;
        }


        /// <summary>
        /// Return the value of the parameter in dicated by key
        /// </summary>
        /// <PARAM name="key">Context parameter key</PARAM>
        /// <returns>Context parameter specified by key</returns>
        public string GetContextParameter(string key)
        {
            string sValue = "";
            try
            {
                sValue = this.Context.Parameters[key].ToString();
            }
            catch
            {
                sValue = "";
            }

            return sValue;
        }

        /// <summary>
        /// This method is run before the install process.
        /// This method is overriden to set the following parameters:
        /// service name (/name switch)
        /// account type (/account switch)
        /// for a user account user name (/user switch)
        /// for a user account password (/password switch)
        /// Note that when using a user account,
        /// if the user name or password is not set,
        /// the installing user is prompted for the credentials to use.
        /// </summary>
        /// <PARAM name="savedState"></PARAM>
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);

            bool isUserAccount = false;

            // Decode the command line switches
            string name = GetContextParameter("name");
            if (!String.IsNullOrEmpty(name))
                serviceInstaller.ServiceName = name;

            // What type of credentials to use to run the service
            // The default is User
            string acct = GetContextParameter("account");

            if (0 == acct.Length) acct = "user";

            // Decode the type of account to use
            switch (acct)
            {
                case "user":
                    processInstaller.Account =
                       System.ServiceProcess.ServiceAccount.User;
                    isUserAccount = true;
                    break;
                case "localservice":
                    processInstaller.Account =
                      System.ServiceProcess.ServiceAccount.LocalService;
                    break;
                case "localsystem":
                    processInstaller.Account =
                      System.ServiceProcess.ServiceAccount.LocalSystem;
                    break;
                case "networkservice":
                    processInstaller.Account =
                      System.ServiceProcess.ServiceAccount.NetworkService;
                    break;
                default:
                    processInstaller.Account =
                      System.ServiceProcess.ServiceAccount.User;
                    isUserAccount = true;
                    break;
            }

            // User name and password
            string username = GetContextParameter("user");
            string password = GetContextParameter("password");

            // Should I use a user account?
            if (isUserAccount)
            {
                // If we need to use a user account,
                // set the user name and password
                processInstaller.Username = username;
                processInstaller.Password = password;
            }
        }

        /// <summary>
        /// Modify the registry to install the new service
        /// </summary>
        /// <PARAM name="stateServer"></PARAM>
        public override void Install(IDictionary stateServer)
        {
            RegistryKey system,
                //HKEY_LOCAL_MACHINE\Services\CurrentControlSet
                currentControlSet,
                //...\Services
                services,
                //...\<Service Name>
                service,
                //...\Parameters - this is where you can 
                //put service-specific configuration
                config;

            base.Install(stateServer);

            // Define the registry keys
            // Navigate to services
            system = Registry.LocalMachine.OpenSubKey("System");
            currentControlSet = system.OpenSubKey("CurrentControlSet");
            services = currentControlSet.OpenSubKey("Services");
            // Add the service
            service =
              services.OpenSubKey(this.serviceInstaller.ServiceName, true);
            // Default service description
            service.SetValue("Description",
                                 this.serviceInstaller.Description);

            // Display the assembly image path
            // and modify to add the service name
            // The executable then strips the name out of the image
            Console.WriteLine("ImagePath: " + service.GetValue("ImagePath"));
            Console.WriteLine("ServiceName: " + this.serviceInstaller.ServiceName);
            string imagePath = (string)service.GetValue("ImagePath");
            imagePath += " -s" + this.serviceInstaller.ServiceName;
            service.SetValue("ImagePath", imagePath);
            // Create a parameters subkey
            config = service.CreateSubKey("Parameters");

            // Close keys
            config.Close();
            service.Close();
            services.Close();
            currentControlSet.Close();
            system.Close();
        }

        /// <summary>
        /// Uninstall based on the service name
        /// </summary>
        /// <PARAM name="savedState"></PARAM>
        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);

            // Set the service name based on the command line
            string name = GetContextParameter("name");
            if (!String.IsNullOrEmpty(name))
                serviceInstaller.ServiceName = name;

            Console.WriteLine("Service Name: " + serviceInstaller.ServiceName);
        }

        /// <summary>
        /// Modify the registry to remove the service
        /// </summary>
        /// <PARAM name="stateServer"></PARAM>
        public override void Uninstall(IDictionary stateServer)
        {
            RegistryKey system,
                //HKEY_LOCAL_MACHINE\Services\CurrentControlSet
                currentControlSet,
                //...\Services
                services,
                //...\<Service Name>
                service;
            //...\Parameters - this is where you can 
            //put service-specific configuration

            // Navigate down the registry path
            system = Registry.LocalMachine.OpenSubKey("System");
            currentControlSet = system.OpenSubKey("CurrentControlSet");
            services = currentControlSet.OpenSubKey("Services");
            service =
               services.OpenSubKey(serviceInstaller.ServiceName, true);
            // Remove the parameters key
            service.DeleteSubKeyTree("Parameters");

            // Close keys
            service.Close();
            services.Close();
            currentControlSet.Close();
            system.Close();

            base.Uninstall(stateServer);
        }
    }
}