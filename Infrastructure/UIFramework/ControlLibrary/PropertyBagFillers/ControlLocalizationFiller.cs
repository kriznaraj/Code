
namespace Controls.ControlLibrary
{
    internal class ControlLocalizationFiller : ControlPropertyFiller
    {
        private void FillLiteral(ControlPropertyBag propertyBag, FillerParams fillerParams, bool skipWatermark=false)
        {
            string externalizationKey = string.Empty;
            if (fillerParams.IsBindingControl)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);
                externalizationKey = propertyConfig != null ? propertyConfig.ExternalizationKey : string.Format(ControlLibConstants.EXTERNALIZATION_KEY_FORMAT, fillerParams.ModelName, fillerParams.PropertyName.Replace(".", "_"));
            }
            else
            {
                externalizationKey = fillerParams.ExternalizationKey;
            }

            propertyBag.Label = ControlLibraryConfig.ResourceService.GetLiteral(string.Format(ControlLibConstants.LABEL_FORMAT, externalizationKey));
            propertyBag.ToolTip = ControlLibraryConfig.ResourceService.GetLiteral(string.Format(ControlLibConstants.TOOLTIP_FORMAT, externalizationKey));
            if (!skipWatermark)
            {
                propertyBag.WaterMarkText = ControlLibraryConfig.ResourceService.GetLiteral(string.Format(ControlLibConstants.WATERMARK_FORMAT, externalizationKey));
            }
        }

        public override void Fill(TextBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams);
        }

        public override void Fill(NumericTextBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams);
        }

        public override void Fill(LabelPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams);
            if(propertyBag.IsCurrency)
                propertyBag.CurrencySymbol = ControlLibraryConfig.ResourceService.GetCurrencySymbol();            
        }

        public override void Fill(PasswordBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams);
        }

        public override void Fill(CheckBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams, true);
        }

        public override void Fill(RadioButtonPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams, true);
        }

        public override void Fill(ButtonPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams, true);

        }

        public override void Fill(TextAreaPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams);
        }

        public override void Fill(DateTimePropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams);
        }

        public override void Fill(DropDownPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams);
        }

        public override void Fill(CheckBoxListPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams);
        }

        public override void Fill(RadioButtonListPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams);
        }

        public override void Fill(TemplateListPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams);
        }

        public override void Fill(GridPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams);
        }

        public override void Fill(ShuttlePropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams);
        }

        public override void Fill(TemplateDropDownPropertyBag propertyBag, FillerParams fillerParams)
        {
            FillLiteral(propertyBag, fillerParams);
        }
    }
}