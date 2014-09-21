using Controls.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.ControlLibrary
{
    public class AccessPolicy : IAccessPolicy
    {
        //public string SecurityCode
        //{
        //    get;
        //    set;
        //}

        //public string SecurityName
        //{
        //    get;
        //    set;
        //}

        public bool ReadOnly
        {
            get;
            set;
        }

        public bool Masking
        {
            get;
            set;
        }

        public bool Enabled
        {
            get;
            set;
        }


        public bool Visibility
        {
            get;
            set;
        }
    }
}
