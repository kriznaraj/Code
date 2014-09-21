
namespace Controls.ControlLibrary
{
    internal class LabelPropertyBag : ControlPropertyBag
    {
        #region "Constructors"

        public LabelPropertyBag(FillerParams fillerParams)
            : base(fillerParams)
        {            
            
        }

        #endregion

        #region "Properties"

        public bool IsMandatory { get; set; }

        public string MandatoryCharCssClass { get; set; }

        public string MandatoryChar { get; set; }

        public DisplayType DisplayType { get; set; }

        public bool Masking { get; set; }

        public bool IsCurrency { get; set; }

        public string CurrencySymbol { get; set; }
        
        public MaskingBehaviourPropertyBag MaskingProperties { get; set; }

        public string OverrideToolTip { get; set; }

        #endregion    
        
        #region "Override Methods"

        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, _fillerParams);
        }

        #endregion
    }
}