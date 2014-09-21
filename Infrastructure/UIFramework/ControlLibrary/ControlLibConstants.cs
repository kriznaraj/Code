using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Controls.ControlLibrary
{
    internal static class ControlLibConstants
    {
        public const string TEMPALTE_KEY = "TemplateKey";
        public const string TEMPALTE_HTML = "TemplateHTML";

        public const string COLUMN_NAME = "ColumnName";
        public const string DISPLAY_MEMBER = "DisplayMember";
        public const string WIDTH = "Width";
        public const string HEIGHT = "Height";
        public const string COLUMN_DATA_TYPE = "ColumnDataType";
        public const string ALIGHNMENT = "Alignment";
        public const string HEADER_NAME = "HeaderName";
        public const string SEARCH_TYPE = "SearchType";
        public const string ACCESS_POLICY_CODE = "AccessPolicyCode";

        public const string NAME = "Name";
        public const string DATA_COLUMN_DEFINITION = "DataColumnDefinition";
        public const string PROPERTY_CONFIGURATION = "PropertyConfiguration";
        public const string KEY = "Key";
        public const string EXTERNALIZATION_KEY = "ExternalizationKey";
        public const string ISCOMPLEXTYPE = "IsComplexType";
        public const string ISENUMERABLE = "IsEnumerable";
        public const string COMPLEX_TYPENAME = "ComplexTypeName";
        public const string CONFIG_KEY = "ConfigKey";

        public const string DENOM_TEMPLATE_NAME = "TemplateName";
        public const string DENOM_TEMPLATE_COLUMN_DEFINITION = "DenomTemplateColumnDefinition";
        public const string DENOM_ISEDITABLE = "IsEditable";
        public const string DENOM_CALCULATION_REQUIRED = "CalculationRequired";
        public const string DENOM_FORMULA = "Formula";
        public const string DENOM_CALCULATION_ON = "CalculationOn";
        public const string DENOM_ON_ROWSELECT_FUNCTION = "OnRowSelectFunction";
        public const string DENOM_TEMPLATE_KEY = "DenomTemplateName";
        public const string DENOM_HEADER_CSS = "HeaderCssClass";
        public const string DENOM_FOOTER_CSS = "FooterCssClass";
        public const string DENOM_GRANT_TOTAL_REQUIRED = "GrantTotalRequired";
        public const string DENOM_TOTAL_REQUIRED = "TotalRequired";
        public const string DENOM_ISREADONLY = "IsReadOnly";
        public const string DENOM_ISVISIBLE = "IsVisible";
        public const string DENOM_FOOTERTEXT = "FooterText";
        public const string DENOM_ALLOW_DECIMAL = "AllowDecimal";
        public const string DENOM_ALLOW_NEGATIVE = "AllowNegativeValue";
        public const string DENOM_OTHERAMOUNT_REQUIRED = "OtherAmountRequired";
        public const string DENOM_OTHERAMOUNT_LABELKEY = "OtherAmountLabelKey";
        public const string DENOM_SPINNER_REQUIRED = "SpinnerRequired";
        public const string DENOM_SEED = "Seed";
        //public const string DENOM_PRIMARY_ID = "PrimaryID";
        public const string DENOM_DECIMALPLACES = "DecimalPlaces";
        public const string DENOM_CURRENCYSYMBOL_REQUIRED = "CurrencySymbolRequired";
        public const string DENOM_CURRENCYSYMBOL_POSITION = "CurrencySymbolPosition";
        public const string DENOM_BINDING_TYPE = "BindingType";
        

        public const string AUTOCOMPLETE_PROPERTIES = "AutoCompleteProperties";
        public const string MASKING_PROPERTIES = "MaskingProperties";
        public const string VALIDATORS = "Validators";
        public const string DATE_PROPERTIES = "DateProperties";
        public const string TIME_PROPERTIES = "TimeProperties";

        public const string SECURITY = "Security";
        public const string SITECONFIG = "SiteConfig";

        public const string AUTOCOMPLETE_BAG = "AutoCompleteBehaviourPropertyBag";
        public const string MASKING_BAG = "MaskingBehaviourPropertyBag";
        public const string DATE_BAG = "DatePropertyBag";
        public const string TIME_BAG = "TimePropertyBag";

        public const string ACTION_URL = "ActionURL";
        public const string IMAGE_PATH = "ImagePath";
        public const string ACTION_NAME = "ActionName";
        public const string CONTROLLER_NAME = "ControllerName";
        public const string MIN_CHAR_REQUIRED = "MinCharRequired";
        public const string MAX_RESULT_COUNT = "MaxResultCount";
        public const string ORDER_BY = "OrderBy";
        public const string OTHER_AMOUNT_POSITION = "OtherAmountPosition";
        public const string IS_VIEW_MODE = "IsViewMode";
        public const string VALIDATION_MESSAGE_KEY = "ValidationMessageKey";
        public const string DENOM_MODE = "DenomMode";
        public const string UNDEFINEDROW_READONLY_COLUMNS = "UndefinedRowReadonlyColumns";
        public const string UNDEFINEDROW_EDITABLE_COLUMNS = "UndefinedRowEditableColumns";
        public const string MOVEMENT_INDICATOR_COLUMN = "MovementIndicatorColumn";
        public const string MOVEMENT_INDICATOR_REQUIRED = "MovementIndicatorRequired";

        public const string DATE_FORMAT = "DateFormat";
        public const string NO_OF_MONTHS = "NumberOfMonths";
        public const string SHOW_BUTTON_PANEL = "ShowButtonPanel";
        public const string MAX_DATE = "MaxDate";
        public const string MIN_DATE = "MinDate";
        public const string CHANGE_MONTH = "ChangeMonth";
        public const string CHANGE_YEAR = "ChangeYear";
        public const string CHANGE_DATE = "ChangeDate";
        public const string DATE_CSS_CLASS = "DateCssClass";
        public const string YEAR_RANGE = "YearRange";

        public const string MASKING_CHAR = "MaskingChar";
        public const string MASKING_TYPE = "MaskingType";
        public const string MASKING_CHAR_LENGTH = "MaskCharLength";
        public const string MASKING_POSITION = "MaskingPosition";

        public const string SHOW_AM_PM = "ShowAmPm";
        public const string TIME_FORMAT = "TimeFormat";
        public const string SHOW_DURATION = "ShowDuration";
        public const string STEP = "Step";
        public const string TIME_CSS_CLASS = "TimeCssClass";

        public const string EXTERNALIZATION_KEY_FORMAT = "{0}_{1}";
        public const string LABEL_FORMAT = "{0}_Label";
        public const string TOOLTIP_FORMAT = "{0}_ToolTip";
        public const string WATERMARK_FORMAT = "{0}_WaterMark";

        public const string EXPRESSION = "Expression";
        public const string VALIDATION_TYPE = "ValidationType";

        public const string TAB_INDEX = "TabIndex";
        public const string ON_LEAVE_FUNCTION = "OnLeaveFunction";
        public const string ON_KEY_UP_FUNCTION = "OnKeyUpFunction";
        public const string ON_KEY_DOWN_FUNCTION = "OnKeyDownFunction";
        public const string ON_CHANGE_FUNCTION = "OnChangeFunction";
        public const string ON_CLICK_FUNCTION = "OnClickFunction";
        public const string AUTOCOMPLETE_INPUT_FUNCTION = "AutoCompInputFunction";
        public const string CASCADE_INPUT_FUNCTION = "CascadeInputFunction";

        public const string IS_CURRENCY = "IsCurrency";
        public const string CSS_CLASS = "CssClass";
        public const string DISPLAY_TYPE = "DisplayType";
        public const string PARENT_ID = "ParentID";
        public const string VALIDATE_FORM = "ValidateForm";
        public const string BUTTON_TYPE = "ButtonType";
        public const string SHOW_DATE = "ShowDate";
        public const string SHOW_TIME = "ShowTime";

        public const string TARGET_CONTROL_ID = "TargetControlId";
        public const string TYPE = "Type";
        public const string LIST_LENGTH = "ListLength";
        public const string SELECTED_VALUE = "SelectedValue";

        public const string IS_VERTICAL_ALLIGN = "IsVerticalAllign";
        public const string TEMPLATE_NAME_KEY = "TemplateNameKey";
        public const string LISTITEM_TEMPLATE_CSSCLASS = "ListItemTemplateCssClass";
        public const string LISTITEM_MOUSEOVER_CSSCLASS = "ListItemMouseOverCssClass";
        public const string LISTITEM_SELECTED_CSSCLASS = "ListItemSelectedCssClass";

        public const string ON_DATAROW_SELECT_FUNCTION = "OnDataRowSelectFunction";
        public const string ON_DATAROW_SELECTION_CHANGE_FN = "OnDataRowSelectChangeFnName";
        public const string GRID_DATACOLUMN_DEFINITION_NAME = "GridDataColumnDefinitionName";
        public const string VALUE_MEMBER = "ValueMember";

        public const string PROPERTY_CONFIGURATION_TYPE = "PropertyConfigurationType";
        public const string MODEL_CONFIGURATION_TYPE = "ModelConfigurationType";
        public const string CONTROL_DEFAULT_PROPERTY_TYPE = "ControlDefaultPropertyType";
        public const string CONTROL_TEMPALTE_CONFIGURATION_TYPE = "ControlTemplateConfigurationType";
        public const string DATACOLUMN_DEFINITION_TYPE = "DataColumnDefinitionType";
        public const string DATAGRID_DEFINITION_TYPE = "DataGridDefinitionType";
        public const string DENOM_TEMPLATE_TYPE = "DenomTemplatesType";

        public const string CUSTOM_VALIDATION_EXPRESSION_CONFIGURATION_TYPE = "CustomValidationExpressionConfigurationType";

        public const string HIDEN_COUMN = "HiddenColumn";
        public const string PAGESIZE = "PageSize";
        public const string GRIDHEIGHT = "GridHeight";
        public const string ENABLEFILTER = "EnableFilter";
        public const string ENABLESORTING = "EnableSorting";
        public const string ENABLEEXPORT = "EnableExport";
        public const string ENABLEPAGINATION = "EnablePagination";
        public const string SERVERPAGINATION = "ServerPagination";
        public const string SELECTOPTION = "SelectOption";
        public const string DEFAULTSORTFIELD = "DefaultSortField";
        public const string IMAGESIZEPROPERTY = "ImageSizeProperty";
        public const string STATUSPROPERTY = "StatusProperty";

        public const string OVERRIDE_TOOLTIP = "OverrideTooltip";
    }
}