using System;
using System.Collections.Generic;
using System.Linq;
using Controls.Framework.Interfaces;

namespace Controls.ControlLibrary
{
    internal class ControlOverriddenBehaviorFiller : ControlPropertyFiller
    {
        public override void Fill(TextBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.OnLeaveFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_LEAVE_FUNCTION];
                propertyBag.OnKeyUpFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_KEY_UP_FUNCTION];
                propertyBag.OnKeyDownFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_KEY_DOWN_FUNCTION];
                propertyBag.OnChangeFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CHANGE_FUNCTION];
                propertyBag.AutoCompleteInputFunction = fillerParams.OverrideSettings[ControlLibConstants.AUTOCOMPLETE_INPUT_FUNCTION];

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
            }
            propertyBag.Attributes = fillerParams.Attributes;
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);            
        }

        public override void Fill(NumericTextBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.OnLeaveFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_LEAVE_FUNCTION];
                propertyBag.OnKeyUpFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_KEY_UP_FUNCTION];
                propertyBag.OnKeyDownFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_KEY_DOWN_FUNCTION];
                propertyBag.OnChangeFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CHANGE_FUNCTION];
                propertyBag.AutoCompleteInputFunction = fillerParams.OverrideSettings[ControlLibConstants.AUTOCOMPLETE_INPUT_FUNCTION];

                propertyBag.IsCurrency = Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.IS_CURRENCY]);
                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
            }
            propertyBag.Attributes = fillerParams.Attributes;
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(LabelPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.DisplayType = (DisplayType)Enum.Parse(typeof(DisplayType), fillerParams.OverrideSettings[ControlLibConstants.DISPLAY_TYPE], true);
                propertyBag.IsCurrency = Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.IS_CURRENCY]);

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }

                propertyBag.OverrideToolTip = fillerParams.OverrideSettings[ControlLibConstants.OVERRIDE_TOOLTIP];
            }
            propertyBag.Attributes = fillerParams.Attributes;
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(PasswordBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.OnLeaveFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_LEAVE_FUNCTION];
                propertyBag.OnKeyUpFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_KEY_UP_FUNCTION];
                propertyBag.OnKeyDownFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_KEY_DOWN_FUNCTION];
                propertyBag.OnChangeFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CHANGE_FUNCTION];

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
            }
            propertyBag.Attributes = fillerParams.Attributes;
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(RadioButtonPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.OnClickFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CLICK_FUNCTION];
                propertyBag.Attributes = fillerParams.Attributes;

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
            }
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(CheckBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.OnClickFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CLICK_FUNCTION];
                propertyBag.Attributes = fillerParams.Attributes;

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
            }
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(ButtonPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.OnClickFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CLICK_FUNCTION];
                propertyBag.ParentID = fillerParams.OverrideSettings[ControlLibConstants.PARENT_ID];
                propertyBag.ValidateForm = Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.VALIDATE_FORM]);
                propertyBag.ActionName = fillerParams.OverrideSettings[ControlLibConstants.ACTION_NAME];
                propertyBag.ImagePath = fillerParams.OverrideSettings[ControlLibConstants.IMAGE_PATH];
                propertyBag.ButtonType = (ButtonType)Enum.Parse(typeof(ButtonType), fillerParams.OverrideSettings[ControlLibConstants.BUTTON_TYPE]);
                propertyBag.AlignLeft = fillerParams.AlignLeft;

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
            }
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(TextAreaPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.OnLeaveFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_LEAVE_FUNCTION];
                propertyBag.OnKeyUpFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_KEY_UP_FUNCTION];
                propertyBag.OnKeyDownFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_KEY_DOWN_FUNCTION];
                propertyBag.OnChangeFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CHANGE_FUNCTION];
                propertyBag.Attributes = fillerParams.Attributes;

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
            }
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(DateTimePropertyBag propertyBag, FillerParams fillerParams)
        {
            //Filling implementation code
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.DateProperties.DateFormat = fillerParams.OverrideSettings[ControlLibConstants.DATE_FORMAT];
                propertyBag.TimeProperties.ShowAmPm = Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.SHOW_AM_PM]);
                propertyBag.TimeProperties.Step = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.STEP]);
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.OnChangeFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CHANGE_FUNCTION];
                propertyBag.ShowDate = Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.SHOW_DATE]);
                propertyBag.ShowTime = Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.SHOW_TIME]);
                propertyBag.DateProperties.YearRange = fillerParams.OverrideSettings[ControlLibConstants.YEAR_RANGE];
                propertyBag.DateProperties.MinDate = fillerParams.minDate.HasValue ? fillerParams.minDate.Value.ToShortDateString() : string.Empty;
                propertyBag.DateProperties.MaxDate = fillerParams.maxDate.HasValue ? fillerParams.maxDate.Value.ToShortDateString() : string.Empty;

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.DATE_CSS_CLASS] != string.Empty)
                {
                    propertyBag.DateProperties.DateCssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.TIME_CSS_CLASS] != string.Empty)
                {
                    propertyBag.TimeProperties.TimeCssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
            }
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(DropDownPropertyBag propertyBag, FillerParams fillerParams)
        {
            //Filling implementation code
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.OnChangeFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CHANGE_FUNCTION];
                propertyBag.CascadeInputFunction = fillerParams.OverrideSettings[ControlLibConstants.CASCADE_INPUT_FUNCTION];
                propertyBag.Attributes = fillerParams.Attributes;

                propertyBag.TargetControlID = fillerParams.OverrideSettings[ControlLibConstants.TARGET_CONTROL_ID];
                propertyBag.ActionURL = fillerParams.OverrideSettings[ControlLibConstants.ACTION_URL];
                propertyBag.DropDownType = (DropDownType)Enum.Parse(typeof(DropDownType), fillerParams.OverrideSettings[ControlLibConstants.TYPE]);
                propertyBag.ListDisplayLength = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.LIST_LENGTH]);
                propertyBag.Options = ControlHelper.GetOptions(fillerParams.List, fillerParams.ValueMember, fillerParams.DisplayMember, fillerParams.OverrideSettings[ControlLibConstants.SELECTED_VALUE], fillerParams.Disabled);

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }

                propertyBag.DisplayMember = fillerParams.DisplayMember;
                propertyBag.ValueMember = fillerParams.ValueMember;
            }
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(CheckBoxListPropertyBag propertyBag, FillerParams fillerParams)
        {
            //Filling implementation code
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.IsVerticalAllign = Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.IS_VERTICAL_ALLIGN]);
                propertyBag.OnClickFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CLICK_FUNCTION];

                propertyBag.ListDisplayLength = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.LIST_LENGTH]);
                propertyBag.ListItem = ControlHelper.GetOptions(fillerParams.List, fillerParams.ValueMember, fillerParams.DisplayMember, fillerParams.OverrideSettings[ControlLibConstants.SELECTED_VALUE], fillerParams.Disabled);

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
            }
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(RadioButtonListPropertyBag propertyBag, FillerParams fillerParams)
        {
            //Filling implementation code
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.IsVerticalAllign = Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.IS_VERTICAL_ALLIGN]);
                propertyBag.ListDisplayLength = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.LIST_LENGTH]);
                propertyBag.OnClickFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CLICK_FUNCTION];

                propertyBag.ListItem = ControlHelper.GetOptions(fillerParams.List, fillerParams.ValueMember, fillerParams.DisplayMember, fillerParams.OverrideSettings[ControlLibConstants.SELECTED_VALUE], fillerParams.Disabled);

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
            }
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(TemplateListPropertyBag propertyBag, FillerParams fillerParams)
        {
            //Filling implementation code
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.ListItem = ControlHelper.GetListItems(fillerParams.List, fillerParams.ValueMember, fillerParams.Param);
                propertyBag.ListDisplayLength = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.LIST_LENGTH]);
                propertyBag.OnClickFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CLICK_FUNCTION];

                propertyBag.TemplateNameKey = fillerParams.OverrideSettings[ControlLibConstants.TEMPLATE_NAME_KEY];

                if (!string.IsNullOrEmpty(fillerParams.OverrideSettings[ControlLibConstants.LISTITEM_TEMPLATE_CSSCLASS]))
                {
                    propertyBag.ListItemTemplateCssClass = fillerParams.OverrideSettings[ControlLibConstants.LISTITEM_TEMPLATE_CSSCLASS];
                }

                if (!string.IsNullOrEmpty(fillerParams.OverrideSettings[ControlLibConstants.LISTITEM_SELECTED_CSSCLASS]))
                {
                    propertyBag.ListItemSelectedCssClass = fillerParams.OverrideSettings[ControlLibConstants.LISTITEM_SELECTED_CSSCLASS];
                }

                IControlTemplateConfiguration templateObject = ControlLibraryConfig.ControlConfigReader.GetTemplateConfiguration(fillerParams.TemplateNameKey);
                if (templateObject != null)
                {
                    propertyBag.TemplateHTML = templateObject.TemplateHTML;
                }

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
            }
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(GridPropertyBag propertyBag, FillerParams fillerParams)
        {
            //Filling implementation code
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.OnDataRowSelectFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_DATAROW_SELECT_FUNCTION];
                propertyBag.OnDataRowSelectionChangeFunctionName = fillerParams.OverrideSettings[ControlLibConstants.ON_DATAROW_SELECTION_CHANGE_FN];
                propertyBag.GridDataColumnDefinitionName = fillerParams.OverrideSettings[ControlLibConstants.GRID_DATACOLUMN_DEFINITION_NAME];
                propertyBag.ActionUrl = fillerParams.OverrideSettings[ControlLibConstants.ACTION_URL];
                propertyBag.ValueMember = fillerParams.OverrideSettings[ControlLibConstants.VALUE_MEMBER];

                propertyBag.GridParam = fillerParams.InputParam;

                propertyBag.GridProperties = new GridBehaviourPropertyBag(
                    Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.PAGESIZE]),
                Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.GRIDHEIGHT]),
                Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.ENABLEFILTER]),
                Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.ENABLESORTING]),
                Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.ENABLEEXPORT]),
                Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.ENABLEPAGINATION]),
                Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.SERVERPAGINATION]),
                Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.SELECTOPTION]),
                fillerParams.OverrideSettings[ControlLibConstants.DEFAULTSORTFIELD],
                fillerParams.OverrideSettings[ControlLibConstants.IMAGESIZEPROPERTY],
                fillerParams.OverrideSettings[ControlLibConstants.STATUSPROPERTY]);

                propertyBag.GridDataColumnDefinition = ControlLibraryConfig.ControlConfigReader.GetGridDataColumnDefinition(propertyBag.GridDataColumnDefinitionName);
                if (propertyBag.GridDataColumnDefinition != null && propertyBag.GridDataColumnDefinition.DataGridColumnDefinition != null && propertyBag.GridDataColumnDefinition.DataGridColumnDefinition.Count > 0)
                {
                    foreach (DataGridColumDefinition item in propertyBag.GridDataColumnDefinition.DataGridColumnDefinition)
                    {
                        if (!string.IsNullOrEmpty(item.AccessPolicyCode))
                        {
                            if (propertyBag.HiddenColumnsList == null)
                            {
                                propertyBag.HiddenColumnsList = new List<string>();
                            }

                            if (!this.IsAuthorized(item.AccessPolicyCode))
                            {
                                propertyBag.HiddenColumnsList.Add(item.ColumnName);
                            }
                        }
                    }

                    if (false == string.IsNullOrEmpty(fillerParams.OverrideSettings[ControlLibConstants.HIDEN_COUMN]))
                    {
                        if (propertyBag.HiddenColumnsList == null)
                        {
                            propertyBag.HiddenColumnsList = new List<string>();
                        }

                        foreach (var hcol in fillerParams.OverrideSettings[ControlLibConstants.HIDEN_COUMN].Split(','))
                        {
                            propertyBag.HiddenColumnsList.Add(hcol.Trim());
                        }
                    }
                }

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
            }
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(ShuttlePropertyBag propertyBag, FillerParams fillerParams)
        {
            //Filling implementation code
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.ActionUrl = fillerParams.OverrideSettings[ControlLibConstants.ACTION_URL];
                propertyBag.ValueMember = fillerParams.OverrideSettings[ControlLibConstants.VALUE_MEMBER];

                propertyBag.DisplayMember = fillerParams.OverrideSettings[ControlLibConstants.DISPLAY_MEMBER];

                propertyBag.OnChangeFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CHANGE_FUNCTION];

                propertyBag.ShuttleParam = fillerParams.InputParam;

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
            }

            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(TemplateDropDownPropertyBag propertyBag, FillerParams fillerParams)
        {
            //Filling implementation code
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.TabIndex = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.TAB_INDEX]);
                propertyBag.ListItem = ControlHelper.GetListItems(fillerParams.List, fillerParams.ValueMember, fillerParams.Param);
                propertyBag.ListDisplayLength = Convert.ToInt16(fillerParams.OverrideSettings[ControlLibConstants.LIST_LENGTH]);
                propertyBag.OnChangeFunction = fillerParams.OverrideSettings[ControlLibConstants.ON_CHANGE_FUNCTION];

                propertyBag.TemplateNameKey = fillerParams.OverrideSettings[ControlLibConstants.TEMPLATE_NAME_KEY];

                IControlTemplateConfiguration templateObject = ControlLibraryConfig.ControlConfigReader.GetTemplateConfiguration(fillerParams.TemplateNameKey);
                if (templateObject != null)
                {
                    propertyBag.TemplateHTML = templateObject.TemplateHTML;
                }

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
            }
            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
        }

        public override void Fill(DenomControlPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.OverrideSettings != null && fillerParams.OverrideSettings.Count > 0)
            {
                propertyBag.OnRowSelectFunction = fillerParams.OverrideSettings[ControlLibConstants.DENOM_ON_ROWSELECT_FUNCTION];
                propertyBag.TemplateNameKey = fillerParams.OverrideSettings[ControlLibConstants.DENOM_TEMPLATE_KEY];
                propertyBag.GrantTotalRequired = Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.DENOM_GRANT_TOTAL_REQUIRED]);
                propertyBag.OtherAmountRequired = Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.DENOM_OTHERAMOUNT_REQUIRED]);
                propertyBag.OtherAmountLabelKey = fillerParams.OverrideSettings[ControlLibConstants.DENOM_OTHERAMOUNT_LABELKEY];
                propertyBag.IsViewMode = Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.IS_VIEW_MODE]);
                propertyBag.ValidationMessageKey = fillerParams.OverrideSettings[ControlLibConstants.VALIDATION_MESSAGE_KEY];
                propertyBag.UndefinedRowReadonlyColumns = fillerParams.OverrideSettings[ControlLibConstants.UNDEFINEDROW_READONLY_COLUMNS];
                propertyBag.UndefinedRowEditableColumns = fillerParams.OverrideSettings[ControlLibConstants.UNDEFINEDROW_EDITABLE_COLUMNS];
                propertyBag.MovementIndicatorColumn = fillerParams.OverrideSettings[ControlLibConstants.MOVEMENT_INDICATOR_COLUMN];
                propertyBag.MovementIndicatorRequired = Convert.ToBoolean(fillerParams.OverrideSettings[ControlLibConstants.MOVEMENT_INDICATOR_REQUIRED]);

                if (fillerParams.OverrideSettings[ControlLibConstants.DENOM_HEADER_CSS] != string.Empty)
                {
                    propertyBag.HeaderCssClass = fillerParams.OverrideSettings[ControlLibConstants.DENOM_HEADER_CSS];
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.DENOM_FOOTER_CSS] != string.Empty)
                {
                    propertyBag.FooterCssClass = fillerParams.OverrideSettings[ControlLibConstants.DENOM_FOOTER_CSS];
                }

                if (propertyBag.Style == null && (fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) || fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT)))
                {
                    propertyBag.Style = new StylePropertyBag();
                }
                if (propertyBag.Style != null)
                {
                    propertyBag.Style.Width = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.WIDTH) ? fillerParams.OverrideSettings[ControlLibConstants.WIDTH] : string.Empty;
                    propertyBag.Style.Height = fillerParams.OverrideSettings.Keys.Contains(ControlLibConstants.HEIGHT) ? fillerParams.OverrideSettings[ControlLibConstants.HEIGHT] : string.Empty;
                }
                if (fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS] != string.Empty)
                {
                    propertyBag.CssClass = fillerParams.OverrideSettings[ControlLibConstants.CSS_CLASS];
                }
            }

            propertyBag.ReadOnly = fillerParams.IsReadOnly.HasValue ? fillerParams.IsReadOnly.Value : propertyBag.ReadOnly;
            propertyBag.Enabled = fillerParams.IsEnabled.HasValue ? fillerParams.IsEnabled.Value : propertyBag.Enabled;
            SetVisibility(propertyBag, fillerParams);
            propertyBag.DenomTemplate = ControlLibraryConfig.ControlConfigReader.GetDenomTemplate(propertyBag.TemplateNameKey);
            //if (fillerParams.OverrideSettings.ContainsKey(ControlLibConstants.DENOM_PRIMARY_ID))
            //    propertyBag.PrimaryIdMember = fillerParams.OverrideSettings[ControlLibConstants.DENOM_PRIMARY_ID];

            if (fillerParams.OverrideSettings.ContainsKey(ControlLibConstants.ACTION_URL))
                propertyBag.ActionUrl = fillerParams.OverrideSettings[ControlLibConstants.ACTION_URL];

            if (fillerParams.OverrideSettings.ContainsKey(ControlLibConstants.OTHER_AMOUNT_POSITION))
                propertyBag.OtherAmountPosition = (PositionType)Enum.Parse(typeof(PositionType), fillerParams.OverrideSettings[ControlLibConstants.OTHER_AMOUNT_POSITION], true);

            if (fillerParams.OverrideSettings.ContainsKey(ControlLibConstants.DENOM_MODE))
                propertyBag.DenomMode = (DenomModeType)Enum.Parse(typeof(DenomModeType), fillerParams.OverrideSettings[ControlLibConstants.DENOM_MODE], true);

            if (fillerParams.InputParam != null)
                propertyBag.TenderInfoParam = fillerParams.InputParam;
        }

        private void SetVisibility(ControlPropertyBag propertyBag, FillerParams fillerParams)
        {
            IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

            if (propertyConfig != null && propertyConfig.PropertyConfiguration != null && propertyConfig.PropertyConfiguration.SiteConfig != null && propertyConfig.PropertyConfiguration.SiteConfig.Count > 0)
            {
                var siteConfig = propertyConfig.PropertyConfiguration.SiteConfig.Find(o => o.SiteConfigType == SiteConfigType.Visible);
                if (siteConfig != null)
                {
                    ISiteConfigSetting siteConfigSetting =this.GetSiteConfigSetting(siteConfig.ConfigKey);
                    if (siteConfigSetting != null)
                    {
                        if (siteConfigSetting.ConfigValue == false)
                        {
                            propertyBag.Visibility = false;
                        }
                    }
                }
            }
        }
    }
}