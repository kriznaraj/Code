using Controls.ControlLibrary;
using Controls.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Controls.Framework
{
    public class ViewModelBase : IModelState
    {
        public List<KeyValuePair<string, string>> ErrorList { get; set; }

        public string ViewName { get; set; }

        private readonly ObjectValidator validator;

        public virtual string ConfigurationKey { get; set; }

        public ViewModelBase()
        {
            this.validator = new ObjectValidator(this, null, ConfigurationKey);
            this.ErrorList = new List<KeyValuePair<string, string>>();
        }

        public bool Validate()
        {
            this.validator.ConfigKey = this.ConfigurationKey;
            if (false == this.validator.Validate())
            {
                this.ErrorList.AddRange(this.validator.ErrorList);
            }

            return this.ErrorList == null ? true : this.ErrorList.Count == 0;
        }

        public string GetErrorMessage(string propertyName)
        {
            string resultStr = string.Empty;
            if (this.ErrorList != null && this.ErrorList.Count > 0)
            {
                var a = this.ErrorList.Where(o => o.Key == propertyName).FirstOrDefault();
                resultStr = a.Value;
            }
            return resultStr;
        }
    }
}
