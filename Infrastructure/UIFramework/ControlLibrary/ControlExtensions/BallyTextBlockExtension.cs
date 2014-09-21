using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    public static partial class ControlExtension
    {
        #region "Public Methods"

        public static MvcHtmlString BallyTextBlock<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool isCurrency = false, StylePropertyBag style = null, string cssClass = "", IDictionary<string, object> attributes = null, string toolTip="")
        {
            string propertyName = string.Empty;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string labelHTMLString = string.Empty;
            string configKey = string.Empty;
            Dictionary<string, string> overrideSettings;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg,out configKey, false);

            overrideSettings = GetLabelOverrideSettings(DisplayType.TextBlock, style, isCurrency: isCurrency, cssClass: cssClass, overrideToolTip: toolTip);

            var fillers = ControlPropertyFillerFactory.Get();
            FillerParams fillerParams = new FillerParams(modelName, propertyName, overrideSettings, attributes: attributes, configKey: configKey);
            var labelPropertyBag = new LabelPropertyBag(fillerParams);
            labelPropertyBag.Accept(fillers);

            value = GetLabelMaskingData(propertyName, modelName, value, labelPropertyBag);

            var labelHTMLEmitter = new LabelHTMLEmitter(value != null ? value.ToString() : string.Empty, labelPropertyBag);
            labelHTMLEmitter.Emit(out labelHTMLString);
            return MvcHtmlString.Create(labelHTMLString);
        }

        public static MvcHtmlString BallyTextBlock(this HtmlHelper htmlHelper, object value, string controlID, string externalizationKey, bool isCurrency = false, bool masking = false, StylePropertyBag style = null, string cssClass = "", IDictionary<string, object> attributes = null, string toolTip="")
        {
            string propertyName = controlID;
            string modelName = string.Empty;
            string labelHTMLString = string.Empty;
            Dictionary<string, string> overrideSettings;

            overrideSettings = GetLabelOverrideSettings(DisplayType.TextBlock, style, isCurrency: isCurrency, cssClass: cssClass, overrideToolTip:toolTip);

            var fillers = ControlPropertyFillerFactory.Get();
            FillerParams fillerParams = new FillerParams(modelName, propertyName, overrideSettings, skipBehaviourFill: true, skipSecurityFill: true, skipValidationFill: true, isBindingControl: false, externalizationKey: externalizationKey, attributes: attributes);
            var labelPropertyBag = new LabelPropertyBag(fillerParams);
            labelPropertyBag.Accept(fillers);

            labelPropertyBag.IsBindingControl = false;
            labelPropertyBag.Masking = masking;
            value = GetLabelMaskingData(propertyName, modelName, value, labelPropertyBag);

            var labelHTMLEmitter = new LabelHTMLEmitter(value != null ? value.ToString() : string.Empty, labelPropertyBag);
            labelHTMLEmitter.Emit(out labelHTMLString);
            return MvcHtmlString.Create(labelHTMLString);
        }

        #endregion "Public Methods"

        #region "Private Methods"

        private static object GetLabelMaskingData(string propertyName, string modelName, object value, LabelPropertyBag labelPropertyBag)
        {
            if (labelPropertyBag.Masking && labelPropertyBag.MaskingProperties != null)
            {
                string maskedData = GetMaskedString(Convert.ToString(value), labelPropertyBag.MaskingProperties);
                value = maskedData;
            }
            return value;
        }
       
        #endregion "Private Methods"
    }
}
