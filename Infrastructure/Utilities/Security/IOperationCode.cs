using System;

namespace Controls.Security
{
    /// <summary>
    /// Operation that represents Task / Transaction
    /// </summary>
    public interface IOperation
    {
        Int32 ID { get; }

        Int32 TranCodeId { get; }
    }
}