using System;

namespace Controls.Security
{
    public interface ITaskLimit
    {
        Int16 AuthorizerLevel
        {
            get;
        }

        Int64 AuthorizationLimit
        {
            get;
        }

        Int64 Daily
        {
            get;
        }

        Int64 Transaction
        {
            get;
        }
    }
}