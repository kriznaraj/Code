
namespace Controls.ControlLibrary
{
    internal class PasswordBoxPropertyBag :ControlPropertyBag
    {
        #region "Constructors"

        public PasswordBoxPropertyBag(FillerParams fillerParams)
            : base(fillerParams)
        {
            
        }

        #endregion

        #region "Implemented Properties - IPasswordBoxPropertyBag"

        public string OnLeaveFunction { get; set; }

        public string OnKeyUpFunction { get; set; }

        public string OnKeyDownFunction { get; set; }

        public string OnChangeFunction { get; set; }

        #endregion

        #region "Override Methods"

        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, _fillerParams);
        }

        #endregion
    }

   
}