using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class DataGridColumDefinition : IDataColumnDefinition, ISerializable
    {
        #region "Constructors"

        public DataGridColumDefinition()
        {
        }

        public DataGridColumDefinition(string columnName, string displayMember, string headerName, GridColumnDataType columnDataType = GridColumnDataType.Text, string alignment = "right", string width = "auto", string accessPolicyCode = "")
        {
            this.ColumnName = columnName;
            this.DisplayMember = displayMember;
            this.HeaderName = headerName;
            this.ColumnDataType = columnDataType;
            this.Alignment = alignment;
            this.Width = width;
            this.AccessPolicyCode = accessPolicyCode;
        }

        public DataGridColumDefinition(SerializationInfo info, StreamingContext context)
        {
            this.ColumnName = (string)info.GetValue(ControlLibConstants.COLUMN_NAME, typeof(string));
            this.DisplayMember = (string)info.GetValue(ControlLibConstants.DISPLAY_MEMBER, typeof(string));
            this.Width = (string)info.GetValue(ControlLibConstants.WIDTH, typeof(string));
            this.ColumnDataType = (GridColumnDataType)info.GetValue(ControlLibConstants.COLUMN_DATA_TYPE, typeof(GridColumnDataType));
            this.Alignment = (string)info.GetValue(ControlLibConstants.ALIGHNMENT, typeof(string));
            this.HeaderName = (string)info.GetValue(ControlLibConstants.HEADER_NAME, typeof(string));
            this.SearchType = (GridFilterType)info.GetValue(ControlLibConstants.SEARCH_TYPE, typeof(GridFilterType));
            this.AccessPolicyCode = (string)info.GetValue(ControlLibConstants.ACCESS_POLICY_CODE, typeof(string));
        }

        #endregion "Constructors"

        #region "Properties"

        [XmlAttribute("ColumnName")]
        public string ColumnName { get; set; }

        [XmlAttribute("DisplayMember")]
        public string DisplayMember { get; set; }

        [XmlAttribute("Width")]
        public string Width { get; set; }

        [XmlAttribute("ColumnDataType")]
        public GridColumnDataType ColumnDataType { get; set; }

        [XmlAttribute("Alignment")]
        public string Alignment { get; set; }

        [XmlAttribute("HeaderName")]
        public string HeaderName { get; set; }

        [XmlAttribute("SearchType")]
        public GridFilterType SearchType { get; set; }

        [XmlAttribute("AccessPolicyCode")]
        public string AccessPolicyCode { get; set; }

        [XmlAttribute("IsSortable")]
        public bool IsSortable { get; set; }

        #endregion "Properties"

        #region "ISerializable"

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ControlLibConstants.COLUMN_NAME, this.ColumnName, typeof(string));
            info.AddValue(ControlLibConstants.DISPLAY_MEMBER, this.DisplayMember, typeof(string));
            info.AddValue(ControlLibConstants.WIDTH, this.Width, typeof(string));
            info.AddValue(ControlLibConstants.COLUMN_DATA_TYPE, this.ColumnDataType, typeof(GridColumnDataType));
            info.AddValue(ControlLibConstants.ALIGHNMENT, this.Alignment, typeof(string));
            info.AddValue(ControlLibConstants.HEADER_NAME, this.HeaderName, typeof(string));
            info.AddValue(ControlLibConstants.SEARCH_TYPE, this.SearchType, typeof(GridFilterType));
            info.AddValue(ControlLibConstants.ACCESS_POLICY_CODE, this.AccessPolicyCode, typeof(string));
        }

        #endregion "ISerializable"
    }
}