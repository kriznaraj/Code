using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    public static partial class ControlExtension
    {
        #region "Public Methods"

        public static MvcHtmlString BallyGrid<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string valueMember, string GridDataColumnDefinitionName, IDictionary<string, object> gridParam = null, string hiddenColumns = "", string actionUrl = "", StylePropertyBag style = null, short tabIndex = 0, int pageSize = 10, int gridHeight = 400, bool enableFilter = false, bool enableSorting = false, bool enableExport = false, bool pagination = true, bool serverPagination = false, bool selectOption = true, string defaultSortField = "", string imageSizeClass = "", string statusProperty = "", string onDataRowSelectFunction = "", string onDataRowSelectionChangeFunctionName = "", string cssClass = "")
        {
            string propertyName = string.Empty;
            string modelName = string.Empty;
            object value = string.Empty;
            string errMsg = string.Empty;
            string controlHtmlString = string.Empty;
            string configKey = string.Empty;
            Dictionary<string, string> overrideSettings;
            GridHTMLEmitter controlHtmlEmitter;

            ControlExtension.GetPropertyNameAndValue<TModel, TProperty>(htmlHelper, expression, out propertyName, out modelName, out value, out errMsg, out configKey);

            overrideSettings = GetGridOverrideSettings(valueMember, actionUrl, style, tabIndex, onDataRowSelectFunction, onDataRowSelectionChangeFunctionName, value, GridDataColumnDefinitionName, hiddenColumns, pageSize, gridHeight, enableFilter, enableSorting, enableExport, pagination, serverPagination, selectOption, defaultSortField, imageSizeClass, statusProperty, cssClass);

            FillerParams fillerParams = new FillerParams(modelName, propertyName, overrideSettings, inputParam: gridParam, configKey: configKey);

            var fillers = ControlPropertyFillerFactory.Get();
            var controlPropertyBag = new GridPropertyBag(fillerParams);
            controlPropertyBag.Accept(fillers);
            controlPropertyBag.ErrorMessage = errMsg;
            controlPropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;
            controlHtmlEmitter = new GridHTMLEmitter(value != null ? value.ToString() : string.Empty, controlPropertyBag);

            controlHtmlEmitter.Emit(out controlHtmlString);
            return MvcHtmlString.Create(controlHtmlString);
        }

        #endregion "Public Methods"

        #region "Private Methods"

        private static Dictionary<string, string> GetGridOverrideSettings(string valueMemebr, string actionUrl, StylePropertyBag style, short tabIndex, string onDataRowSelectFunction, string onDataSelectionChangeFunctionName, object value, string gridDataColumnDefinitionName, string hiddencolumns, int pageSize, int gridHeight, bool enableFilter, bool enableSorting, bool enableExport, bool pagination, bool serverPagination, bool selectOption, string defaultSortField, string imageSizeProperty, string statusProperty, string cssClass)
        {
            Dictionary<string, string> overrideSettings;
            overrideSettings = new Dictionary<string, string>();
            overrideSettings.Add(ControlLibConstants.TAB_INDEX, tabIndex.ToString());
            overrideSettings.Add(ControlLibConstants.ON_DATAROW_SELECT_FUNCTION, onDataRowSelectFunction);
            overrideSettings.Add(ControlLibConstants.ON_DATAROW_SELECTION_CHANGE_FN, onDataSelectionChangeFunctionName);
            overrideSettings.Add(ControlLibConstants.VALUE_MEMBER, valueMemebr);
            overrideSettings.Add(ControlLibConstants.ACTION_URL, actionUrl);
            overrideSettings.Add(ControlLibConstants.GRID_DATACOLUMN_DEFINITION_NAME, Convert.ToString(gridDataColumnDefinitionName));
            overrideSettings.Add(ControlLibConstants.PAGESIZE, pageSize.ToString());
            overrideSettings.Add(ControlLibConstants.GRIDHEIGHT, gridHeight.ToString());
            overrideSettings.Add(ControlLibConstants.ENABLEFILTER, enableFilter.ToString());
            overrideSettings.Add(ControlLibConstants.ENABLESORTING, enableSorting.ToString());
            overrideSettings.Add(ControlLibConstants.ENABLEEXPORT, enableExport.ToString());
            overrideSettings.Add(ControlLibConstants.ENABLEPAGINATION, pagination.ToString());
            overrideSettings.Add(ControlLibConstants.SERVERPAGINATION, serverPagination.ToString());
            overrideSettings.Add(ControlLibConstants.SELECTOPTION, selectOption.ToString());
            overrideSettings.Add(ControlLibConstants.DEFAULTSORTFIELD, defaultSortField);
            overrideSettings.Add(ControlLibConstants.IMAGESIZEPROPERTY, imageSizeProperty);
            overrideSettings.Add(ControlLibConstants.STATUSPROPERTY, statusProperty);
            overrideSettings.Add(ControlLibConstants.HIDEN_COUMN, hiddencolumns);
            overrideSettings.Add(ControlLibConstants.CSS_CLASS, cssClass);
            SetStyleSettings(style, overrideSettings);
            return overrideSettings;
        }

        #endregion "Private Methods"
    }
}