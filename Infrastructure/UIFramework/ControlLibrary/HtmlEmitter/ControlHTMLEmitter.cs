using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    internal abstract class ControlHTMLEmitter
    {
        #region "Member Variables"

        internal const string ATTRIBUTE_ONKEYUP = "onkeyup";
        internal const string ATTRIBUTE_ONCHANGE = "onchange";
        internal const string ATTRIBUTE_ONKEYDOWN = "onkeydown";
        internal const string ATTRIBUTE_ONKEYPRESS = "onkeypress";
        internal const string ATTRIBUTE_ONCLICK = "onclick";
        internal const string ATTRIBUTE_ONLEAVE = "onblur";
        internal const string ATTRIBUTE_ONFOCUS = "onfocus";

        internal const string ATTRIBUTE_DATA_ONLEAVE = "data-onblur";
        internal const string ATTRIBUTE_DATA_ONKEYPRESS = "data-onkeypress";
        internal const string ATTRIBUTE_DATA_ONCHANGE = "data-onchange";
        internal const string ATTRIBUTE_DATA_ONCLICK = "data-onclick";
        internal const string ATTRIBUTE_DATA_BAUTOCOMPLETE = "data-bautocomplete";
        internal const string ATTRIBUTE_DATA_AUTOCOMP_INPUT_FUNCTION = "data-acip";
        internal const string ATTRIBUTE_DATA_CASCADE_INPUT_FUNCTION = "data-cdip";
        internal const string ATTRIBUTE_DATA_SELECTION_CHANGE_FUNCTION = "data-scfn";
        internal const string ATTRIBUTE_DATA_ISCURRENCY = "data-iscurrency";
        internal const string ATTRIBUTE_DATA_ISNUMBER = "data-isnumber";
        internal const string ATTRIBUTE_DATA_BGRID = "data-bgrid";
        internal const string ATTRIBUTE_DATA_BSHUTTLE = "data-bshuttle";

        internal const string SCRIPT_OPEN_TAG = "<script language='javascript' type='text/javascript'>";
        internal const string SCRIPT_CLOSE_TAG = "</script>";
        internal const string SCRIPT_NAME = "javascript";
        internal const string ERROR_LBL_PREFIX = "err_lbl_";

        internal const string TEMPLATE_SCRIPT_TYPE = "text/html";
        internal const string SCRIPT_EVENT_NAME = "javascript:controlHelper.event";

        internal const string TAG_SCRIPT = "script";
        internal const string TAG_INPUT = "input";
        internal const string TAG_TEXTAREA = "textarea";
        internal const string TAG_LABEL = "label";
        internal const string TAG_SPAN = "span";
        internal const string TAG_DIV = "div";
        internal const string TAG_BUTTON = "button";
        internal const string TAG_A = "a";
        internal const string TAG_UL = "ul";
        internal const string TAG_LI = "li";
        internal const string TAG_SELECT = "select";
        internal const string TAG_OPTION = "option";
        internal const string TAG_IMAGE = "img";
        internal const string TAG_TABLE = "table";
        internal const string TAG_THEAD = "thead";
        internal const string TAG_TR = "tr";
        internal const string TAG_TH = "th";
        internal const string TAG_TD = "td";
        internal const string TAG_TBODY = "tbody";
        internal const string TAG_TFOOT = "tfoot";

        internal const string ATTR_VAL_TEXT = "text";
        internal const string ATTR_VAL_PASSWORD = "password";
        internal const string ATTR_VAL_NUMBER = "number";
        internal const string ATTR_VAL_CHECKED = "checked";
        internal const string ATTR_VAL_CHECKBOX = "checkbox";
        internal const string ATTR_VAL_RADIO = "radio";
        internal const string ATTR_VAL_HIDDEN = "hidden";
        internal const string ATTR_VAL_DISABLED = "disabled";
        internal const string ATTR_VAL_READONLY = "readonly";
        internal const string ATTR_VAL_ALIGN_CENTER = "center";

        internal const string ATTRIBUTE_ID = "id";
        internal const string ATTRIBUTE_STYLE = "style";
        internal const string ATTRIBUTE_TITLE = "title";
        internal const string ATTRIBUTE_VALUE = "value";
        internal const string ATTRIBUTE_TYPE = "type";
        internal const string ATTRIBUTE_FOR = "for";
        internal const string ATTRIBUTE_NAME = "name";
        internal const string ATTRIBUTE_PLACEHOLDER = " placeholder";
        internal const string ATTRIBUTE_TABINDEX = "tabindex";
        internal const string ATTRIBUTE_LANG = "language";
        internal const string ATTRIBUTE_CHECKED = "checked";
        internal const string ATTRIBUTE_SELECTED = "selected";
        internal const string ATTRIBUTE_DISABLED = "disabled";
        internal const string ATTRIBUTE_READONLY = "readonly";
        internal const string ATTRIBUTE_HREF = "href";
        internal const string ATTRIBUTE_MULTIPLE = "multiple";
        internal const string ATTRIBUTE_MAXLENGTH = "maxlength";
        internal const string ATTRIBUTE_ONINPUT = "oninput";
        internal const string ATTRIBUTE_ALIGN = "align";
        internal const string ATTRIBUTE_SOURCE = "src";
        internal const string ATTRIBUTE_WIDTH = "width";
        internal const string ATTRIBUTE_HEIGHT = "height";
        internal const string ATTRIBUTE_CLASS = "class";

        internal const string ATTRIBUTE_DATA_JSON = "data-json";
        internal const string ATTRIBUTE_DATA_OPTIONS = "data-options";
        internal const string ATTRIBUTE_DATA_COMBOTYPE = "data-combotype";
        internal const string ATTRIBUTE_DATA_ISDIRTY = "data-isdirty";
        internal const string ATTRIBUTE_DATA_ACPARAM = "data-acparam";
        internal const string ATTRIBUTE_DATA_GRIDPARAM = "data-gridparam";
        internal const string ATTRIBUTE_DATA_GRIDCOLUMNPROP = "data-gridcolprop";
        internal const string ATTRIBUTE_DATA_ISBALLYBUTTON = "data-isbbtn";
        internal const string ATTRIBUTE_DATA_SELECTED_VAL = "data-selectedval";
        internal const string ATTRIBUTE_DATA_SKIP_STATECHANGE = "data-skipstatechange";

        internal const string ATTRIBUTE_DATA_VALIDATIONS = "data-validations";
        internal const string ATTRIBUTE_DATA_VALPARAMS = "data-valparams";
        internal const string ATTRIBUTE_DATA_VALMSGS = "data-valmsgs";
        internal const string ATTRIBUTE_DATA_BTEMPLATELIST = "data-btemplatelist";
        internal const string ATTRIBUTE_DATA_BTEMPLATECOMBO = "data-btemplatecombo";
        internal const string ATTRIBUTE_DATA_CUSTOMTEMPLATE = "data-customtemplate";
        internal const string ATTRIBUTE_DATA_TEMPLATENAME = "data-templatename";
        internal const string ATTRIBUTE_DATA_TARGETCONTROL = "data-targetcontrol";
        internal const string ATTRIBUTE_DATA_COMBOTITLE = "data-combotitle";
        internal const string ATTRIBUTE_DATA_ACTIONURL = "data-actionurl";
        internal const string ATTRIBUTE_DATA_CONTAINSROW = "data-containsrow";
        internal const string ATTRIBUTE_DATA_BCHECKLIST = "data-checklist";
        internal const string ATTRIBUTE_DATA_BRADIOLIST = "data-radiolist";
        internal const string ATTRIBUTE_DATA_BCOMBO = "data-bcombo";
        internal const string ATTRIBUTE_DATA_DISPLAYMEMBER = "data-displaymember";
        internal const string ATTRIBUTE_DATA_VALUEMEMBER = "data-valuemember";
        internal const string ATTRIBUTE_DATA_INPUTPARAM = "data-inputparam";
        internal const string ATTRIBUTE_DATA_ISVIEWMODE = "data-isviewmode";
        internal const string ATTRIBUTE_DATA_VALIDATIONMESSAGE = "data-validationmessage";
        internal const string ATTRIBUTE_DATA_CURRENCYSYMBOL = "data-currencysymbol";

        internal const string ATTRIBUTE_DATA_DATEPROP = "data-dateprop";
        internal const string ATTRIBUTE_DATA_TIMEPROP = "data-timeprop";
        internal const string ATTRIBUTE_DATA_DATETIME = "data-datetime";
        internal const string ATTRIBUTE_DATA_PARENTID = "data-parentid";
        internal const string ATTRIBUTE_DATA_UTCConversion = "data-UTCConversion";
        internal const string ATTRIBUTE_DATA_DateKind = "data-dateKind";

        internal const string ATTRIBUTE_DATA_ONROW_SELECTFUNCTION = "data-onrowselectfunction";
        internal const string ATTRIBUTE_DATA_HEADER_CSS = "data-headercssclass";
        internal const string ATTRIBUTE_DATA_FOOTER_CSS = "data-footercssclass";
        internal const string ATTRIBUTE_DATA_GRANTTOTAL_REQUIRED = "data-granttotalrequired";
        internal const string ATTRIBUTE_DATA_CSS_CALSS = "data-cssclass";
        internal const string ATTRIBUTE_DATA_OTHERAMOUNT_REQUIRED = "data-otheramountrequired";
        internal const string ATTRIBUTE_DATA_OTHERAMOUNT_LABEL = "data-otheramountlabel";
        internal const string ATTRIBUTE_DATA_COLUMNS = "data-columns";
        internal const string ATTRIBUTE_DATA_PRIMARYID = "data-primaryid";

        internal const string ATTRIBUTE_DATA_UNDEFINED_COLUMN = "data-undefinedcolumn";
        internal const string ATTRIBUTE_DATA_UNDEFINED_COLUMNVALUE = "data-undefinedcolumnvalue";
        internal const string ATTRIBUTE_DATA_UNDEFINED_READONLY_COLUMNS = "data-undefinedreadonlycolumns";
        internal const string ATTRIBUTE_DATA_UNDEFINED_EDITABLE_COLUMNS = "data-undefinededitablecolumns";
        internal const string ATTRIBUTE_DATA_DENOMMODE = "data-denommode";
        internal const string ATTRIBUTE_DATA_OTHERAMOUNT_POSITION = "data-otheramountposition";
        internal const string ATTRIBUTE_DATA_MOVEMENT_COLUMN = "data-movementcolumn";
        internal const string ATTRIBUTE_DATA_MOVEMENT_REQUIRED = "data-movementrequired";

        internal const string ATTRIBUTE_DATA_LISTLENGTH = "data-listlength";
        internal const string ATTRIBUTE_DATA_ISVERTICAL = "data-isvertical";

        internal const string ATTRIBUTE_DATA_BTN_VALIDATE = "data-btnvalidate";
        internal const string ATTRIBUTE_DATA_HASDATEANDTIME = "data-hasdateandtime";

        internal const string ATTRIBUTE_DATA_TEMPLATETEXT = "data-btemplatetext";
        internal const string ATTRIBUTE_DATA_TEMPLATEID = "data-btemplateid";
        internal const string ATTRIBUTE_DATA_SELECTED_ITEM_CSSCLASS = "data-selecteditemcssclass";
        internal const string ATTRIBUTE_DATA_ERROR_CSSCLASS = "data-errorclass";
        internal const string ATTRIBUTE_DATA_CSSCLASS = "data-cssclass";
        internal const string DATA_REQUIRED = "data-Required";
        internal const string DATA_VALIDATION = "data-Validation";
        internal const string DENOM_FIELD_TYPE_INPUT = "input";
        internal const string DENOM_FIELD_TYPE_TEXT = "text";

        internal const string ATTRIBUTE_DATA_COLUMNNAME = "data-displaymember";
        internal const string ATTRIBUTE_DATA_TOTALREQUIRED = "data-totalrequired";
        internal const string ATTRIBUTE_DATA_TARGET_TOTAL_CONTROL_ID = "data-targettotalcontrolid";
        internal const string ATTRIBUTE_DATA_PRIMARY_ID = "data-primaryid";
        internal const string ATTRIBUTE_DATA_CALCULATIONREQUIRED = "data-calculationrequired";
        internal const string ATTRIBUTE_DATA_FORMULA = "data-formula";
        internal const string ATTRIBUTE_DATA_DATATYPE = "data-datatype";
        internal const string ATTRIBUTE_DATA_ALLOWNEGATIVE = "data-allownegative";
        internal const string ATTRIBUTE_DATA_ALLOWDECIMAL = "data-allowdecimal";
        internal const string ATTRIBUTE_DATA_SEED = "data-seed";
        internal const string ATTRIBUTE_DATA_COLUMNDATATYPE = "data-columndatatype";
        internal const string ATTRIBUTE_DATA_ISVISIBLE = "data-isvisible";
        internal const string ATTRIBUTE_DATA_ISEDITABLE = "data-iseditable";
        internal const string ATTRIBUTE_DATA_SPINNERREQUIRED = "data-spinnerrequired";
        internal const string ATTRIBUTE_DATA_ISREADONLY = "data-isreadonly";
        internal const string ATTRIBUTE_DATA_DECIMALPLACES = "data-decimalplaces";
        internal const string ATTRIBUTE_DATA_BINDINGMODE = "data-bindingtype";
        internal const string ATTRIBUTE_DATA_ALIGNMENT = "data-alignment";
        internal const string ATTRIBUTE_DATA_INDICATOR = "data-indicator";

        internal const string VAL_TRUE = "true";
        internal const string VAL_FALSE = "false";

        internal const string ATTRIBUTE_DATA_CURRENCYFORMAT = "data-currencyformat";

        internal const string TEMPLATE_PREFIX = "<li>";
        internal const string TEMPLATE_SUFFIX = "</li>";

        internal const string TEMPLATE_UL_PREFIX = "<ul BallylistView-selected-itemkey='{0}'>";
        internal const string TEMPLATE_UL_SUFFIX = "</ul>";

        #endregion "Member Variables"

        #region "Properties"

        public string Value { get; set; }

        protected ILengthValidator LengthValidation { get; private set; }

        protected Dictionary<string, string> ValidationAttributes { get; private set; }

        #endregion "Properties"

        #region "Constructors"

        protected ControlHTMLEmitter(List<ValidationBase> validators, bool? mandatory)
        {
            this.ValidationAttributes = new Dictionary<string, string>();
            if (validators != null)
            {
                CreateValidationAttributes(validators, mandatory);
            }
        }

        #endregion "Constructors"

        #region "Public Methods"

        public abstract void Emit(out string controlHTMLString);

        #endregion "Public Methods"

        #region "Protected Members"

        protected IModelPropertyConfiguration ReadPropertyConfiguration(string modelName, string propertyName, string configKey)
        {
            IModelPropertyConfiguration propertyConfig = ControlLibraryConfig.ControlConfigReader.GetConfigurationSettings(modelName, propertyName, configKey);
            return propertyConfig;
        }

        protected void CreateValidationAttributes(List<ValidationBase> validators, bool? mandatory)
        {
            if (validators != null)
            {
                StringBuilder strValidations = new StringBuilder();
                StringBuilder strValidationParams = new StringBuilder();
                StringBuilder strValidationMessages = new StringBuilder();

                foreach (IValidator validation in validators)
                {
                    if (validation.DoValidate)
                    {
                        switch (validation.Type)
                        {
                            case ValidatorsType.Required:
                                {
                                    if (mandatory.HasValue && mandatory.Value)
                                    {
                                        appendValidation(strValidations, ValidatorsType.Required.ToString());
                                        appendValidation(strValidationParams, "[]");
                                        appendValidation(strValidationMessages, string.Format("[{0}]", validation.Message)); 
                                    }
                                    break;
                                }
                            case ValidatorsType.SpecialChar:
                                {
                                    var validator = validation as ISpecialCharValidator;
                                    if (validator != null)
                                    {
                                        if (validator.Restriction == RestrictionType.Allow)
                                        {
                                            appendValidation(strValidations, string.Format("{0}{1}", ValidatorsType.SpecialChar.ToString(), "Allow"));
                                        }
                                        else
                                        {
                                            appendValidation(strValidations, string.Format("{0}{1}", ValidatorsType.SpecialChar.ToString(), "Restrict"));
                                        }
                                        appendValidation(strValidationParams, string.Format("[{0}]", validator.SpecialChars.Replace("\"", "\\\"")));
                                        appendValidation(strValidationMessages, string.Format("[{0}]", validation.Message));
                                    }
                                    break;
                                }
                            case ValidatorsType.Length:
                                {
                                    var validator = validation as ILengthValidator;
                                    if (validator != null)
                                    {
                                        appendValidation(strValidations, ValidatorsType.Length.ToString());
                                        appendValidation(strValidationParams, string.Format("[{0}|{1}]", validator.MinLength, validator.MaxLength));
                                        appendValidation(strValidationMessages, string.Format("[{0}]", validation.Message));

                                        LengthValidation = validator;
                                    }
                                    break;
                                }
                            case ValidatorsType.Range:
                                {
                                    var validator = validation as IRangeValidator;
                                    if (validator != null)
                                    {
                                        appendValidation(strValidations, ValidatorsType.Range.ToString());
                                        appendValidation(strValidationParams, string.Format("[{0}|{1}]", validator.MinValue, validator.MaxValue));
                                        appendValidation(strValidationMessages, string.Format("[{0}]", validation.Message));
                                    }
                                    break;
                                }
                            case ValidatorsType.RegExp:
                                {
                                    var validator = validation as IRegExValidator;
                                    if (validator != null)
                                    {
                                        appendValidation(strValidations, ValidatorsType.RegExp.ToString());
                                        appendValidation(strValidationParams, string.Format("[{0}]", validator.RegExpression));
                                        appendValidation(strValidationMessages, string.Format("[{0}]", validation.Message));
                                    }
                                    break;
                                }
                            case ValidatorsType.Custom:
                                {
                                    var validator = validation as ICustomValidator;
                                    if (validator != null)
                                    {
                                        appendValidation(strValidations, ValidatorsType.Custom.ToString());

                                        //This code will set the default custom validation expression if the user not overriden the expression
                                        var validationExpression = validator.Expression;
                                        if (string.IsNullOrEmpty(validationExpression))
                                        {
                                            CustomValidationExpressionConfiguration customExpression = ControlLibraryConfig.ControlConfigReader.GetCustomValidationExpressionConfiguration(validator.ValidationType.ToString());
                                            if (customExpression != null)
                                            {
                                                validationExpression = customExpression.Expression;
                                            }
                                        }
                                        appendValidation(strValidationParams, string.Format("[{0}]", validationExpression));

                                        //**************************************************************************************************
                                        //appendValidation(strValidationParams, string.Format("[{0}]", validator.Expression));
                                        appendValidation(strValidationMessages, string.Format("[{0}]", validation.Message));
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }

                if (false == string.IsNullOrEmpty(strValidations.ToString()))
                {
                    ValidationAttributes.Add(ATTRIBUTE_DATA_VALIDATIONS, strValidations.ToString());
                    ValidationAttributes.Add(ATTRIBUTE_DATA_VALPARAMS, strValidationParams.ToString());
                    ValidationAttributes.Add(ATTRIBUTE_DATA_VALMSGS, strValidationMessages.ToString());
                }
            }
        }

        private void appendValidation(StringBuilder validation, string toAppend)
        {
            if (true == string.IsNullOrEmpty(validation.ToString()))
            {
                validation.Append(toAppend);
            }
            else
            {
                validation.AppendFormat(",{0}", toAppend);
            }
        }

        protected void BuildHiddenTag(string name, string value, out string hiddenTag)
        {
            TagBuilder hidden = new TagBuilder(TAG_INPUT);
            this.SetAttribute(hidden, ATTRIBUTE_TYPE, ATTR_VAL_HIDDEN);
            this.SetAttribute(hidden, ATTRIBUTE_NAME, name);
            this.SetAttribute(hidden, ATTRIBUTE_VALUE, value);
            hiddenTag = hidden.ToString(TagRenderMode.SelfClosing);
        }

        protected void BuildHiddenTag(string id, string value, out string hiddenTag, out string hiddenTagID)
        {
            TagBuilder hidden = new TagBuilder(TAG_INPUT);
            this.SetAttribute(hidden, ATTRIBUTE_TYPE, ATTR_VAL_HIDDEN);
            this.SetID(hidden, id, out hiddenTagID);
            this.SetAttribute(hidden, ATTRIBUTE_NAME, id);
            this.SetAttribute(hidden, ATTRIBUTE_VALUE, value);
            hiddenTag = hidden.ToString(TagRenderMode.SelfClosing);
        }

        protected void BuildHiddenTag(string id, string value, out TagBuilder hiddenTag)
        {
            hiddenTag = new TagBuilder(TAG_INPUT);
            this.SetAttribute(hiddenTag, ATTRIBUTE_TYPE, ATTR_VAL_HIDDEN);
            this.SetAttribute(hiddenTag, ATTRIBUTE_NAME, id);
            this.SetID(hiddenTag, id, out id);
            this.SetAttribute(hiddenTag, ATTRIBUTE_VALUE, value);
        }

        protected void BuildHiddenTag(string id, string name, string value, out TagBuilder hiddenTag)
        {
            hiddenTag = new TagBuilder(TAG_INPUT);
            this.SetAttribute(hiddenTag, ATTRIBUTE_TYPE, ATTR_VAL_HIDDEN);
            this.SetID(hiddenTag, id, out id);
            this.SetAttribute(hiddenTag, ATTRIBUTE_NAME, name);
            this.SetAttribute(hiddenTag, ATTRIBUTE_VALUE, value);
        }

        protected void SetControlCssClasses(TagBuilder tag, string controlClass, string errorClass)
        {
            this.SetCssClass(tag, controlClass);
            this.SetAttribute(tag, ATTRIBUTE_DATA_ERROR_CSSCLASS, errorClass);
            //this.SetAttribute(tag, ATTRIBUTE_DATA_CSSCLASS, controlClass);
        }

        protected void SetValidations(TagBuilder tag, bool isReadonly)
        {
            if (this.ValidationAttributes != null && this.ValidationAttributes.Count > 0 && false == isReadonly)
            {
                this.SetAttribute(tag, ATTRIBUTE_DATA_ISDIRTY, "true");
                foreach (var item in this.ValidationAttributes)
                {
                    this.SetAttribute(tag, item.Key, item.Value);
                }
            }
        }

        protected void SetAttribute(TagBuilder tag, string attribute, object value)
        {
            if (value != null)
            {
                tag.MergeAttribute(attribute, value.ToString());
            }
        }

        protected void SetAttribute(TagBuilder tag, string attribute, string value, bool check)
        {
            if (true == check)
            {
                tag.MergeAttribute(attribute, value);
            }
        }

        protected void SetAttribute(TagBuilder tag, object source, string attribute, string value)
        {
            if (source != null && false == string.IsNullOrEmpty(value))
            {
                tag.MergeAttribute(attribute, value);
            }
        }

        protected void SetAttribute(TagBuilder tag, string attribute, string value)
        {
            if (false == string.IsNullOrEmpty(value))
            {
                tag.MergeAttribute(attribute, value);
            }
        }

        protected void SetID(TagBuilder tag, string id, out string controlID)
        {
            if (false == string.IsNullOrEmpty(id))
            {
                tag.GenerateId(id);
                controlID = tag.Attributes["id"];
            }
            else
            {
                controlID = null;
            }
        }

        protected void SetCssClass(TagBuilder tag, string value)
        {
            if (false == string.IsNullOrEmpty(value))
            {
                tag.AddCssClass(value);
            }
        }

        protected void SetCssClass(TagBuilder tag, string value, bool check)
        {
            if (false == string.IsNullOrEmpty(value) && check)
            {
                tag.AddCssClass(value);
            }
        }

        protected void SetInnerValue(TagBuilder tag, string value)
        {
            if (false == string.IsNullOrEmpty(value))
            {
                tag.SetInnerText(value);
            }
        }

        protected void SetInnerHtml(TagBuilder tag, string value)
        {
            if (false == string.IsNullOrEmpty(value))
            {
                tag.InnerHtml = value;
            }
        }

        protected void SetInnerHtml(TagBuilder tag, string value, bool isAppend)
        {
            if (false == string.IsNullOrEmpty(value))
            {
                if (isAppend)
                {
                    tag.InnerHtml = tag.InnerHtml + value;
                }
                else
                {
                    tag.InnerHtml = value;
                }
            }
        }

        protected void SetFunction(TagBuilder tag, string eventName, string functionName)
        {
            if (false == string.IsNullOrEmpty(functionName))
            {
                tag.MergeAttribute(eventName, string.Format("{0}:{1}", SCRIPT_NAME, functionName));
            }
        }

        protected void SetLeaveEvent(TagBuilder tag)
        {
            tag.MergeAttribute(ATTRIBUTE_ONLEAVE, string.Format("{0}.{1}", SCRIPT_EVENT_NAME, "onLeaveTextBox(event)"));
        }

        protected void SetInputTextEvent(TagBuilder tag)
        {
            tag.MergeAttribute(ATTRIBUTE_ONINPUT, string.Format("{0}.{1}", SCRIPT_EVENT_NAME, "onInputText(event)"));
        }

        protected void SetTextBoxKeyPressEvent(TagBuilder tag)
        {
            tag.MergeAttribute(ATTRIBUTE_ONKEYPRESS, string.Format("{0}.{1}", SCRIPT_EVENT_NAME, "onKeyPressTextBox(event)"));
        }

        protected void SetClickEvent(TagBuilder tag)
        {
            tag.MergeAttribute(ATTRIBUTE_ONCLICK, string.Format("{0}.{1}", SCRIPT_EVENT_NAME, "onClickButton(event)"));
        }

        protected void SetChangeEvent(TagBuilder tag)
        {
            tag.MergeAttribute(ATTRIBUTE_ONCHANGE, string.Format("{0}.{1}", SCRIPT_EVENT_NAME, "onChangeControl(event)"));
        }

        protected string GetScriptString(TagBuilder tag, StringBuilder scriptStr)
        {
            if (false == string.IsNullOrEmpty(scriptStr.ToString()))
            {
                tag.InnerHtml = scriptStr.ToString();
                return Convert.ToString(tag);
            }
            else
            {
                return string.Empty;
            }
        }

        protected void SetCustomAttributes(TagBuilder tag, IDictionary<string, object> attributes)
        {
            if (attributes != null)
            {
                var keys = attributes.Keys;
                foreach (var key in keys)
                {
                    tag.MergeAttribute(key, Convert.ToString(attributes[key]));
                }
            }
        }

        protected void SetAutoCompleteAttributes(TagBuilder tag, bool isAutoComp, string autoCompInputFunction)
        {
            if (true == isAutoComp)
            {
                this.SetAttribute(tag, ATTRIBUTE_DATA_BAUTOCOMPLETE, VAL_TRUE);

                this.SetAttribute(tag, ATTRIBUTE_DATA_AUTOCOMP_INPUT_FUNCTION, autoCompInputFunction);
            }
        }

        internal string GetErrorLabel(ControlPropertyBag propertyBag)
        {
            string resultHtml = string.Empty;

            if (propertyBag != null)
            {
                TagBuilder errorLabel = new TagBuilder(TAG_LABEL);
                string id = string.Empty;
                this.SetID(errorLabel, ERROR_LBL_PREFIX + propertyBag.ControlName, out id);
                SetCssClass(errorLabel, propertyBag.ValidationErrorCssClass);
                errorLabel.InnerHtml = string.IsNullOrEmpty(propertyBag.ErrorMessage) ? "&nbsp;" : propertyBag.ErrorMessage;
                resultHtml = errorLabel.ToString();
            }
            return resultHtml;
        }

        #endregion "Protected Members"
    }
}