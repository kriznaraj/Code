using System.Collections.Generic;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    public static partial class ControlExtension
    {
        #region "Public Methods"

        public static MvcHtmlString BallyButton<TModel>(this HtmlHelper<TModel> htmlHelper, ButtonType buttonType, string controlName, string parentID, bool validateForm = true, StylePropertyBag style = null, short tabIndex = 0, string onClickFunction = "", string cssClass = "", bool? isEnabled = null, List<Security> taskCodes = null)
        {
            object value = string.Empty;
            string errMsg = string.Empty;
            string buttonHTMLString = string.Empty;
            string modelName = htmlHelper.ViewData.Model.GetType().Name;
            Dictionary<string, string> overrideSettings;

            overrideSettings = GetButtonOverrideSettings(buttonType, parentID, validateForm, string.Empty, style, tabIndex, onClickFunction, cssClass: cssClass);

            var fillers = ControlPropertyFillerFactory.Get();
            FillerParams fillerParams = new FillerParams(modelName, controlName, overrideSettings, isEnabled: isEnabled, userTaskCodes: taskCodes);
            var buttonpropertyBag = new ButtonPropertyBag(fillerParams);
            buttonpropertyBag.ButtonCatagory = ButtonCatagory.BallyButton;
            buttonpropertyBag.Accept(fillers);

            buttonpropertyBag.ErrorMessage = errMsg;
            buttonpropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;

            var buttonHTMLEmitter = new ButtonHTMLEmitter(value != null ? value.ToString() : string.Empty, buttonpropertyBag);
            buttonHTMLEmitter.Emit(out buttonHTMLString);
            return MvcHtmlString.Create(buttonHTMLString);
        }

        public static MvcHtmlString BallyImageButton<TModel>(this HtmlHelper<TModel> htmlHelper, string controlName, string parentID, string imagePath, bool validateForm = true, StylePropertyBag style = null, short tabIndex = 0, string onClickFunction = "", bool alignLeft = false, string cssClass = "", bool? isEnabled = null, List<Security> taskCodes = null)
        {
            object value = string.Empty;
            string errMsg = string.Empty;
            string buttonHTMLString = string.Empty;
            string modelName = htmlHelper.ViewData.Model.GetType().Name;
            Dictionary<string, string> overrideSettings;

            overrideSettings = GetButtonOverrideSettings(ButtonType.Button, parentID, validateForm, string.Empty, style, tabIndex, onClickFunction, cssClass: cssClass, imagePath: imagePath);

            var fillers = ControlPropertyFillerFactory.Get();
            FillerParams fillerParams = new FillerParams(modelName, controlName, overrideSettings, isEnabled: isEnabled, alignLeft: alignLeft, userTaskCodes: taskCodes);
            var buttonpropertyBag = new ButtonPropertyBag(fillerParams);
            buttonpropertyBag.ButtonCatagory = ButtonCatagory.BallyImageButton;
            buttonpropertyBag.Accept(fillers);

            buttonpropertyBag.ErrorMessage = errMsg;
            buttonpropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;

            var buttonHTMLEmitter = new ButtonHTMLEmitter(value != null ? value.ToString() : string.Empty, buttonpropertyBag);
            buttonHTMLEmitter.Emit(out buttonHTMLString);
            return MvcHtmlString.Create(buttonHTMLString);
        }

        public static MvcHtmlString BallyLinkButton<TModel>(this HtmlHelper<TModel> htmlHelper, string controlName, string actionName = "#", StylePropertyBag style = null, short tabIndex = 0, string onClickFunction = "", string cssClass = "", bool? isEnabled = null, List<Security> taskCodes = null)
        {
            object value = string.Empty;
            string errMsg = string.Empty;
            string buttonHTMLString = string.Empty;
            string modelName = htmlHelper.ViewData.Model.GetType().Name;
            Dictionary<string, string> overrideSettings;

            overrideSettings = GetButtonOverrideSettings(ButtonType.Button, string.Empty, false, actionName, style, tabIndex, onClickFunction, cssClass: cssClass);

            var fillers = ControlPropertyFillerFactory.Get();
            FillerParams fillerParams = new FillerParams(modelName, controlName, overrideSettings, isEnabled: isEnabled, userTaskCodes: taskCodes);
            var buttonpropertyBag = new ButtonPropertyBag(fillerParams);
            buttonpropertyBag.ButtonCatagory = ButtonCatagory.BallyLinkButton;
            buttonpropertyBag.Accept(fillers);
            buttonpropertyBag.ErrorMessage = errMsg;
            buttonpropertyBag.IsDirty = string.IsNullOrEmpty(errMsg) ? false : true;

            var buttonHTMLEmitter = new ButtonHTMLEmitter(value != null ? value.ToString() : string.Empty, buttonpropertyBag);
            buttonHTMLEmitter.Emit(out buttonHTMLString);
            return MvcHtmlString.Create(buttonHTMLString);
        }

        #endregion "Public Methods"

        #region "Private Methods"

        private static Dictionary<string, string> GetButtonOverrideSettings(ButtonType buttonType, string parentID, bool validateForm, string actionName, StylePropertyBag style, short tabIndex, string onClickFunction, string cssClass = "", string imagePath = "")
        {
            Dictionary<string, string> overrideSettings;
            overrideSettings = new Dictionary<string, string>();
            overrideSettings.Add(ControlLibConstants.BUTTON_TYPE, buttonType.ToString());
            overrideSettings.Add(ControlLibConstants.PARENT_ID, parentID);
            overrideSettings.Add(ControlLibConstants.TAB_INDEX, tabIndex.ToString());
            overrideSettings.Add(ControlLibConstants.ON_CLICK_FUNCTION, onClickFunction);
            overrideSettings.Add(ControlLibConstants.VALIDATE_FORM, validateForm.ToString());
            overrideSettings.Add(ControlLibConstants.ACTION_NAME, actionName);
            overrideSettings.Add(ControlLibConstants.IMAGE_PATH, imagePath);
            overrideSettings.Add(ControlLibConstants.CSS_CLASS, cssClass);
            SetStyleSettings(style, overrideSettings);
            return overrideSettings;
        }

        #endregion "Private Methods"
    }
}