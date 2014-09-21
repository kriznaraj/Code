using Controls.Framework.Interfaces;
using System;

namespace Controls.Framework
{
    public class SiteConfigSetting : ISiteConfigSetting
    {
        public string ConfigKey
        {
            get;
            set;
        }

        public bool ConfigValue
        {
            get;
            set;
        }
    }
}
