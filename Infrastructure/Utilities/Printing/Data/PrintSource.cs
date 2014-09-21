using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    /// <summary>
    /// Information about the source that initiated the print request
    /// </summary>
    [DataContract]
    public class PrintSource
    {
        /// <summary>
        /// Gets or Sets the Location that initiated the print request
        /// </summary>
        [DataMember]
        public string Location
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the site that initiated the print request
        /// </summary>
        [DataMember]
        public string Site
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the terminal that initiated the print request
        /// </summary>
        [DataMember]
        public string Terminal
        {
            get;
            set;
        }
    }
}