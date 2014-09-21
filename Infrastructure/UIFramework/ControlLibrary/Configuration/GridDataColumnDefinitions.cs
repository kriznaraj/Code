using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BallyTech.UI.Web.ControlLibrary
{
    [Serializable]
    public class GridDataColumnDefinitions : IGridDataColumnDefinitions, ISerializable
    {
        /*This class will hold all the Name and the Collection of DataColumnDefinition*/
        private IDictionary<string, IDataColumnDefinition> _columnsList;

        #region "Constructors"

        public GridDataColumnDefinitions()
        {
            
        }

        public GridDataColumnDefinitions(string gridName, List<IDataColumnDefinition> columnsList)
        {
            this.DataColumnDefinition = columnsList;
            this.GridName = gridName;
        }

        public GridDataColumnDefinitions(SerializationInfo info, StreamingContext context)
        {
            this.GridName = (string)info.GetValue(ControlLibConstants.NAME, typeof(string));
            this.DataColumnDefinition = (List<IDataColumnDefinition>)info.GetValue(ControlLibConstants.DATA_COLUMN_DEFINITION, typeof(List<IDataColumnDefinition>));
        }

        #endregion

        #region "Properties"

        public string GridName { get; private set; }

        public List<IDataColumnDefinition> DataColumnDefinition {get; private set;}

        public IDictionary<string, IDataColumnDefinition> IndexedDataColumnDefinition
        {
            get
            {
                if (_columnsList == null)
                {
                    _columnsList = new Dictionary<string, IDataColumnDefinition>();

                    foreach (var item in DataColumnDefinition)
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
            info.AddValue(ControlLibConstants.DATA_COLUMN_DEFINITION, this.DataColumnDefinition, typeof(List<DataColumnDefinition>));
        }

        #endregion
    }    
}