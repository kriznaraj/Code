using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controls.Printing
{
    public interface IPrintManagerNotificationHandler
    {
        /// <summary>
        /// Notifies that print request is completed
        /// </summary>
        /// <param name="printResponse">response of the print job Id</param>
        /// <param name="printJobId">Print Job Id which is completed</param>
        void OnPrintCompleted(PrintJobId printJobId, IPrintSuccessResult printResponse);

        /// <summary>
        /// Notifies that print request is failed
        /// </summary>
        /// <param name="printResponse">response of the print job Id</param>
        /// <param name="printJobId">Print Job Id which is completed</param>
        void OnPrintFailed(PrintJobId printJobId, IPrintErrorResult printResponse);
    }
}