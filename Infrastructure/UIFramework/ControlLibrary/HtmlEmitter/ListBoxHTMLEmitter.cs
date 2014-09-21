using System.Text;
using System.Web.Mvc;
namespace Controls.ControlLibrary
{
    internal class ListBoxHTMLEmitter : ControlHTMLEmitter
    {
        #region "Member Variables"

        private TemplateListPropertyBag _propertyBag;
        private string _controlID;
        private TagBuilder _hiddenTag;
        string _templateHtml = string.Empty;
        string _templateScriptTagID = string.Empty;

        #endregion

        #region "Constructors"

        public ListBoxHTMLEmitter(string value, TemplateListPropertyBag propertyBag)
            : base(propertyBag.Validators, propertyBag.Mandatory)
        {
            this._propertyBag = propertyBag;
            this.Value = value;
            getTemplateHtml(propertyBag.TemplateHTML, out _templateHtml);
            _templateScriptTagID = string.Format("template_{0}", this._propertyBag.ControlName);
        }

        #endregion

        #region "Implemented Methods"

        public override void Emit(out string controlHTMLString)
        {
            controlHTMLString = string.Empty;
            if (_propertyBag.Visibility)
            {
                TagBuilder div = new TagBuilder(TAG_DIV);
                this.SetInnerHtml(div, BuildList());

                this.SetInnerHtml(div, GetErrorLabel(this._propertyBag), true);
                controlHTMLString = div.ToString();
            }
        }

        #endregion

        #region "Private Methods"


        private string BuildList()
        {
            StringBuilder control = new StringBuilder();

            TagBuilder div = new TagBuilder(TAG_DIV);
            this.SetID(div, this._propertyBag.ControlName, out _controlID);
            this.SetFunction(div, ATTRIBUTE_DATA_ONCLICK, _propertyBag.OnClickFunction);
            this.SetAttribute(div, ATTRIBUTE_DATA_BTEMPLATELIST, VAL_TRUE);

            this.SetAttribute(div, ATTRIBUTE_DATA_TEMPLATEID, _templateScriptTagID);
            this.SetAttribute(div, ATTRIBUTE_DATA_SELECTED_ITEM_CSSCLASS, _propertyBag.ListItemSelectedCssClass);

            this.SetInnerHtml(div, buildTemplate());

            this.SetControlCssClasses(div, this._propertyBag.ListItemTemplateCssClass, this._propertyBag.ControlErrorCssClass);
            this.BuildHiddenTag("hid_" + _propertyBag.ControlName, _propertyBag.ControlName, Value, out _hiddenTag);
            this.SetValidations(_hiddenTag, _propertyBag.ReadOnly);
            this.SetAttribute(div, ATTRIBUTE_DATA_LISTLENGTH, _propertyBag.ListDisplayLength);

            control.Append(div.ToString());
            control.Append(_hiddenTag.ToString(TagRenderMode.SelfClosing));
            control.Append(AppendTemplate());
            control.Append(BuildScript());
            return control.ToString();
        }        

        private string buildTemplate()
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
                    var dropItems = item.Split(',');
                    templateList.AppendFormat(_templateHtml, dropItems);
                }
            }
            return templateList.ToString();
        }

        private string BuildScript()
        {
            StringBuilder script = new StringBuilder();
            return script.ToString();
        }

        private string AppendTemplate()
        {
            TagBuilder html = new TagBuilder(TAG_SCRIPT);
            SetAttribute(html, ATTRIBUTE_LANG, SCRIPT_NAME);
            SetAttribute(html, ATTRIBUTE_TYPE, TEMPLATE_SCRIPT_TYPE);
            SetAttribute(html, ATTRIBUTE_ID, _templateScriptTagID);
            SetInnerHtml(html, ConvertToJSTemplate(_templateHtml));
            return html.ToString();

        }

        private void getTemplateHtml(string templateHtml, out string template)
        {
            template = string.Format("{0} {1} {2} {3} {4}", TEMPLATE_UL_PREFIX, TEMPLATE_PREFIX, templateHtml, TEMPLATE_SUFFIX, TEMPLATE_UL_SUFFIX);
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
