using System;

namespace Controls.ControlLibrary
{
    internal class DropDownPropertyBag : ControlPropertyBag
    {
        #region "Constructors"

        public DropDownPropertyBag(FillerParams fillerParam)
            : base(fillerParam)
        {
            this._fillerParams = fillerParam;
        }

        #endregion

        #region "Implemented Properties - IDropDownPropertyBag"

        public DropDownType DropDownType { get; set; }

        public string TargetControlID { get; set; }

        public string ActionURL { get; set; }

        public string Options { get; set; }

        public string OnChangeFunction { get; set; }

        public string CascadeInputFunction { get; set; }

        public int ListDisplayLength { get; set; }

        public string ValueMember { get; set; }

        public string DisplayMember { get; set; }

        #endregion

        #region "Methods"

        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, _fillerParams);
        }

        #endregion
    }

   
}