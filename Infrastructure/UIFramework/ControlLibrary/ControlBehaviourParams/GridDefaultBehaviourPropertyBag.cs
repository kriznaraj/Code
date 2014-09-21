using System;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace BallyTech.UI.Web.ControlLibrary
{
    [Serializable]
    public class GridBehaviourPropertyBag : IGridDefaultBehaviourPropertyBag, ISerializable
    {
        [XmlAttribute("PageSize")]
        public int PageSize { get; set; }

        [XmlAttribute("GridHeight")]
        public int GridHeight { get; set; }

        [XmlAttribute("EnableFilter")]
        public bool EnableFilter { get; set; }

        [XmlAttribute("EnableSorting")]
        public bool EnableSorting { get; set; }

        [XmlAttribute("EnableExport")]
        public bool EnableExport { get; set; }

        [XmlAttribute("ServerPagination")]
        public bool ServerPagination { get; set; }

        [XmlAttribute("SelectOption")]
        public bool SelectOption { get; set; }

        public GridBehaviourPropertyBag()
        {            
            
        }

        public GridBehaviourPropertyBag(int PageSize, int GridHeight, bool EnableFilter, bool EnableSorting, bool EnableExport, bool ServerPagination, bool SelectOption)
        {
            this.PageSize = PageSize;
            this.GridHeight = GridHeight;
            this.EnableFilter = EnableFilter;
            this.EnableSorting = EnableSorting;
            this.EnableExport = EnableExport;
            this.ServerPagination = ServerPagination;
            this.SelectOption = SelectOption;
        }

        public GridBehaviourPropertyBag(SerializationInfo info, StreamingContext context)
        {
            this.PageSize = (int)info.GetValue(ControlLibConstants.PAGESIZE, typeof(int));
            this.GridHeight = (int)info.GetValue(ControlLibConstants.GRIDHEIGHT, typeof(int));
            this.EnableFilter = (bool)info.GetValue(ControlLibConstants.ENABLEFILTER, typeof(bool));
            this.EnableSorting = (bool)info.GetValue(ControlLibConstants.ENABLESORTING, typeof(bool));
            this.EnableExport = (bool)info.GetValue(ControlLibConstants.ENABLEEXPORT, typeof(bool));
            this.ServerPagination = (bool)info.GetValue(ControlLibConstants.SERVERPAGINATION, typeof(bool));
            this.SelectOption = (bool)info.GetValue(ControlLibConstants.SELECTOPTION, typeof(bool));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ControlLibConstants.PAGESIZE, this.PageSize, typeof(int));
            info.AddValue(ControlLibConstants.GRIDHEIGHT, this.GridHeight, typeof(int));
            info.AddValue(ControlLibConstants.ENABLEFILTER, this.EnableFilter, typeof(bool));
            info.AddValue(ControlLibConstants.ENABLESORTING, this.EnableSorting, typeof(bool));
            info.AddValue(ControlLibConstants.ENABLEEXPORT, this.EnableExport, typeof(bool));
            info.AddValue(ControlLibConstants.SERVERPAGINATION, this.ServerPagination, typeof(bool));
            info.AddValue(ControlLibConstants.SELECTOPTION, this.SelectOption, typeof(bool));
        }
    }
}