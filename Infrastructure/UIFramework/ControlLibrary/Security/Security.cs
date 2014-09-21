using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class Security : ISecurity
    {
        #region "Constructors"

        public Security()
        {

        }

        public Security(AccessType accessType, string taskCode)
        {
            this.AccessType = accessType;
            this.TaskCode = taskCode;
        }

        public Security(SerializationInfo info, StreamingContext context)
        {
            this.AccessType = (AccessType)info.GetValue("AccessType", typeof(AccessType));
            this.TaskCode = (string)info.GetValue("TaskCode", typeof(string));
        }

        #endregion "Constructors"

        #region "Properties"

        [XmlAttribute("AccessType")]
        public AccessType AccessType { get; set; }

        [XmlAttribute("TaskCode")]
        public string TaskCode { get; set; }

        #endregion "Properties"        

        #region "ISerializable"

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("AccessType", this.AccessType, typeof(AccessType));
            info.AddValue("TaskCode", this.TaskCode, typeof(string));
        }

        #endregion "ISerializable"
    }
}