using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    public interface IDataGridDefinitions
    {
        string GridName { get; }

        List<DataGridColumDefinition> DataGridColumnDefinition { get; }

        IDictionary<string, DataGridColumDefinition> IndexedDataColumnDefinition { get; }
    }
}