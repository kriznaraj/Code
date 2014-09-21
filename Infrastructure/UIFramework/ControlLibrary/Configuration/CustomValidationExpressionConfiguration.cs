using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class CustomValidationExpressionConfiguration : ICustomValidationExpressionConfiguration, ISerializable
    {

        #region "Properties"

        [XmlAttribute("ValidationType")]
        public CustomValidationType ValidationType { get; set; }

        [XmlElement("Expression")]
        public string Expression { get; set; }

        #endregion

        #region "Constructors"

        public CustomValidationExpressionConfiguration()
        {

        }

        public CustomValidationExpressionConfiguration(CustomValidationType validationType, string expression)
        {
            this.ValidationType = validationType;
            this.Expression = expression;
        }

        public CustomValidationExpressionConfiguration(SerializationInfo info, StreamingContext context)
        {
            this.ValidationType = (CustomValidationType)info.GetValue(ControlLibConstants.VALIDATION_TYPE, typeof(CustomValidationType));
            this.Expression = (string)info.GetValue(ControlLibConstants.EXPRESSION, typeof(string));
        }

        #endregion

        #region "ISerializable"

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ControlLibConstants.VALIDATION_TYPE, this.ValidationType, typeof(CustomValidationType));
            info.AddValue(ControlLibConstants.EXPRESSION, this.Expression, typeof(string));
        }

        #endregion
    }    
}