using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    /// <summary>
    /// Print Job Id to uniquely identify a job submitted to the print manager
    /// </summary>
    [DataContract]
    public class PrintJobId
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Key
        /// </summary>
        [DataMember]
        public string Key { get; set; }

        public override bool Equals(object obj)
        {
            return obj.ToString() == this.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return this.Id + "_" + this.Key;
        }
    }
}