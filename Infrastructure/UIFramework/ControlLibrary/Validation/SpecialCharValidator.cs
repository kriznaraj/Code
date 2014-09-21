using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class SpecialCharValidator : ValidationBase, ISpecialCharValidator
    {
        public SpecialCharValidator()
        {
        }

        public SpecialCharValidator(Dictionary<string, string> specialAttributes)
            : base(ValidatorsType.SpecialChar, Convert.ToBoolean(specialAttributes["Validate"]), specialAttributes["MessageKey"])
        {
            this.SpecialChars = specialAttributes["Expression"];
            this.Restriction = (RestrictionType)Enum.Parse(typeof(RestrictionType), specialAttributes["Restriction"]);
        }

        public SpecialCharValidator(SerializationInfo info, StreamingContext context)
        {
            this.SpecialChars = (string)info.GetValue("SpecialChars", typeof(string));
            this.Restriction = (RestrictionType)info.GetValue("Restriction", typeof(RestrictionType));
        }

        [XmlElement("SpecialChars")]
        public string SpecialChars { get; set; }

        [XmlAttribute("Restriction")]
        public RestrictionType Restriction { get; set; }

        public override bool Validate(object data, string expression)
        {
            Regex regex;
            bool validateSucess = false;
            string specailCharacterMaster = ",\\.>\\+<!@#$)(\\*&^%';\\[\\]:\\|`~}{\\-_=\"";

            if (data == null || string.IsNullOrEmpty(data.ToString())) return true;

            if (data != null)
            {
                if (Restriction == RestrictionType.Allow)
                {
                    for (int i = 0; i < this.SpecialChars.Length; i++)
                    {
                        if (this.SpecialChars.IndexOf(this.SpecialChars[i]) > -1)
                        {
                            specailCharacterMaster.Replace(this.SpecialChars[i].ToString(), "");
                        }

                        regex = new Regex("[" + specailCharacterMaster + "]");
                        validateSucess = regex.IsMatch(data.ToString());
                    }
                }
                else if (Restriction == RestrictionType.Restrict)
                {
                    regex = new Regex("[" + this.SpecialChars + "]");
                    validateSucess = regex.IsMatch(data.ToString());
                }
            }
            else
            {
                validateSucess = true;
            }
            return validateSucess;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("SpecialChars", this.SpecialChars, typeof(string));
            info.AddValue("Restriction", this.Restriction, typeof(RestrictionType));
        }
    }
}