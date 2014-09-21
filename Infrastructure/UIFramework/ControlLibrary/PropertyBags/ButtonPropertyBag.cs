using System.Collections.Generic;
namespace Controls.ControlLibrary
{
    internal class ButtonPropertyBag : ControlPropertyBag
    {
        #region "Constructors"

        public ButtonPropertyBag(FillerParams fillerParams)
            : base(fillerParams)
        {
        }

        #endregion "Constructors"

        #region "Properties"

        public ButtonType ButtonType { get; set; }

        public string OnClickFunction { get; set; }

        public bool AjaxButton { get; set; }

        public bool ValidateForm { get; set; }

        public string ParentID { get; set; }

        public ButtonCatagory ButtonCatagory { get; set; }

        public string ActionName { get; set; }

        public string ImagePath { get; set; }

        public bool AlignLeft { get; set; }

        public string ImageButtonDisableClass { get; set; }

        public string ImageClass { get; set; }

        public string ImageButtonLabelDisableClass { get; set; }

        public string ImageButtonLeftAlignClass { get; set; }

        public List<Security> TaskCodes { get; set; }

        #endregion "Properties"

        #region "Override Methods"

        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, _fillerParams);
        }

        #endregion "Override Methods"
    }
}