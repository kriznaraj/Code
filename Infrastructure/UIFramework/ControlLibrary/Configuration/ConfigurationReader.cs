using System;
using System.Collections.Generic;
using System.Linq;

namespace Controls.ControlLibrary
{
    internal class ConfigurationReader : IConfigurationReader
    {

        #region "Member Variables"

        private static ConfigurationReader _configurationReader;

        #endregion

        #region "Constructors"

        private ConfigurationReader()
        {

        }

        #endregion

        #region "Properties"

        public static ConfigurationReader Instance
        {
            get
            {
                if (_configurationReader == null)
                {
                    _configurationReader = new ConfigurationReader();

                }
                return _configurationReader;
            }
        }

        #endregion

        #region "Implemented MEmbers - IConfig"

        public ModelPropertyConfiguration GetConfigurationSettings(string modelName, string propertyKey, string configKey = "")
        {
            ModelConfiguration modelConfig = null;
            ModelPropertyConfiguration propertyConfig = null;

            modelConfig = ControlPropertyConfigurationCache.GetModelConfiguration(modelName, configKey);
            if (modelConfig != null)
            {
                string[] propertyNames = propertyKey.Split('.');
                if (propertyNames.Length == 1)
                {
                    if (modelConfig.IndexedPropertyConfiguration.Keys.Contains(propertyKey, StringComparer.InvariantCultureIgnoreCase))
                    {
                        propertyConfig = modelConfig.IndexedPropertyConfiguration[propertyKey];
                    }
                }
                else
                {
                    for (int propertyCount = 0; propertyCount < propertyNames.Length; propertyCount++)
                    {
                        string actualPropertyName = propertyNames[propertyCount].Contains("[") ? propertyNames[propertyCount].Substring(0, propertyNames[propertyCount].IndexOf("[")) : propertyNames[propertyCount];
                        if (modelConfig != null)
                        {
                            if (modelConfig.IndexedPropertyConfiguration.Keys.Contains(actualPropertyName, StringComparer.InvariantCultureIgnoreCase))
                            {
                                ModelPropertyConfiguration propConfig = modelConfig.IndexedPropertyConfiguration[actualPropertyName];
                                if (propConfig != null)
                                {
                                    if (propConfig.IsComplexType || propConfig.IsEnumerable)
                                    {
                                        modelConfig = ControlPropertyConfigurationCache.GetModelConfiguration(propConfig.ComplexTypeName, configKey);
                                    }
                                    else
                                    {
                                        propertyConfig = propConfig;
                                        break;
                                    }
                                }
                            } 
                        }
                    }
                }
            }
            return propertyConfig;
        }


        public ControlDefaultPropertyBag GetControlDefaultsValues(ControlNames controlName)
        {
            return ControlPropertyConfigurationCache.GetDefaultPropertyBag(controlName.ToString());
        }

        public IDictionary<string, ModelPropertyConfiguration> GetModelConfigurationSettings(string modelName, string configKey)
        {
            ModelConfiguration mConfiguration = ControlPropertyConfigurationCache.GetModelConfiguration(modelName, configKey);
            return mConfiguration != null ? mConfiguration.IndexedPropertyConfiguration : null;
        }

        public ControlTemplateConfiguration GetTemplateConfiguration(string templateKey)
        {
            ControlTemplateConfiguration resultHTML = null;
            resultHTML = ControlPropertyConfigurationCache.GetTemplateConfiguration(templateKey);
            return resultHTML;
        }

        public DataGridDefinitions GetGridDataColumnDefinition(string gridName)
        {
            return ControlPropertyConfigurationCache.GetGridDataColumnDefinitions(gridName);
        }

        public DenomTemplates GetDenomTemplate(string denomTemplateName)
        {
            return ControlPropertyConfigurationCache.GetDenomTemplates(denomTemplateName);
        }

        public CustomValidationExpressionConfiguration GetCustomValidationExpressionConfiguration(string validationType)
        {
            return ControlPropertyConfigurationCache.GetCustomValidationExpressionConfiguration(validationType);
        }

        #endregion        
    }
}