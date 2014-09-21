using Controls.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Controls.Printing
{
    [Serializable]
    public enum PrintManagerType
    {
        None = 0,
        BallyCentralPrintManager = 1
    }

    [Serializable]
    public class PrintManagerConfig
    {
        [XmlAttribute]
        public string Key
        {
            get;
            set;
        }

        [XmlElement]
        public PrintManagerType PrintManagerType
        {
            get;
            set;
        }
    }
}