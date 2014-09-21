using System;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    [XmlRoot("ControlDefaultProperty")]
    public class ControlDefaultPropertyBag : IControlDefaultPropertyBag
    {
        public ControlDefaultPropertyBag()
        {
        }

        [XmlAttribute("CssClass")]
        public string CssClass { get; set; }

        //ImageButton
        [XmlAttribute("ImageButtonDisableClass")]
        public string ImageButtonDisableClass { get; set; }

        [XmlAttribute("ImageClass")]
        public string ImageClass { get; set; }

        [XmlAttribute("ImageButtonLabelDisableClass")]
        public string ImageButtonLabelDisableClass { get; set; }

        [XmlAttribute("ImageButtonLeftAlignClass")]
        public string ImageButtonLeftAlignClass { get; set; }

        [XmlAttribute("MandatoryCssClass")]
        public string MandatoryCssClass { get; set; }

        [XmlAttribute("MandatoryChar")]
        public string MandatoryChar { get; set; }

        [XmlAttribute("CurrencyFormatString")]
        public string CurrencyFormatString { get; set; }

        [XmlAttribute("ValidationErrorCssClass")]
        public string ValidationErrorCssClass { get; set; }

        [XmlAttribute("ControlErrorCssClass")]
        public string ControlErrorCssClass { get; set; }

        [XmlAttribute("ListItemTemplateCssClass")]
        public string ListItemTemplateCssClass { get; set; }

        //[XmlAttribute("ListItemMouseOverCssClass")]
        //public string ListItemMouseOverCssClass { get; set; }

        [XmlAttribute("ListItemSelectedCssClass")]
        public string ListItemSelectedCssClass { get; set; }

        [XmlAttribute("Name")]
        public ControlNames ControlName { get; set; }

        [XmlElement("Autocomplete")]
        public AutoCompleteBehaviourPropertyBag AutoCompleteProperty { get; set; }

        [XmlElement("Masking")]
        public MaskingBehaviourPropertyBag MaskingProperty { get; set; }

        [XmlElement("DateFormat")]
        public DatePropertyBag DateProperty { get; set; }

        [XmlElement("TimeFormat")]
        public TimePropertyBag TimeProperty { get; set; }

        [XmlAttribute("DenomHeaderCssClass")]
        public string DenomHeaderCssClass { get; set; }

        [XmlAttribute("DenomFooterCssClass")]
        public string DenomFooterCssClass { get; set; }		
        
    }
}