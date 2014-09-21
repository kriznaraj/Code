using System.Text;
using System.Web.Mvc;
namespace Controls.ControlLibrary
{
    internal class ShuttleHTMLEmitter : ControlHTMLEmitter
    {
        #region "Member Variables"

        private ShuttlePropertyBag _propertyBag;
        private string _controlID;
        private TagBuilder _hiddenTag;

        #endregion

        #region "Constructors"

        public ShuttleHTMLEmitter(string value, ShuttlePropertyBag propertyBag)
            : base(propertyBag.Validators, propertyBag.Mandatory)
        {
            this._propertyBag = propertyBag;
            this.Value = value;
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
                this.SetInnerHtml(div, BuildGrid());

                this.SetInnerHtml(div, GetErrorLabel(this._propertyBag), true);
                buildHidden();
                this.SetInnerHtml(div, _hiddenTag.ToString(TagRenderMode.SelfClosing), true);
                controlHTMLString = div.ToString();
            }
        }

        #endregion

        #region "Private Methods"

        private string BuildGrid()
        {
            TagBuilder div = new TagBuilder(TAG_DIV);
            this.SetID(div, this._propertyBag.ControlName, out _controlID);
            //this.SetAttribute(div, ATTRIBUTE_DATA_GRIDPARAM, HTMLEmitterUtility.GetGridControlProperty(this.propertyBag));
            //this.SetAttribute(div, ATTRIBUTE_DATA_GRIDCOLUMNPROP, HTMLEmitterUtility.GetGridControlColumnsProperties(this.propertyBag));

            this.SetAttribute(div, ATTRIBUTE_DATA_INPUTPARAM, HTMLEmitterUtility.GetJsonOfObject(this._propertyBag.ShuttleParam));
            this.SetAttribute(div, ATTRIBUTE_DATA_VALUEMEMBER, _propertyBag.ValueMember);
            this.SetAttribute(div, ATTRIBUTE_DATA_ACTIONURL, _propertyBag.ActionUrl);
            this.SetAttribute(div, ATTRIBUTE_DATA_DISPLAYMEMBER, _propertyBag.DisplayMember);
            this.SetAttribute(div, ATTRIBUTE_DATA_BSHUTTLE, VAL_TRUE);

            return div.ToString();
        }

        private void buildHidden()
        {
            this.BuildHiddenTag("hid_" + this._propertyBag.ControlName, this._propertyBag.ControlName, this.Value, out _hiddenTag);
            this.SetFunction(_hiddenTag, ATTRIBUTE_ONCHANGE, this._propertyBag.OnChangeFunction);
        }


        private string BuildScript()
        {
            StringBuilder script = new StringBuilder();
            return script.ToString();
        }

        #endregion

    }
}
    