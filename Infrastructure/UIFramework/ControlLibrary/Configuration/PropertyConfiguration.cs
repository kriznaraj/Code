using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class PropertyConfiguration : IPropertyConfiguration, ISerializable
    {

        #region "Properties"

        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlElement("Autocomplete")]
        public AutoCompleteBehaviourPropertyBag AutoCompleteProperties { get; set; }

        [XmlElement("Masking")]
        public MaskingBehaviourPropertyBag MaskingProperties { get; set; }

        [XmlAttribute("AccessPolicyCode")]
        public string AccessPolicyCode { get; set; }

        [XmlElement(typeof(RequiredValidator))]
        [XmlElement(typeof(LengthValidator))]
        [XmlElement(typeof(RangeValidator))]
        [XmlElement(typeof(RegExValidator))]
        [XmlElement(typeof(CustomValidator))]
        [XmlElement(typeof(SpecialCharValidator))]
        public List<ValidationBase> Validators { get; set; }

        [XmlElement(typeof(Security))]
        public List<Security> Security { get; set; }

        [XmlElement(typeof(SiteConfig))]
        public List<SiteConfig> SiteConfig { get; set; }

        [XmlElement("DateFormat")]
        public DatePropertyBag DateProperties { get; set; }

        [XmlElement("TimeFormat")]
        public TimePropertyBag TimeProperties { get; set; }

        #endregion

        #region "Constructors"

        public PropertyConfiguration()
        {

        }

        public PropertyConfiguration(string key, List<ValidationBase> validators, string accessPolicyCode = "",Dictionary<string,object> behaviourDtls = null)
        {
            this.Key = key;
            this.AccessPolicyCode = accessPolicyCode;
            this.Validators = validators;
            if (null != behaviourDtls)
            {
                this.AutoCompleteProperties = behaviourDtls.ContainsKey(ControlLibConstants.AUTOCOMPLETE_BAG) ? behaviourDtls[ControlLibConstants.AUTOCOMPLETE_BAG] as AutoCompleteBehaviourPropertyBag : null;
                this.MaskingProperties = behaviourDtls.ContainsKey(ControlLibConstants.MASKING_BAG) ? behaviourDtls[ControlLibConstants.MASKING_BAG] as MaskingBehaviourPropertyBag : null; ;
                this.DateProperties = behaviourDtls.ContainsKey(ControlLibConstants.DATE_BAG) ? behaviourDtls[ControlLibConstants.DATE_BAG] as DatePropertyBag : null;
                this.TimeProperties = behaviourDtls.ContainsKey(ControlLibConstants.TIME_BAG) ? behaviourDtls[ControlLibConstants.TIME_BAG] as TimePropertyBag : null; ;
            }
        }

        public PropertyConfiguration(SerializationInfo info, StreamingContext context)
        {
            this.Key = (string)info.GetValue(ControlLibConstants.KEY, typeof(string));
            this.AutoCompleteProperties = (AutoCompleteBehaviourPropertyBag)info.GetValue(ControlLibConstants.AUTOCOMPLETE_PROPERTIES, typeof(AutoCompleteBehaviourPropertyBag));
            this.MaskingProperties = (MaskingBehaviourPropertyBag)info.GetValue(ControlLibConstants.MASKING_PROPERTIES, typeof(MaskingBehaviourPropertyBag));
            this.AccessPolicyCode = (string)info.GetValue(ControlLibConstants.ACCESS_POLICY_CODE, typeof(string));
            this.Validators = (List<ValidationBase>)info.GetValue(ControlLibConstants.VALIDATORS, typeof(List<ValidationBase>));
            this.DateProperties = (DatePropertyBag)info.GetValue(ControlLibConstants.DATE_PROPERTIES, typeof(DatePropertyBag)); ;
            this.TimeProperties = (TimePropertyBag)info.GetValue(ControlLibConstants.TIME_PROPERTIES, typeof(TimePropertyBag)); ;
            this.Security = (List<Security>)info.GetValue(ControlLibConstants.SECURITY, typeof(List<Security>));
            this.SiteConfig = (List<SiteConfig>)info.GetValue(ControlLibConstants.SITECONFIG, typeof(List<SiteConfig>));
        }

        #endregion

        #region "ISerializable"

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ControlLibConstants.KEY, this.Key, typeof(string));
            info.AddValue(ControlLibConstants.AUTOCOMPLETE_PROPERTIES, this.AutoCompleteProperties, typeof(AutoCompleteBehaviourPropertyBag));
            info.AddValue(ControlLibConstants.MASKING_PROPERTIES, this.MaskingProperties, typeof(MaskingBehaviourPropertyBag));
            info.AddValue(ControlLibConstants.ACCESS_POLICY_CODE, this.AccessPolicyCode, typeof(string));
            info.AddValue(ControlLibConstants.VALIDATORS, this.Validators, typeof(List<ValidationBase>));
            info.AddValue(ControlLibConstants.DATE_PROPERTIES, this.DateProperties, typeof(DatePropertyBag));
            info.AddValue(ControlLibConstants.TIME_PROPERTIES, this.TimeProperties, typeof(TimePropertyBag));
            info.AddValue(ControlLibConstants.SECURITY, this.Security, typeof(List<Security>));
            info.AddValue(ControlLibConstants.SITECONFIG, this.SiteConfig, typeof(List<SiteConfig>));
        }

        #endregion
    }    
}