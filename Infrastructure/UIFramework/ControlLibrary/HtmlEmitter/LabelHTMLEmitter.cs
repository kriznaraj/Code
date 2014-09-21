using Controls.Framework.Interfaces;
using System.Text;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    internal class LabelHTMLEmitter : ControlHTMLEmitter
    {
        #region "Member Variables"

        private LabelPropertyBag _propertyBag;

        #endregion "Member Variables"

        #region "Constructors"

        public LabelHTMLEmitter(string value, LabelPropertyBag propertyBag)
            : base(null, null)
        {
            this._propertyBag = propertyBag;
            this.Value = value;
        }

        #endregion "Constructors"

        #region "Implemented Methods"

        public override void Emit(out string controlHTMLString)
        {
            controlHTMLString = string.Empty;
            if (_propertyBag.Visibility)
            {
                controlHTMLString = BuildSpan(); 
            }
        }

        #endregion "Implemented Methods"

        #region "Private Methods"

        private string BuildSpan()
        {
            StringBuilder control = new StringBuilder();

            control.Append(BuildLabel());
            //After build 1
            //control.Append(BuildMandatorySpan());
            return control.ToString();
        }

        private string BuildLabel()
        {
            TagBuilder controlTag = new TagBuilder(TAG_LABEL);

            if (this._propertyBag.IsBindingControl)
                this.SetAttribute(controlTag, ATTRIBUTE_NAME, string.Format("lbl_{0}", this._propertyBag.ControlName));
            else
                this.SetAttribute(controlTag, ATTRIBUTE_NAME, this._propertyBag.ControlName);

            this.SetCssClass(controlTag, this._propertyBag.CssClass);
            
            if (this._propertyBag.Style != null)
            {
                this.SetAttribute(controlTag, ATTRIBUTE_STYLE, this._propertyBag.Style.GetStyle());
            }

            if (this._propertyBag.DisplayType == DisplayType.Label)
            {
                this.SetAttribute(controlTag, ATTRIBUTE_TITLE, this._propertyBag.ToolTip);
                this.SetInnerValue(controlTag, this._propertyBag.Label);
            }

            else if (this._propertyBag.DisplayType == DisplayType.TextBlock)
            {
                if (!string.IsNullOrEmpty(_propertyBag.OverrideToolTip))
                {
                    this.SetAttribute(controlTag, ATTRIBUTE_TITLE, _propertyBag.OverrideToolTip);
                }
                else
                {
                    this.SetAttribute(controlTag, ATTRIBUTE_TITLE, this._propertyBag.ToolTip);
                }
                if (_propertyBag.IsCurrency)
                {
                    this.SetInnerHtml(controlTag, _propertyBag.CurrencySymbol + this.Value);
                }
                else
                {
                    this.SetInnerHtml(controlTag, this.Value);
                }
            }
            this.SetInnerHtml(controlTag, BuildMandatorySpan(), true);
            //After build 1
            this.SetCustomAttributes(controlTag, _propertyBag.Attributes);

            return controlTag.ToString();
        }

        private string BuildMandatorySpan()
        {
            if (_propertyBag.DisplayType == DisplayType.Label)
            {
                if (this._propertyBag.Validators != null)
                {
                    foreach (ValidationBase vali in this._propertyBag.Validators)
                    {
                        if (vali.Type == ValidatorsType.Required && vali.DoValidate)
                        {
                            if (MandatoryCheck())
                            {
                                return GetMandatoryLabelHTML();
                            }                            
                        }
                    }
                }
                if (this._propertyBag.IsMandatory)
                {
                    return GetMandatoryLabelHTML();
                }
            }
            return string.Empty;
        }

        private bool MandatoryCheck()
        {
            bool mandatory = true;

            IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(_propertyBag._fillerParams.ModelName, _propertyBag._fillerParams.PropertyName, _propertyBag._fillerParams.ConfigKey);

            if (propertyConfig != null && propertyConfig.PropertyConfiguration != null && propertyConfig.PropertyConfiguration.SiteConfig != null && propertyConfig.PropertyConfiguration.SiteConfig.Count > 0)
            {
                var siteConfig = propertyConfig.PropertyConfiguration.SiteConfig.Find(o => o.SiteConfigType == SiteConfigType.Mandatory);
                if (siteConfig != null)
                {
                    ISiteConfigSetting siteConfigSetting = ControlLibraryConfig.SiteConfigProvider.Current.GetSiteConfig(siteConfig.ConfigKey);
                    if (siteConfigSetting != null)
                    {
                        if (siteConfigSetting.ConfigValue == false)
                        {
                            mandatory = false;
                        }
                    }
                }
            }
            return mandatory;
        }

        private string GetMandatoryLabelHTML()
        {
            TagBuilder mandarotySpanTag = new TagBuilder(TAG_SPAN);
            this.SetCssClass(mandarotySpanTag, this._propertyBag.MandatoryCharCssClass);
            this.SetInnerValue(mandarotySpanTag, this._propertyBag.MandatoryChar);
            return mandarotySpanTag.ToString();
        }

        #endregion "Private Methods"
    }
}