using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    public interface IDenomTemplateColumnDefinition
    {
        string ColumnName { get; }

        string DisplayMember { get; }

        string Width { get; }

        DenomColumnDataType ColumnDataType { get; }

        string Alignment { get; }

        string HeaderName { get; }

        bool IsEditable { get; }

        bool CalculationRequired { get; }

        string Formula { get; }

        string CalculationOn { get; }
    }
}