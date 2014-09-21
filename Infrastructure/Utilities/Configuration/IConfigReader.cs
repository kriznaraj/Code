using System;
using System.Configuration;

namespace Controls.Configuration
{
    public interface IConfigReader
    {
        TSection GetSection<TSection>(String name) where TSection : ConfigurationSection;
    }
}