using Controls.ControlLibrary;
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
    [XmlRoot("ControlTemplateConfiguration")]
    public class ControlTemplateConfigurationType : ISerializable
    {
        [XmlElement("Template")]
        public List<ControlTemplateConfiguration> ControlTemplateConfiguration
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
