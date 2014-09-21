using Controls.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Controls.Framework
{
    public class SiteConfigService : ISiteConfigService
    {
        /// <summary>
        /// 
        /// </summary>
        private List<ISiteConfigSetting> siteConfigSettingsList = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteConfigSettings"></param>
        public SiteConfigService(List<ISiteConfigSetting> siteConfigSettings)
        {
            siteConfigSettingsList = siteConfigSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityCode"></param>
        /// <returns></returns>
        public ISiteConfigSetting GetSiteConfig(string siteConfigKey)
        {
            if (siteConfigSettingsList != null && siteConfigSettingsList.Count > 0)
            {
                return siteConfigSettingsList.Where(o => o.ConfigKey == siteConfigKey).FirstOrDefault();
            }

            return null;
        }
    }
}
