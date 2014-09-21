namespace Controls.ExceptionHandling
{
    public interface ISafeBlockProvider
    {
        ISafeActionBlock Create(string exceptionHandlePolicy);

        ISafeActionReturnBlock CreateResult(string exceptionHandlePolicy);
    }
}