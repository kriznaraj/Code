using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Controls.Printing
{
    [Serializable]
    public class PrintManagerChannelConfig : ISerializable
    {
        public PrintManagerChannelConfig(string key, string bindingType, string address)
        {
            this.Key = key;
            this.Address = new EndpointAddress(address);
            this.Binding = PrintManagerChannelConfig.GetBinding(bindingType);
        }

        private PrintManagerChannelConfig()
        {
        }

        [XmlIgnore]
        public EndpointAddress Address { get; set; }

        [XmlElement(ElementName = "Address")]
        public string AddressString
        {
            get
            {
                return this.Address.Uri.AbsoluteUri;
            }
            set
            {
                this.Address = new EndpointAddress(value);
            }
        }

        [XmlIgnore]
        public Binding Binding { get; set; }

        [XmlElement(ElementName = "Binding")]
        public string BindingString
        {
            get
            {
                return this.Binding.Name;
            }
            set
            {
                this.Binding = GetBinding(value);
            }
        }

        [XmlAttribute]
        public string Key { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Key", this.Key);
            info.AddValue("Address", this.Address.Uri.AbsoluteUri);
            info.AddValue("Binding", this.Binding.Name);
        }

        private static Binding GetBinding(string bindingType)
        {
            switch (bindingType.ToUpperInvariant())
            {
                case "NET.TCP":
                    return new NetTcpBinding() { Name = "NET.TCP" };

                case "WSHTTP":
                    return new WSHttpBinding() { Name = "WSHTTP" };

                default:
                    throw new NotSupportedException();
            }
        }
    }
}