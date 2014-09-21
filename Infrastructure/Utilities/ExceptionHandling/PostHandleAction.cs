namespace Controls.ExceptionHandling
{
    public enum PostHandleAction : short
    {
        None = 0,
        Swallow,
        Throw,
        Rethrow,
        Retry,
        InvokeOnFailure,
        Convert
    }
}