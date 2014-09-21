using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class ControlTemplateConfiguration : IControlTemplateConfiguration, ISerializable
    {
        #region Properties

        [XmlAttribute("TemplateKey")]
        public string TemplateKey { get; set; }

        [XmlElement("TemplateHTML")]
        public string TemplateHTML { get; set; }

        #endregion

        #region Constructor

        public ControlTemplateConfiguration()
        {

        }

        public ControlTemplateConfiguration(string templateKey, string templateHTML)
        {
            this.TemplateKey = templateKey;
            this.TemplateHTML = templateHTML;
        }

        public ControlTemplateConfiguration(SerializationInfo info, StreamingContext context)
        {
            this.TemplateKey = (string)info.GetValue(ControlLibConstants.TEMPALTE_KEY, typeof(string));
            this.TemplateHTML = (string)info.GetValue(ControlLibConstants.TEMPALTE_HTML, typeof(string));
        }
        #endregion

        #region "ISerializable"

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ControlLibConstants.TEMPALTE_KEY, this.TemplateKey, typeof(string));
            info.AddValue(ControlLibConstants.TEMPALTE_HTML, this.TemplateHTML, typeof(string));
        }

        #endregion
    }    
}
