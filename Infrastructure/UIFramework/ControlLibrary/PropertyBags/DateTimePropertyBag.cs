
using System;
namespace Controls.ControlLibrary
{
    internal class DateTimePropertyBag :ControlPropertyBag
    {
        #region "Constructors"

        public DateTimePropertyBag(FillerParams fillerParam)
            : base(fillerParam)
        {
            
        }

        #endregion

        #region "Implemented Properties - IDateTimePropertyBag"

        public bool ShowDate { get; set; }

        public bool ShowTime { get; set; }

        public DatePropertyBag DateProperties { get; set; }

        public TimePropertyBag TimeProperties { get; set; }

        public string OnChangeFunction { get; set; }

        public DateTimeKind DateTimeKind { get; set; }

        public bool UTCConversion { get; set; }

        #endregion

        #region "Override Methods"

        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, _fillerParams);
        }

        #endregion

        
    }

   
}