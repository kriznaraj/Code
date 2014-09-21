using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class RegExValidator : ValidationBase, IRegExValidator
    {
        private string _message;

        public RegExValidator()
        {
        }

        public RegExValidator(Dictionary<string, string> regExAttributes)
            : base(ValidatorsType.RegExp, Convert.ToBoolean(regExAttributes["Validate"]), regExAttributes["MessageKey"])
        {
            this.RegExpression = regExAttributes["Expression"];
        }

        public RegExValidator(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.RegExpression = (string)info.GetValue("RegExpression", typeof(string));
        }

        [XmlElement("RegExpression")]
        public string RegExpression { get; set; }

        public override bool Validate(object data, string expression)
        {
            if (data == null || string.IsNullOrEmpty(data.ToString())) return true;

            try
            {
                Regex r = new Regex(expression);
                return r.IsMatch(Convert.ToString(data));
            }
            catch
            {
                return false;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("RegExpression", this.RegExpression, typeof(string));
        }
    }
}