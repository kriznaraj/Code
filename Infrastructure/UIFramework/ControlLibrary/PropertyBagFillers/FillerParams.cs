using System;
using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    internal class FillerParams
    {
        public FillerParams(string modelName, string propertyName, Dictionary<string, string> overrideSettings, bool? isEnabled = null, bool? isReadOnly = null, IEnumerable<object> list = null, string valueMember = null, string displayMember = null, string[] disabled = null, string[] param = null, string templateKeyName = "", bool skipValidationFill = false, bool skipSecurityFill = false, bool skipBehaviourFill = false, bool isBindingControl = true, bool alignLeft = false, string externalizationKey = "", ListBoxType listType = ListBoxType.None, IDictionary<string, object> inputParam = null, IDictionary<string, object> attributes = null, string configKey = "", DateTime? minDate = null, DateTime? maxDate = null, List<Security> userTaskCodes = null)
        {
            this.ModelName = modelName;
            this.PropertyName = GetFormattedPropertyName(propertyName);
            this.ControlName = propertyName;
            this.OverrideSettings = overrideSettings;
            this.IsEnabled = isEnabled;
            this.IsReadOnly = isReadOnly;
            this.ValueMember = valueMember;
            this.DisplayMember = displayMember;
            this.Disabled = disabled;
            this.List = list;
            this.Param = param;
            this.TemplateNameKey = templateKeyName;
            this.SkipBehaviourFill = skipBehaviourFill;
            this.SkipSecurityFill = skipSecurityFill;
            this.SkipValidationFill = skipValidationFill;
            this.IsBindingControl = isBindingControl;
            this.AlignLeft = alignLeft;
            this.ExternalizationKey = externalizationKey;
            this.ListType = listType;
            this.InputParam = inputParam;
            this.Attributes = attributes;
            this.ConfigKey = configKey;
            this.minDate = minDate;
            this.maxDate = maxDate;
            this.UserTaskCodes = userTaskCodes;
        }

        public Dictionary<string, string> OverrideSettings { get; private set; }

        public string ControlName { get; private set; }

        public string PropertyName { get; private set; }

        public string ModelName { get; private set; }

        public IEnumerable<object> List { get; private set; }

        public string DisplayMember { get; private set; }

        public string ValueMember { get; private set; }

        public string[] Disabled { get; private set; }

        public string[] Param { get; private set; }

        public string TemplateNameKey { get; private set; }

        public bool SkipValidationFill { get; private set; }

        public bool SkipSecurityFill { get; private set; }

        public bool SkipBehaviourFill { get; private set; }

        public bool IsBindingControl { get; private set; }

        public bool? IsEnabled { get; private set; }

        public bool? IsReadOnly { get; private set; }

        public bool AlignLeft { get; private set; }

        public string ExternalizationKey { get; private set; }

        public ListBoxType ListType { get; private set; }

        public IDictionary<string, object> InputParam { get; set; }

        public IDictionary<string, object> Attributes { get; set; }

        public string ConfigKey { get; set; }

        public List<Security> UserTaskCodes { get; set; }

        public DateTime? minDate { get; set; }

        public DateTime? maxDate { get; set; }

        private string GetFormattedPropertyName(string propertyName)
        {
            while (propertyName.Contains("[") && propertyName.Contains("]"))
            {
                propertyName = propertyName.Remove(propertyName.IndexOf("["), (propertyName.IndexOf("]") - propertyName.IndexOf("[")) + 1);
            }
            return propertyName;
        }
    }
}