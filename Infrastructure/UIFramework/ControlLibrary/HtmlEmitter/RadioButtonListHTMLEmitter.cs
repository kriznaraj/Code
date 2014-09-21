using System.Text;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    internal class RadioButtonListHTMLEmitter : ControlHTMLEmitter
    {
        #region "Member Variables"

        private RadioButtonListPropertyBag _propertyBag;
        private string _controlID;
        private TagBuilder _hiddenTag;

        #endregion "Member Variables"

        #region "Constructors"

        public RadioButtonListHTMLEmitter(string value, RadioButtonListPropertyBag propertyBag)
            : base(propertyBag.Validators, propertyBag.Mandatory)
        {
            this._propertyBag = propertyBag;
            this.Value = value;
        }

        #endregion "Constructors"

        #region "Properties"

        #endregion "Properties"

        #region "Implemented Methods"

        public override void Emit(out string controlHTMLString)
        {
            controlHTMLString = string.Empty;
            if (_propertyBag.Visibility)
            {
                controlHTMLString = BuildList();
            }
        }

        #endregion "Implemented Methods"

        #region "Private Methods"

        private string BuildList()
        {
            StringBuilder control = new StringBuilder();

            TagBuilder div = new TagBuilder(TAG_DIV);
            this.SetID(div, this._propertyBag.ControlName, out _controlID);
            this.SetAttribute(div, ATTRIBUTE_DATA_BRADIOLIST, VAL_TRUE);
            this.SetFunction(div, ATTRIBUTE_DATA_ONCLICK, _propertyBag.OnClickFunction);
            BuildHidden();
            StringBuilder insideDiv = new StringBuilder();
            if (false == string.IsNullOrEmpty(_propertyBag.ListItem))
            {
                var chkItems = this._propertyBag.ListItem.Split('|');
                if (_propertyBag.IsVerticalAllign)
                {
                    //this.SetControlCssClasses(div, this._propertyBag.CssClass + "-vertical", this._propertyBag.ControlErrorCssClass);
                    TagBuilder ul = new TagBuilder(TAG_UL);
                    StringBuilder insideUl = new StringBuilder();
                    var itemRemaining = _propertyBag.ListDisplayLength;
                    foreach (var item in chkItems)
                    {
                        if (false == string.IsNullOrEmpty(item.Trim()))
                        {
                            if (itemRemaining-- == 0)
                            {
                                break;
                            }
                            TagBuilder li = new TagBuilder(TAG_LI);
                            var checkboxItem = item.Split(',');
                            this.SetInnerHtml(li, buildInputTag(checkboxItem));
                            insideUl.Append(li);
                        }
                    }
                    this.SetInnerHtml(ul, insideUl.ToString());
                    insideDiv.Append(ul);
                    //set vertical allign to true
                    this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_ISVERTICAL, VAL_TRUE);
                }
                else
                {
                    //this.SetControlCssClasses(div, this._propertyBag.CssClass, this._propertyBag.ControlErrorCssClass);
                    var itemRemaining = _propertyBag.ListDisplayLength;
                    foreach (var item in chkItems)
                    {
                        if (false == string.IsNullOrEmpty(item.Trim()))
                        {
                            if (itemRemaining-- == 0)
                            {
                                break;
                            }
                            var checkboxItem = item.Split(',');
                            insideDiv.Append(buildInputTag(checkboxItem));
                        }
                    }
                }
            }
            if (_propertyBag.IsVerticalAllign)
            {
                this.SetControlCssClasses(div, this._propertyBag.CssClass + "-vertical", this._propertyBag.ControlErrorCssClass);
                //set vertical allign to true
                this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_ISVERTICAL, VAL_TRUE);
            }
            else
            {
                this.SetControlCssClasses(div, this._propertyBag.CssClass, this._propertyBag.ControlErrorCssClass);
            }
            insideDiv.Append(GetErrorLabel(this._propertyBag));

            this.SetInnerHtml(div, insideDiv.ToString());
            control.Append(div.ToString());
            control.Append(_hiddenTag.ToString(TagRenderMode.SelfClosing));
            return control.ToString();
        }

        private void BuildHidden()
        {
            this.BuildHiddenTag("hid_" + _propertyBag.ControlName, _propertyBag.ControlName, Value, out _hiddenTag);
            this.SetValidations(_hiddenTag, _propertyBag.ReadOnly);
            this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_LISTLENGTH, _propertyBag.ListDisplayLength);
        }

        private string buildInputTag(string[] checkBoxItem)
        {
            StringBuilder control = new StringBuilder();

            TagBuilder label = new TagBuilder(TAG_LABEL);

            TagBuilder input = new TagBuilder(TAG_INPUT);
            this.SetAttribute(input, ATTRIBUTE_TYPE, ATTR_VAL_RADIO);
            this.SetAttribute(input, ATTRIBUTE_VALUE, checkBoxItem[0]);
            this.SetAttribute(input, ATTRIBUTE_NAME, "radio_" + _propertyBag.ControlName);
            if (checkBoxItem.Length > 2)
            {
                if (checkBoxItem[2].Equals("selected"))
                {
                    this.SetAttribute(input, ATTRIBUTE_CHECKED, ATTRIBUTE_CHECKED);
                }
                if (checkBoxItem[2].Equals("disabled"))
                {
                    this.SetAttribute(input, ATTRIBUTE_DISABLED, ATTRIBUTE_DISABLED);
                    control.Append(input.ToString(TagRenderMode.SelfClosing));
                }
            }
            control.Append(input.ToString(TagRenderMode.SelfClosing));
            TagBuilder span = new TagBuilder(TAG_SPAN);
            this.SetInnerValue(span, checkBoxItem[1]);
            control.Append(span.ToString());
            this.SetInnerHtml(label, control.ToString());

            return label.ToString();
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