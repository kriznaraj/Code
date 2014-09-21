using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    public static partial class ControlExtension
    {
        #region "Public Methods"

        public static MvcHtmlString BallyLabel<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, StylePropertyBag style = null, string cssClass = "", IDictionary<string, object> attributes = null)
        {
            string propertyName = string.Empty;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string labelHTMLString = string.Empty;
            string configKey = string.Empty;
            Dictionary<string, string> overrideSettings;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg, out configKey, false);

            overrideSettings = GetLabelOverrideSettings(DisplayType.Label, style, cssClass: cssClass);

            var fillers = ControlPropertyFillerFactory.Get();
            FillerParams fillerParams = new FillerParams(modelName, propertyName, overrideSettings, attributes: attributes, configKey: configKey);
            var labelPropertyBag = new LabelPropertyBag(fillerParams);
            labelPropertyBag.Accept(fillers);

            var labelHTMLEmitter = new LabelHTMLEmitter(value != null ? value.ToString() : string.Empty, labelPropertyBag);
            labelHTMLEmitter.Emit(out labelHTMLString);
            return MvcHtmlString.Create(labelHTMLString);
        }

        public static MvcHtmlString BallyLabel(this HtmlHelper htmlHelper, string controlID, string externalizationKey, bool isMandatory = false, StylePropertyBag style = null, string cssClass = "", IDictionary<string, object> attributes = null)
        {
            string propertyName = controlID;
            string modelName = string.Empty;
            string labelHTMLString = string.Empty;
            Dictionary<string, string> overrideSettings;

            overrideSettings = GetLabelOverrideSettings(DisplayType.Label, style, cssClass: cssClass);

            var fillers = ControlPropertyFillerFactory.Get();
            FillerParams fillerParams = new FillerParams(modelName, propertyName, overrideSettings, skipBehaviourFill: true, skipSecurityFill: true, skipValidationFill: true, isBindingControl: false, externalizationKey: externalizationKey, attributes: attributes);
            var labelPropertyBag = new LabelPropertyBag(fillerParams);
            labelPropertyBag.Accept(fillers);

            labelPropertyBag.IsMandatory = isMandatory;
            labelPropertyBag.IsBindingControl = false;
            var labelHTMLEmitter = new LabelHTMLEmitter(string.Empty, labelPropertyBag);
            labelHTMLEmitter.Emit(out labelHTMLString);
            return MvcHtmlString.Create(labelHTMLString);
        }

        #endregion "Public Methods"

        #region "Private Methods"

        private static Dictionary<string, string> GetLabelOverrideSettings(DisplayType DisplayType, StylePropertyBag style, bool isCurrency = false, string cssClass = "", string overrideToolTip="")
        {
            Dictionary<string, string> overrideSettings;
            overrideSettings = new Dictionary<string, string>();
            SetStyleSettings(style, overrideSettings);
            overrideSettings.Add(ControlLibConstants.DISPLAY_TYPE, DisplayType.ToString());
            overrideSettings.Add(ControlLibConstants.IS_CURRENCY, isCurrency.ToString());
            overrideSettings.Add(ControlLibConstants.CSS_CLASS, cssClass);
            overrideSettings.Add(ControlLibConstants.OVERRIDE_TOOLTIP, overrideToolTip);
            return overrideSettings;
        }

        #endregion "Private Methods"
    }
}