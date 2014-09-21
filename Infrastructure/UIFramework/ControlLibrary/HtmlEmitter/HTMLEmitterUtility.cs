using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace Controls.ControlLibrary
{
    internal class HTMLEmitterUtility
    {
        internal static string GetAutoCompleteParams(AutoCompleteBehaviourPropertyBag autoCompleteProperty)
        {
            StringBuilder json = new StringBuilder();

            if (autoCompleteProperty != null)
            {
                json.AppendFormat(@"{{""MinCharRequired"": {0}, ""MaxResultCount"": {1}, ""OrderBy"": ""{2}"", ""SearchType"": ""{3}"", ""ActionURL"": ""{4}""}}",
                    autoCompleteProperty.MinCharRequired,
                    autoCompleteProperty.MaxResultCount,
                    autoCompleteProperty.OrderBy.ToString(),
                    autoCompleteProperty.SearchType.ToString(),
                    autoCompleteProperty.ActionURL);
            }

            return json.ToString();
        }

        public static string GetGridControlProperty(GridPropertyBag gridProperty)
        {
            StringBuilder json = new StringBuilder();

            if (gridProperty != null)
            {
                json.AppendFormat(@"{{""Pagination"": {0}, ""EnableExport"": {1}, ""EnableSorting"": {2}, ""EnableFilter"": {3}, ""PageSize"": {4},""SelectOption"": {5}, ""BodyHeight"": {6}, ""DefaultSortField"": ""{7}"", ""ImageSizeProperty"": ""{8}"", ""DisplayText"": ""{9}"", ""SelectedText"": ""{10}"", ""ExportText"": ""{11}"", ""ClearText"": ""{12}"", ""StatusProperty"": ""{13}"", ""ServerPagination"": {14} }}",
                    gridProperty.GridProperties.Pagination.ToString().ToLower(),
                    gridProperty.GridProperties.EnableExport.ToString().ToLower(),
                    gridProperty.GridProperties.EnableSorting.ToString().ToLower(),
                    gridProperty.GridProperties.EnableFilter.ToString().ToLower(),
                    gridProperty.GridProperties.PageSize,
                    gridProperty.GridProperties.SelectOption.ToString().ToLower(),
                    gridProperty.GridProperties.GridHeight,
                    gridProperty.GridProperties.DefaultSortField,
                    gridProperty.GridProperties.ImageSizeProperty,
                    ControlLibraryConfig.ResourceService.GetLiteral("Grid_DisplayText"),
                    ControlLibraryConfig.ResourceService.GetLiteral("Grid_SelectedText"),
                    ControlLibraryConfig.ResourceService.GetLiteral("Grid_ExportText"),
                    ControlLibraryConfig.ResourceService.GetLiteral("Grid_ClearText"), 
                    gridProperty.GridProperties.StatusProperty, 
                    gridProperty.GridProperties.ServerPagination.ToString().ToLower());
            }

            return json.ToString();
        }

        public static string GetGridControlColumnsProperties(GridPropertyBag gridProperty)
        {
            StringBuilder json = new StringBuilder();
            foreach (DataGridColumDefinition item in gridProperty.GridDataColumnDefinition.DataGridColumnDefinition)
            {
                if (gridProperty.HiddenColumnsList == null || false == gridProperty.HiddenColumnsList.Contains(item.ColumnName))
                {
                    json.AppendFormat(@"{0},{1},{2},{3},{4},{5},{6}|",
                    item.DisplayMember,
                    item.ColumnDataType,
                    ControlLibraryConfig.ResourceService.GetLiteral(item.HeaderName),
                    item.SearchType,
                    item.Alignment,
                    item.Width, item.IsSortable.ToString().ToLower());
                }
            }
            return json.ToString();
        }

        public static string GetJsonOfObject(object obj)
        {
            string json = new JavaScriptSerializer().Serialize(obj);
            return json;
        }
    }
}