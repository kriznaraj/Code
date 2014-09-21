using Controls.Framework;
using Controls.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    /*This Class used to hold the entire session info for the paritcular user per session*/
    public class SessionContext : ISessionContext
    {
        public GUIHost HostDevice
        {
            get;
            set;
        }

        public List<object> UserSecurity
        {
            get;
            set;
        }

        public List<object> UserTransactionLimit
        {
            get;
            set;
        }

        public IUserProfile UserProfile
        {
            get;
            set;
        }

        public string SessionToken
        {
            get;
            set;
        }

        public object UserPermisssion
        {
            get { throw new NotImplementedException(); }
        }

        object ISessionContext.UserTransactionLimit
        {
            get { throw new NotImplementedException(); }
        }


        public List<UILayoutConfig> LayoutConfig
        {
            get;
            set;
        }

        public List<ISiteConfigSetting> SiteConfig
        {
            get;
            private set;
        }

        public List<IUserTask> UserTask
        {
            get;
            set;
        }

        public string HostName { get; set; }

        public string HostIP { get; set; }

        public void SetSiteConfig(List<ISiteConfigSetting> configSettings)
        {
            this.SiteConfig = configSettings;
        }
    }
}
