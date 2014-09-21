using System.Text;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    internal class DropDownHTMLEmitter : ControlHTMLEmitter
    {
        #region "Member Variables"

        private DropDownPropertyBag _propertyBag;
        private TagBuilder _hiddenTag;

        #endregion "Member Variables"

        #region "Constructors"

        public DropDownHTMLEmitter(string value, DropDownPropertyBag propertyBag)
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
                TagBuilder divTag = new TagBuilder(TAG_DIV);
                this.SetControlCssClasses(divTag, this._propertyBag.CssClass, this._propertyBag.ControlErrorCssClass);
                StringBuilder control = new StringBuilder();
                control.Append(BuildSelect());
                control.Append(GetErrorLabel(this._propertyBag));
                divTag.InnerHtml = control.ToString();
                controlHTMLString = divTag.ToString();
            }
        }

        #endregion "Implemented Methods"

        #region "Private Methods"

        private string BuildSelect()
        {
            StringBuilder control = new StringBuilder();

            if (_propertyBag.DropDownType == DropDownType.MultiSelect)
            {
                this.BuildHiddenTag(_propertyBag.ControlName, Value, out _hiddenTag);
                this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_SELECTED_VAL, Value);
                control.Append(buildComboTag("combo_" + _propertyBag.ControlName, "hid_" + _propertyBag.ControlName));
                //For multiselct set validation on hiddenTag
                this.SetValidations(_hiddenTag, _propertyBag.ReadOnly);

                SetFunction(_hiddenTag, ATTRIBUTE_ONCHANGE, _propertyBag.OnChangeFunction);
            }
            else
            {
                //this.BuildHiddenTag(_propertyBag.ControlName, "hid_" + _propertyBag.ControlName, Value, out _hiddenTag);
                //control.Append(buildComboTag("combo_" + _propertyBag.ControlName, _propertyBag.ControlName));

                //Changes made after Venkat changes in krishnaCombo-box.js
                this.BuildHiddenTag(_propertyBag.ControlName, Value, out _hiddenTag);
                this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_SELECTED_VAL, Value);
                control.Append(buildComboTag("combo_" + _propertyBag.ControlName, "hid_" + _propertyBag.ControlName));

                this.SetValidations(_hiddenTag, _propertyBag.ReadOnly);

                if (_propertyBag.DropDownType == DropDownType.CascadeSelect)
                {
                    this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_TARGETCONTROL, this._propertyBag.TargetControlID);
                    this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_ACTIONURL, this._propertyBag.ActionURL);
                    this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_CASCADE_INPUT_FUNCTION, this._propertyBag.CascadeInputFunction);
                }
                this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_DISPLAYMEMBER, this._propertyBag.DisplayMember);
                this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_VALUEMEMBER, this._propertyBag.ValueMember);
            }

            this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_BCOMBO, VAL_TRUE);

            //Water marktext will be used as title for combo
            this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_COMBOTITLE, _propertyBag.WaterMarkText);

            this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_COMBOTYPE, _propertyBag.DropDownType);

            this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_LISTLENGTH, _propertyBag.ListDisplayLength);

            control.Append(_hiddenTag.ToString(TagRenderMode.SelfClosing));

            return control.ToString();
        }

        private string buildOptions(string[] items)
        {
            TagBuilder option = new TagBuilder(TAG_OPTION);
            this.SetAttribute(option, ATTRIBUTE_VALUE, items[0]);
            this.SetInnerValue(option, items[1]);

            if (items.Length > 2)
            {
                if (items[2].Equals("selected"))
                {
                    this.SetAttribute(option, ATTRIBUTE_SELECTED, ATTRIBUTE_SELECTED);
                }
                if (items[2].Equals("disabled"))
                {
                    this.SetAttribute(option, ATTRIBUTE_DISABLED, ATTRIBUTE_DISABLED);
                }
            }

            return option.ToString();
        }

        private string buildComboTag(string id, string name, string value = null)
        {
            TagBuilder comboTag = new TagBuilder(TAG_SELECT);
            this.SetAttribute(comboTag, ATTRIBUTE_NAME, name);
            this.SetAttribute(comboTag, ATTRIBUTE_TITLE, this._propertyBag.ToolTip);
            this.SetAttribute(comboTag, ATTRIBUTE_TABINDEX, this._propertyBag.TabIndex);
            this.SetID(comboTag, id, out id);

            var options = this._propertyBag.Options.Split('|');
            if (_propertyBag.DropDownType != DropDownType.MultiSelect)
            {
                //For other combo set validation on comboTag
                //this.SetValidations(comboTag, _propertyBag.ReadOnly);

                //Changes made after Venkat changes in krishnaCombo-box.js
                SetFunction(comboTag, ATTRIBUTE_ONCHANGE, _propertyBag.OnChangeFunction);
            }
            else
            {
                SetFunction(comboTag, ATTRIBUTE_ONCHANGE, _propertyBag.OnChangeFunction);
                //This line is required for multiselect first item default selection issue.
                this.SetAttribute(comboTag, ATTRIBUTE_MULTIPLE, ATTRIBUTE_MULTIPLE);
            }
            var itemRemaining = _propertyBag.ListDisplayLength;
            foreach (var item in options)
            {
                if (false == string.IsNullOrEmpty(item.Trim()))
                {
                    if (itemRemaining-- == 0)
                    {
                        break;
                    }
                    var dropItems = item.Split(',');
                    this.SetInnerHtml(comboTag, buildOptions(dropItems), true);
                }
            }
            this.SetAttribute(comboTag, ATTRIBUTE_DISABLED, ATTR_VAL_DISABLED, !this._propertyBag.Enabled);
            this.SetCustomAttributes(comboTag, _propertyBag.Attributes);
            return comboTag.ToString();
        }

        private string BuildScript()
        {
            TagBuilder ScriptTag = new TagBuilder(TAG_SCRIPT);
            this.SetAttribute(ScriptTag, ATTRIBUTE_LANG, SCRIPT_NAME);

            StringBuilder script = new StringBuilder();
            //script.Append(string.Format("controlHelper.OnComboDataLoaded('{0}')", this.propertyBag.ControlName));
            return GetScriptString(ScriptTag, script);
        }

        #endregion "Private Methods"
    }
}