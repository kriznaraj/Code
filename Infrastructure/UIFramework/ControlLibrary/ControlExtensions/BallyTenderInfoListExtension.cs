using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;


namespace Controls.ControlLibrary
{

    public static partial class ControlExtension
    {

        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="controlName"></param>
        /// <param name="denomTemplateName"></param>
        /// <param name="dataSource"></param>
        /// <param name="tenderInfoParam"></param>
        /// <param name="actionUrl"></param>
        /// <param name="style"></param>
        /// <param name="onRowSelectFunction"></param>
        /// <param name="headerCssClass"></param>
        /// <param name="footerCssClass"></param>
        /// <param name="grantTotalRequired"></param>
        /// <param name="cssClass"></param>
        /// <param name="otherAmountRequired"></param>
        /// <param name="otherAmountPosition"></param>
        /// <param name="otherAmountLabelKey"></param>
        /// <param name="isViewMode"></param>
        /// <param name="validationMessageKey"></param>
        /// <param name="denomMode"></param>
        /// <param name="undefinedRowReadonlyColumns">Readonly columns name with , seperated</param>
        /// <returns></returns>
        public static MvcHtmlString BallyTenderInfoList(this HtmlHelper htmlHelper, string controlName, 
            string denomTemplateName, DenomDataSource dataSource, IDictionary<string, object> tenderInfoParam = null, string actionUrl = "", StylePropertyBag style = null, string onRowSelectFunction = "", string headerCssClass = "",
            string footerCssClass = "", bool grantTotalRequired = true, string cssClass = "", bool otherAmountRequired = false, PositionType otherAmountPosition = PositionType.Bottom, string otherAmountLabelKey = "other_amount", bool isViewMode = false, string validationMessageKey = "", DenomModeType denomMode = DenomModeType.Full, string undefinedRowReadonlyColumns = "", string undefinedRowEditableColumns = "", bool movementIndicatorRequired = false, string movementIndicatorColumn="")
        {
            string propertyName = controlName;
            string modelName = string.Empty;
            string controlHtmlString = string.Empty;
            Dictionary<string, string> overrideSettings;
            TenderInfoListHTMLEmitter controlHtmlEmitter;

            overrideSettings = GetBallyTenderInfoListOverrideSettings(denomTemplateName, style, onRowSelectFunction, headerCssClass, footerCssClass, grantTotalRequired, cssClass, otherAmountRequired, otherAmountLabelKey, actionUrl, otherAmountPosition, isViewMode, validationMessageKey, denomMode, undefinedRowReadonlyColumns, undefinedRowEditableColumns, movementIndicatorRequired, movementIndicatorColumn);
            FillerParams fillerParams = new FillerParams(modelName, propertyName, overrideSettings, inputParam: tenderInfoParam);

            var fillers = ControlPropertyFillerFactory.Get();
            var controlPropertyBag = new DenomControlPropertyBag(fillerParams);
            controlPropertyBag.Accept(fillers);
            controlHtmlEmitter = new TenderInfoListHTMLEmitter(dataSource, controlPropertyBag);

            controlHtmlEmitter.Emit(out controlHtmlString);
            return MvcHtmlString.Create(controlHtmlString);
        }

        #endregion

        #region "Private Methods"

        private static Dictionary<string, string> GetBallyTenderInfoListOverrideSettings(string denomTemplateName, StylePropertyBag style, string onRowSelectFunction,
            string headerCssClass, string footerCssClass, bool grantTotalRequired, string cssClass, bool otherAmountRequired, string otherAmountLabelKey, string actionUrl, PositionType otherAmountPosition, bool isViewMode, string validationMessageKey, DenomModeType denomMode, string undefinedRowReadonlyColumns, string undefinedRowEditableColumns, bool movementIndicatorRequired, string movementIndicatorColumn)
        {
            Dictionary<string, string> overrideSettings;
            overrideSettings = new Dictionary<string, string>();
            overrideSettings.Add(ControlLibConstants.DENOM_ON_ROWSELECT_FUNCTION, onRowSelectFunction);
            overrideSettings.Add(ControlLibConstants.DENOM_TEMPLATE_KEY, denomTemplateName);
            overrideSettings.Add(ControlLibConstants.DENOM_HEADER_CSS, headerCssClass);
            overrideSettings.Add(ControlLibConstants.DENOM_FOOTER_CSS, footerCssClass);
            overrideSettings.Add(ControlLibConstants.DENOM_GRANT_TOTAL_REQUIRED, grantTotalRequired.ToString());
            overrideSettings.Add(ControlLibConstants.CSS_CLASS, cssClass);
            overrideSettings.Add(ControlLibConstants.DENOM_OTHERAMOUNT_REQUIRED, otherAmountRequired.ToString());
            overrideSettings.Add(ControlLibConstants.DENOM_OTHERAMOUNT_LABELKEY, otherAmountLabelKey);
            overrideSettings.Add(ControlLibConstants.ACTION_URL, actionUrl);
            overrideSettings.Add(ControlLibConstants.OTHER_AMOUNT_POSITION, otherAmountPosition.ToString());
            overrideSettings.Add(ControlLibConstants.IS_VIEW_MODE, isViewMode.ToString());
            overrideSettings.Add(ControlLibConstants.VALIDATION_MESSAGE_KEY, validationMessageKey);
            overrideSettings.Add(ControlLibConstants.DENOM_MODE, denomMode.ToString());
            overrideSettings.Add(ControlLibConstants.UNDEFINEDROW_READONLY_COLUMNS, undefinedRowReadonlyColumns);
            overrideSettings.Add(ControlLibConstants.UNDEFINEDROW_EDITABLE_COLUMNS, undefinedRowEditableColumns);
            overrideSettings.Add(ControlLibConstants.MOVEMENT_INDICATOR_COLUMN, movementIndicatorColumn);
            overrideSettings.Add(ControlLibConstants.MOVEMENT_INDICATOR_REQUIRED, movementIndicatorRequired.ToString());
            SetStyleSettings(style, overrideSettings);
            return overrideSettings;
        }

        #endregion

    }
}