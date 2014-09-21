
using System;
namespace BallyTech.Infrastructure.Session
{
    /// <summary>
    /// Operation that represents Task / Transaction
    /// </summary>
    public interface IOperation
    {
        Int32 ID { get; }
    }
}