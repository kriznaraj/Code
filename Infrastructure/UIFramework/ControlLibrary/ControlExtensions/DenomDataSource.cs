using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    public class DenomDataSource
    {
        public DenomDataSource(Dictionary<string, Dictionary<string, string>> dataSource, string primaryKey)
        {
            this.DataSource = dataSource;
            this.PrimaryKey = primaryKey;
        }

        public DenomDataSource(Dictionary<string, Dictionary<string, string>> dataSource, string primaryKey, string undefinedColumnName, string undefinedColumnValue="0")
        {
            this.DataSource = dataSource;
            this.PrimaryKey = primaryKey;
            this.UndefinedColumnName = undefinedColumnName;
            this.UndefinedColumnValue = undefinedColumnValue;
        }
        //public DenomDataSource(IDictionary<string, IDictionary<string, string>> dataSource, string valueMember, string[] displayMembers)
        //{
        //    this.DataSource = dataSource;
        //    this.ValueMember = valueMember;
        //}
        
        public Dictionary<string,Dictionary<string,string>> DataSource { get; set; }
        public string PrimaryKey { get; set; }
        public string UndefinedColumnName { get; set; }
        public string UndefinedColumnValue { get; set; }
    }

    
}
