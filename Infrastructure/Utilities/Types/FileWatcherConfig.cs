using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Controls.Types
{
    [Serializable]
    public class FileWatcherConfig
    {
        [XmlElement("Directory")]
        public string Directory { get; set; }

        [XmlElement("FileFilter")]
        public string FileFilters { get; set; }

        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlIgnore]
        public NotifyFilters NotificationFilter { get; set; }

        [XmlElement("NotifyFilter")]
        public int NotifyFilter
        {
            get
            {
                return (int)this.NotificationFilter;
            }
            set
            {
                this.NotificationFilter = (NotifyFilters)value;
            }
        }
    }
}