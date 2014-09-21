using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    public interface IModelPropertyConfiguration
    {
        string Key { get; }

        string Name { get; }

        string ExternalizationKey { get; }

        bool IsComplexType { get; }

        bool IsEnumerable { get; }

        IPropertyConfiguration PropertyConfiguration { get; }
    }
}