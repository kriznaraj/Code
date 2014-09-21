using System.Collections.Generic;

namespace BallyTech.UI.Web.ControlLibrary
{
    internal static class Data
    {

        public static List<ModelConfiguration> GetModelConfigurationData()
        {
            List<ModelConfiguration> dataList = new List<ModelConfiguration>();
            Dictionary<string, PropertyConfiguration> properties = new Dictionary<string, PropertyConfiguration>();


            //Property Configuration
            List<ValidationBase> validators1 = new List<ValidationBase>();
            validators1.Add(new RequiredValidator(new Dictionary<string, string>() { { "Validate", "true" }, { "MessageKey", "LoginInputView_UserName_Required" } }));
            PropertyConfiguration usernameProperty = new PropertyConfiguration("UserName", validators1, accessPolicyCode: "UN_ACCESS_POLICY");

            List<ValidationBase> validators2 = new List<ValidationBase>();
            validators2.Add(new RequiredValidator(new Dictionary<string, string>() { { "Validate", "true" }, { "MessageKey", "LoginInputView_Password_Required" } }));
            PropertyConfiguration passwordProperty = new PropertyConfiguration("Password", validators2, accessPolicyCode: "PWD_ACCESS_POLICY");

            List<ValidationBase> validators3 = new List<ValidationBase>();
            validators3.Add(new RequiredValidator(new Dictionary<string, string>() { { "Validate", "true" }, { "MessageKey", "AddPlayerModel_PostalCode_Required" } }));

            properties.Add(usernameProperty.Key, usernameProperty);
            properties.Add(passwordProperty.Key, passwordProperty);

            
            //Model Property Configuration
            List<IModelPropertyConfiguration> mlist = new List<IModelPropertyConfiguration>();
            mlist.Add(new ModelPropertyConfiguration("UserName", "UserName", "LoginInputView_UserName", properties["UserName"]));
            mlist.Add(new ModelPropertyConfiguration("Password", "Password", "LoginInputView_Password", properties["Password"]));

            ModelConfiguration mConfig = new ModelConfiguration("LoginInputView", mlist);

            dataList.Add(mConfig);

            //**************************************************** Add player Model Configuration ************************************************************************
            List<IModelPropertyConfiguration> playerModelPropertyList = new List<IModelPropertyConfiguration>();

            Dictionary<string, string> adb = new Dictionary<string, string>() { { "ActionName", "process" }, { "ControllerName", "ControlLibrary_PostalCode_Get" }, { "ActionURL", "ControlLibrary_PostalCode_Get/process" } };
            AutoCompleteBehaviourPropertyBag abag = new AutoCompleteBehaviourPropertyBag(adb);
            Dictionary<string, object> bd = new Dictionary<string, object>() { { "AutoCompleteBehaviourPropertyBag", abag } };

            playerModelPropertyList.Add(new ModelPropertyConfiguration("PostalCode", "HomeAddress.PostalCode", "AddPlayerModel_HomeAddress_PostalCode",
                    new PropertyConfiguration("PostalCode", new List<ValidationBase>() {
                                new RequiredValidator(new Dictionary<string, string>() { { "Validate", "true" }, { "MessageKey", "AddPlayerModel_PostalCode_Required" } })
            }, behaviourDtls: bd)));

            playerModelPropertyList.Add(new ModelPropertyConfiguration("AccountNumber", "AccountNumber", "AddPlayerModel_AccountNumber",
                    new PropertyConfiguration("AccountNumber", new List<ValidationBase>() {
                                new RequiredValidator(new Dictionary<string, string>() { { "Validate", "true" }, { "MessageKey", "AddPlayerModel_AccountNumber_Required" } })
            })));

            playerModelPropertyList.Add(new ModelPropertyConfiguration("FirstName", "FirstName", "AddPlayerModel_FirstName",
                    new PropertyConfiguration("FirstName", new List<ValidationBase>() {
                                new RequiredValidator(new Dictionary<string, string>() { { "Validate", "true" }, { "MessageKey", "AddPlayerModel_FirstName_Required" } })
            })));

            playerModelPropertyList.Add(new ModelPropertyConfiguration("LastName", "LastName", "AddPlayerModel_LastName",
                    new PropertyConfiguration("LastName", new List<ValidationBase>() {
                                new RequiredValidator(new Dictionary<string, string>() { { "Validate", "true" }, { "MessageKey", "AddPlayerModel_LastName_Required" } })
            })));

            playerModelPropertyList.Add(new ModelPropertyConfiguration("PhoneNumber", "PhoneNumber", "AddPlayerModel_PhoneNumber",
                    new PropertyConfiguration("PhoneNumber", new List<ValidationBase>() {
                                new RequiredValidator(new Dictionary<string, string>() { { "Validate", "true" }, { "MessageKey", "AddPlayerModel_PhoneNumber_Required" } }),
                                new CustomValidators(new Dictionary<string, string>() { { "Validate", "true" }, { "MessageKey", "AddPlayerModel_PhoneNumber_Invalid" },{"ValidationType","Phone"} })
            })));

            playerModelPropertyList.Add(new ModelPropertyConfiguration("SSN", "SSN", "AddPlayerModel_SSN",
                    new PropertyConfiguration("SSN", new List<ValidationBase>() {
                                new RequiredValidator(new Dictionary<string, string>() { { "Validate", "true" }, { "MessageKey", "AddPlayerModel_SSN_Required" } }),
                                //new SpecialCharValidator(new Dictionary<string, string>() { { "Validate", "true" }, { "MessageKey", "AddPlayerModel_SSN_SpecialCharNotAllowed" },{"Expression",""},{"Restriction","Allow"} })
                                new CustomValidators(new Dictionary<string, string>() { { "Validate", "true" }, { "MessageKey", "AddPlayerModel_SSN_Invalid" },{"ValidationType","SSN"} })
            })));

            ModelConfiguration playerAddConfig = new ModelConfiguration("AddPlayerModel", playerModelPropertyList);
            dataList.Add(playerAddConfig);

            //**************************************************** END of Add player Model Configuration ************************************************************************

            /*Control Library Controls Test Validators*/
            List<ValidationBase> fn_ReqValidator = new List<ValidationBase>();
            fn_ReqValidator.Add(new RequiredValidator(new Dictionary<string, string>() { { "Validate", "true" }, { "MessageKey", "AllControlModel_UserName_Required" } }));
            fn_ReqValidator.Add(new LengthValidator(new Dictionary<string, string>() { { "MinLength", "2" }, { "MaxLength", "18" }, { "Validate", "true" }, { "MessageKey", "AllControlModel_UserName_Length" } }));
            fn_ReqValidator.Add(new SpecialCharValidator(new Dictionary<string, string>() { { "Expression", "#$" }, { "Restriction", RestrictionType.Restrict.ToString() }, { "Validate", "true" }, { "MessageKey", "AllControlModel_UserName_SpecialChar" } }));
            //fn_ReqValidator.Add(new RangeValidator(new Dictionary<string, string>() { { "MinLength", "2" }, { "MaxLength", "5" }, { "Validate", "true" }, { "MessageKey", "AllControlModel_UserName_Range" } }));


            //PropertyConfiguration firstNameProperty = new PropertyConfiguration("FirstName", fn_ReqValidator, accessPolicyCode: "UN_ACCESS_POLICY");

            //PropertyConfiguration firstNameProperty = new PropertyConfiguration("FirstName", fn_ReqValidator, accessPolicyCode: "UN_ACCESS_POLICY",);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ActionURL", @"ControlLibrary_AutoComplete_load");
            AutoCompleteBehaviourPropertyBag autoCompleteBhBag = new AutoCompleteBehaviourPropertyBag(dic);
            Dictionary<string, object> behaviourDtls = new Dictionary<string, object>();
            behaviourDtls.Add("AutoCompleteBehaviourPropertyBag", autoCompleteBhBag);

            /*Masking Property Configuration*/
            Dictionary<string, string> Maskingdic = new Dictionary<string, string>();
            Maskingdic.Add("MaskingChar", "X");
            Maskingdic.Add("MaskingType", MaskingType.Partial.ToString());
            Maskingdic.Add("MaskCharLength", 4.ToString());
            Maskingdic.Add("MaskingPosition", MaskingPosition.Last.ToString());
            MaskingBehaviourPropertyBag maskingBag = new MaskingBehaviourPropertyBag(Maskingdic);
            behaviourDtls.Add("MaskingBehaviourPropertyBag", maskingBag);

            PropertyConfiguration firstNameProperty = new PropertyConfiguration("FirstName", fn_ReqValidator, accessPolicyCode: "FN_ACCESS_POLICY",
                behaviourDtls: behaviourDtls);
            properties.Add(firstNameProperty.Key, firstNameProperty);

            List<IModelPropertyConfiguration> modelList = new List<IModelPropertyConfiguration>();

            modelList.Add(new ModelPropertyConfiguration("FirstName", "FirstName", "AllControlModel_FirstName", properties["FirstName"]));

            ModelConfiguration modelConfig = new ModelConfiguration("AllControlModel", modelList);

            dataList.Add(modelConfig);

            return dataList;

        }

