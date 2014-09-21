namespace Controls.Security
{
    public interface IAPIRequestHeader
    {
        string SessionId { get; }

        string APIName { get; }
    }
}