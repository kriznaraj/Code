
namespace Controls.ControlLibrary
{
    internal class TemplateListPropertyBag :ControlPropertyBag
    {
        #region "Constructors"

        public TemplateListPropertyBag(FillerParams fillerParam)
            : base(fillerParam)
        {
        }


        #endregion

        #region "Implemented properties ITemplateListPropertyBag"

        public string TemplateNameKey { get; set; }

        public string TemplateHTML { get; set; }

        //public ListBoxType ListBoxType { get; set; }

        public int ListDisplayLength { get; set; }

        public string ListItem { get; set; }

        public string OnClickFunction { get; set; }

        public string ListItemTemplateCssClass { get; set; }

        //public string ListItemMouseOverCssClass { get; set; }

        public string ListItemSelectedCssClass { get; set; }

        #endregion

        #region "Override Methods"

        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, _fillerParams);
        }

        #endregion        
    }   
}