        internal static List<ControlDefaultPropertyBag> GetControlsDefaultPropertyData()
        {
            List<ControlDefaultPropertyBag> defaultList = new List<ControlDefaultPropertyBag>();

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyLabel,
                CssClass = "label",
                MandatoryChar = "*",
                MandatoryCssClass = "label-mandatory",
                MaskingType = MaskingType.Complete,
                MaskingChar = '#',
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyTextBox,
                CssClass = "bally-textbox",
                MaskingType = MaskingType.Complete,
                MaskingChar = '#',
                MaxResultCount = 10,
                MinCharRequired = 3,
                OrderBy = OrderByType.Asc,
                SearchType = SearchType.Contains,
                ValidationErrorCssClass = "lbl-errormsg",
                ControlErrorCssClass = "error-bally-textbox"
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyNumericTextBox,
                CssClass = "bally-textbox",
                MaskingType = MaskingType.Complete,
                MaskingChar = '#',
                MaxResultCount = 10,
                MinCharRequired = 3,
                OrderBy = OrderByType.Asc,
                SearchType = SearchType.Contains,
                CurrencyFormatString = "n2",
                ValidationErrorCssClass = "lbl-errormsg",
                ControlErrorCssClass = "error-bally-textbox"
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyPasswordBox,
                CssClass = "bally-passwordbox",
                ValidationErrorCssClass = "lbl-errormsg",
                ControlErrorCssClass = "error-bally-passwordbox"
                //Height = "Auto",
                //Width = "Auto"
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyCheckBox,
                CssClass = "bally-checkbox",
                ValidationErrorCssClass = "lbl-errormsg",
                ControlErrorCssClass = "bally-checkbox-error"
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyRadioButton,
                CssClass = "bally-radio",
                ValidationErrorCssClass = "lbl-errormsg",
                ControlErrorCssClass = "bally-radio-error"
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyTextArea,
                CssClass = "bally-textarea",
                ValidationErrorCssClass = "lbl-errormsg",
                ControlErrorCssClass = "error-bally-textarea"
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyButton,
                CssClass = "bally-button",
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyLinkButton,
                CssClass = "linkbtn-tooltip",
                //Height = "Auto",
                //Width = "Auto"
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyDateTime,
                CssClass = "date-time",
                TimeCssClass = "bally-textbox-timepicker",
                DateCssClass = "bally-textbox-datepicker",
                ChangeMonth = true,
                ChangeDate = true,
                ChangeYear = true,
                ShowButtonPanel = true,
                Step = 5,
                ShowAmPm = false,
                ShowDuration = false,
                TimeFormat = "H:i",
                DateFormat = "yy-mm-dd",
                ValidationErrorCssClass = "lbl-errormsg",
                ControlErrorCssClass = "date-time-error"
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyDropDown,
                CssClass = "bally-drop-down-wrapper",
                ValidationErrorCssClass = "lbl-errormsg",
                ControlErrorCssClass = "bally-drop-down-wrapper-error"
                //CssClass = "bally-combobox",
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyCheckBoxList,
                CssClass = "bally-checkbox",
                ValidationErrorCssClass = "lbl-errormsg",
                ControlErrorCssClass = "bally-checkbox-error"
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyRadioButtonList,
                CssClass = "bally-radio",
                ValidationErrorCssClass = "lbl-errormsg",
                ControlErrorCssClass = "bally-radio-error"
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyListBox,
                CssClass = "bally-listbox",
                ValidationErrorCssClass = "lbl-errormsg",
                ControlErrorCssClass = "bally-listbox-error",
                ListItemTemplateCssClass = "bally-listbox"
            });

