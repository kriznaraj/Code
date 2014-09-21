using System.Text;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    internal class GridHTMLEmitter : ControlHTMLEmitter
    {
        #region "Member Variables"

        private GridPropertyBag _propertyBag;
        private string _controlID;
        private TagBuilder _hiddenTag;
        private TagBuilder _sortedHiddenTag;
        private string _templateHtml = string.Empty;
        private string _templateScriptTagID = string.Empty;

        #endregion "Member Variables"

        #region "Constructors"

        public GridHTMLEmitter(string value, GridPropertyBag propertyBag)
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
                TagBuilder div = new TagBuilder(TAG_DIV);
                this.SetInnerHtml(div, BuildGrid());

                this.SetInnerHtml(div, GetErrorLabel(this._propertyBag), true);
                buildHidden();
                buildSortedFieldHidden();
                this.SetInnerHtml(div, _hiddenTag.ToString(TagRenderMode.SelfClosing), true);
                this.SetInnerHtml(div, _sortedHiddenTag.ToString(TagRenderMode.SelfClosing), true);
                controlHTMLString = div.ToString();
            }
        }

        #endregion "Implemented Methods"

        #region "Private Methods"

        private string BuildGrid()
        {
            TagBuilder div = new TagBuilder(TAG_DIV);
            this.SetID(div, this._propertyBag.ControlName, out _controlID);
            this.SetAttribute(div, ATTRIBUTE_DATA_GRIDPARAM, HTMLEmitterUtility.GetGridControlProperty(this._propertyBag));
            this.SetAttribute(div, ATTRIBUTE_DATA_GRIDCOLUMNPROP, HTMLEmitterUtility.GetGridControlColumnsProperties(this._propertyBag));

            this.SetAttribute(div, ATTRIBUTE_DATA_INPUTPARAM, HTMLEmitterUtility.GetJsonOfObject(this._propertyBag.GridParam));

            this.SetAttribute(div, ATTRIBUTE_DATA_VALUEMEMBER, _propertyBag.ValueMember);
            this.SetAttribute(div, ATTRIBUTE_DATA_ACTIONURL, _propertyBag.ActionUrl);

            this.SetAttribute(div, ATTRIBUTE_DATA_BGRID, VAL_TRUE);
            this.SetAttribute(div, ATTRIBUTE_DATA_SELECTION_CHANGE_FUNCTION, this._propertyBag.OnDataRowSelectionChangeFunctionName);

            return div.ToString();
        }

        private void buildHidden()
        {
            this.BuildHiddenTag("hid_" + this._propertyBag.ControlName, this._propertyBag.ControlName, this.Value, out _hiddenTag);
            this.SetFunction(_hiddenTag, ATTRIBUTE_ONCHANGE, this._propertyBag.OnDataRowSelectFunction);
        }

        //Added in 2nd build.
        private void buildSortedFieldHidden()
        {
            this.BuildHiddenTag("sort_" + this._propertyBag.ControlName, "sort." + this._propertyBag.ControlName, this.Value, out _sortedHiddenTag);
        }

        private string BuildScript()
        {
            StringBuilder script = new StringBuilder();
            return script.ToString();
        }

        #endregion "Private Methods"
    }
}