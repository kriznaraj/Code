using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class SiteConfig : ISiteConfig
    {
        #region "Constructors"

        public SiteConfig()
        {

        }

        public SiteConfig(SiteConfigType siteConfigType, string configKey)
        {
            this.SiteConfigType = siteConfigType;
            this.ConfigKey = configKey;
        }

        public SiteConfig(SerializationInfo info, StreamingContext context)
        {
            this.SiteConfigType = (SiteConfigType)info.GetValue("SiteConfigType", typeof(SiteConfigType));
            this.ConfigKey = (string)info.GetValue("ConfigKey", typeof(string));
        }

        #endregion "Constructors"

        #region "Properties"

        [XmlAttribute("SiteConfigType")]
        public SiteConfigType SiteConfigType { get; set; }

        [XmlAttribute("ConfigKey")]
        public string ConfigKey { get; set; }

        #endregion "Properties"

        #region "ISerializable"

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SiteConfigType", this.SiteConfigType, typeof(SiteConfigType));
            info.AddValue("ConfigKey", this.ConfigKey, typeof(string));
        }

        #endregion "ISerializable"
    }
}
