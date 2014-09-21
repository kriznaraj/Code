using Controls.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    public class UserTask : IUserTask
    {
        public Int64 TaskId
        {
            get;
            set;
        }

        public string TaskCode
        {
            get;
            set;
        }
    }
}
