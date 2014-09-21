namespace Controls.Types
{
    public class QueryCriteria : IQueryCriteria
    {
        public ICriteria Root
        {
            get;
            set;
        }

        public string QueryKey
        {
            get;
            set;
        }
    }
}