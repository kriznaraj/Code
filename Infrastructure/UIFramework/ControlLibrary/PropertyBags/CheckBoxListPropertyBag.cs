
namespace Controls.ControlLibrary
{
    internal class CheckBoxListPropertyBag : ControlPropertyBag
    {
        #region "Constructors"

        public CheckBoxListPropertyBag(FillerParams fillerParam)
            : base(fillerParam)
        {
        }

        #endregion

        #region "Implemented Properties - ICheckListPropertyBag"

        public int ListDisplayLength { get; set; }

        public string ListItem { get; set; }

        public bool IsVerticalAllign { get; set; }

        public string OnClickFunction { get; set; }

        #endregion

        #region "Methods"
        
        
        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, _fillerParams);
        }

        #endregion

    }

   
}