namespace Controls.Types
{
    public interface IQueryCriteria
    {
        string QueryKey { get; }

        ICriteria Root { get; }
    }
}