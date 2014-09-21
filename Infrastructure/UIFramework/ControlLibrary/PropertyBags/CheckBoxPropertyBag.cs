
namespace Controls.ControlLibrary
{
    internal class CheckBoxPropertyBag :ControlPropertyBag
    {
        #region "Constructors"

        public CheckBoxPropertyBag(FillerParams fillerParams)
            : base(fillerParams)
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