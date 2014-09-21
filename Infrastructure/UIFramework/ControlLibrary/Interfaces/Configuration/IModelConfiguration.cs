using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    public interface IModelConfiguration
    {
        string Name { get; }

        List<ModelPropertyConfiguration> PropertyConfiguration { get; }

        IDictionary<string, ModelPropertyConfiguration> IndexedPropertyConfiguration { get; }
    }
}