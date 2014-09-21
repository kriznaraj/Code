using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.ControlLibrary
{
    public interface ISiteConfig
    {
        #region "Properties"

        /// <summary>
        /// Specifies the type of access the validator should handle
        /// </summary>
        SiteConfigType SiteConfigType { get; }

        /// <summary>
        /// 
        /// </summary>
        string ConfigKey { get; }

        #endregion
    }
}