using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class AutoCompleteBehaviourPropertyBag : IAutoCompleteBehaviourPropertyBag, ISerializable
    {

        #region "Constuctor"

        public AutoCompleteBehaviourPropertyBag()
        {

        }

        public AutoCompleteBehaviourPropertyBag(Dictionary<string,string> autoCompleteAttributes)
        {
            this.ActionURL = autoCompleteAttributes.ContainsKey(ControlLibConstants.ACTION_URL) ? autoCompleteAttributes[ControlLibConstants.ACTION_URL] : string.Empty;
            this.ActionName = autoCompleteAttributes.ContainsKey(ControlLibConstants.ACTION_NAME) ? autoCompleteAttributes[ControlLibConstants.ACTION_NAME] : string.Empty;
            this.ControllerName = autoCompleteAttributes.ContainsKey(ControlLibConstants.CONTROLLER_NAME) ? autoCompleteAttributes[ControlLibConstants.CONTROLLER_NAME] : string.Empty;
            this.MinCharRequired = autoCompleteAttributes.ContainsKey(ControlLibConstants.MIN_CHAR_REQUIRED) ? Convert.ToInt32(autoCompleteAttributes[ControlLibConstants.MIN_CHAR_REQUIRED]) : 0;
            this.MaxResultCount = autoCompleteAttributes.ContainsKey(ControlLibConstants.MAX_RESULT_COUNT) ? Convert.ToInt32(autoCompleteAttributes[ControlLibConstants.MAX_RESULT_COUNT]) : 0;
            this.OrderBy = autoCompleteAttributes.ContainsKey(ControlLibConstants.ORDER_BY) ? (OrderByType)Enum.Parse(typeof(OrderByType), autoCompleteAttributes[ControlLibConstants.ORDER_BY]) : OrderByType.Asc;
            this.SearchType = autoCompleteAttributes.ContainsKey(ControlLibConstants.SEARCH_TYPE) ? (SearchType)Enum.Parse(typeof(SearchType), autoCompleteAttributes[ControlLibConstants.SEARCH_TYPE]) : SearchType.None;
        }

        public AutoCompleteBehaviourPropertyBag(SerializationInfo info, StreamingContext context)
        {
            this.ActionName = (string)info.GetValue(ControlLibConstants.ACTION_NAME, typeof(string));
            this.ControllerName = (string)info.GetValue(ControlLibConstants.CONTROLLER_NAME, typeof(string));
            this.MinCharRequired = (int)info.GetValue(ControlLibConstants.MIN_CHAR_REQUIRED, typeof(int));
            this.MaxResultCount = (int)info.GetValue(ControlLibConstants.MAX_RESULT_COUNT, typeof(int));
            this.OrderBy = (OrderByType)info.GetValue(ControlLibConstants.ORDER_BY, typeof(OrderByType));
            this.SearchType = (SearchType)info.GetValue(ControlLibConstants.SEARCH_TYPE, typeof(SearchType));
            this.ActionURL = (string)info.GetValue(ControlLibConstants.ACTION_URL, typeof(string));
        }

        #endregion

        #region "Implemented Properties - IAutoCompleteFieldProperties"

        [XmlAttribute("ActionName")]
        public string ActionName { get; set; }

        [XmlAttribute("ControllerName")]
        public string ControllerName { get; set; }

        [XmlAttribute("MinCharRequired")]
        public int MinCharRequired { get; set; }

        [XmlAttribute("MaxResultCount")]
        public int MaxResultCount { get; set; }

        [XmlAttribute("OrderBy")]
        public OrderByType OrderBy { get; set; }

        [XmlAttribute("SearchType")]
        public SearchType SearchType { get; set; }

        [XmlAttribute("ActionURL")]
        public string ActionURL { get; set; }

        #endregion

        #region "Public Methods"

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ControlLibConstants.ACTION_NAME, this.ActionName, typeof(string));
            info.AddValue(ControlLibConstants.CONTROLLER_NAME, this.ControllerName, typeof(string));
            info.AddValue(ControlLibConstants.MIN_CHAR_REQUIRED, this.MinCharRequired, typeof(int));
            info.AddValue(ControlLibConstants.MAX_RESULT_COUNT, this.MaxResultCount, typeof(int));
            info.AddValue(ControlLibConstants.ORDER_BY, this.OrderBy, typeof(OrderByType));
            info.AddValue(ControlLibConstants.SEARCH_TYPE, this.SearchType, typeof(SearchType));
            info.AddValue(ControlLibConstants.ACTION_URL, this.ActionURL, typeof(string));
        }

        #endregion
    }
}