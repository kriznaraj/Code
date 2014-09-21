using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal class PrintServiceNull : IPrintService
    {
        public event Action<IPrintResult> OnResponseHandlerNotFound;

        public IPrintResult Print(IPrintData printData)
        {
            throw new NotSupportedException("Print Service is not configured to run. Please check configuration");
        }

        public void PrintAsync(IPrintData printData, Action<IPrintSuccessResult> onPrintSuccess = null, Action<IPrintErrorResult> onPrintFailure = null)
        {
            throw new NotSupportedException("Print Service is not configured to run. Please check configuration");
        }

        /// <summary>
        /// Method Add to Avoid Warning issued. Null Check not done since its private method and the method is not invoked
        /// </summary>
        private void InvokeResponseNotFound()
        {
            this.OnResponseHandlerNotFound(null);
        }
    }
}