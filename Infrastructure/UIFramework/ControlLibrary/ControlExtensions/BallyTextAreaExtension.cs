using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    public static partial class ControlExtension
    {
        #region "Public Methods"

        public static MvcHtmlString BallyTextArea<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, StylePropertyBag style = null, short tabIndex = 0, string onLeaveFunction = "", string onKeyUpFunction = "", string onKeyDownFunction = "", string onChangeFunction = "", string cssClass = "", bool? isEnabled = null, bool? isReadOnly = null, IDictionary<string, object> attributes = null)
        {
            string propertyName = string.Empty;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string textAreaHTMLString = string.Empty;
            string configKey = string.Empty;
            Dictionary<string, string> overrideSettings;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg, out configKey);

            overrideSettings = GetTextAreaOverrideSettings(style, tabIndex, onLeaveFunction, onKeyUpFunction, onKeyDownFunction, onChangeFunction, cssClass);

            var fillers = ControlPropertyFillerFactory.Get();
            FillerParams fillerParams = new FillerParams(modelName, propertyName, overrideSettings, isEnabled: isEnabled, isReadOnly: isReadOnly, attributes: attributes, configKey: configKey);
            var textAreapropertyBag = new TextAreaPropertyBag(fillerParams);
            textAreapropertyBag.Accept(fillers);
            textAreapropertyBag.ErrorMessage = errMsg;
            textAreapropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;

            var textAreaHTMLEmitter = new TextAreaHTMLEmitter(value != null ? value.ToString() : string.Empty, textAreapropertyBag);
            textAreaHTMLEmitter.Emit(out textAreaHTMLString);
            return MvcHtmlString.Create(textAreaHTMLString);
        }

        #endregion "Public Methods"

        #region "Private Methods"

        private static Dictionary<string, string> GetTextAreaOverrideSettings(StylePropertyBag style, short tabIndex, string onLeaveFunction, string onKeyUpFunction, string onKeyDownFunction, string onChangeFunction, string cssClass)
        {
            Dictionary<string, string> overrideSettings;
            overrideSettings = new Dictionary<string, string>();
            overrideSettings.Add(ControlLibConstants.TAB_INDEX, tabIndex.ToString());
            overrideSettings.Add(ControlLibConstants.ON_LEAVE_FUNCTION, onLeaveFunction);
            overrideSettings.Add(ControlLibConstants.ON_KEY_UP_FUNCTION, onKeyUpFunction);
            overrideSettings.Add(ControlLibConstants.ON_KEY_DOWN_FUNCTION, onKeyDownFunction);
            overrideSettings.Add(ControlLibConstants.ON_CHANGE_FUNCTION, onChangeFunction);
            overrideSettings.Add(ControlLibConstants.CSS_CLASS, cssClass);
            SetStyleSettings(style, overrideSettings);
            return overrideSettings;
        }

        #endregion "Private Methods"
    }
}