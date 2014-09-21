
namespace Controls.ControlLibrary
{
    internal class RadioButtonPropertyBag :ControlPropertyBag
    {

        #region "Constructors"

        public RadioButtonPropertyBag(FillerParams fillerParams)
            : base( fillerParams)
        {
            
        }

        #endregion

        #region Implemented Properties

        public string OnClickFunction { get; set; }
        
        #endregion

        #region "Override Methods"

        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, _fillerParams);
        }

        #endregion
        
    }
}