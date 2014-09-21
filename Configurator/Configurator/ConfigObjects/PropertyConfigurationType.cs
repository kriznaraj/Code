using Controls.ControlLibrary;
using Controls.Framework;
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
    [XmlRoot("ModelProperties")]
    public class PropertyConfigurationType : ISerializable
    {
        [XmlElement("Property")]
        public List<PropertyConfiguration> PropertyConfiguration
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
