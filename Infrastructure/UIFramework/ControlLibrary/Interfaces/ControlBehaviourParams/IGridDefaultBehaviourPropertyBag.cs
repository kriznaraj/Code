
namespace Controls.ControlLibrary
{
    public interface IGridDefaultBehaviourPropertyBag
    {
        int PageSize { get; set; }

        int GridHeight { get; set; }

        bool EnableFilter { get; set; }

        bool EnableSorting { get; set; }

        bool EnableExport { get; set; }

        bool ServerPagination { get; set; }

        bool SelectOption { get; set; }

    }
}
