namespace Controls.ControlLibrary
{
    public interface IControlDefaultPropertyBag
    {
        #region "Properties"

        string CssClass { get; set; }

        string MandatoryCssClass { get; set; }

        string MandatoryChar { get; set; }

        string CurrencyFormatString { get; set; }

        string ValidationErrorCssClass { get; set; }

        string ControlErrorCssClass { get; set; }

        string ListItemTemplateCssClass { get; set; }

        //string ListItemMouseOverCssClass { get; set; }
        string ListItemSelectedCssClass { get; set; }

        ControlNames ControlName { get; }

        AutoCompleteBehaviourPropertyBag AutoCompleteProperty { get; set; }

        MaskingBehaviourPropertyBag MaskingProperty { get; set; }

        DatePropertyBag DateProperty { get; set; }

        TimePropertyBag TimeProperty { get; set; }

        //GridBehaviourPropertyBag GridDefaultProperties { get; set; }

        //ImageButton
        string ImageButtonDisableClass { get; set; }

        string ImageClass { get; set; }

        string ImageButtonLabelDisableClass { get; set; }

        string ImageButtonLeftAlignClass { get; set; }

	string DenomHeaderCssClass { get; set; }

        string DenomFooterCssClass { get; set; }

        #endregion "Properties"
    }
}