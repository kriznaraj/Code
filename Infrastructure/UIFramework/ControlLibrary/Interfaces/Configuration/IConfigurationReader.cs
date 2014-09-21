using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    public interface IConfigurationReader
    {
        #region "Methods"

        ControlDefaultPropertyBag GetControlDefaultsValues(ControlNames controlName);

        ModelPropertyConfiguration GetConfigurationSettings(string modelName, string PropertyKey,string configKey = "");

        IDictionary<string, ModelPropertyConfiguration> GetModelConfigurationSettings(string modelName, string configKey="");

        ControlTemplateConfiguration GetTemplateConfiguration(string templateKey);

        DataGridDefinitions GetGridDataColumnDefinition(string gridName);

        DenomTemplates GetDenomTemplate(string denomTemplateName);

        CustomValidationExpressionConfiguration GetCustomValidationExpressionConfiguration(string validationType);

        #endregion
    }
}