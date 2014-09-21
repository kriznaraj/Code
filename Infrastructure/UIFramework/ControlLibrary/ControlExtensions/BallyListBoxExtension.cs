using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;


namespace Controls.ControlLibrary
{

    public static partial class ControlExtension
    {

        #region "Public Methods"

        public static MvcHtmlString BallyListBox<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, ItemDataSource dataSource, int listLength = -1, StylePropertyBag style = null, short tabIndex = 0, string onClickFunction = "", string listItemTemplateCssClass = "", string listItemMouseOverCssClass = "", string listItemSelectedCssClass = "")
        {
            string propertyName = string.Empty;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string controlHtmlString = string.Empty;
            Dictionary<string, string> overrideSettings;
            string configKey = string.Empty;
            ListBoxHTMLEmitter controlHtmlEmitter;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg, out configKey);

            overrideSettings = GetBallyListBoxOverrideSettings(listLength, style, tabIndex, onClickFunction, value, listItemTemplateCssClass, listItemMouseOverCssClass, listItemSelectedCssClass);
            FillerParams fillerParams = null;
            if (dataSource != null)
            {
                string[] displayParam= new string[1];
                displayParam[0] = dataSource.DisplayMember;
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: dataSource.DataSource, valueMember: dataSource.ValueMember, param: displayParam, templateKeyName: DefaultTemplate.SimpleListTemplate.ToString(), listType: ListBoxType.SimpleList, configKey: configKey);
            }
            else
            {
                fillerParams = new FillerParams(modelName, propertyName, overrideSettings, list: null, valueMember: string.Empty, param: null, templateKeyName: DefaultTemplate.SimpleListTemplate.ToString(), listType: ListBoxType.SimpleList, configKey: configKey);
            }

            var fillers = ControlPropertyFillerFactory.Get();
            var controlPropertyBag = new TemplateListPropertyBag(fillerParams);
            controlPropertyBag.Accept(fillers);
            controlPropertyBag.ErrorMessage = errMsg;
            controlPropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;
            controlHtmlEmitter = new ListBoxHTMLEmitter(value != null ? value.ToString() : string.Empty, controlPropertyBag);

            controlHtmlEmitter.Emit(out controlHtmlString);
            return MvcHtmlString.Create(controlHtmlString);
        }

        #endregion

        #region "Private Methods"

        private static Dictionary<string, string> GetBallyListBoxOverrideSettings(int listLength, StylePropertyBag style, short tabIndex, string onChangeFunction, object value, string listItemTemplateCssClass, string listItemMouseOverCssClass, string listItemSelectedCssClass)
        {
            Dictionary<string, string> overrideSettings;
            overrideSettings = new Dictionary<string, string>();
            overrideSettings.Add(ControlLibConstants.TAB_INDEX, tabIndex.ToString());
            overrideSettings.Add(ControlLibConstants.LIST_LENGTH, listLength.ToString());
            overrideSettings.Add(ControlLibConstants.ON_CLICK_FUNCTION, onChangeFunction);
            overrideSettings.Add(ControlLibConstants.SELECTED_VALUE, Convert.ToString(value));
            overrideSettings.Add(ControlLibConstants.TEMPLATE_NAME_KEY, string.Empty);
            overrideSettings.Add(ControlLibConstants.LISTITEM_TEMPLATE_CSSCLASS, listItemTemplateCssClass);
            overrideSettings.Add(ControlLibConstants.LISTITEM_MOUSEOVER_CSSCLASS, listItemMouseOverCssClass);
            overrideSettings.Add(ControlLibConstants.LISTITEM_SELECTED_CSSCLASS, listItemSelectedCssClass);
            SetStyleSettings(style, overrideSettings);
            return overrideSettings;
        }

        #endregion

    }
}