            defaultList.Add(new ControlDefaultPropertyBag()
            {
                ControlName = ControlNames.BallyTemplateListBox,
                CssClass = "bally-listbox",
                ValidationErrorCssClass = "lbl-errormsg",
                ControlErrorCssClass = "bally-listbox-error",
                ListItemTemplateCssClass = "bally-player-info-list"
            });

            return defaultList;
        }

        public static List<IControlTemplateConfiguration> GetTemplateConfigurationData()
        {
            List<IControlTemplateConfiguration> templateList = new List<IControlTemplateConfiguration>();

            //templateList.Add(new ControlTemplateConfiguration() { TemplateKey = "PlayerModel.PlayerMultiListTemplate", TemplateHTML = @"<ul BallylistView-selected-itemkey='{0}'><li><div><img src='../../DefaultTheme/Images/Users.png' alt='Player'/><h4>{1}</h4><span><label>Player ID:</label> <label>{2}</label></span> <span> <label>Account ID:</label><label>{3}</label></span></div></li></ul>" });
            //templateList.Add(new ControlTemplateConfiguration() { TemplateKey = "PlayerModel.PlayerSingleListTemplate", TemplateHTML = @"<ul BallylistView-selected-itemkey='${0}'> <li> <div>${1}</div> </li> </ul>" });
            templateList.Add(new ControlTemplateConfiguration("player-info-list", @"<div><img src='../../Content/MetroTheme/Images/Users.png' alt='Player'/><h4>{1}</h4><span><label>Player ID:</label> <label>{2}</label></span> <span> <label>Account ID:</label><label>{3}</label></span></div></li></ul>" ));
            templateList.Add(new ControlTemplateConfiguration("SimpleListTemplate", @"<div>{1}</div>"));

            return templateList;
        }

        public static List<GridDataColumnDefinitions> GetGridDataColumnDefinitionData()
        {
            List<GridDataColumnDefinitions> gridDataColumnDefinitionList = new List<GridDataColumnDefinitions>();

            gridDataColumnDefinitionList.Add(new GridDataColumnDefinitions("PlayerGrid", new List<IDataColumnDefinition>() {
                new DataColumnDefinition("PlayerID", "PlayerID","Player ID", GridColumnDataType.Number),
                new DataColumnDefinition("PlayerName", "PlayerName","Player Name"),
                new DataColumnDefinition("AccountNumber", "AccountNumber","Account Number", GridColumnDataType.Number),
                new DataColumnDefinition("PhoneNumber", "PhoneNumber","Phone Number", GridColumnDataType.Text),
                new DataColumnDefinition("City", "City","City"),
                new DataColumnDefinition("DOB", "DOB","Date of birth", GridColumnDataType.Date)
            }));

            gridDataColumnDefinitionList.Add(new GridDataColumnDefinitions("PlayersListGrid", new List<IDataColumnDefinition>() {
                new DataColumnDefinition("PlayerID", "PlayerID","Player ID", GridColumnDataType.Number),
                new DataColumnDefinition("FirstName", "FirstName","First Name"),
                new DataColumnDefinition("LastName", "LastName","Last Name"),
                new DataColumnDefinition("AccountNumber", "AccountNumber","Account Number", GridColumnDataType.Number),
                new DataColumnDefinition("PhoneNumber", "PhoneNumber","Phone Number", GridColumnDataType.Text),
                new DataColumnDefinition("DOB", "DOB","Date of birth", GridColumnDataType.Date),
                new DataColumnDefinition("SSN", "SSN","SSN"),
                
            }));

            return gridDataColumnDefinitionList;
        }
    }
}
