using System;
using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    internal class ControlDefaultsFiller : ControlPropertyFiller
    {
        public override void Fill(TextBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            propertyBag.ControlName = fillerParams.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyTextBox);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;
                if (propertyBag.AutoCompleteProperties == null)
                {
                    propertyBag.AutoCompleteProperties = new AutoCompleteBehaviourPropertyBag(new Dictionary<string, string>() {
                    { ControlLibConstants.ACTION_URL, controlDefault.AutoCompleteProperty.ActionURL },
                    { ControlLibConstants.ACTION_NAME, controlDefault.AutoCompleteProperty.ActionName },
                    { ControlLibConstants.CONTROLLER_NAME, controlDefault.AutoCompleteProperty.ControllerName},
                    { ControlLibConstants.MIN_CHAR_REQUIRED, controlDefault.AutoCompleteProperty.MinCharRequired.ToString() },
                    { ControlLibConstants.MAX_RESULT_COUNT, controlDefault.AutoCompleteProperty.MaxResultCount.ToString() },
                    { ControlLibConstants.ORDER_BY, controlDefault.AutoCompleteProperty.OrderBy.ToString() },
                    { ControlLibConstants.SEARCH_TYPE, controlDefault.AutoCompleteProperty.SearchType.ToString() }});
                }

                if (propertyBag.MaskingProperties == null)
                {
                    propertyBag.MaskingProperties = new MaskingBehaviourPropertyBag(new Dictionary<string, string>() {
                    { "MaskingChar", controlDefault.MaskingProperty.MaskingChar.ToString() },
                    { "MaskingType", controlDefault.MaskingProperty.MaskingType.ToString() },
                    { "MaskCharLength", controlDefault.MaskingProperty.MaskCharLength.ToString() },
                    { "MaskingPosition", controlDefault.MaskingProperty.MaskingPosition.ToString() } });
                }
            }
        }

        public override void Fill(NumericTextBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            propertyBag.ControlName = fillerParams.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyNumericTextBox);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;
                propertyBag.CurrencyFormatString = controlDefault.CurrencyFormatString;
                if (propertyBag.AutoCompleteProperties == null)
                {
                    propertyBag.AutoCompleteProperties = new AutoCompleteBehaviourPropertyBag(new Dictionary<string, string>() {
                    { ControlLibConstants.ACTION_URL, controlDefault.AutoCompleteProperty.ActionURL },
                    { ControlLibConstants.ACTION_NAME, controlDefault.AutoCompleteProperty.ActionName },
                    { ControlLibConstants.CONTROLLER_NAME, controlDefault.AutoCompleteProperty.ControllerName},
                    { ControlLibConstants.MIN_CHAR_REQUIRED, controlDefault.AutoCompleteProperty.MinCharRequired.ToString() },
                    { ControlLibConstants.MAX_RESULT_COUNT, controlDefault.AutoCompleteProperty.MaxResultCount.ToString() },
                    { ControlLibConstants.ORDER_BY, controlDefault.AutoCompleteProperty.OrderBy.ToString() },
                    { ControlLibConstants.SEARCH_TYPE, controlDefault.AutoCompleteProperty.SearchType.ToString() }});
                }

                if (propertyBag.MaskingProperties == null)
                {
                    propertyBag.MaskingProperties = new MaskingBehaviourPropertyBag(new Dictionary<string, string>() {
                    { ControlLibConstants.MASKING_CHAR, controlDefault.MaskingProperty.MaskingChar.ToString() },
                    { ControlLibConstants.MASKING_TYPE, controlDefault.MaskingProperty.MaskingType.ToString() },
                    { ControlLibConstants.MASKING_CHAR_LENGTH, controlDefault.MaskingProperty.MaskCharLength.ToString() },
                    { ControlLibConstants.MASKING_POSITION, controlDefault.MaskingProperty.MaskingPosition.ToString() } });
                }
            }
        }

        public override void Fill(LabelPropertyBag propertyBag, FillerParams fillerParams)
        {
            propertyBag.ControlName = fillerParams.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyLabel);
            if (controlDefault != null)
            {
                propertyBag.MandatoryChar = controlDefault.MandatoryChar;
                propertyBag.MandatoryCharCssClass = controlDefault.MandatoryCssClass;
                propertyBag.CssClass = controlDefault.CssClass;

                if (propertyBag.MaskingProperties == null)
                {
                    propertyBag.MaskingProperties = new MaskingBehaviourPropertyBag(new Dictionary<string, string>() {
                    { ControlLibConstants.MASKING_CHAR, controlDefault.MaskingProperty.MaskingChar.ToString() },
                    { ControlLibConstants.MASKING_TYPE, controlDefault.MaskingProperty.MaskingType.ToString() },
                    { ControlLibConstants.MASKING_CHAR_LENGTH, controlDefault.MaskingProperty.MaskCharLength.ToString() },
                    { ControlLibConstants.MASKING_POSITION, controlDefault.MaskingProperty.MaskingPosition.ToString() } });
                }
            }
        }

        public override void Fill(PasswordBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            propertyBag.ControlName = fillerParams.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyPasswordBox);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;
            }
        }

        public override void Fill(CheckBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            propertyBag.ControlName = fillerParams.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyCheckBox);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;
            }
        }

        public override void Fill(RadioButtonPropertyBag propertyBag, FillerParams fillerParams)
        {
            propertyBag.ControlName = fillerParams.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyRadioButton);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;
            }
        }

        public override void Fill(ButtonPropertyBag propertyBag, FillerParams fillerParams)
        {
            propertyBag.ControlName = fillerParams.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration((ControlNames)Enum.Parse(typeof(ControlNames), Convert.ToString(propertyBag.ButtonCatagory)));
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                if (propertyBag.ButtonCatagory == ButtonCatagory.BallyImageButton)
                {
                    propertyBag.ImageButtonDisableClass = controlDefault.ImageButtonDisableClass;
                    propertyBag.ImageButtonLabelDisableClass = controlDefault.ImageButtonLabelDisableClass;
                    propertyBag.ImageButtonLeftAlignClass = controlDefault.ImageButtonLeftAlignClass;
                    propertyBag.ImageClass = controlDefault.ImageClass;
                }
            }
        }

        public override void Fill(TextAreaPropertyBag propertyBag, FillerParams fillerParams)
        {
            propertyBag.ControlName = fillerParams.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyTextArea);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;
            }
        }

        public override void Fill(DateTimePropertyBag propertyBag, FillerParams fillerParams)
        {
            propertyBag.ControlName = fillerParams.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyDateTime);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;

                if (propertyBag.DateProperties == null)
                {
                    propertyBag.DateProperties = new DatePropertyBag(controlDefault.DateProperty.DateFormat,
                        controlDefault.DateProperty.NumberOfMonths,
                        controlDefault.DateProperty.ShowButtonPanel,
                        controlDefault.DateProperty.MaxDate,
                        controlDefault.DateProperty.MinDate,
                        controlDefault.DateProperty.ChangeMonth,
                        controlDefault.DateProperty.ChangeYear,
                        controlDefault.DateProperty.ChangeDate,
                        controlDefault.DateProperty.DateCssClass, string.Empty);
                }

                if (propertyBag.TimeProperties == null)
                {
                    propertyBag.TimeProperties = new TimePropertyBag(
                        controlDefault.TimeProperty.ShowAmPm,
                        controlDefault.TimeProperty.TimeFormat,
                        controlDefault.TimeProperty.ShowDuration,
                        controlDefault.TimeProperty.Step,
                        controlDefault.TimeProperty.TimeCssClass);
                }
            }
        }

        public override void Fill(DropDownPropertyBag propertyBag, FillerParams fillerParam)
        {
            propertyBag.ControlName = fillerParam.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyDropDown);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;
            }
        }

        public override void Fill(CheckBoxListPropertyBag propertyBag, FillerParams fillerParam)
        {
            propertyBag.ControlName = fillerParam.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyCheckBoxList);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;
            }
        }

        public override void Fill(RadioButtonListPropertyBag propertyBag, FillerParams fillerParam)
        {
            propertyBag.ControlName = fillerParam.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyRadioButtonList);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;
            }
        }

        public override void Fill(TemplateListPropertyBag propertyBag, FillerParams fillerParam)
        {
            propertyBag.ControlName = fillerParam.ControlName;
            IControlDefaultPropertyBag controlDefault = null;
            if (fillerParam.ListType == ListBoxType.SimpleList)
            {
                controlDefault = ReadDefaultConfiguration(ControlNames.BallyListBox);
            }
            else
            {
                controlDefault = ReadDefaultConfiguration(ControlNames.BallyTemplateListBox);
            }

            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;
                propertyBag.ListItemTemplateCssClass = controlDefault.ListItemTemplateCssClass;
                //propertyBag.ListItemMouseOverCssClass = controlDefault.ListItemMouseOverCssClass;
                propertyBag.ListItemSelectedCssClass = controlDefault.ListItemSelectedCssClass;
            }
        }

        public override void Fill(GridPropertyBag propertyBag, FillerParams fillerParam)
        {
            propertyBag.ControlName = fillerParam.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyGrid);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;
            }
        }

        public override void Fill(ShuttlePropertyBag propertyBag, FillerParams fillerParam)
        {
            propertyBag.ControlName = fillerParam.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyShuttle);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;
            }
        }

        public override void Fill(TemplateDropDownPropertyBag propertyBag, FillerParams fillerParam)
        {
            propertyBag.ControlName = fillerParam.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyTemplateDropDown);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
                propertyBag.ControlErrorCssClass = controlDefault.ControlErrorCssClass;
                //propertyBag.ListTemplateCssClass = controlDefault.ListTemplateCssClass;
            }
        }

        public override void Fill(DenomControlPropertyBag propertyBag, FillerParams fillerParams) 
        {
            propertyBag.ControlName = fillerParams.ControlName;

            IControlDefaultPropertyBag controlDefault = ReadDefaultConfiguration(ControlNames.BallyDenomList);
            if (controlDefault != null)
            {
                propertyBag.CssClass = controlDefault.CssClass;
                propertyBag.HeaderCssClass = controlDefault.DenomHeaderCssClass;
                propertyBag.FooterCssClass = controlDefault.DenomFooterCssClass;
                propertyBag.ValidationErrorCssClass = controlDefault.ValidationErrorCssClass;
            }
        }
    }
}