using System.Text;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    internal class TextBoxHTMLEmitter : ControlHTMLEmitter
    {
        #region "Member Variables"

        private TextBoxPropertyBag _propertyBag;
        private string _controlID;
        private string _hiddenTag = string.Empty;

        #endregion "Member Variables"

        #region "Constructors"

        public TextBoxHTMLEmitter(string value, TextBoxPropertyBag propertyBag)
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
            StringBuilder control = new StringBuilder();
            control.Append(BuildTextBox());
            control.Append(_hiddenTag);
            control.Append(GetErrorLabel(this._propertyBag));
            return control.ToString();
        }

        private string BuildTextBox()
        {
            TagBuilder controlTag = new TagBuilder(TAG_INPUT);
            this.SetAttribute(controlTag, ATTRIBUTE_TYPE, ATTR_VAL_TEXT);
            this.SetID(controlTag, this._propertyBag.ControlName, out _controlID);
            this.SetControlCssClasses(controlTag, this._propertyBag.CssClass, this._propertyBag.ControlErrorCssClass);

            if (true == _propertyBag.Masking)
            {
                this.BuildHiddenTag(this._propertyBag.ControlName, this._propertyBag.EncryptedValue, out _hiddenTag);
                this.SetAttribute(controlTag, ATTRIBUTE_NAME, "hid_" + this._propertyBag.ControlName);
            }
            else
            {
                this.SetAttribute(controlTag, ATTRIBUTE_NAME, this._propertyBag.ControlName);
            }

            if (this._propertyBag.Style != null)
            {
                this.SetAttribute(controlTag, ATTRIBUTE_STYLE, this._propertyBag.Style.GetStyle());
            }

            if (_propertyBag.AutoCompleteProperties != null && _propertyBag.AutoComplete)
            {
                this.SetAttribute(controlTag, _propertyBag.AutoComplete, ATTRIBUTE_DATA_ACPARAM, HTMLEmitterUtility.GetAutoCompleteParams(_propertyBag.AutoCompleteProperties));
            }

            this.SetValidations(controlTag, _propertyBag.ReadOnly);
            this.SetAutoCompleteAttributes(controlTag, this._propertyBag.AutoComplete, _propertyBag.AutoCompleteInputFunction);

            this.SetAttribute(controlTag, ATTRIBUTE_TABINDEX, this._propertyBag.TabIndex);
            this.SetAttribute(controlTag, ATTRIBUTE_TITLE, this._propertyBag.ToolTip);
            this.SetAttribute(controlTag, ATTRIBUTE_VALUE, this.Value);
            this.SetAttribute(controlTag, ATTRIBUTE_PLACEHOLDER, this._propertyBag.WaterMarkText);
            this.SetAttribute(controlTag, ATTRIBUTE_READONLY, ATTR_VAL_READONLY, this._propertyBag.ReadOnly);
            this.SetAttribute(controlTag, ATTRIBUTE_DISABLED, ATTR_VAL_DISABLED, !this._propertyBag.Enabled);
            this.SetLeaveEvent(controlTag);
            this.SetFunction(controlTag, ATTRIBUTE_DATA_ONLEAVE, _propertyBag.OnLeaveFunction);
            this.SetFunction(controlTag, ATTRIBUTE_ONKEYUP, _propertyBag.OnKeyUpFunction);
            this.SetFunction(controlTag, ATTRIBUTE_ONCHANGE, _propertyBag.OnChangeFunction);
            this.SetFunction(controlTag, ATTRIBUTE_ONKEYDOWN, _propertyBag.OnKeyDownFunction);
            this.SetCustomAttributes(controlTag, _propertyBag.Attributes);

            if (this.LengthValidation != null)
            {
                this.SetAttribute(controlTag, ATTRIBUTE_MAXLENGTH, this.LengthValidation.MaxLength);
            }

            return controlTag.ToString(TagRenderMode.SelfClosing);
        }

        private string BuildScript()
        {
            TagBuilder ScriptTag = new TagBuilder(TAG_SCRIPT);
            this.SetAttribute(ScriptTag, ATTRIBUTE_LANG, SCRIPT_NAME);

            StringBuilder script = new StringBuilder();
            return GetScriptString(ScriptTag, script);
        }

        #endregion "Private Methods"
    }
}