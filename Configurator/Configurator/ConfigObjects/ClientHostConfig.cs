using BallyTech.Infrastructure.Hosting;
using BallyTech.UI.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configurator
{
    [Serializable]
    public class ClientHost
    {
        [XmlAttribute("ConfigName")]
        public string ConfigName
        { get; set; }

        [XmlElement("HostConfig")]
        public HostConfig HostConfig
        {
            get;
            set;
        }
    }

    [Serializable]
    public class ClientHostConfig : ISerializable
    {
        [XmlElement("ClientHost")]
        public List<ClientHost> Clients
        {
            get;
            set;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}