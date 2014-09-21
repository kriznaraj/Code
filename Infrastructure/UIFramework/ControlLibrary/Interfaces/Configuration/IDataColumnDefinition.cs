using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    public interface IDataColumnDefinition
    {
        string ColumnName { get; }

        string DisplayMember { get; }

        string Width { get; }

        GridColumnDataType ColumnDataType { get; }

        string Alignment { get; }

        string HeaderName { get; }

        GridFilterType SearchType { get; }

        string AccessPolicyCode { get; }
    }
}