using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    /// <summary>
    /// Interface to be implemented by the print service
    /// </summary>
    public interface IPrintService
    {
        /// <summary>
        /// Event will be invoked to identify the suitable handler for the given print response in case of system failure
        /// </summary>
        event Action<IPrintResult> OnResponseHandlerNotFound;

        /// <summary>
        /// Prints the data synchronously
        /// </summary>
        /// <exception cref="PrintServiceException">Throws on failure to print</exception>
        /// <param name="printData">Data to be printed</param>
        /// <returns>Returns the print result based on the status</returns>
        IPrintResult Print(IPrintData printData);

        /// <summary>
        /// Prints the data Asynchronously
        /// </summary>
        /// <param name="printData">Data to be printed</param>
        /// <param name="onPrintFailure">Delegate to invoke on failure of the printing</param>
        /// <param name="onPrintSuccess">Delegate to invoke on success of printing</param>
        void PrintAsync(IPrintData printData, Action<IPrintSuccessResult> onPrintSuccess = null, Action<IPrintErrorResult> onPrintFailure = null);
    }
}