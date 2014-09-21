using System;
using System.Text;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    internal class TextAreaHTMLEmitter : ControlHTMLEmitter
    {
        #region "Member Variables"

        private TextAreaPropertyBag _propertyBag;
        private string _controlID;

        #endregion "Member Variables"

        #region "Constructors"

        public TextAreaHTMLEmitter(string value, TextAreaPropertyBag propertyBag)
            : base(propertyBag.Validators, propertyBag.Mandatory)
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
                TagBuilder divTag = new TagBuilder(TAG_DIV);
                StringBuilder control = new StringBuilder();
                control.Append(BuildSpan());
                control.Append(BuildScript());
                divTag.InnerHtml = control.ToString();
                controlHTMLString = divTag.ToString();
            }
        }

        #endregion "Implemented Methods"

        #region "Private Methods"

        private string BuildSpan()
        {
            //TagBuilder spanTag = new TagBuilder(TAG_SPAN);
            StringBuilder control = new StringBuilder();
            control.Append(BuildTextArea());
            control.Append(GetErrorLabel(this._propertyBag));
            //spanTag.InnerHtml = control.ToString();
            //return spanTag.ToString();
            return control.ToString();
        }

        private string BuildTextArea()
        {
            TagBuilder controlTag = new TagBuilder(TAG_TEXTAREA);
            this.SetAttribute(controlTag, ATTRIBUTE_TYPE, ATTR_VAL_TEXT);

            this.SetID(controlTag, this._propertyBag.ControlName, out _controlID);
            this.SetAttribute(controlTag, ATTRIBUTE_NAME, this._propertyBag.ControlName);
            this.SetControlCssClasses(controlTag, this._propertyBag.CssClass, this._propertyBag.ControlErrorCssClass);
            if (this._propertyBag.Style != null)
            {
                this.SetAttribute(controlTag, ATTRIBUTE_STYLE, this._propertyBag.Style.GetStyle());
            }

            this.SetValidations(controlTag, _propertyBag.ReadOnly);

            this.SetAttribute(controlTag, ATTRIBUTE_TABINDEX, this._propertyBag.TabIndex);
            this.SetAttribute(controlTag, ATTRIBUTE_TITLE, this._propertyBag.ToolTip);
            this.SetInnerValue(controlTag, this.Value);
            this.SetAttribute(controlTag, ATTRIBUTE_PLACEHOLDER, this._propertyBag.WaterMarkText);
            this.SetAttribute(controlTag, ATTRIBUTE_READONLY, ATTR_VAL_READONLY, this._propertyBag.ReadOnly);
            this.SetAttribute(controlTag, ATTRIBUTE_DISABLED, ATTR_VAL_DISABLED, !this._propertyBag.Enabled);
            this.SetFunction(controlTag, ATTRIBUTE_DATA_ONLEAVE, _propertyBag.OnLeaveFunction);
            this.SetFunction(controlTag, ATTRIBUTE_ONKEYUP, _propertyBag.OnKeyUpFunction);
            this.SetFunction(controlTag, ATTRIBUTE_ONCHANGE, _propertyBag.OnChangeFunction);
            this.SetFunction(controlTag, ATTRIBUTE_ONKEYDOWN, _propertyBag.OnKeyDownFunction);
            this.SetCustomAttributes(controlTag, _propertyBag.Attributes);

            if (this.LengthValidation != null)
            {
                this.SetAttribute(controlTag, ATTRIBUTE_MAXLENGTH, this.LengthValidation.MaxLength);
            }

            this.SetLeaveEvent(controlTag);
            return controlTag.ToString();
        }

        private string BuildScript()
        {
            TagBuilder ScriptTag = new TagBuilder(TAG_SCRIPT);
            this.SetAttribute(ScriptTag, ATTRIBUTE_LANG, SCRIPT_NAME);
            StringBuilder script = new StringBuilder();
            ScriptTag.InnerHtml = script.ToString();
            return Convert.ToString(ScriptTag);
        }

        #endregion "Private Methods"
    }
}