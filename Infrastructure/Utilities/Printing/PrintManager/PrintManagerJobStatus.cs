using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    /// <summary>
    /// Contract to return the status of the print job
    /// </summary>
    [DataContract]
    public class PrintManagerJobStatus
    {
        /// <summary>
        /// Gets or Sets the Reason for the failure
        /// </summary>
        [DataMember]
        public FailReason Reason { get; set; }

        /// <summary>
        /// Gets or Sets the reason code for the failure
        /// </summary>
        [DataMember]
        public string ReasonCode { get; set; }

        /// <summary>
        /// Sets or Gets the status of the job
        /// </summary>
        [DataMember]
        public bool Status { get; set; }
    }
}