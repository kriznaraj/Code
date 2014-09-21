
using Controls.Framework.Interfaces;
namespace Controls.ControlLibrary
{
    internal class ControlValidationFiller : ControlPropertyFiller
    {
        public override void Fill(TextBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            fillValidators(propertyBag, fillerParams);
        }

        public override void Fill(NumericTextBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            fillValidators(propertyBag, fillerParams);
        }

        public override void Fill(PasswordBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            fillValidators(propertyBag, fillerParams);
        }

        public override void Fill(TextAreaPropertyBag propertyBag, FillerParams fillerParams)
        {
            fillValidators(propertyBag, fillerParams);
        }

        public override void Fill(LabelPropertyBag propertyBag, FillerParams fillerParams)
        {
            fillValidators(propertyBag, fillerParams);
        }

        public override void Fill(DateTimePropertyBag propertyBag, FillerParams fillerParams)
        {
            fillValidators(propertyBag, fillerParams);
        }

        public override void Fill(DropDownPropertyBag propertyBag, FillerParams fillerParams)
        {
            fillValidators(propertyBag, fillerParams);
        }

        public override void Fill(CheckBoxListPropertyBag propertyBag, FillerParams fillerParams)
        {
            fillValidators(propertyBag, fillerParams);
        }

        public override void Fill(RadioButtonListPropertyBag propertyBag, FillerParams fillerParams)
        {
            fillValidators(propertyBag, fillerParams);
        }

        public override void Fill(TemplateListPropertyBag propertyBag, FillerParams fillerParams)
        {
            fillValidators(propertyBag, fillerParams);
        }

        public override void Fill(GridPropertyBag propertyBag, FillerParams fillerParams)
        {
            fillValidators(propertyBag, fillerParams);
        }
		
		public override void Fill(ShuttlePropertyBag propertyBag, FillerParams fillerParams)
        {
            fillValidators(propertyBag, fillerParams);
        }

        public override void Fill(TemplateDropDownPropertyBag propertyBag, FillerParams fillerParams)
        {
            fillValidators(propertyBag, fillerParams);
        }

        #region "Private Methods"
        
        private void fillValidators(ControlPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipValidationFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null)
                {
                    propertyBag.Mandatory = false;
                    var isHavingConfigKey = false;
                    if (propertyConfig.PropertyConfiguration != null && propertyConfig.PropertyConfiguration.Validators != null)
                    {
                        propertyBag.Validators = propertyConfig.PropertyConfiguration.Validators;

                        if (propertyConfig.PropertyConfiguration.SiteConfig != null && propertyConfig.PropertyConfiguration.SiteConfig.Count > 0)
                        {
                            var siteConfig = propertyConfig.PropertyConfiguration.SiteConfig.Find(o => o.SiteConfigType == SiteConfigType.Mandatory);
                            if (siteConfig != null)
                            {
                                ISiteConfigSetting siteConfigSetting = this.GetSiteConfigSetting(siteConfig.ConfigKey);
                                if (siteConfigSetting != null)
                                {
                                    if (siteConfigSetting.ConfigValue == true)
                                    {
                                        propertyBag.Mandatory = true;
                                    }
                                    isHavingConfigKey = true;
                                }
                            }
                        }

                        if (!isHavingConfigKey && propertyBag.Mandatory == false)
                        {
                            if (propertyBag.Validators != null && propertyBag.Validators.Count > 0)
                            {
                                var requiredValidation = propertyBag.Validators.Find(o => o.Type == ValidatorsType.Required);
                                if (requiredValidation != null)
                                {
                                    propertyBag.Mandatory = true;
                                }
                            }
                        }
                    }
                } 
            }
        }

        #endregion


    }
}