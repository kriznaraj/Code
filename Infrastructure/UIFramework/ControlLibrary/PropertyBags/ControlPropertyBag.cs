using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    internal class ControlPropertyBag
    {
        #region "Member Variables"

        internal FillerParams _fillerParams;

        #endregion

        public ControlPropertyBag(FillerParams fillerParams)
        {
            
            this._fillerParams = fillerParams;
            this.IsBindingControl = true;
        }
        internal virtual void Accept(ControlPropertyFiller filler)
        {
            filler.Fill(this);
        }

        public string ValidationErrorCssClass { get; set; }

        public string ControlErrorCssClass { get; set; }

        public StylePropertyBag Style { get; set; }

        public short TabIndex { get; set; }

        public bool IsDirty { get; set; }

        public string ErrorMessage { get; set; }

        public string ToolTip { get; set; }

        public string Label { get; set; }

        public string WaterMarkText { get; set; }

        public string ControlName { get; set; }

        public bool ReadOnly { get; set; }

        public bool Enabled { get; set; }

        public bool Visibility { get; set; }

        public string CssClass { get; set; }

        public List<ValidationBase> Validators { get; set; }

        public bool IsBindingControl { get; set; }

        public IDictionary<string, object> Attributes { get; set; }

        public bool Mandatory { get; set; }
    }
}