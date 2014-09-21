using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class RangeValidator : ValidationBase, IRangeValidator
    {
        public RangeValidator()
        {
        }

        public RangeValidator(Dictionary<string,string> rangeAttributes)
            : base(ValidatorsType.Range, Convert.ToBoolean(rangeAttributes["Validate"]), rangeAttributes["MessageKey"])
        {
            this.MinValue = Convert.ToDecimal(rangeAttributes["MinLength"]);
            this.MaxValue = Convert.ToDecimal(rangeAttributes["MaxLength"]);
        }
        
        public RangeValidator(SerializationInfo info, StreamingContext context)
			:base(info, context)
        {
            this.MinValue = (decimal)info.GetValue("MinValue", typeof(decimal));
            this.MaxValue = (decimal)info.GetValue("MaxValue", typeof(decimal));
        }

        [XmlAttribute("MinValue")]
        public decimal MinValue { get; set; }

        [XmlAttribute("MaxValue")]
        public decimal MaxValue { get; set; }

        public override bool Validate(object data, string expression)
        {
            if (data == null || string.IsNullOrEmpty(data.ToString())) return true;

            return (Convert.ToDecimal(data) >= MinValue && Convert.ToDecimal(data) <= MaxValue) ? true : false;
        }

        #region "ISerializable"

        public override  void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("MinValue", this.MinValue, typeof(decimal));
            info.AddValue("MaxValue", this.MaxValue, typeof(decimal));
        }

        #endregion
    }
}