using System;
using System.Threading;
using System.Threading.Tasks;

namespace BallyTech.Infrastructure.Hosting
{
    public sealed class ServiceHostController
    {
        private readonly ManualResetEvent _waitHandle;
        private readonly IServiceHost _host;

        public Boolean Running { get; private set; }

        public ServiceHostController(IServiceHost host)
        {
            _host = host;
            _waitHandle = new ManualResetEvent(false);
        }

        public void StartHost()
        {
            if (Running) return;

            Task.Factory.StartNew(Start);
        }

        public void StopHost()
        {
            if (!Running) return;

            _waitHandle.Set();
            _waitHandle.Reset();
            _host.Shutdown();
            this.Running = false;
        }

        private void Start()
        {
            _host.Run();
            this.Running = true;
            _waitHandle.WaitOne();
        }
    }
}