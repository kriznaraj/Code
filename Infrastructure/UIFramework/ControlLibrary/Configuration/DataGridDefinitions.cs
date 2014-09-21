using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class DataGridDefinitions : IDataGridDefinitions, ISerializable
    {
        /*This class will hold all the Name and the Collection of DataColumnDefinition*/
        private IDictionary<string, DataGridColumDefinition> _columnsList;

        #region "Constructors"

        public DataGridDefinitions()
        {
            
        }

        public DataGridDefinitions(string gridName, List<DataGridColumDefinition> columnsList)
        {
            this.DataGridColumnDefinition = columnsList;
            this.GridName = gridName;
        }

        public DataGridDefinitions(SerializationInfo info, StreamingContext context)
        {
            this.GridName = (string)info.GetValue(ControlLibConstants.NAME, typeof(string));
            this.DataGridColumnDefinition = (List<DataGridColumDefinition>)info.GetValue(ControlLibConstants.DATA_COLUMN_DEFINITION, typeof(List<DataGridColumDefinition>));
        }

        #endregion

        #region "Properties"

        [XmlAttribute("GridName")]
        public string GridName { get; set; }

        [XmlElement("DataGridColumnDefinition")]
        public List<DataGridColumDefinition> DataGridColumnDefinition { get; set; }

        [XmlIgnore]
        public IDictionary<string, DataGridColumDefinition> IndexedDataColumnDefinition
        {
            get
            {
                if (_columnsList == null)
                {
                    _columnsList = new Dictionary<string, DataGridColumDefinition>();

                    foreach (var item in DataGridColumnDefinition)
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
            info.AddValue(ControlLibConstants.NAME, this.GridName, typeof(string));
            info.AddValue(ControlLibConstants.DATA_COLUMN_DEFINITION, this.DataGridColumnDefinition, typeof(List<DataGridColumDefinition>));
        }

        #endregion

    }
}