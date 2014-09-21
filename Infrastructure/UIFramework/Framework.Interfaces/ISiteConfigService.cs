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
    public interface ISiteConfigService
    {
        /// <summary>
        /// Gets the access policy for given key based on configuration
        /// </summary>
        /// <param name="securityCode"></param>
        /// <returns></returns>
        ISiteConfigSetting GetSiteConfig(string siteConfigKey);

    }
}
