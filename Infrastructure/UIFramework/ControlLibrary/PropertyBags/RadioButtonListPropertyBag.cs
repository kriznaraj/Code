
namespace Controls.ControlLibrary
{
    internal class RadioButtonListPropertyBag : ControlPropertyBag
    {
        #region "Constructors"
        private FillerParams fillerParams;

        public RadioButtonListPropertyBag(FillerParams fillerParam)
            : base(fillerParam)
        {
            this.fillerParams = fillerParam;
        }

        #endregion

        #region "Implemented Properties - IRadioButtonPropertyBag"

        public int ListDisplayLength { get; set; }

        public string ListItem { get; set; }

        public bool IsVerticalAllign { get; set; }

        public string OnClickFunction { get; set; }

        #endregion

        #region "Methods"

        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, fillerParams);
        }

        #endregion        
    }   
}