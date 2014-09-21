using System.Text;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    internal class ButtonHTMLEmitter : ControlHTMLEmitter
    {
        #region "Member Variables"

        private ButtonPropertyBag _propertyBag;
        private string _controlID;

        #endregion "Member Variables"

        #region "Constructors"

        public ButtonHTMLEmitter(string value, ButtonPropertyBag propertyBag)
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

                switch (_propertyBag.ButtonCatagory)
                {
                    case ButtonCatagory.BallyImageButton:
                        controlHTMLString = buildImageButton();
                        return;

                    case ButtonCatagory.BallyLinkButton:
                        this.SetInnerHtml(divTag, buildLinkButton());
                        break;

                    case ButtonCatagory.BallyButton:
                        this.SetInnerHtml(divTag, buildNormalButton());
                        break;

                    default:
                        break;
                }

                controlHTMLString = divTag.ToString();
            }
        }

        #endregion "Implemented Methods"

        #region "Private Methods"

        private string buildNormalButton()
        {
            TagBuilder controlTag = new TagBuilder(TAG_INPUT);

            this.SetAttribute(controlTag, ATTRIBUTE_TYPE, _propertyBag.ButtonType.ToString().ToLower());

            this.SetID(controlTag, this._propertyBag.ControlName, out _controlID);

            this.SetCssClass(controlTag, this._propertyBag.CssClass);

            if (this._propertyBag.Style != null)
            {
                this.SetAttribute(controlTag, ATTRIBUTE_STYLE, this._propertyBag.Style.GetStyle());
            }

            this.SetAttribute(controlTag, ATTRIBUTE_TABINDEX, this._propertyBag.TabIndex);
            this.SetAttribute(controlTag, ATTRIBUTE_TITLE, this._propertyBag.ToolTip);

            this.SetAttribute(controlTag, ATTRIBUTE_DISABLED, ATTR_VAL_DISABLED, !this._propertyBag.Enabled);

            this.SetAttribute(controlTag, ATTRIBUTE_VALUE, this._propertyBag.Label);

            if (true == _propertyBag.ValidateForm)
            {
                this.SetClickEvent(controlTag);
                this.SetFunction(controlTag, ATTRIBUTE_DATA_ONCLICK, _propertyBag.OnClickFunction);
            }
            else
            {
                this.SetFunction(controlTag, ATTRIBUTE_ONCLICK, _propertyBag.OnClickFunction);
            }

            this.SetAttribute(controlTag, ATTRIBUTE_DATA_BTN_VALIDATE, _propertyBag.ValidateForm.ToString().ToLower());

            this.SetAttribute(controlTag, ATTRIBUTE_DATA_PARENTID, _propertyBag.ParentID);

            return controlTag.ToString(TagRenderMode.SelfClosing);
        }

        private string buildImageButton()
        {
            TagBuilder ulTag = new TagBuilder(TAG_UL);
            this.SetID(ulTag, this._propertyBag.ControlName, out _controlID);
            TagBuilder liTag = new TagBuilder(TAG_LI);
            TagBuilder divTag = new TagBuilder(TAG_DIV);
            TagBuilder btnTag = new TagBuilder(TAG_BUTTON);
            TagBuilder imgTag = new TagBuilder(TAG_IMAGE);
            TagBuilder spanTag = new TagBuilder(TAG_SPAN);

            this.SetCssClass(ulTag, this._propertyBag.CssClass);
            this.SetAttribute(divTag, ATTRIBUTE_ALIGN, ATTR_VAL_ALIGN_CENTER);
            this.SetAttribute(imgTag, ATTRIBUTE_SOURCE, this._propertyBag.ImagePath);

            this.SetCssClass(imgTag, this._propertyBag.ImageClass);

            this.SetAttribute(imgTag, ATTRIBUTE_DATA_ISBALLYBUTTON, VAL_TRUE);
            this.DisableImageButton(btnTag, spanTag, this._propertyBag.Enabled);
            this.SetLeftAligned(ulTag, this._propertyBag.AlignLeft);

            if (this._propertyBag.Style != null)
            {
                this.SetAttribute(btnTag, ATTRIBUTE_STYLE, this._propertyBag.Style.GetStyle());
            }
            this.SetAttribute(btnTag, ATTRIBUTE_TABINDEX, this._propertyBag.TabIndex);
            this.SetAttribute(btnTag, ATTRIBUTE_TITLE, this._propertyBag.ToolTip);

            if (true == _propertyBag.ValidateForm)
            {
                this.SetClickEvent(btnTag);
                this.SetFunction(btnTag, ATTRIBUTE_DATA_ONCLICK, _propertyBag.OnClickFunction);
            }
            else
            {
                this.SetFunction(btnTag, ATTRIBUTE_ONCLICK, _propertyBag.OnClickFunction);
            }
            this.SetAttribute(btnTag, ATTRIBUTE_DATA_BTN_VALIDATE, _propertyBag.ValidateForm.ToString().ToLower());

            this.SetAttribute(btnTag, ATTRIBUTE_DATA_PARENTID, _propertyBag.ParentID);

            this.SetInnerValue(spanTag, this._propertyBag.Label);
            this.SetInnerHtml(btnTag, imgTag.ToString());
            this.SetInnerHtml(divTag, btnTag.ToString());
            this.SetInnerHtml(divTag, spanTag.ToString(), true);
            this.SetInnerHtml(liTag, divTag.ToString());
            this.SetInnerHtml(ulTag, liTag.ToString());

            return ulTag.ToString();
        }

        private string buildLinkButton()
        {
            TagBuilder aTag = new TagBuilder(TAG_A);
            this.SetID(aTag, this._propertyBag.ControlName, out _controlID);

            this.SetCssClass(aTag, _propertyBag.CssClass);
            if (this._propertyBag.Style != null)
            {
                this.SetAttribute(aTag, ATTRIBUTE_STYLE, this._propertyBag.Style.GetStyle());
            }
            if (string.IsNullOrEmpty(_propertyBag.OnClickFunction))
            {
                this.SetAttribute(aTag, ATTRIBUTE_HREF, this._propertyBag.ActionName);
            }
            else
            {
                this.SetAttribute(aTag, ATTRIBUTE_HREF, "#");
            }

            this.SetFunction(aTag, ATTRIBUTE_ONCLICK, _propertyBag.OnClickFunction);

            this.SetAttribute(aTag, ATTRIBUTE_TABINDEX, this._propertyBag.TabIndex);

            this.SetAttribute(aTag, ATTRIBUTE_DISABLED, ATTR_VAL_DISABLED, !this._propertyBag.Enabled);

            this.SetInnerValue(aTag, this._propertyBag.Label);

            TagBuilder sTag = new TagBuilder(TAG_SPAN);
            this.SetInnerValue(sTag, this._propertyBag.ToolTip);

            this.SetInnerHtml(aTag, sTag.ToString(), true);

            return aTag.ToString();
        }

        private void DisableImageButton(TagBuilder imgBtnTag, TagBuilder spanTag, bool isEnabled)
        {
            if (false == this._propertyBag.Enabled)
            {
                this.SetCssClass(imgBtnTag, _propertyBag.ImageButtonDisableClass);
                this.SetCssClass(spanTag, _propertyBag.ImageButtonLabelDisableClass);
                this.SetAttribute(imgBtnTag, ATTRIBUTE_DISABLED, ATTR_VAL_DISABLED);
            }
        }

        private void SetLeftAligned(TagBuilder ulTag, bool alignLeft)
        {
            if (alignLeft)
            {
                this.SetCssClass(ulTag, _propertyBag.ImageButtonLeftAlignClass);
            }
        }

        private string BuildScript()
        {
            TagBuilder ScriptTag = new TagBuilder(TAG_SCRIPT);
            this.SetAttribute(ScriptTag, ATTRIBUTE_LANG, SCRIPT_NAME);

            StringBuilder script = new StringBuilder();
            //TODO:

            return GetScriptString(ScriptTag, script);
        }

        #endregion "Private Methods"
    }
}