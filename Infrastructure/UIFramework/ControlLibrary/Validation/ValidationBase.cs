using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public abstract class ValidationBase : Externalizer, IValidator, ISerializable
    {
        #region "Methods"

        /// <summary>
        /// Check the string valid email..
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true</returns>
        public bool IsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check the string is Valid AlphaNumberic string
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true</returns>
        public bool IsAlphaNumericString(string s, string expression)
        {
            Regex r = new Regex(expression);
            return r.IsMatch(s);
        }

        /// <summary>
        /// Check the string is Valid AlphaNumberic string
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true</returns>
        public bool IsAlphaetString(string s, string expression)
        {
            Regex r = new Regex(expression);
            return r.IsMatch(s);
        }

        /// <summary>
        /// Check the string
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true</returns>
        public bool IsValidSSN(string s, string expression)
        {
            Regex r = new Regex(expression);
            return r.IsMatch(s);
        }

        /// <summary>
        /// check the string is valid number.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true</returns>
        public bool IsValidNumber(string s, string expression)
        {
            Regex r = new Regex(expression);
            return r.IsMatch(s);
        }

        /// <summary>
        /// check the string is Valid card number.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true</returns>
        public bool IsValidCardNumber(string s, string expression)
        {
            Regex r = new Regex(expression);
            return r.IsMatch(s);
        }

        /// <summary>
        /// check the string is valid phone number.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true</returns>
        public bool IsValidPhone(string s, string expression)
        {
            Regex r = new Regex(expression);
            return r.IsMatch(s);
        }

        /// <summary>
        /// check the string is valid URL.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true</returns>
        public bool isValidUrl(string s, string expression)
        {
            Regex r = new Regex(expression);
            return r.IsMatch(s);
        }

        #endregion "Methods"

        #region "Constructors"

        public ValidationBase()
        {
        }

        public ValidationBase(ValidatorsType type, bool doValidate, string message)
        {
            this.Type = type;
            this.DoValidate = doValidate;
            this.MessageKey = message;
        }

        public ValidationBase(SerializationInfo info, StreamingContext context)
        {
            this.Type = (ValidatorsType)info.GetValue("Type", typeof(ValidatorsType));
            this.DoValidate = (bool)info.GetValue("DoValidate", typeof(bool));
            this.MessageKey = (string)info.GetValue("Message", typeof(string));
        }

        #endregion "Constructors"

        #region "IValidator"

        [XmlAttribute("DoValidate")]
        public bool DoValidate { get; set; }

        public string Message
        {
            get
            {
                return GetExternalizedMessage(MessageKey);
            }
        }

        [XmlAttribute("Type")]
        public ValidatorsType Type { get; set; }

        [XmlAttribute("MessageKey")]
        public string MessageKey { get; set; }

        public abstract bool Validate(object data, string expression);

        #endregion "IValidator"

        #region "ISerializable"

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Type", this.Type, typeof(ValidatorsType));
            info.AddValue("DoValidate", this.DoValidate, typeof(bool));
            info.AddValue("Message", this.Message, typeof(string));
        }

        #endregion "ISerializable"
    }
}