using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    public static partial class ControlExtension
    {
        #region "Public Methods"

        public static MvcHtmlString BallyDateTimePicker<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool showDate = true, bool showTime = true, string dateFormat = "yy-mm-dd", bool showAmPm = false, int step = 5, StylePropertyBag style = null, short tabIndex = 0, string onChangeFunction = "", string dateCssClass = "", string timeCssClass = "", int minYear = -1, int maxYear = -1, DateTime? minDate = null, DateTime? maxDate = null)//bool UTCConversion = true,
        {
            string propertyName = string.Empty;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string dateTimeHTMLString = string.Empty;
            Dictionary<string, string> overrideSettings;
            string configKey = string.Empty;
            DateTimeHTMLEmitter dateTimeHTMLEmitter;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg, out configKey);

            overrideSettings = GetBallyDateTimePickerOverrideSettings(showDate, showTime, dateFormat, showAmPm, step, style, tabIndex, onChangeFunction, dateCssClass, timeCssClass, getYearRange(minYear, maxYear));
            var fillers = ControlPropertyFillerFactory.Get();
            FillerParams fillerParams = new FillerParams(modelName, propertyName, overrideSettings, configKey: configKey, minDate: minDate, maxDate: maxDate);
            var dateTimepropertyBag = new DateTimePropertyBag(fillerParams);
            dateTimepropertyBag.Accept(fillers);
            dateTimepropertyBag.ErrorMessage = errMsg;
            dateTimepropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;
             //dateTimepropertyBag.UTCDateTime = UTCValue;
            //dateTimepropertyBag.UTCConversion = UTCConversion;
            DateTime dt = DateTime.MinValue;
            if (value != null)
            {
                DateTime.TryParse(value.ToString(), out dt);
              // dateTimepropertyBag.DateTimeKind = dt.Kind; /*Check the values- UTC,LOCAL,Unspecified*/
            }

            // dateTimeHTMLEmitter = new DateTimeHTMLEmitter(!showTime ? ( default(DateTime) == dt ? string.Empty : dt.ToShortDateString()) : (!showDate ? dt.ToShortTimeString() : dt.ToString()), dateTimepropertyBag);
            dateTimeHTMLEmitter = new DateTimeHTMLEmitter(dt.ToString(), dateTimepropertyBag);
            dateTimeHTMLEmitter.Emit(out dateTimeHTMLString);
            return MvcHtmlString.Create(dateTimeHTMLString);
        }

        #endregion "Public Methods"

        #region "Private Methods"

        private static string getYearRange(int minYear, int maxYear)
        {
            string range = string.Empty;
            if (minYear != -1 && maxYear != -1)
            {
                range = string.Format("{0}:{1}", minYear, maxYear);
            }
            else if (minYear == -1 && maxYear != -1)
            {
                range = string.Format("{0}:{1}", DateTime.Now.Year, maxYear);
            }
            else if (minYear != -1 && maxYear == -1)
            {
                range = string.Format("{0}:{1}", minYear, minYear + 10);
            }

            return range;
        }

        private static Dictionary<string, string> GetBallyDateTimePickerOverrideSettings(bool showDate, bool showTime, string dateFormat, bool showAmPm, int step, StylePropertyBag style, short tabIndex, string onChangeFunction, string dateCssClass, string timeCssClass, string range)
        {
            Dictionary<string, string> overrideSettings;
            overrideSettings = new Dictionary<string, string>();
            overrideSettings.Add(ControlLibConstants.DATE_FORMAT, dateFormat.ToString());
            overrideSettings.Add(ControlLibConstants.SHOW_AM_PM, showAmPm.ToString());
            overrideSettings.Add(ControlLibConstants.STEP, step.ToString());
            overrideSettings.Add(ControlLibConstants.SHOW_DATE, showDate.ToString());
            overrideSettings.Add(ControlLibConstants.SHOW_TIME, showTime.ToString());
            overrideSettings.Add(ControlLibConstants.DATE_CSS_CLASS, dateCssClass);
            overrideSettings.Add(ControlLibConstants.TIME_CSS_CLASS, timeCssClass);
            overrideSettings.Add(ControlLibConstants.TAB_INDEX, tabIndex.ToString());
            overrideSettings.Add(ControlLibConstants.ON_CHANGE_FUNCTION, onChangeFunction);
            overrideSettings.Add(ControlLibConstants.YEAR_RANGE, range);

            SetStyleSettings(style, overrideSettings);
            return overrideSettings;
        }

        #endregion "Private Methods"
    }
}