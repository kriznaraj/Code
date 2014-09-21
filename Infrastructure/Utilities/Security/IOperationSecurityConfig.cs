using System;

namespace Controls.Security
{
    public interface IOperationSecurityConfig
    {
        Nullable<Int32> ID { get; }

        Nullable<Int32> OperationID { get; }

        Boolean RequiresSession { get; }
    }
}