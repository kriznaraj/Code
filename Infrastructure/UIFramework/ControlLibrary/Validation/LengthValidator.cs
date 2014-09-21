using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class LengthValidator : ValidationBase, ILengthValidator
    {
        public LengthValidator()
        {
        }

        public LengthValidator(Dictionary<string, string> lengthAttributes)
            : base(ValidatorsType.Length, Convert.ToBoolean(lengthAttributes["Validate"]), lengthAttributes["MessageKey"])
        {
            this.MinLength = Convert.ToInt32(lengthAttributes["MinLength"]);
            this.MaxLength = Convert.ToInt32(lengthAttributes["MaxLength"]);
        }

        public LengthValidator(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.MinLength = (int)info.GetValue("MinLength", typeof(int));
            this.MaxLength = (int)info.GetValue("MaxLength", typeof(int));
        }

        [XmlAttribute("MinLength")]
        public int MinLength { get; set; }

        [XmlAttribute("MaxLength")]
        public int MaxLength { get; set; }

        public override bool Validate(object data, string expression)
        {
            if (data == null || string.IsNullOrEmpty(data.ToString())) return true;

            return (data.ToString().Length >= MinLength && data.ToString().Length <= MaxLength) ? true : false;
        }

        #region "ISerializable"

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("MinLength", this.MinLength, typeof(int));
            info.AddValue("MaxLength", this.MaxLength, typeof(int));
        }

        #endregion "ISerializable"
    }
}