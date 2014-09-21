using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;


namespace Controls.ControlLibrary
{

    public static partial class ControlExtension
    {

        #region "Public Methods"

        public static MvcHtmlString BallyShuttleList<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string actionUrl, string valueMember, string displayMember, IDictionary<string, object> shuttleParam = null, StylePropertyBag style = null, short tabIndex = 0, string onChangeFunction = "", string cssClass = "")
        {
            string propertyName = string.Empty;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string controlHTMLString = string.Empty;
            Dictionary<string, string> overrideSettings;
            string configKey = string.Empty;
            ShuttleHTMLEmitter controlHTMLEmitter;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg, out configKey);

            overrideSettings = GetBallyShuttleListOverrideSettings(actionUrl, valueMember, displayMember, style, tabIndex, onChangeFunction, value, cssClass);

            FillerParams fillerParams = new FillerParams(modelName, propertyName, overrideSettings, inputParam: shuttleParam, configKey: configKey);

            var fillers = ControlPropertyFillerFactory.Get();
            var controlpropertyBag = new ShuttlePropertyBag(fillerParams);
            controlpropertyBag.Accept(fillers);
            controlpropertyBag.ErrorMessage = errMsg;
            controlpropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;
            controlHTMLEmitter = new ShuttleHTMLEmitter(value != null ? value.ToString() : string.Empty, controlpropertyBag);

            controlHTMLEmitter.Emit(out controlHTMLString);
            return MvcHtmlString.Create(controlHTMLString);
        }

        #endregion

        #region "Private Methods"

        private static Dictionary<string, string> GetBallyShuttleListOverrideSettings(string actionUrl, string valMember, string dispMember, StylePropertyBag style, short tabIndex, string onChangeFunction, object value, string cssClass)
        {
            Dictionary<string, string> overrideSettings;
            overrideSettings = new Dictionary<string, string>();
            overrideSettings.Add(ControlLibConstants.TAB_INDEX, tabIndex.ToString());
            overrideSettings.Add(ControlLibConstants.ON_CHANGE_FUNCTION, onChangeFunction);
            overrideSettings.Add(ControlLibConstants.ACTION_URL, actionUrl);
            overrideSettings.Add(ControlLibConstants.VALUE_MEMBER, valMember);
            overrideSettings.Add(ControlLibConstants.DISPLAY_MEMBER, dispMember);
            overrideSettings.Add(ControlLibConstants.CSS_CLASS, cssClass);
            SetStyleSettings(style, overrideSettings);
            return overrideSettings;
        }

        #endregion

    }
}