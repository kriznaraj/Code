using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class ModelConfiguration : IModelConfiguration, ISerializable
    {
        /*This class will hold all the Name and the Collection of ModelPropertyConfiguration*/
        private IDictionary<string, ModelPropertyConfiguration> _propertyList;

        #region "Constructors"

        public ModelConfiguration()
        {
            
        }

        public ModelConfiguration(string name, List<ModelPropertyConfiguration> propertyList, string configKey = "")
        {
            this.PropertyConfiguration = propertyList;
            this.Name = name;
            this.ConfigKey = configKey;
        }

        public ModelConfiguration(SerializationInfo info, StreamingContext context)
        {
            this.Name = (string)info.GetValue(ControlLibConstants.NAME, typeof(string));
            this.PropertyConfiguration = (List<ModelPropertyConfiguration>)info.GetValue(ControlLibConstants.PROPERTY_CONFIGURATION, typeof(List<ModelPropertyConfiguration>));
            this.ConfigKey = (string)info.GetValue(ControlLibConstants.CONFIG_KEY, typeof(string));
        }

        #endregion

        #region "Properties"

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlIgnore]
        public string LookUpKey
        {
            get
            {
                return string.IsNullOrEmpty(ConfigKey) ? Name : string.Format("{0}.{1}", Name, ConfigKey);
            }
        }

        [XmlAttribute("ConfigKey")]
        public string ConfigKey { get; set; }

        [XmlElement("property")]
        public List<ModelPropertyConfiguration> PropertyConfiguration {get; set;}

        [XmlIgnore]
        public IDictionary<string, ModelPropertyConfiguration> IndexedPropertyConfiguration
        {
            get
            {
                if (_propertyList == null)
                {
                    _propertyList = new Dictionary<string, ModelPropertyConfiguration>();

                    foreach (var item in PropertyConfiguration)
                    {
                        _propertyList.Add(item.Name, item);
                    }
                }

                return _propertyList;
            }            
        }

        #endregion

        #region "ISerializable"


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ControlLibConstants.NAME, this.Name, typeof(string));
            info.AddValue(ControlLibConstants.PROPERTY_CONFIGURATION, this.PropertyConfiguration, typeof(List<ModelPropertyConfiguration>));
            info.AddValue(ControlLibConstants.CONFIG_KEY, this.ConfigKey, typeof(string));
        }

        #endregion
    }    
}