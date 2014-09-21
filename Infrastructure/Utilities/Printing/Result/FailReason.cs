using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    [DataContract]
    public enum FailReason
    {
        [EnumMember]
        Unknown = 0,

        [EnumMember]
        PrinterNotFound = 1,

        [EnumMember]
        PrinterUnavailable = 2,

        [EnumMember]
        OutOfPaper = 3,

        [EnumMember]
        ServiceUnavailable = 4,

        [EnumMember]
        Timeout = 5
    }
}