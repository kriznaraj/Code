using Controls.Configuration;
using Controls.Utilities;
using Controls.Framework.Interfaces;
using Controls.ResourceManager;
using System;

namespace Controls.ControlLibrary
{
    public static class ControlLibraryConfig
    {
        internal static IConfigService ConfigService { get; private set; }

        internal static IResourceService ResourceService { get; private set; }

        public static IConfigurationReader ControlConfigReader { get; private set; }

        public static IEncryptionService EncryptionService { get; private set; }

        public static IAccessPolicyProvider AccessPolicyProvider { get; private set; }

        public static IUtilityProvider UtilityProvider { get; private set; }

        public static ISiteConfigProvider SiteConfigProvider { get; private set; }

        public static void Register(IConfigService configService, IResourceService resourceService, IEncryptionService encryptionService, IAccessPolicyProvider accessPolicyProvider, IUtilityProvider utilityProvider, ISiteConfigProvider siteConfigProvider)
        {
            if (configService == null)
            {
                throw new Exception("Please provide a config service");
            }

            if (resourceService == null)
            {
                throw new Exception("Please provide a resource service");
            }

            if (encryptionService == null)
            {
                throw new Exception("Please provide a Encryption service");
            }

            if (accessPolicyProvider == null)
            {
                throw new Exception("Please provide a Access Policy Service Provider");
            }

            if(utilityProvider == null)
            {
                throw new Exception("Please provide a Utility Provider");
            }

            if (siteConfigProvider == null)
            {
                throw new Exception("Please provide a Site Specific Config Service");
            }

            ConfigService = configService;
            ResourceService = resourceService;
            EncryptionService = encryptionService;
            AccessPolicyProvider = accessPolicyProvider;
            UtilityProvider = utilityProvider;
            SiteConfigProvider = siteConfigProvider;
            ControlConfigReader = ConfigurationReader.Instance;
            PropertyConfigurator.Configure();
        }
    }
}
