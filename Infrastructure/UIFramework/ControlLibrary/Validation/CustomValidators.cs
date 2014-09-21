using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class CustomValidator : ValidationBase, ICustomValidator
    {
        #region "Constructors"

        public CustomValidator()
        {
        }

        public CustomValidator(Dictionary<string, string> customAttributes)
            : base(ValidatorsType.Custom, Convert.ToBoolean(customAttributes["Validate"]), customAttributes["MessageKey"])
        {
            this.ValidationType = (CustomValidationType)Enum.Parse(typeof(CustomValidationType), customAttributes["ValidationType"]);
        }

        public CustomValidator(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.ValidationType = (CustomValidationType)info.GetValue("ValidationType", typeof(CustomValidationType));
        }

        #endregion "Constructors"

        #region "Properties"

        [XmlAttribute("ValidationType")]
        public CustomValidationType ValidationType { get; set; }

        [XmlElement("Expression")]
        public string Expression { get; set; }

        #endregion "Properties"

        #region "Override Methods"

        public override bool Validate(object data, string expression)
        {
            bool bRetValue = false;

            if (data == null || string.IsNullOrEmpty(data.ToString())) return true;

            string valData = data.ToString();

            string _expression = string.IsNullOrEmpty(this.Expression) ? expression : this.Expression;

            switch (this.ValidationType)
            {
                case CustomValidationType.Email:
                    bRetValue = base.IsValidEmail(valData);
                    break;

                case CustomValidationType.AlphaNumeric:
                    bRetValue = base.IsAlphaNumericString(valData, _expression);
                    break;

                case CustomValidationType.Alphabet:
                    bRetValue = base.IsAlphaetString(valData, _expression);
                    break;

                case CustomValidationType.Phone:
                    bRetValue = base.IsValidPhone(valData, _expression);
                    break;

                case CustomValidationType.SSN:
                    bRetValue = base.IsValidSSN(valData, _expression);
                    break;

                case CustomValidationType.CardNumber:
                    bRetValue = base.IsValidCardNumber(valData, _expression);
                    break;

                case CustomValidationType.URL:
                    bRetValue = base.isValidUrl(valData, _expression);
                    break;

                case CustomValidationType.Number:
                    bRetValue = base.IsValidNumber(valData, _expression);
                    break;

                default:
                    break;
            }

            return bRetValue;
        }

        #endregion "Override Methods"

        #region "ISerializable"

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ValidationType", this.ValidationType, typeof(CustomValidationType));
            info.AddValue("Expression", this.Expression, typeof(string));
        }

        #endregion "ISerializable"
    }
}