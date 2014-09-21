using Controls.Utilities;
using Controls.Data;
using System;
using System.Configuration;
using System.Reflection;

namespace Controls.ResourceManager
{
    public class ResourceServiceFactory
    {
        public static IResourceProvider Create(IUtilityProvider utilityProvider)
        {
            IResourceProvider resourceProvider = null;
            /*TODO - Need to find out the way to copy the config file on Post Build Process*/
            var map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + @"bin\Controls.ResourceManager.dll.config";
            var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            /*Provides what type of provider to be used to for Error Messages and Literals Persistance*/
            Type providerType = Type.GetType(config.AppSettings.Settings["DataProvider"].Value);           
            

            /*Provides what type of Resource provider to be used to keep the Error Messages and Literals*/
            Type resourceProviderType = Type.GetType(config.AppSettings.Settings["ResourceProvider"].Value);   

            if(resourceProviderType == typeof(FileResourceProvider))
            {
                string baseName = config.AppSettings.Settings["BaseResource"].Value;
                if (String.IsNullOrEmpty(baseName))
                {

                    utilityProvider.GetLogger().LogFatal("ResourceServiceFactory.Resource File", 9005);
                    throw new Exception("FileResourceProvider configuration missing");
                }

                Type assemblyType = Type.GetType(baseName);
                Assembly assembly = Assembly.GetAssembly(assemblyType);
                resourceProvider = (IResourceProvider)Activator.CreateInstance(resourceProviderType, assemblyType.FullName, assembly);
                if (resourceProvider == null)
                {
                    utilityProvider.GetLogger().LogFatal("ResourceServiceFactory.Resource Provider", 9002);
                }
            }
            else if (resourceProviderType == typeof(DBResourceProvider))
            {
                /*Reads the Connection String for the provider mentioned above.*/
                IDataProvider dataProvider = (IDataProvider)Activator.CreateInstance(providerType, String.Format(config.AppSettings.Settings["ConnectionString"].Value, AppDomain.CurrentDomain.BaseDirectory));
                if (dataProvider == null)
                {
                    utilityProvider.GetLogger().LogFatal("ResourceServiceFactory.Data Provider", 9003);
                }

                resourceProvider = (IResourceProvider)Activator.CreateInstance(resourceProviderType, dataProvider);
                if (resourceProvider == null)
                {
                    utilityProvider.GetLogger().LogFatal("ResourceServiceFactory.Resource Provider", 9004);
                }
            }
            return resourceProvider;
        }
    }
}
