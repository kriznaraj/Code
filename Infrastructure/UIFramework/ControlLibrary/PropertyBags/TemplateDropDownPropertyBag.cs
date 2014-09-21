
namespace Controls.ControlLibrary
{
    internal class TemplateDropDownPropertyBag :ControlPropertyBag
    {
        #region "Constructors"

        public TemplateDropDownPropertyBag(FillerParams fillerParam)
            : base(fillerParam)
        {
        }


        #endregion

        public string TemplateNameKey { get; set; }

        public string TemplateHTML { get; set; }

        public int ListDisplayLength { get; set; }

        public string ListItem { get; set; }

        public string OnChangeFunction { get; set; }

        public string ListTemplateCssClass { get; set; }

        #region "Override Methods"

        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, _fillerParams);
        }

        #endregion        
    }   
}