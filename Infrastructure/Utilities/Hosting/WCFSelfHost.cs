using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;

namespace BallyTech.Infrastructure.Hosting
{
    public class WCFSelfHost : ServiceHost, IServiceHost
    {
        public WCFSelfHost(Object singletonInstance, params Uri[] baseAddresses) :
            base(singletonInstance, baseAddresses)
        {
        }

        public WCFSelfHost(Type serviceType, params Uri[] baseAddresses) :
            base(serviceType, baseAddresses)
        {
        }

        void IServiceHost.Run()
        {
            this.Open();
        }

        void IServiceHost.Shutdown()
        {
            this.Close();
        }

        protected override void ApplyConfiguration()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var configFilePath = configuration.FilePath;
            this.LoadFromFile(configFilePath);
        }

        private void LoadFromFile(string configFilePath)
        {
            var filemap = new ExeConfigurationFileMap() { ExeConfigFilename = configFilePath };
            var config = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
            var serviceModel = ServiceModelSectionGroup.GetSectionGroup(config);

            bool loaded = false;
            foreach (ServiceElement se in serviceModel.Services.Services)
            {
                if (!loaded)
                    if (se.Name == this.Description.ConfigurationName)
                    {
                        base.LoadConfigurationSection(se);
                        loaded = true;
                    }
            }
            if (!loaded)
                throw new ArgumentException("ServiceElement doesn't exist");
        }
    }
}