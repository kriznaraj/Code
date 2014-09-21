using Controls.Configuration;
using Controls.Debugging;
using Controls.ExceptionHandling;
using Controls.Logging;
using Controls.ControlLibrary;
using Controls.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Configurator
{
    internal class Program
    {
        private static IConfigService config;

        public static bool Serialize<T>(T value, String filename)
        {
            if (value == null)
            {
                return false;
            }
            try
            {
                XmlSerializer _xmlserializer = new XmlSerializer(typeof(T));
                Stream stream = new FileStream(filename, FileMode.Create);
                _xmlserializer.Serialize(stream, value);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static void Configure()
        {
            config = new ConfigService(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }

        //private static void ConfigureHostingClient()
        //{
        //    config.Save("Hosting", "PlatformRESTApi", RESTHostingType.RESTHttp);
        //    config.Save("WCFClient", "PlatformRESTApi", "net.tcp://localhost:8001/");
        //    config.Save("HttpClient", "PlatformRESTApi", "http://localhost:9955/");
        //    config.Save("HttpClientHandler", "PlatformRESTApi", "BallyTech.UI.Web.Platform.WebService.HttpClientHandler, BallyTech.UI.Web.Platform.WebService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
        //    string fileName = @"HostingConfig\ClientHostConfig.xml";
        //    ClientHostConfig configs = Deserialize<ClientHostConfig>(fileName);
        //    foreach (var item in configs.Clients)
        //    {
        //        config.Save("WCFClient", "PlatformRESTApi." + item.ConfigName, item.HostConfig);
        //    }
        //}

        private static T Deserialize<T>(string fileName) where T : class, ISerializable
        {
            XmlSerializer _xmlserializer = new XmlSerializer(typeof(T));

            T obj = default(T);
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                obj = (T)_xmlserializer.Deserialize(stream);
                stream.Close();
            }
            return obj;
        }

        private static T DeserializeT<T>(string fileName) where T : class
        {
            XmlSerializer _xmlserializer = new XmlSerializer(typeof(T));

            T obj = default(T);
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                obj = (T)_xmlserializer.Deserialize(stream);
                stream.Close();
            }
            return obj;
        }

        private static void LoadCommandActionXmlConfigurations()
        {
            string fileName = @"Configurations\CommandActionTypeConfig.xml";

            Save<CommandActionTypeConfig, CommandActionConfig>("CommandActionTypeConfig", fileName, new Func<CommandActionTypeConfig, IEnumerable<CommandActionConfig>>(o => o.CommandActionConfig), new Func<CommandActionConfig, string>(o => o.ActionKey));
        }

        private static void LoadCommandXmlConfigurations()
        {
            string fileName = @"Configurations\CommandTypeConfig.xml";

            Save<CommandTypeConfig, CommandConfig>("CommandTypeConfig", fileName, new Func<CommandTypeConfig, IEnumerable<CommandConfig>>(o => o.CommandConfig), new Func<CommandConfig, string>(o => o.CommandKey));
        }

        private static void LoadControlDefaultXmlConfigurations()
        {
            string fileName = @"Configurations\ControlDefaultConfigurations.xml";

            Save<ControlDefaultPropertyType, ControlDefaultPropertyBag>("ControlDefaultPropertyType", fileName, new Func<ControlDefaultPropertyType, IEnumerable<ControlDefaultPropertyBag>>(o => o.ControlDefaultProperty), new Func<ControlDefaultPropertyBag, string>(o => Enum.GetName(typeof(ControlNames), o.ControlName)));
        }

        private static void LoadControlTemplateXmlConfigurations()
        {
            string fileName = @"Configurations\ControlTemplateConfiguration.xml";

            Save<ControlTemplateConfigurationType, ControlTemplateConfiguration>("ControlTemplateConfigurationType", fileName, new Func<ControlTemplateConfigurationType, IEnumerable<ControlTemplateConfiguration>>(o => o.ControlTemplateConfiguration), new Func<ControlTemplateConfiguration, string>(o => o.TemplateKey));
        }

        private static void LoadCustomValidationExpressionConfigurations()
        {
            string fileName = @"Configurations\CustomValidationExpressionConfig.xml";

            Save<CustomValidationExpressionConfigurationType, CustomValidationExpressionConfiguration>("CustomValidationExpressionConfigurationType", fileName, new Func<CustomValidationExpressionConfigurationType, IEnumerable<CustomValidationExpressionConfiguration>>(o => o.CustomValidationExpressionConfiguration), new Func<CustomValidationExpressionConfiguration, string>(o => o.ValidationType.ToString()));
        }

        private static void LoadDataGridXmlConfigurations()
        {
            string fileName = @"Configurations\DataGridDefinitions.xml";

            Save<DataGridDefinitionType, DataGridDefinitions>("DataGridDefinitionType", fileName, new Func<DataGridDefinitionType, IEnumerable<DataGridDefinitions>>(o => o.DataGridDefinition), new Func<DataGridDefinitions, string>(o => o.GridName));
        }

        private static void LoadModelXmlConfigurations()
        {
            string fileName = @"Configurations\ModelConfigurations.xml";

            Save<ModelConfigurationType, ModelConfiguration>("ModelConfigurationType", fileName, new Func<ModelConfigurationType, IEnumerable<ModelConfiguration>>(o => o.ModelConfiguration), new Func<ModelConfiguration, string>(o => o.LookUpKey));
        }

        private static void LoadPropertyXmlConfigurations()
        {
            string fileName = @"Configurations\PropertyConfigurations.xml";

            Save<PropertyConfigurationType, PropertyConfiguration>("PropertyConfigurationType", fileName, new Func<PropertyConfigurationType, IEnumerable<PropertyConfiguration>>(o => o.PropertyConfiguration), new Func<PropertyConfiguration, string>(o => o.Key));
        }

	private static void LoadDenomTemplateXmlConfigurations()
        {
            string fileName = @"Configurations\DenomTemplates.xml";

            Save<DenomTemplatesType, DenomTemplates>("DenomTemplatesType", fileName, new Func<DenomTemplatesType, IEnumerable<DenomTemplates>>(o => o.DenomTemplate), new Func<DenomTemplates, string>(o => o.TemplateName));
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Reading XML files and updating the configuration database, Please wait...");

            DeleteConfigurationDB();

            //CreateConfigxml.CreateConfigXml();

            SaveResourceXml.SaveStringResourceXml();

            SaveResourceXml.SaveErrorResourceXml();

            CreateConfigxml.SaveConfigXml();

            Configure();

            LoadCommandXmlConfigurations();

            LoadCommandActionXmlConfigurations();

            LoadControlDefaultXmlConfigurations();

            LoadControlTemplateXmlConfigurations();

            LoadDataGridXmlConfigurations();

            LoadPropertyXmlConfigurations();

            LoadModelXmlConfigurations();

            LoadCustomValidationExpressionConfigurations();

            //LoadUIExceptionHandlePolicy();

            //ConfigureHostingClient();

		LoadDenomTemplateXmlConfigurations();

            #region Platform Server Reference xml Configuration

            LoadExceptionHandlePolicyRetryCount();
            LoadTraceConfiguration();
            LoadLoggerConfiguration();
            LoadInstrumentationConfiguration();
            //WriteEventMessageConfig();
            //WriteMomConfiguration();

            #endregion Platform Server Reference xml Configuration

            Console.WriteLine("Configuration database updated successfully...., Press any key to exit.");
            Console.ReadKey();
        }

        private static void Save<T, K>(string typeKey, string fileName, Func<T, IEnumerable<K>> getConfigs, Func<K, string> getKey) where T : class, ISerializable
        {
            T obj = Deserialize<T>(fileName);
            IEnumerable<K> configs = getConfigs(obj);

            config.Save<K>(typeKey, configs, getKey);
        }

        private static void SaveT<T, K>(string typeKey, string fileName, Func<T, IEnumerable<K>> getConfigs, Func<K, string> getKey) where T : class
        {
            T obj = DeserializeT<T>(fileName);
            IEnumerable<K> configs = getConfigs(obj);

            config.Save<K>(typeKey, configs, getKey);
        }

        #region Platform service configuration

        private static void DeleteConfigurationDB()
        {
            string connectionString = ConfigurationManager.AppSettings.Get("StorageProviderConnection");
            string commandText = "Delete tConfiguration";

            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();
                using (SqlCeTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        SqlCeCommand command = new SqlCeCommand(commandText, connection, transaction);
                        command.CommandType = CommandType.Text;
                        command.Prepare();
                        command.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (SqlCeException exception)
                    {
                        transaction.Rollback();
                        throw exception;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private static void LoadExceptionHandlePolicyRetryCount()
        {
            config.Save<int>("ExceptionPolicy", "RetryCount", 1);
        }

        private static void LoadInstrumentationConfiguration()
        {
            string fileName = @"Configurations\InstrumentationConfiguration.xml";

            Save<InstrumentationConfiguration>("Instrumentation", fileName, "Instrumentation");
        }

        private static void LoadLoggerConfiguration()
        {
            string fileName = @"Configurations\LoggerConfiguration.xml";

            Save<LoggerConfiguration>("Logging", fileName, "Logging");
        }

        private static void LoadTraceConfiguration()
        {
            string fileName = @"Configurations\TraceConfiguration.xml";

            Save<TraceConfiguration>("Trace", fileName, "Trace");
        }

        //private static void LoadUIExceptionHandlePolicy()
        //{
        //    string fileName = @"Configurations\UIExceptionHandlePolicy.xml";

        //    Save<ExceptionHandlePolicies, ExceptionHandlePolicy>("ExceptionHandlePolicy", fileName, new Func<ExceptionHandlePolicies, IEnumerable<ExceptionHandlePolicy>>(o => o.ExceptionHandlePolicy), new Func<ExceptionHandlePolicy, string>(o => o.Name));
        //}


        //private static void WriteEventMessageConfig()
        //{
        //    string fileName = @"Configurations\EventMessageConfig.xml";
        //    SaveT<EventMessageConfigCollection, EventMessageConfig>(
        //                  "EventMessageConfig",
        //                  fileName,
        //                  new Func<EventMessageConfigCollection, IEnumerable<EventMessageConfig>>(o => o.EventMessageCollection),
        //                  new Func<EventMessageConfig, string>(o => o.EventName));
        //}

        //private static void WriteMomConfiguration()
        //{
        //    string fileName = @"Configurations\MessagingConfiguration.xml";
        //    SaveT<MessagingConfig>(
        //         "MOM",
        //         fileName, "Config");
        //}

        private static void Save<T>(string typeKey, string fileName, string key) where T : class, ISerializable
        {
            //typeKey = null;
            T obj = Deserialize<T>(fileName);

            config.Save<T>(typeKey, key, obj);
        }

        private static void SaveT<T>(string typeKey, string fileName, string key) where T : class
        {
            //typeKey = null;
            T obj = DeserializeT<T>(fileName);

            config.Save<T>(typeKey, key, obj);
        }

        #endregion Platform service configuration
    }
}