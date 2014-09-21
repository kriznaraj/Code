using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    /// <summary>
    /// Settings information needed for the print job
    /// </summary>
    [DataContract]
    public class PrintSettings
    {
        /// <summary>
        /// Gets or Sets the no of copies to be printed
        /// </summary>
        [DataMember]
        public int NoOfCopies
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets the Printer to be specifically used for printing
        /// </summary>
        [DataMember]
        public string PrinterName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the timeout for the completion of the print job
        /// </summary>
        [DataMember]
        public TimeSpan Timeout
        {
            get;
            set;
        }
    }
}