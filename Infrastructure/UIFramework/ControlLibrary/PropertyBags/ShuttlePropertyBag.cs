using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    internal class ShuttlePropertyBag : ControlPropertyBag
    {
        #region "Constructors"

        public ShuttlePropertyBag(FillerParams fillerParam)
            : base(fillerParam)
        {
            
        }

        #endregion

        #region "Implemented Properties - ICheckListPropertyBag"

        public int ListDisplayLength { get; set; }

        public string ListItem { get; set; }

        public string ValueMember { get; set; }

        public string ActionUrl { get; set; }

        public string DisplayMember { get; set; }

        public string OnChangeFunction { get; set; }

        public IDictionary<string, object> ShuttleParam { get; set; }

        #endregion

        #region "Methods"
        
        
        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, _fillerParams);
        }

        #endregion
    }  
}