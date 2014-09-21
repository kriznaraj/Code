using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class RequiredValidator : ValidationBase, IRequiredValidator
    {
        public RequiredValidator()
        {

        }

        public RequiredValidator(Dictionary<string, string> requiredAttributes)
            : base(ValidatorsType.Required, Convert.ToBoolean(requiredAttributes["Validate"]), requiredAttributes["MessageKey"])
        {
        }

        public RequiredValidator(SerializationInfo info, StreamingContext context):base(info, context)
        {

        }

        public override bool Validate(object data, string expression)
        {
            if (data == null) return false;
            return string.IsNullOrEmpty(data.ToString()) ? false : true;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}