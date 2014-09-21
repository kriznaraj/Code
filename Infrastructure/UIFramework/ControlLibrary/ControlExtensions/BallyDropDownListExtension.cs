using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    public static partial class ControlExtension
    {
        #region "Public Methods"

        public static MvcHtmlString BallyDropDownList<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, ItemDataSource dataSource, string[] disabled = null, DropDownType type = DropDownType.SingleSelect, int listLength = -1, StylePropertyBag style = null, short tabIndex = 0, string onChangeFunction = "", string cssClass = "", IDictionary<string, object> attributes = null, bool? isEnabled = null, bool? isReadOnly = null)
        {
            string propertyName = string.Empty;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string dropDownHTMLString = string.Empty;
            Dictionary<string, string> overrideSettings;
            string configKey = string.Empty;
            DropDownHTMLEmitter dropDownHTMLEmitter;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg, out configKey);

            overrideSettings = GetBallyDropdownListOverrideSettings(type, listLength, "", "", style, tabIndex, onChangeFunction, value, cssClass);
            FillerParams fillerParams = null;
            if (dataSource != null)
            {
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: dataSource.DataSource, valueMember: dataSource.ValueMember, displayMember: dataSource.DisplayMember, disabled: disabled, attributes: attributes, isEnabled: isEnabled, isReadOnly: isReadOnly, configKey: configKey);
            }
            else
            {
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: null, valueMember: string.Empty, displayMember: string.Empty, disabled: disabled, attributes: attributes, isEnabled: isEnabled, configKey: configKey);
            }

            var fillers = ControlPropertyFillerFactory.Get();
            var dropDownpropertyBag = new DropDownPropertyBag(fillerParams);
            dropDownpropertyBag.Accept(fillers);
            dropDownpropertyBag.ErrorMessage = errMsg;
            dropDownpropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;
            dropDownHTMLEmitter = new DropDownHTMLEmitter(value != null ? value.ToString() : string.Empty, dropDownpropertyBag);

            dropDownHTMLEmitter.Emit(out dropDownHTMLString);
            return MvcHtmlString.Create(dropDownHTMLString);
        }

        //Cascade combo
        public static MvcHtmlString BallyCascadeDropDown<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string targetControlID, string actionUrl, ItemDataSource dataSource, string[] disabled = null, int listLength = -1, StylePropertyBag style = null, short tabIndex = 0, string onChangeFunction = "", string cssClass = "", string cascadeInputFunction = "", IDictionary<string, object> attributes = null)
        {
            string propertyName = string.Empty;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string dropDownHTMLString = string.Empty;
            Dictionary<string, string> overrideSettings;
            string configKey = string.Empty;
            DropDownHTMLEmitter dropDownHTMLEmitter;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg, out configKey);

            overrideSettings = GetBallyDropdownListOverrideSettings(DropDownType.CascadeSelect, listLength, targetControlID, actionUrl, style, tabIndex, onChangeFunction, value, cssClass, cascadeInputFunction);
            FillerParams fillerParams = null;
            if (dataSource != null)
            {
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: dataSource.DataSource, valueMember: dataSource.ValueMember, displayMember: dataSource.DisplayMember, disabled: disabled, attributes: attributes, configKey: configKey);
            }
            else
            {
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: null, valueMember: string.Empty, displayMember: string.Empty, disabled: disabled, attributes: attributes, configKey: configKey);
            }

            var fillers = ControlPropertyFillerFactory.Get();
            var dropDownpropertyBag = new DropDownPropertyBag(fillerParams);
            dropDownpropertyBag.Accept(fillers);
            dropDownpropertyBag.ErrorMessage = errMsg;
            dropDownpropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;
            dropDownHTMLEmitter = new DropDownHTMLEmitter(value != null ? value.ToString() : string.Empty, dropDownpropertyBag);

            dropDownHTMLEmitter.Emit(out dropDownHTMLString);
            return MvcHtmlString.Create(dropDownHTMLString);
        }

        public static MvcHtmlString BallyCascadeDropDown<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, NameGenerator controlId, string actionUrl, ItemDataSource dataSource, string[] disabled = null, int listLength = -1, StylePropertyBag style = null, short tabIndex = 0, string onChangeFunction = "", string cssClass = "", string cascadeInputFunction = "", IDictionary<string, object> attributes = null)
        {
            string propertyName = string.Empty;
            string targetControlID = controlId.ControlID;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string dropDownHTMLString = string.Empty;
            Dictionary<string, string> overrideSettings;
            string configKey = string.Empty;
            DropDownHTMLEmitter dropDownHTMLEmitter;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg, out configKey);

            overrideSettings = GetBallyDropdownListOverrideSettings(DropDownType.CascadeSelect, listLength, targetControlID, actionUrl, style, tabIndex, onChangeFunction, value, cssClass, cascadeInputFunction);
            FillerParams fillerParams = null;
            if (dataSource != null)
            {
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: dataSource.DataSource, valueMember: dataSource.ValueMember, displayMember: dataSource.DisplayMember, disabled: disabled, attributes: attributes, configKey: configKey);
            }
            else
            {
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: null, valueMember: string.Empty, displayMember: string.Empty, disabled: disabled, attributes: attributes, configKey: configKey);
            }

            var fillers = ControlPropertyFillerFactory.Get();
            var dropDownpropertyBag = new DropDownPropertyBag(fillerParams);
            dropDownpropertyBag.Accept(fillers);
            dropDownpropertyBag.ErrorMessage = errMsg;
            dropDownpropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;
            dropDownHTMLEmitter = new DropDownHTMLEmitter(value != null ? value.ToString() : string.Empty, dropDownpropertyBag);

            dropDownHTMLEmitter.Emit(out dropDownHTMLString);
            return MvcHtmlString.Create(dropDownHTMLString);
        }

        private static Dictionary<string, string> GetBallyDropdownListOverrideSettings(DropDownType type, int listLength, string targetControlID, string actionUrl, StylePropertyBag style, short tabIndex, string onChangeFunction, object value, string cssClass, string cascadeInputFunction = "")
        {
            Dictionary<string, string> overrideSettings;
            overrideSettings = new Dictionary<string, string>();
            overrideSettings.Add(ControlLibConstants.TAB_INDEX, tabIndex.ToString());
            overrideSettings.Add(ControlLibConstants.LIST_LENGTH, listLength.ToString());
            overrideSettings.Add(ControlLibConstants.ON_CHANGE_FUNCTION, onChangeFunction);
            overrideSettings.Add(ControlLibConstants.TARGET_CONTROL_ID, targetControlID);
            overrideSettings.Add(ControlLibConstants.ACTION_URL, actionUrl);
            overrideSettings.Add(ControlLibConstants.TYPE, type.ToString());
            overrideSettings.Add(ControlLibConstants.CSS_CLASS, cssClass);
            overrideSettings.Add(ControlLibConstants.CASCADE_INPUT_FUNCTION, cascadeInputFunction);
            overrideSettings.Add(ControlLibConstants.SELECTED_VALUE, Convert.ToString(value));
            SetStyleSettings(style, overrideSettings);
            return overrideSettings;
        }

        #endregion "Public Methods"
    }
}