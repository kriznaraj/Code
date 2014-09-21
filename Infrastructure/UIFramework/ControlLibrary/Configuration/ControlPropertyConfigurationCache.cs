using System;
using System.Collections.Generic;
using System.Linq;

namespace Controls.ControlLibrary
{
    internal class ControlPropertyConfigurationCache
    {

        #region "Properties"

        private readonly static Dictionary<string, ControlDefaultPropertyBag> _controlDefaultPropertiesList = new Dictionary<string, ControlDefaultPropertyBag>(StringComparer.InvariantCultureIgnoreCase);

        private readonly static List<ModelConfiguration> _modelConfigurationList = new List<ModelConfiguration>();

        private readonly static Dictionary<string, ControlTemplateConfiguration> _templateConfigurationList = new Dictionary<string, ControlTemplateConfiguration>(StringComparer.InvariantCultureIgnoreCase);

        private readonly static Dictionary<string, DataGridDefinitions> _gridDataColumnDefinitionsList = new Dictionary<string, DataGridDefinitions>(StringComparer.InvariantCultureIgnoreCase);

        private readonly static Dictionary<string, DenomTemplates> _denomTemplateList = new Dictionary<string, DenomTemplates>(StringComparer.InvariantCultureIgnoreCase);

        private readonly static Dictionary<string, CustomValidationExpressionConfiguration> _customValidationExpressionConfigurationsList = new Dictionary<string, CustomValidationExpressionConfiguration>(StringComparer.InvariantCultureIgnoreCase);

        #endregion

        #region "Public Methods"

        public static void SetDefaultPropertyBag(string key, ControlDefaultPropertyBag propertyBag)
        {
            ControlDefaultPropertyBag controlDefaultPropertyBag;
            _controlDefaultPropertiesList.TryGetValue(key, out controlDefaultPropertyBag);
            if (propertyBag != null && controlDefaultPropertyBag == null)
            {
                _controlDefaultPropertiesList.Add(key, propertyBag);
            }
        }

        public static ControlDefaultPropertyBag GetDefaultPropertyBag(string key)
        {
            ControlDefaultPropertyBag result = null;
            _controlDefaultPropertiesList.TryGetValue(key, out result);
            return result;
        }

        public static void SetCustomValidationExpressionConfiguration(string validationType, CustomValidationExpressionConfiguration customValidationExpression)
        {
            CustomValidationExpressionConfiguration customValidationExpressionConfiguration;
            _customValidationExpressionConfigurationsList.TryGetValue(validationType, out customValidationExpressionConfiguration);
            if (customValidationExpression != null && customValidationExpressionConfiguration == null)
            {
                _customValidationExpressionConfigurationsList.Add(validationType, customValidationExpression);
            }
        }

        public static CustomValidationExpressionConfiguration GetCustomValidationExpressionConfiguration(string validationType)
        {
            CustomValidationExpressionConfiguration result = null;
            _customValidationExpressionConfigurationsList.TryGetValue(validationType, out result);
            return result;
        }

        public static void SetModelConfiguration(string name, string configKey, ModelConfiguration modelConfiguration, IEnumerable<PropertyConfiguration> propertyConfigurations)
        {
            ModelConfiguration modelConfig = null;
            if (string.IsNullOrEmpty(configKey))
            {
                modelConfig = _modelConfigurationList.Where(o => o.LookUpKey == name).FirstOrDefault();
            }
            else
            {
                modelConfig = _modelConfigurationList.Where(o => o.LookUpKey == string.Format("{0}.{1}", name, configKey)).FirstOrDefault();
            }

            if (modelConfiguration != null && modelConfig == null)
            {

                foreach (var property in modelConfiguration.PropertyConfiguration)
                {
                    PropertyConfiguration p = propertyConfigurations.Where(o=>o.Key == property.Key).FirstOrDefault();
                    if (p != null)
                        property.PropertyConfiguration = p;
                }

                _modelConfigurationList.Add(modelConfiguration);
            }
        }

        public static ModelConfiguration GetModelConfiguration(string name, string configKey)
        {
            ModelConfiguration modelConfig = null;
            if (string.IsNullOrEmpty(configKey))
            {
                modelConfig = _modelConfigurationList.Where(o => o.LookUpKey == name).FirstOrDefault();
            }
            else
            {
                modelConfig = _modelConfigurationList.Where(o => o.LookUpKey == string.Format("{0}.{1}", name, configKey)).FirstOrDefault();
            }
            return modelConfig;
        }

        public static void SetTemplateConfiguration(string templateKey, ControlTemplateConfiguration configuration)
        {
            ControlTemplateConfiguration templateConfiguration;
            _templateConfigurationList.TryGetValue(templateKey, out templateConfiguration);
            if (configuration != null && templateConfiguration == null)
            {
                _templateConfigurationList.Add(templateKey, configuration);
            }
        }

        public static ControlTemplateConfiguration GetTemplateConfiguration(string templateKey)
        {
            ControlTemplateConfiguration result;
            _templateConfigurationList.TryGetValue(templateKey, out result);

            return result;
        }

        public static void SetGridDataColumnDefinitions(string gridName, DataGridDefinitions gridDataColumnDefinitions)
        {
            DataGridDefinitions objGridDataColumnDefinitions;
            _gridDataColumnDefinitionsList.TryGetValue(gridName, out objGridDataColumnDefinitions);
            if (gridDataColumnDefinitions != null && objGridDataColumnDefinitions == null)
            {                
                _gridDataColumnDefinitionsList.Add(gridName, gridDataColumnDefinitions);
            }
        }

        public static DataGridDefinitions GetGridDataColumnDefinitions(string gridName)
        {
            DataGridDefinitions result;
            _gridDataColumnDefinitionsList.TryGetValue(gridName, out result);
            return result;
        }

        public static void SetDenomTemplates(string templateName, DenomTemplates denomTemplates)
        {
            DenomTemplates objDenomTemplates;
            _denomTemplateList.TryGetValue(templateName, out objDenomTemplates);
            if (denomTemplates != null && objDenomTemplates == null)
            {
                _denomTemplateList.Add(templateName, denomTemplates);
            }
        }

        public static DenomTemplates GetDenomTemplates(string templateName)
        {
            DenomTemplates result;
            _denomTemplateList.TryGetValue(templateName, out result);
            return result;
        }

        #endregion

    }
}
