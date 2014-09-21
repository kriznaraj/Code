using System;

namespace BallyTech.Infrastructure.Communication
{
    /// <summary>
    /// Transport, Protocol agnostic message dispatcher based on Reactor pattern.
    /// </summary>
    internal interface IReactor
    {
        void AddResult(IAsyncResult ar, Action<IAsyncResult, Object> callback, Object state);

        void Run();

        void Shutdown();
    }
}