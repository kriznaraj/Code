namespace Controls.Security
{
    public interface IOperationSecurityConfigProvider
    {
        IOperationSecurityConfig Get(string apiName);
    }
}