
namespace Controls.ControlLibrary
{
    internal class TextBoxPropertyBag :ControlPropertyBag
    {
        #region "Constructors"

        public TextBoxPropertyBag(FillerParams fillerParams)
            : base(fillerParams)
        {
            
        }

        #endregion

        #region "Properties"

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