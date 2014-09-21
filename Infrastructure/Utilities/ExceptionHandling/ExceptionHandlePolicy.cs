using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ExceptionHandling
{
    [Serializable]
    [XmlRoot("ExceptionHandlePolicy")]
    public class ExceptionHandlePolicy : ISerializable
    {
        public ExceptionHandlePolicy()
        {
        }

        protected ExceptionHandlePolicy(SerializationInfo info, StreamingContext context)
        {
            this.Name = info.GetString("Name");
            this.HandlerConfigList = info.GetValue("HandlerConfigList", typeof(List<ExceptionHandlerConfig>)) as List<ExceptionHandlerConfig>;
            this.ConvertorConfigList = info.GetValue("ConvertorConfigList", typeof(List<ExceptionConvertorConfig>)) as List<ExceptionConvertorConfig>;
        }

        /// <summary>
        /// The list of handlers that would handle this Exception
        /// </summary>
        [XmlElement("ExceptionHandlerConfig")]
        public List<ExceptionHandlerConfig> HandlerConfigList
        {
            get;
            set;
        }

        /// <summary>
        /// The list of handlers that would handle this Exception
        /// </summary>
        [XmlElement("ExceptionConvertorConfig")]
        public List<ExceptionConvertorConfig> ConvertorConfigList
        {
            get;
            set;
        }

        /// <summary>
        /// Name of the policy for the Exception Manager to use
        /// </summary>
        [XmlAttribute("name")]
        public String Name
        {
            get;
            set;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name);
            info.AddValue("HandlerConfigList", this.HandlerConfigList, typeof(IEnumerable<ExceptionHandlerConfig>));
            info.AddValue("ConvertorConfigList", this.ConvertorConfigList, typeof(IEnumerable<ExceptionConvertorConfig>));
        }
    }
}