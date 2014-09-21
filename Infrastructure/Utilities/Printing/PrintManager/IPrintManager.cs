using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    /// <summary>
    /// Interface to be implemented by the print manager for printing
    /// </summary>
    [ServiceContract]
    public interface IPrintManager
    {
        /// <summary>
        /// Cancel the printing of the job if not completed
        /// </summary>
        /// <param name="printJobId">Print job to be cancelled</param>
        /// <returns>Successfully cancelled or not</returns>
        [OperationContract]
        bool CancelPrint(PrintJobId printJobId);

        /// <summary>
        /// Gets the status of the submitted print job
        /// </summary>
        /// <param name="printJobId">Print job to get the status</param>
        /// <returns>Returns the status of the print job</returns>
        [OperationContract]
        PrintManagerJobStatus GetStatus(PrintJobId printJobId);

        /// <summary>
        /// Prints the data to the printer for the given print data
        /// </summary>
        /// <param name="printJobId">Id to uniquely identify the print job</param>
        /// <param name="printSource">Source from which the print is initiated</param>
        /// <param name="settings">Settings for the print job</param>
        /// <param name="dataSource">Data source to be used for printing</param>
        [OperationContract]
        void Print(PrintJobId printJobId, PrintSource printSource, PrintSettings settings, DataSet dataSource);
    }
}