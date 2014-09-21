using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Controls.Printing
{
    [Serializable]
    public class PrintServiceConfig : ISerializable
    {
        [XmlArray("ChannelConfig")]
        public List<PrintManagerChannelConfig> ChannelConfig { get; set; }

        [XmlElement]
        public string ListenUri { get; set; }

        [XmlArray("PrinterConfig")]
        public List<PrintManagerConfig> PrinterConfig { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ChannelConfig", this.ChannelConfig);
            info.AddValue("ListenUri", this.ListenUri);
            info.AddValue("PrinterConfig", this.PrinterConfig);
        }
    }
}