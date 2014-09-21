using System;
using System.Collections.Generic;

namespace Controls.Types
{
    public class CriticalFinalizer : IDisposable
    {
        private List<IDisposable> managedDisposables;
        private List<IDisposable> unmanagedDisposables;

        public CriticalFinalizer()
        {
            this.managedDisposables = new List<IDisposable>();
            this.unmanagedDisposables = new List<IDisposable>();
        }

        protected void Register(IDisposable disposable, ResourceType resourceType = ResourceType.Managed)
        {
            switch (resourceType)
            {
                case ResourceType.Managed:
                    this.managedDisposables.Add(disposable);
                    break;

                case ResourceType.Unmanaged:
                    this.unmanagedDisposables.Add(disposable);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("resourceType", "resourceType  is not invalid");
            }
        }

        protected void Register(IEnumerable<IDisposable> disposables, ResourceType resourceType = ResourceType.Managed)
        {
            switch (resourceType)
            {
                case ResourceType.Managed:
                    this.managedDisposables.AddRange(disposables);
                    break;

                case ResourceType.Unmanaged:
                    this.unmanagedDisposables.AddRange(disposables);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("resourceType", "resourceType  is not invalid");
            }
        }

        ~CriticalFinalizer()
        {
            this.Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            foreach (var item in this.unmanagedDisposables)
            {
                item.Dispose();
            }

            if (disposing)
            {
                foreach (var item in this.managedDisposables)
                {
                    item.Dispose();
                }

                GC.SuppressFinalize(this);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }
    }

    public enum ResourceType
    {
        Managed,
        Unmanaged,
    }
}