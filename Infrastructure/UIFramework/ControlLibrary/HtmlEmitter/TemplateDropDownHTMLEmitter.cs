using System.Text;
using System.Web.Mvc;
namespace Controls.ControlLibrary
{
    internal class TemplateDropDownHTMLEmitter : ControlHTMLEmitter
    {
        #region "Member Variables"

        private TemplateDropDownPropertyBag _propertyBag;
        private string _controlID;
        private TagBuilder _hiddenTag;
        string _templateHtml = string.Empty;
        string _templateScriptTagID = string.Empty;
        #endregion

        #region "Constructors"

        public TemplateDropDownHTMLEmitter(string value, TemplateDropDownPropertyBag propertyBag)
            : base(propertyBag.Validators, propertyBag.Mandatory)
        {
            this._propertyBag = propertyBag;
            this.Value = value;
            getTemplateHtml(propertyBag.TemplateHTML, out _templateHtml);
            _templateScriptTagID = string.Format("template_{0}", this._propertyBag.ControlName);
        }


        #endregion

        #region "Properties"

        #endregion

        #region "Implemented Methods"

        public override void Emit(out string controlHTMLString)
        {
            controlHTMLString = string.Empty;
            if (_propertyBag.Visibility)
            {
                TagBuilder div = new TagBuilder(TAG_DIV);
                this.buildHidden();
                this.SetInnerHtml(div, BuildMultiColumnCombo());
                this.SetInnerHtml(div, GetErrorLabel(this._propertyBag), true);
                controlHTMLString = div.ToString();
            }
        }

        #endregion

        #region "Private Methods"

        private string BuildMultiColumnCombo()
        {
            StringBuilder control = new StringBuilder();
            TagBuilder ul = new TagBuilder(TAG_UL);
            this.SetID(ul, this._propertyBag.ControlName, out _controlID);
            this.SetAttribute(ul, ATTRIBUTE_DATA_BTEMPLATECOMBO, VAL_TRUE);

            this.SetControlCssClasses(ul, this._propertyBag.CssClass, this._propertyBag.ControlErrorCssClass);
            this.SetAttribute(ul, ATTRIBUTE_DATA_TEMPLATEID, _templateScriptTagID);
            this.SetInnerHtml(ul, buildTemplate());
            this.SetValidations(_hiddenTag, _propertyBag.ReadOnly);
            this.SetAttribute(ul, ATTRIBUTE_DATA_LISTLENGTH, _propertyBag.ListDisplayLength);
            control.Append(this._hiddenTag.ToString(TagRenderMode.SelfClosing));
            control.Append(ul.ToString());
            control.Append(AppendTemplate());
            control.Append(BuildScript());
            return control.ToString();
        }

        private string buildTemplate()
        {
            if (false == string.IsNullOrEmpty(this._propertyBag.ListItem))
            {
                var options = this._propertyBag.ListItem.Split('|');
                StringBuilder templateList = new StringBuilder();
                var itemRemaining = _propertyBag.ListDisplayLength;
                foreach (var item in options)
                {
                    if (false == string.IsNullOrEmpty(item.Trim()))
                    {
                        if (itemRemaining-- == 0)
                        {
                            break;
                        }
                        var listItems = item.Split(',');
                        templateList.AppendFormat(_templateHtml, listItems);
                    }
                }
                return templateList.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private string BuildScript()
        {
            StringBuilder script = new StringBuilder();
            return script.ToString();
        }

        private string AppendTemplate()
        {
            if (true == string.IsNullOrEmpty( this._propertyBag.ListItem))
            {
                TagBuilder html = new TagBuilder(TAG_SCRIPT);
                SetAttribute(html, ATTRIBUTE_LANG, SCRIPT_NAME);
                SetAttribute(html, ATTRIBUTE_TYPE, TEMPLATE_SCRIPT_TYPE);
                SetAttribute(html, ATTRIBUTE_ID, _templateScriptTagID);
                SetInnerHtml(html, ConvertToJSTemplate(_templateHtml));
                return html.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private void getTemplateHtml(string templateHtml, out string template)
        {
            template = templateHtml;
        }

        private void buildHidden()
        {
            this.BuildHiddenTag("hid_" + this._propertyBag.ControlName, this._propertyBag.ControlName, this.Value, out _hiddenTag);
            //this.SetFunction(_hiddenTag, ATTRIBUTE_ONCHANGE, this._propertyBag.OnDataRowSelectFunction);
        }

        private string ConvertToJSTemplate(string template)
        {
            template = template.Replace("{0}", "${id}");
            template = template.Replace("{1}", "${d1}");
            template = template.Replace("{2}", "${d2}");
            template = template.Replace("{3}", "${d3}");
            template = template.Replace("{4}", "${d4}");
            template = template.Replace("{5}", "${d5}");
            return template;
        }

        #endregion

    }
}
