using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    /*This Class used by the UI Framework to set the Security Token /Other Info the Session Context.*/
    public class UserContext
    {
        //Server Token
        public string SecurityToken
        {
            get;
            set;
        }

        public System.Globalization.CultureInfo UserCultureInfo
        {
            get;
            set;
        }

        public GUIHost UserHostDevice { get; set; }

        public string HostName { get; set; }

        public string HostIP { get; set; }
    }
}
