
using Controls.Framework.Interfaces;
namespace Controls.ControlLibrary
{
    internal class ControlPropertyFiller
    {
        public virtual void Fill(ControlPropertyBag propertyBag) { }

        public virtual void Fill(TextBoxPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(PasswordBoxPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(TextAreaPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(NumericTextBoxPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(LabelPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(CheckBoxPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(RadioButtonPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(ButtonPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(DateTimePropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(DropDownPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(CheckBoxListPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(RadioButtonListPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(TemplateListPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(ShuttlePropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(GridPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(TemplateDropDownPropertyBag propertyBag, FillerParams fillerParams) { }

        public virtual void Fill(DenomControlPropertyBag propertyBag, FillerParams fillerParams) { }

        internal IModelPropertyConfiguration ReadPropertyConfiguration(string modelName, string propertyName, string configKey)
        {
            IModelPropertyConfiguration propertyConfig = ControlLibraryConfig.ControlConfigReader.GetConfigurationSettings(modelName, propertyName, configKey);
            return propertyConfig;
        }

        internal IControlDefaultPropertyBag ReadDefaultConfiguration(ControlNames controlName)
        {
            IControlDefaultPropertyBag defaultConfig = ControlLibraryConfig.ControlConfigReader.GetControlDefaultsValues(controlName);
            return defaultConfig;
        }

        internal bool IsAuthorized(string accessPolicyCode)
        {
            return ControlLibraryConfig.AccessPolicyProvider.Current.GetAccessPolicy(accessPolicyCode);
        }

        internal ISiteConfigSetting GetSiteConfigSetting(string configKey)
        {
            return ControlLibraryConfig.SiteConfigProvider.Current.GetSiteConfig(configKey);
        }
    }
}