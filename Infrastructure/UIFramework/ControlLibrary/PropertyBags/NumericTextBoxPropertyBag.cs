
namespace Controls.ControlLibrary
{
    internal class NumericTextBoxPropertyBag :ControlPropertyBag
    {
        #region "Constructors"

        public NumericTextBoxPropertyBag(FillerParams fillerParams)
            : base(fillerParams)
        {
            
        }

        #endregion

        #region "Properties"

        public bool IsCurrency { get; set; }

        public string CurrencyFormatString { get; set; }

        public string OnLeaveFunction { get; set; }

        public string OnKeyUpFunction { get; set; }

        public string OnKeyDownFunction { get; set; }

        public string OnChangeFunction { get; set; }

        public string AutoCompleteInputFunction { get; set; }

        public string EncryptedValue { get; set; }

        public bool Masking { get; set; }

        public bool AutoComplete { get; set; }

        public AutoCompleteBehaviourPropertyBag AutoCompleteProperties { get; set; }

        public MaskingBehaviourPropertyBag MaskingProperties { get; set; }

        #endregion

        #region "Override Methods"

        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, _fillerParams);
        }

        #endregion        
    }
}