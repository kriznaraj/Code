
using System.Collections.Generic;
namespace Controls.ControlLibrary
{

    internal static class PropertyConfigurator
    {

        #region "Public Methods"

        public static void Configure()
        {

            IEnumerable<PropertyConfiguration> propertyConfigurations = ControlLibraryConfig.ConfigService.Get<PropertyConfiguration>(ControlLibConstants.PROPERTY_CONFIGURATION_TYPE);
            IEnumerable<ModelConfiguration> modelConfigurations = ControlLibraryConfig.ConfigService.Get<ModelConfiguration>(ControlLibConstants.MODEL_CONFIGURATION_TYPE);
            foreach (ModelConfiguration item in modelConfigurations)
            {
                if (ControlLibraryConfig.ControlConfigReader.GetModelConfigurationSettings(item.Name, item.ConfigKey) == null)
                {
                    ControlPropertyConfigurationCache.SetModelConfiguration(item.Name,item.ConfigKey, item, propertyConfigurations);
                }                
            }

            IEnumerable<ControlDefaultPropertyBag> controlProperties = ControlLibraryConfig.ConfigService.Get<ControlDefaultPropertyBag>(ControlLibConstants.CONTROL_DEFAULT_PROPERTY_TYPE);
            foreach (ControlDefaultPropertyBag item in controlProperties)
            {
                if (ControlLibraryConfig.ControlConfigReader.GetControlDefaultsValues(item.ControlName) == null)
                {
                    ControlPropertyConfigurationCache.SetDefaultPropertyBag(item.ControlName.ToString(), item);
                }
            }

            IEnumerable<ControlTemplateConfiguration> controlTemplateConfigurations = ControlLibraryConfig.ConfigService.Get<ControlTemplateConfiguration>(ControlLibConstants.CONTROL_TEMPALTE_CONFIGURATION_TYPE);
            foreach (ControlTemplateConfiguration item in controlTemplateConfigurations)
            {
                if (ControlLibraryConfig.ControlConfigReader.GetTemplateConfiguration(item.TemplateKey) == null)
                {
                    ControlPropertyConfigurationCache.SetTemplateConfiguration(item.TemplateKey, item);
                }
            }
                        
            IEnumerable<DataGridDefinitions> dataGridColumDefinitions = ControlLibraryConfig.ConfigService.Get<DataGridDefinitions>(ControlLibConstants.DATAGRID_DEFINITION_TYPE);
            foreach (DataGridDefinitions item in dataGridColumDefinitions)
            {
                if (ControlLibraryConfig.ControlConfigReader.GetGridDataColumnDefinition(item.GridName) == null)
                {
                    ControlPropertyConfigurationCache.SetGridDataColumnDefinitions(item.GridName, item);
                }
            }

            IEnumerable<DenomTemplates> denomTemplatess = ControlLibraryConfig.ConfigService.Get<DenomTemplates>(ControlLibConstants.DENOM_TEMPLATE_TYPE);
            foreach (DenomTemplates item in denomTemplatess)
            {
                if (ControlLibraryConfig.ControlConfigReader.GetDenomTemplate(item.TemplateName) == null)
                {
                    ControlPropertyConfigurationCache.SetDenomTemplates(item.TemplateName, item);
                }
            }

            IEnumerable<CustomValidationExpressionConfiguration> customValidationExpressionConfigurations = ControlLibraryConfig.ConfigService.Get<CustomValidationExpressionConfiguration>(ControlLibConstants.CUSTOM_VALIDATION_EXPRESSION_CONFIGURATION_TYPE);
            foreach (CustomValidationExpressionConfiguration item in customValidationExpressionConfigurations)
            {
                if (ControlLibraryConfig.ControlConfigReader.GetCustomValidationExpressionConfiguration(item.ValidationType.ToString()) == null)
                {
                    ControlPropertyConfigurationCache.SetCustomValidationExpressionConfiguration(item.ValidationType.ToString(), item);
                }
            }
        }

        #endregion

    }
}
