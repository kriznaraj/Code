using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISiteConfigSetting
    {
        string ConfigKey
        {
            get;
            set;
        }

        bool ConfigValue
        {
            get;
            set;
        }

        
    }
}
