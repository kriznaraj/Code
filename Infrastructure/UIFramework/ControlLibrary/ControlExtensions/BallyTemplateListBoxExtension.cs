using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;


namespace Controls.ControlLibrary
{

    public static partial class ControlExtension
    {

        #region "Public Methods"

        public static MvcHtmlString BallyTemplateListBox<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string templateNameKey, ItemDataSource dataSource, int listLength = -1, StylePropertyBag style = null, short tabIndex = 0, string onClickFunction = "", string listItemTemplateCssClass = "", string listItemMouseOverCssClass="", string listItemSelectedCssClass="", string cssClass = "")
        {
            string propertyName = string.Empty;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string controlHtmlString = string.Empty;
            string configKey = string.Empty;
            Dictionary<string, string> overrideSettings;
            TemplateListHTMLEmitter controlHtmlEmitter;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg, out configKey);

            overrideSettings = GetBallyTemplateListBoxOverrideSettings(templateNameKey, listLength, style, tabIndex, onClickFunction, value, listItemTemplateCssClass, listItemMouseOverCssClass, listItemSelectedCssClass, cssClass);
            FillerParams fillerParams = null;
            if (dataSource != null)
            {
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: dataSource.DataSource, valueMember: dataSource.ValueMember, param: dataSource.DisplayParams, templateKeyName: templateNameKey, listType: ListBoxType.MultiColumnList, configKey: configKey);
            }
            else
            {
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: null, valueMember: string.Empty, param: null, templateKeyName: templateNameKey, listType: ListBoxType.MultiColumnList, configKey: configKey);
            }

            var fillers = ControlPropertyFillerFactory.Get();
            var controlPropertyBag = new TemplateListPropertyBag(fillerParams);
            controlPropertyBag.Accept(fillers);
            controlPropertyBag.ErrorMessage = errMsg;
            controlPropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;
            controlHtmlEmitter = new TemplateListHTMLEmitter(value != null ? value.ToString() : string.Empty, controlPropertyBag);

            controlHtmlEmitter.Emit(out controlHtmlString);
            return MvcHtmlString.Create(controlHtmlString);
        }

        #endregion

        #region "Private Methods"

        private static Dictionary<string, string> GetBallyTemplateListBoxOverrideSettings(string templateNameKey, int listLength, StylePropertyBag style, short tabIndex, string onChangeFunction, object value, string listItemTemplateCssClass, string listItemMouseOverCssClass, string listItemSelectedCssClass, string cssClass)
        {
            Dictionary<string, string> overrideSettings;
            overrideSettings = new Dictionary<string, string>();
            overrideSettings.Add(ControlLibConstants.TAB_INDEX, tabIndex.ToString());
            overrideSettings.Add(ControlLibConstants.LIST_LENGTH, listLength.ToString());
            overrideSettings.Add(ControlLibConstants.ON_CLICK_FUNCTION, onChangeFunction);
            overrideSettings.Add(ControlLibConstants.SELECTED_VALUE, Convert.ToString(value));
            overrideSettings.Add(ControlLibConstants.TEMPLATE_NAME_KEY, templateNameKey);
            overrideSettings.Add(ControlLibConstants.LISTITEM_TEMPLATE_CSSCLASS, listItemTemplateCssClass);
            overrideSettings.Add(ControlLibConstants.LISTITEM_MOUSEOVER_CSSCLASS, listItemMouseOverCssClass);
            overrideSettings.Add(ControlLibConstants.LISTITEM_SELECTED_CSSCLASS, listItemSelectedCssClass);
            overrideSettings.Add(ControlLibConstants.CSS_CLASS, cssClass);
            SetStyleSettings(style, overrideSettings);
            return overrideSettings;
        }

        #endregion

    }
}