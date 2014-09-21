using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    /// <summary>
    /// The callback interface to be used by print manager for updating
    /// </summary>
    [ServiceContract]
    public interface IPrintManagerCallback
    {
        /// <summary>
        /// Notifies that the given print job failed
        /// </summary>
        /// <param name="printJobId">Print Job id that is failed</param>
        /// <param name="reasonCode">Reason code for the failure</param>
        /// <param name="reason">Error number</param>
        [OperationContract]
        void NotifyFailure(PrintJobId printJobId, int reason, string reasonCode);

        /// <summary>
        /// Notifies the given print job is printed successfully
        /// </summary>
        /// <param name="printJobId">Print Job id that is printed</param>
        [OperationContract]
        void NotifySuccess(PrintJobId printJobId);
    }
}