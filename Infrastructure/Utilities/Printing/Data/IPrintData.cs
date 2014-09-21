using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    /// <summary>
    /// Interface to be implemented by the object that is to be printed
    /// </summary>
    public interface IPrintData
    {
        /// <summary>
        /// Gets the print job information for the job
        /// </summary>
        PrintJobId PrintJobIInfo { get; }

        /// <summary>
        /// Gets the setting information for the print job
        /// </summary>
        PrintSettings Settings { get; }

        /// <summary>
        /// Gets the information about the source that initiated the print request
        /// </summary>
        PrintSource Source { get; }

        /// <summary>
        /// Gets the data source that has to be used for printing
        /// </summary>
        /// <returns></returns>
        DataSet GetDatasource();
    }
}