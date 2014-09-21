using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;


namespace Controls.ControlLibrary
{

    public static partial class ControlExtension
    {

        #region "Public Methods"

        public static MvcHtmlString BallyTemplateDropDown<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string templateNameKey, ItemDataSource dataSource, int listLength = -1, StylePropertyBag style = null, short tabIndex = 0, string onChangeFunction = "", string cssClass = "")
        {
            string propertyName = string.Empty;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string controlHtmlString = string.Empty;
            string configKey = string.Empty;
            Dictionary<string, string> overrideSettings;
            TemplateDropDownHTMLEmitter controlHtmlEmitter;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg, out configKey);

            overrideSettings = GetBallyMultiColumnDropDownOverrideSettings(templateNameKey, listLength, style, tabIndex, onChangeFunction, value, cssClass);
            FillerParams fillerParams = null;
            if (dataSource != null)
            {
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: dataSource.DataSource, valueMember: dataSource.ValueMember, param: dataSource.DisplayParams, templateKeyName: templateNameKey, configKey: configKey);
            }
            else
            {
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: null, valueMember: string.Empty, param: null, templateKeyName: templateNameKey, configKey: configKey);
            }

            var fillers = ControlPropertyFillerFactory.Get();
            var controlPropertyBag = new TemplateDropDownPropertyBag(fillerParams);
            controlPropertyBag.Accept(fillers);
            controlPropertyBag.ErrorMessage = errMsg;
            controlPropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;
            controlHtmlEmitter = new TemplateDropDownHTMLEmitter(value != null ? value.ToString() : string.Empty, controlPropertyBag);

            controlHtmlEmitter.Emit(out controlHtmlString);
            return MvcHtmlString.Create(controlHtmlString);
        }

        #endregion

        #region "Private Methods"

        private static Dictionary<string, string> GetBallyMultiColumnDropDownOverrideSettings(string templateNameKey, int listLength, StylePropertyBag style, short tabIndex, string onChangeFunction, object value, string cssClass)
        {
            Dictionary<string, string> overrideSettings;
            overrideSettings = new Dictionary<string, string>();
            overrideSettings.Add(ControlLibConstants.TAB_INDEX, tabIndex.ToString());
            overrideSettings.Add(ControlLibConstants.LIST_LENGTH, listLength.ToString());
            overrideSettings.Add(ControlLibConstants.ON_CHANGE_FUNCTION, onChangeFunction);
            overrideSettings.Add(ControlLibConstants.SELECTED_VALUE, Convert.ToString(value));
            overrideSettings.Add(ControlLibConstants.CSS_CLASS, cssClass);
            overrideSettings.Add(ControlLibConstants.TEMPLATE_NAME_KEY, templateNameKey);
            SetStyleSettings(style, overrideSettings);
            return overrideSettings;
        }

        #endregion

    }
}