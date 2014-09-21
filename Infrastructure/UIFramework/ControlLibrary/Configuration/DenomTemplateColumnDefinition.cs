using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class DenomTemplateColumnDefinition : IDenomTemplateColumnDefinition, ISerializable
    {
        #region "Constructors"

        public DenomTemplateColumnDefinition()
        {

        }

        public DenomTemplateColumnDefinition(string columnName, string displayMember, string headerName, DenomColumnDataType columnDataType = DenomColumnDataType.Text, string alignment = "left", string width = "auto", bool isEditable = false, bool calculationRequired = false, string formula = "", string calculationOn = "", bool totalRequired = false, string footerText = "", bool isReadOnly = false, bool isVisible = true, bool allowDecimal = false, bool allowNegativeValue = false, bool spinnerRequired = false, string seed = "1", int decimalPlaces = 0, bool currencySymbolRequired = false, CurrencySymbolPositionType currencySymbolPosition= CurrencySymbolPositionType.Header, BindingMode bindingType= BindingMode.OneWay)
        {
            this.ColumnName = columnName;
            this.DisplayMember = displayMember;
            this.Width = width;
            this.ColumnDataType = columnDataType;
            this.Alignment = alignment;
            this.HeaderName = headerName;
            this.IsEditable = isEditable;
            this.CalculationRequired = calculationRequired;
            this.Formula = formula;
            this.CalculationOn= calculationOn;
            this.TotalRequired = totalRequired;
            this.FooterText = footerText;
            this.IsReadOnly = isReadOnly;
            this.IsVisible = isVisible;
            this.AllowDecimal = allowDecimal;
            this.AllowNegativeValue = allowNegativeValue;
            this.SpinnerRequired = spinnerRequired;
            this.Seed = seed;
            this.DecimalPlaces = decimalPlaces;
            this.CurrencySymbolRequired = currencySymbolRequired;
            this.CurrencySymbolPosition = currencySymbolPosition;
            this.BindingType = bindingType;
        }

        public DenomTemplateColumnDefinition(SerializationInfo info, StreamingContext context)
        {

            this.ColumnName = (string)info.GetValue(ControlLibConstants.COLUMN_NAME, typeof(string));
            this.DisplayMember = (string)info.GetValue(ControlLibConstants.DISPLAY_MEMBER, typeof(string));
            this.Width = (string)info.GetValue(ControlLibConstants.WIDTH, typeof(string));
            this.ColumnDataType = (DenomColumnDataType)info.GetValue(ControlLibConstants.COLUMN_DATA_TYPE, typeof(DenomColumnDataType));
            this.Alignment = (string)info.GetValue(ControlLibConstants.ALIGHNMENT, typeof(string));
            this.HeaderName = (string)info.GetValue(ControlLibConstants.HEADER_NAME, typeof(string));
            this.IsEditable = (bool)info.GetValue(ControlLibConstants.DENOM_ISEDITABLE, typeof(bool));
            this.CalculationRequired = (bool)info.GetValue(ControlLibConstants.DENOM_CALCULATION_REQUIRED, typeof(bool));
            this.Formula = (string)info.GetValue(ControlLibConstants.DENOM_FORMULA, typeof(string));
            this.CalculationOn = (string)info.GetValue(ControlLibConstants.DENOM_CALCULATION_ON, typeof(string));
            this.TotalRequired = (bool)info.GetValue(ControlLibConstants.DENOM_TOTAL_REQUIRED, typeof(bool));
            this.FooterText = (string)info.GetValue(ControlLibConstants.DENOM_FOOTERTEXT, typeof(string));
            this.IsReadOnly = (bool)info.GetValue(ControlLibConstants.DENOM_ISREADONLY, typeof(bool));
            this.IsVisible = (bool)info.GetValue(ControlLibConstants.DENOM_ISVISIBLE, typeof(bool));
            this.AllowDecimal = (bool)info.GetValue(ControlLibConstants.DENOM_ALLOW_DECIMAL, typeof(bool));
            this.AllowNegativeValue = (bool)info.GetValue(ControlLibConstants.DENOM_ALLOW_NEGATIVE, typeof(bool));
            this.SpinnerRequired = (bool)info.GetValue(ControlLibConstants.DENOM_SPINNER_REQUIRED, typeof(bool));
            this.Seed = (string)info.GetValue(ControlLibConstants.DENOM_SEED, typeof(string));
            this.DecimalPlaces = (int)info.GetValue(ControlLibConstants.DENOM_DECIMALPLACES, typeof(int));

            this.CurrencySymbolRequired = (bool)info.GetValue(ControlLibConstants.DENOM_CURRENCYSYMBOL_REQUIRED, typeof(bool));
            this.CurrencySymbolPosition = (CurrencySymbolPositionType)info.GetValue(ControlLibConstants.DENOM_CURRENCYSYMBOL_POSITION, typeof(CurrencySymbolPositionType));
            this.BindingType = (BindingMode)info.GetValue(ControlLibConstants.DENOM_BINDING_TYPE, typeof(BindingMode));
            
        }

        #endregion

        #region "Properties"

        [XmlAttribute("ColumnName")]
        public string ColumnName { get; set; }

        [XmlAttribute("DisplayMember")]
        public string DisplayMember { get; set; }

        [XmlAttribute("Width")]
        public string Width { get; set; }

        [XmlAttribute("ColumnDataType")]
        public DenomColumnDataType ColumnDataType { get; set; }

        [XmlAttribute("Alignment")]
        public string Alignment { get; set; }

        [XmlAttribute("HeaderName")]
        public string HeaderName { get; set; }

        [XmlAttribute("IsEditable")]
        public bool IsEditable { get; set; }

        [XmlAttribute("CalculationRequired")]
        public bool CalculationRequired { get; set; }

        [XmlAttribute("Formula")]
        public string Formula { get; set; }

        [XmlAttribute("CalculationOn")]
        public string CalculationOn { get; set; }

        [XmlAttribute("TotalRequired")]
        public bool TotalRequired { get; set; }

        [XmlAttribute("FooterText")]
        public string FooterText { get; set; }

        [XmlAttribute("IsReadOnly")]
        public bool IsReadOnly { get; set; }

        [XmlAttribute("IsVisible")]
        public bool IsVisible { get; set; }

        [XmlAttribute("AllowDecimal")]
        public bool AllowDecimal { get; set; }

        [XmlAttribute("AllowNegativeValue")]
        public bool AllowNegativeValue { get; set; }

        [XmlAttribute("SpinnerRequired")]
        public bool SpinnerRequired { get; set; }

        [XmlAttribute("Seed")]
        public string Seed { get; set; }

        [XmlAttribute("DecimalPlaces")]
        public int DecimalPlaces { get; set; }

        [XmlAttribute("CurrencySymbolRequired")]
        public bool CurrencySymbolRequired { get; set; }

        [XmlAttribute("CurrencySymbolPosition")]
        public CurrencySymbolPositionType CurrencySymbolPosition { get; set; }

        [XmlAttribute("BindingType")]
        public BindingMode BindingType { get; set; }

        #endregion

        #region "ISerializable"

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ControlLibConstants.COLUMN_NAME, this.ColumnName, typeof(string));
            info.AddValue(ControlLibConstants.DISPLAY_MEMBER, this.DisplayMember, typeof(string));
            info.AddValue(ControlLibConstants.WIDTH, this.Width, typeof(string));
            info.AddValue(ControlLibConstants.COLUMN_DATA_TYPE, this.ColumnDataType, typeof(DenomColumnDataType));
            info.AddValue(ControlLibConstants.ALIGHNMENT, this.Alignment, typeof(string));
            info.AddValue(ControlLibConstants.HEADER_NAME, this.HeaderName, typeof(string));
            info.AddValue(ControlLibConstants.DENOM_ISEDITABLE, this.IsEditable, typeof(bool));
            info.AddValue(ControlLibConstants.DENOM_CALCULATION_REQUIRED, this.CalculationRequired, typeof(bool));
            info.AddValue(ControlLibConstants.DENOM_FORMULA, this.Formula, typeof(string));
            info.AddValue(ControlLibConstants.DENOM_CALCULATION_ON, this.CalculationOn, typeof(string));
            info.AddValue(ControlLibConstants.DENOM_TOTAL_REQUIRED, this.TotalRequired, typeof(bool));
            info.AddValue(ControlLibConstants.DENOM_FOOTERTEXT, this.FooterText, typeof(string));
            info.AddValue(ControlLibConstants.DENOM_ISREADONLY, this.IsReadOnly, typeof(bool));
            info.AddValue(ControlLibConstants.DENOM_ISVISIBLE, this.IsVisible, typeof(bool));
            info.AddValue(ControlLibConstants.DENOM_ALLOW_DECIMAL, this.AllowDecimal, typeof(bool));
            info.AddValue(ControlLibConstants.DENOM_ALLOW_NEGATIVE, this.AllowNegativeValue, typeof(bool));
            info.AddValue(ControlLibConstants.DENOM_SPINNER_REQUIRED, this.SpinnerRequired, typeof(bool));
            info.AddValue(ControlLibConstants.DENOM_SEED, this.Seed, typeof(string));
            info.AddValue(ControlLibConstants.DENOM_DECIMALPLACES, this.DecimalPlaces, typeof(int));

            info.AddValue(ControlLibConstants.DENOM_CURRENCYSYMBOL_REQUIRED, this.CurrencySymbolRequired, typeof(bool));
            info.AddValue(ControlLibConstants.DENOM_CURRENCYSYMBOL_POSITION, this.CurrencySymbolPosition, typeof(CurrencySymbolPositionType));
            info.AddValue(ControlLibConstants.DENOM_BINDING_TYPE, this.BindingType, typeof(BindingMode));
            
        }

        #endregion
    }    
}
