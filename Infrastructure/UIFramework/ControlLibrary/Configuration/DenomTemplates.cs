using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class DenomTemplates : IDenomTemplates, ISerializable
    {
        /*This class will hold all the Name and the Collection of DataColumnDefinition*/
        private IDictionary<string, DenomTemplateColumnDefinition> _columnsList;

        #region "Constructors"

        public DenomTemplates()
        {
            
        }

        public DenomTemplates(string templateName, List<DenomTemplateColumnDefinition> columnsList)
        {
            this.DenomTemplateColumnDefinition = columnsList;
            this.TemplateName = templateName;
        }

        public DenomTemplates(SerializationInfo info, StreamingContext context)
        {
            this.TemplateName = (string)info.GetValue(ControlLibConstants.DENOM_TEMPLATE_NAME, typeof(string));
            this.DenomTemplateColumnDefinition = (List<DenomTemplateColumnDefinition>)info.GetValue(ControlLibConstants.DENOM_TEMPLATE_COLUMN_DEFINITION, typeof(List<DenomTemplateColumnDefinition>));
        }

        #endregion

        #region "Properties"

        [XmlAttribute("TemplateName")]
        public string TemplateName { get; set; }

        [XmlElement("DenomTemplateColumnDefinition")]
        public List<DenomTemplateColumnDefinition> DenomTemplateColumnDefinition { get; set; }

        [XmlIgnore]
        public IDictionary<string, DenomTemplateColumnDefinition> IndexedDenomTemplateColumnDefinition
        {
            get
            {
                if (_columnsList == null)
                {
                    _columnsList = new Dictionary<string, DenomTemplateColumnDefinition>();

                    foreach (var item in DenomTemplateColumnDefinition)
                    {
                        _columnsList.Add(item.ColumnName, item);
                    }
                }

                return _columnsList;
            }
        }

        #endregion

        #region "ISerializable"


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ControlLibConstants.DENOM_TEMPLATE_NAME, this.TemplateName, typeof(string));
            info.AddValue(ControlLibConstants.DENOM_TEMPLATE_COLUMN_DEFINITION, this.DenomTemplateColumnDefinition, typeof(List<DenomTemplateColumnDefinition>));
        }

        #endregion

    }
}