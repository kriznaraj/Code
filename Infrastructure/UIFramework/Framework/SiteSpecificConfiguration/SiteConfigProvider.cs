using Controls.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    public class SiteConfigProvider : ISiteConfigProvider
    {
        public ISiteConfigService Current
        {
            get
            {
                return new SiteConfigService(SessionStore.Get<SessionContext>("SessionContext") != null ? SessionStore.Get<SessionContext>("SessionContext").SiteConfig : null);

                //List<ISiteConfigSetting> configList = new List<ISiteConfigSetting>();
                //configList.Add(new SiteConfigSetting { ConfigKey = "AssetNumber_REQUIRED", ConfigValue = true });
                //configList.Add(new SiteConfigSetting { ConfigKey = "AssetNumber_VISIBILITY", ConfigValue = true });
                //configList.Add(new SiteConfigSetting { ConfigKey = "HandPayAmount_REQUIRED", ConfigValue = true });
                //configList.Add(new SiteConfigSetting { ConfigKey = "HandPayAmount_VISIBILITY", ConfigValue = true });

                //return new SiteConfigService(configList);
            }
        }
    }
}
