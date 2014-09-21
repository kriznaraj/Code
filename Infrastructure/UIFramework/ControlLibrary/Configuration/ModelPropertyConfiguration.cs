using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    /*This class will hold all the Key and the PropertyConfiguration*/
    [Serializable]
    public class ModelPropertyConfiguration : IModelPropertyConfiguration, ISerializable
    {

        #region "Properties"

        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("ExternalizationKey")]
        public string ExternalizationKey { get; set; }

        [XmlAttribute("IsComplexType")]
        public bool IsComplexType { get; set; }

        [XmlAttribute("IsEnumerable")]
        public bool IsEnumerable { get; set; }

        [XmlAttribute("ComplexTypeName")]
        public string ComplexTypeName { get; set; }

        [XmlIgnore]
        public IPropertyConfiguration PropertyConfiguration { get; set; }

        #endregion

        #region "Constuctors"

        public ModelPropertyConfiguration()
        {

        }

        public ModelPropertyConfiguration(string key, string propertyName, string externalizationKey, IPropertyConfiguration propertyConfiguration, bool isComplexType, bool isEnumerable, string complexTypeName)
        {
            this.Key = key;
            this.Name = propertyName;
            this.ExternalizationKey = externalizationKey;
            this.PropertyConfiguration = propertyConfiguration;
            this.IsComplexType = isComplexType;
            this.IsEnumerable = isEnumerable;
            this.ComplexTypeName = complexTypeName;
        }

        public ModelPropertyConfiguration(SerializationInfo info, StreamingContext context)
        {
            this.Key = (string)info.GetValue(ControlLibConstants.KEY, typeof(string));
            this.Name = (string)info.GetValue(ControlLibConstants.NAME, typeof(string));
            this.ExternalizationKey = (string)info.GetValue(ControlLibConstants.EXTERNALIZATION_KEY, typeof(string));
            this.PropertyConfiguration = (PropertyConfiguration)info.GetValue(ControlLibConstants.PROPERTY_CONFIGURATION, typeof(PropertyConfiguration));
            this.IsComplexType = (bool)info.GetValue(ControlLibConstants.ISCOMPLEXTYPE, typeof(bool));
            this.IsEnumerable = (bool)info.GetValue(ControlLibConstants.ISENUMERABLE, typeof(bool));
            this.ComplexTypeName = (string)info.GetValue(ControlLibConstants.COMPLEX_TYPENAME, typeof(string));
        }

        #endregion

        #region "ISerializable"

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ControlLibConstants.KEY, this.Key, typeof(string));
            info.AddValue(ControlLibConstants.NAME, this.Name, typeof(string));
            info.AddValue(ControlLibConstants.EXTERNALIZATION_KEY, this.ExternalizationKey, typeof(string));
            info.AddValue(ControlLibConstants.PROPERTY_CONFIGURATION, this.PropertyConfiguration, typeof(PropertyConfiguration));
            info.AddValue(ControlLibConstants.ISCOMPLEXTYPE, this.IsComplexType, typeof(bool));
            info.AddValue(ControlLibConstants.ISENUMERABLE, this.IsEnumerable, typeof(bool));
            info.AddValue(ControlLibConstants.COMPLEX_TYPENAME, this.ComplexTypeName, typeof(string));
        }

        #endregion


    }
    
}