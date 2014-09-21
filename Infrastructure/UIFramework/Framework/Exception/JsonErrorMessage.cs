using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    public class JsonErrorMessage : ViewModelBase
    {
        public string Message { get; set; }

        public List<ExceptionActionConfig> ActionCommand { get; set; }

        public string URI { get; set; }

    }
}
