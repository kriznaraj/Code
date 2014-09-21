using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    public interface IDenomTemplates
    {
        string TemplateName { get; }

        List<DenomTemplateColumnDefinition> DenomTemplateColumnDefinition { get; }

        IDictionary<string, DenomTemplateColumnDefinition> IndexedDenomTemplateColumnDefinition { get; }
    }
}