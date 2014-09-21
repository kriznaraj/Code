using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    internal class ControlBehaviorFiller : ControlPropertyFiller
    {  
        public override void Fill(LabelPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipBehaviourFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);
                propertyBag.Masking = false;
                if (propertyConfig != null && propertyConfig.PropertyConfiguration != null)
                {
                    if (propertyConfig.PropertyConfiguration.MaskingProperties != null)
                    {

                        propertyBag.MaskingProperties = new MaskingBehaviourPropertyBag(new Dictionary<string, string>() { {ControlLibConstants.MASKING_CHAR, !string.IsNullOrEmpty(propertyConfig.PropertyConfiguration.MaskingProperties.MaskingChar.ToString()) ? propertyConfig.PropertyConfiguration.MaskingProperties.MaskingChar.ToString() : propertyBag.MaskingProperties.MaskingChar.ToString() },
                    { ControlLibConstants.MASKING_TYPE, propertyConfig.PropertyConfiguration.MaskingProperties.MaskingType.ToString() },
                    { ControlLibConstants.MASKING_CHAR_LENGTH, propertyConfig.PropertyConfiguration.MaskingProperties.MaskCharLength > 0 ? propertyConfig.PropertyConfiguration.MaskingProperties.MaskCharLength.ToString() : propertyBag.MaskingProperties.MaskCharLength.ToString() },
                    { ControlLibConstants.MASKING_POSITION, propertyConfig.PropertyConfiguration.MaskingProperties.MaskingPosition.ToString() } });
                    }
                } 
            }
        }

        public override void Fill(TextBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipBehaviourFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);
                //propertyBag.AutoComplete = false;
                propertyBag.Masking = false;

                if (propertyConfig != null && propertyConfig.PropertyConfiguration != null)
                {
                    if (propertyConfig.PropertyConfiguration.AutoCompleteProperties != null && propertyBag.AutoComplete)
                    {

                        propertyBag.AutoCompleteProperties = new AutoCompleteBehaviourPropertyBag(new Dictionary<string, string>() { {ControlLibConstants.ACTION_URL, propertyConfig.PropertyConfiguration.AutoCompleteProperties.ActionURL },
                    { ControlLibConstants.ACTION_NAME, propertyConfig.PropertyConfiguration.AutoCompleteProperties.ActionName }, 
                    { ControlLibConstants.CONTROLLER_NAME, propertyConfig.PropertyConfiguration.AutoCompleteProperties.ControllerName}, 
                    {ControlLibConstants.MIN_CHAR_REQUIRED, propertyConfig.PropertyConfiguration.AutoCompleteProperties.MinCharRequired > 0 ? propertyConfig.PropertyConfiguration.AutoCompleteProperties.MinCharRequired.ToString() : propertyBag.AutoCompleteProperties.MinCharRequired.ToString()},
                    { ControlLibConstants.MAX_RESULT_COUNT, propertyConfig.PropertyConfiguration.AutoCompleteProperties.MaxResultCount > 0 ? propertyConfig.PropertyConfiguration.AutoCompleteProperties.MaxResultCount.ToString() : propertyBag.AutoCompleteProperties.MaxResultCount.ToString() }, 
                    { ControlLibConstants.ORDER_BY,  propertyConfig.PropertyConfiguration.AutoCompleteProperties.OrderBy.ToString() }, 
                    { ControlLibConstants.SEARCH_TYPE, propertyConfig.PropertyConfiguration.AutoCompleteProperties.SearchType.ToString() }});

                        //propertyBag.AutoComplete = true;
                    }

                    if (propertyConfig.PropertyConfiguration.MaskingProperties != null)
                    {

                        propertyBag.MaskingProperties = new MaskingBehaviourPropertyBag(new Dictionary<string, string>() { { ControlLibConstants.MASKING_CHAR, !string.IsNullOrEmpty(propertyConfig.PropertyConfiguration.MaskingProperties.MaskingChar.ToString()) ? propertyConfig.PropertyConfiguration.MaskingProperties.MaskingChar.ToString() : propertyBag.MaskingProperties.MaskingChar.ToString() },
                    { ControlLibConstants.MASKING_TYPE, propertyConfig.PropertyConfiguration.MaskingProperties.MaskingType.ToString() },
                    {ControlLibConstants.MASKING_CHAR_LENGTH, propertyConfig.PropertyConfiguration.MaskingProperties.MaskCharLength > 0 ? propertyConfig.PropertyConfiguration.MaskingProperties.MaskCharLength.ToString() : propertyBag.MaskingProperties.MaskCharLength.ToString() },
                    { ControlLibConstants.MASKING_POSITION, propertyConfig.PropertyConfiguration.MaskingProperties.MaskingPosition.ToString() } });
                    }
                } 
            }

        }

        public override void Fill(NumericTextBoxPropertyBag propertyBag, FillerParams fillerParams)
        {

            if (fillerParams.SkipBehaviourFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);
                //propertyBag.AutoComplete = false;
                propertyBag.Masking = false;

                if (propertyConfig != null && propertyConfig.PropertyConfiguration != null)
                {
                    if (propertyConfig.PropertyConfiguration.AutoCompleteProperties != null && propertyBag.AutoComplete)
                    {
                        propertyBag.AutoCompleteProperties = new AutoCompleteBehaviourPropertyBag(new Dictionary<string, string>() { { ControlLibConstants.ACTION_URL, propertyConfig.PropertyConfiguration.AutoCompleteProperties.ActionURL },
                    { ControlLibConstants.ACTION_NAME, propertyConfig.PropertyConfiguration.AutoCompleteProperties.ActionName }, 
                    { ControlLibConstants.CONTROLLER_NAME, propertyConfig.PropertyConfiguration.AutoCompleteProperties.ControllerName}, 
                    { ControlLibConstants.MIN_CHAR_REQUIRED, propertyConfig.PropertyConfiguration.AutoCompleteProperties.MinCharRequired > 0 ? propertyConfig.PropertyConfiguration.AutoCompleteProperties.MinCharRequired.ToString() : propertyBag.AutoCompleteProperties.MinCharRequired.ToString()},
                    { ControlLibConstants.MAX_RESULT_COUNT, propertyConfig.PropertyConfiguration.AutoCompleteProperties.MaxResultCount > 0 ? propertyConfig.PropertyConfiguration.AutoCompleteProperties.MaxResultCount.ToString() : propertyBag.AutoCompleteProperties.MaxResultCount.ToString() }, 
                    { ControlLibConstants.ORDER_BY,  propertyConfig.PropertyConfiguration.AutoCompleteProperties.OrderBy.ToString() }, 
                    { ControlLibConstants.SEARCH_TYPE, propertyConfig.PropertyConfiguration.AutoCompleteProperties.SearchType.ToString() }});
                        //propertyBag.AutoComplete = true;
                    }

                    if (propertyConfig.PropertyConfiguration.MaskingProperties != null)
                    {
                        propertyBag.MaskingProperties = new MaskingBehaviourPropertyBag(new Dictionary<string, string>() { { ControlLibConstants.MASKING_CHAR, !string.IsNullOrEmpty(propertyConfig.PropertyConfiguration.MaskingProperties.MaskingChar.ToString()) ? propertyConfig.PropertyConfiguration.MaskingProperties.MaskingChar.ToString() : propertyBag.MaskingProperties.MaskingChar.ToString() },
                    { ControlLibConstants.MASKING_TYPE, propertyConfig.PropertyConfiguration.MaskingProperties.MaskingType.ToString() },
                    { ControlLibConstants.MASKING_CHAR_LENGTH, propertyConfig.PropertyConfiguration.MaskingProperties.MaskCharLength > 0 ? propertyConfig.PropertyConfiguration.MaskingProperties.MaskCharLength.ToString() : propertyBag.MaskingProperties.MaskCharLength.ToString() },
                    { ControlLibConstants.MASKING_POSITION, propertyConfig.PropertyConfiguration.MaskingProperties.MaskingPosition.ToString() } });
                    }
                } 
            }
        }

        public override void Fill(DateTimePropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipBehaviourFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);
                propertyBag.ShowDate = false;
                propertyBag.ShowTime = false;
                if (propertyConfig != null && propertyConfig.PropertyConfiguration != null)
                {
                    if (propertyConfig.PropertyConfiguration.DateProperties != null)
                    {
                        ///******** Adding config properties to datePropertyBag **********/
                        
                        propertyBag.DateProperties = propertyConfig.PropertyConfiguration.DateProperties;
                        propertyBag.ShowDate = true;
                    }
                    if (propertyConfig.PropertyConfiguration.TimeProperties != null)
                    {
                        ///******** Adding config properties to datePropertyBag **********/
                        ///
                        propertyBag.TimeProperties = propertyConfig.PropertyConfiguration.TimeProperties;
                        propertyBag.ShowTime = true;
                    }
                } 
            }
        }

    }
}