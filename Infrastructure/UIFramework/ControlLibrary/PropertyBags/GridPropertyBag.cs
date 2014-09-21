using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    internal class GridPropertyBag : ControlPropertyBag
    {
        #region "Constructors"

        public GridPropertyBag(FillerParams fillerParam)
            : base(fillerParam)
        {
        }

        #endregion "Constructors"

        #region "Properties"

        public GridBehaviourPropertyBag GridProperties { get; set; }

        public int TotalItemsCount { get; set; }

        public string OnDataRowSelectFunction { get; set; }

        public string OnDataRowSelectionChangeFunctionName { get; set; }

        public string GridDataColumnDefinitionName { get; set; }

        public DataGridDefinitions GridDataColumnDefinition { get; set; }

        public List<string> HiddenColumnsList { get; set; }

        public string ValueMember { get; set; }

        public string ActionUrl { get; set; }

        public IDictionary<string, object> GridParam { get; set; }

        #endregion "Properties"

        #region "Override Methods"

        internal override void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this, _fillerParams);
        }

        #endregion "Override Methods"
    }

    internal class GridBehaviourPropertyBag
    {
        public int PageSize { get; set; }

        public int GridHeight { get; set; }

        public bool EnableFilter { get; set; }

        public bool EnableSorting { get; set; }

        public bool EnableExport { get; set; }

        public bool ServerPagination { get; set; }

        public bool Pagination { get; set; }

        public bool SelectOption { get; set; }

        public string DefaultSortField { get; set; }

        public string ImageSizeProperty { get; set; }

        public string StatusProperty { get; set; }

        public GridBehaviourPropertyBag(int PageSize, int GridHeight, bool EnableFilter, bool EnableSorting, bool EnableExport, bool Pagination, bool ServerPagination, bool SelectOption, string defaultSortField, string imageSizeProperty, string statusProperty)
        {
            this.PageSize = PageSize;
            this.GridHeight = GridHeight;
            this.EnableFilter = EnableFilter;
            this.EnableSorting = EnableSorting;
            this.EnableExport = EnableExport;
            this.Pagination = Pagination;
            this.ServerPagination = ServerPagination;
            this.SelectOption = SelectOption;
            this.DefaultSortField = defaultSortField;
            this.ImageSizeProperty = imageSizeProperty;
            this.StatusProperty = statusProperty;
        }
    }
}