using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    public static partial class ControlExtension
    {
        #region "Public Methods"

        public static MvcHtmlString BallyCheckBox<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, StylePropertyBag style = null, short tabIndex = 0, string onClickFunction = "", string cssClass = "", IDictionary<string, object> attributes = null, bool? isEnabled = null)
        {
            string propertyName = string.Empty;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string checkBoxHTMLString = string.Empty;
            string configKey = string.Empty;
            Dictionary<string, string> overrideSettings;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg, out configKey);

            if (value == null)
                value = false;

            overrideSettings = GetCheckBoxOverrideSettings(style, tabIndex, onClickFunction, cssClass);

            var fillers = ControlPropertyFillerFactory.Get();
            FillerParams fillerParams = new FillerParams(modelName, propertyName, overrideSettings, attributes: attributes, isEnabled: isEnabled, configKey: configKey);
            var checkBoxpropertyBag = new CheckBoxPropertyBag(fillerParams);
            checkBoxpropertyBag.Accept(fillers);
            checkBoxpropertyBag.ErrorMessage = errMsg;
            checkBoxpropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;

            var checkBoxHTMLEmitter = new CheckBoxHTMLEmitter(value != null ? value.ToString() : string.Empty, checkBoxpropertyBag);
            checkBoxHTMLEmitter.Emit(out checkBoxHTMLString);
            return MvcHtmlString.Create(checkBoxHTMLString);
        }

        public static MvcHtmlString BallyCheckBoxList<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, ItemDataSource dataSource, int listLength = -1, string[] disabled = null, StylePropertyBag style = null, short tabIndex = 0, string onClickFunction = "", bool isVerticalAllign = false, string cssClass = "")
        {
            string propertyName = string.Empty;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string chkBoxListHTMLString = string.Empty;
            Dictionary<string, string> overrideSettings;
            string configKey = string.Empty;
            CheckBoxListHTMLEmitter checkBoxListHTMLEmitter;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg, out configKey);

            overrideSettings = GetCheckBoxListOverrideSettings(listLength, style, tabIndex, onClickFunction, isVerticalAllign, value, cssClass);
            FillerParams fillerParams = null;
            if (dataSource != null)
            {
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: dataSource.DataSource, valueMember: dataSource.ValueMember, displayMember: dataSource.DisplayMember, configKey: configKey);
            }
            else
            {
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: null, valueMember: string.Empty, displayMember: string.Empty, configKey: configKey);
            }
            var fillers = ControlPropertyFillerFactory.Get();
            var chkListpropertyBag = new CheckBoxListPropertyBag(fillerParams);
            chkListpropertyBag.Accept(fillers);
            chkListpropertyBag.ErrorMessage = errMsg;
            chkListpropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;
            checkBoxListHTMLEmitter = new CheckBoxListHTMLEmitter(value != null ? value.ToString() : string.Empty, chkListpropertyBag);

            checkBoxListHTMLEmitter.Emit(out chkBoxListHTMLString);
            return MvcHtmlString.Create(chkBoxListHTMLString);
        }

        #endregion "Public Methods"

        #region "Private Methods"

        private static Dictionary<string, string> GetCheckBoxOverrideSettings(StylePropertyBag style, short tabIndex, string onClickFunction, string cssClass)
        {
            Dictionary<string, string> overrideSettings;
            overrideSettings = new Dictionary<string, string>();
            overrideSettings.Add(ControlLibConstants.TAB_INDEX, tabIndex.ToString());
            overrideSettings.Add(ControlLibConstants.ON_CLICK_FUNCTION, onClickFunction);
            overrideSettings.Add(ControlLibConstants.CSS_CLASS, cssClass);
            SetStyleSettings(style, overrideSettings);
            return overrideSettings;
        }

        private static Dictionary<string, string> GetCheckBoxListOverrideSettings(int listLength, StylePropertyBag style, short tabIndex, string onClickFunction, bool isVerticalAllign, object value, string cssClass)
        {
            Dictionary<string, string> overrideSettings;
            overrideSettings = new Dictionary<string, string>();
            overrideSettings.Add(ControlLibConstants.TAB_INDEX, tabIndex.ToString());
            overrideSettings.Add(ControlLibConstants.ON_CLICK_FUNCTION, onClickFunction);
            overrideSettings.Add(ControlLibConstants.SELECTED_VALUE, Convert.ToString(value));
            overrideSettings.Add(ControlLibConstants.LIST_LENGTH, listLength.ToString());
            overrideSettings.Add(ControlLibConstants.IS_VERTICAL_ALLIGN, Convert.ToString(isVerticalAllign));
            overrideSettings.Add(ControlLibConstants.CSS_CLASS, cssClass);
            SetStyleSettings(style, overrideSettings);
            return overrideSettings;
        }

        #endregion "Private Methods"
    }
}