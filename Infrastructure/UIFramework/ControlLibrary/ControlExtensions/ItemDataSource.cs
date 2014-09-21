using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    public class ItemDataSource
    {
        public ItemDataSource(IEnumerable<object> dataSource, string valueMember, string displayMember)
        {
            this.DataSource = dataSource;
            this.ValueMember = valueMember;
            this.DisplayMember = displayMember;
        }
        public ItemDataSource(IEnumerable<object> dataSource, string valueMember, string[] displayMembers)
        {
            this.DataSource = dataSource;
            this.ValueMember = valueMember;
            this.DisplayParams = displayMembers;
        }

        
        public IEnumerable<object> DataSource { get; set; }
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }
        public string[] DisplayParams { get; set; }
    }

    
}
