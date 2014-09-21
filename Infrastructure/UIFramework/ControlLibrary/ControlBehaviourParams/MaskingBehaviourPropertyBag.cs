using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class MaskingBehaviourPropertyBag : IMaskingBehaviourPropertyBag, ISerializable
    {
        
        #region "Constuctor"

        public MaskingBehaviourPropertyBag()
        {
            
        }

        public MaskingBehaviourPropertyBag(Dictionary<string,string> maskingAttributes)
        {
            this.MaskingChar = maskingAttributes[ControlLibConstants.MASKING_CHAR];
            this.MaskingType = (MaskingType)Enum.Parse(typeof(MaskingType), maskingAttributes[ControlLibConstants.MASKING_TYPE]);
            this.MaskCharLength = Convert.ToInt32(maskingAttributes[ControlLibConstants.MASKING_CHAR_LENGTH]);
            this.MaskingPosition = (MaskingPosition)Enum.Parse(typeof(MaskingPosition), maskingAttributes[ControlLibConstants.MASKING_POSITION]);
        }

        public MaskingBehaviourPropertyBag(SerializationInfo info, StreamingContext context)
        {
            this.MaskingChar = (string)info.GetValue(ControlLibConstants.MASKING_CHAR, typeof(string));
            this.MaskingType = (MaskingType)info.GetValue(ControlLibConstants.MASKING_TYPE, typeof(MaskingType));
            this.MaskCharLength = (int)info.GetValue(ControlLibConstants.MASKING_CHAR_LENGTH, typeof(int));
            this.MaskingPosition = (MaskingPosition)info.GetValue(ControlLibConstants.MASKING_POSITION, typeof(MaskingPosition));
        }

        #endregion

        #region "Implemented Properties - ITextMaskingFieldProperties"

        [XmlAttribute("MaskingChar")]
        public string MaskingChar { get; set; }

        [XmlAttribute("MaskingType")]
        public MaskingType MaskingType { get; set; }

        [XmlAttribute("MaskCharLength")]
        public int MaskCharLength { get; set; }

        [XmlAttribute("MaskingPosition")]
        public MaskingPosition MaskingPosition { get; set; }

        #endregion

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ControlLibConstants.MASKING_CHAR, this.MaskingChar, typeof(string));
            info.AddValue(ControlLibConstants.MASKING_TYPE, this.MaskingType, typeof(MaskingType));
            info.AddValue(ControlLibConstants.MASKING_CHAR_LENGTH, this.MaskCharLength, typeof(int));
            info.AddValue(ControlLibConstants.MASKING_POSITION, this.MaskingPosition, typeof(MaskingPosition));
        }
    }
}