
using System.Collections.Generic;
namespace Controls.ControlLibrary
{
    internal class DenomControlPropertyBag :ControlPropertyBag
    {
        #region "Constructors"

        public DenomControlPropertyBag(FillerParams fillerParam)
            : base(fillerParam)
        {
        }


        #endregion

        public bool GrantTotalRequired { get; set; }

        public string TemplateNameKey { get; set; }

        public string OnRowSelectFunction { get; set; }

        public string HeaderCssClass { get; set; }

        public string FooterCssClass { get; set; }

        public bool OtherAmountRequired { get; set; }

        public DenomTemplates DenomTemplate { get; set; }

        public string OtherAmountLabelKey { get; set; }

        //public string PrimaryIdMember { get; set; }

        public string ActionUrl { get; set; }

        public bool IsViewMode { get; set; }

        public string ValidationMessageKey { get; set; }

        public string UndefinedRowReadonlyColumns { get; set; }

        public string UndefinedRowEditableColumns { get; set; }

        public bool MovementIndicatorRequired { get; set; }

        public string MovementIndicatorColumn { get; set; }

        public PositionType OtherAmountPosition { get; set; }

        public DenomModeType DenomMode { get; set; }

        public IDictionary<string, object> TenderInfoParam { get; set; }

        #region "Override Methods"

        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, _fillerParams);
        }

        #endregion        
    }   
}