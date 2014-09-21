using System.Text;

namespace Controls.ControlLibrary
{
    public class StylePropertyBag
    {
        public StylePropertyBag()
        {

        }
        public StylePropertyBag(string Width="", string Height="")
        {
            this.Width = Width;
            this.Height = Height;
        }

        public string Width { get; set; }

        public string Height { get; set; }

        public string GetStyle()
        {
            StringBuilder result = new StringBuilder();
            if (false == string.IsNullOrEmpty(this.Height))
            {
                result.AppendFormat("height:{0} !important;", this.Height);
            }
            if (false == string.IsNullOrEmpty(this.Width))
            {
                result.AppendFormat("width:{0} !important;", this.Width);
            }
            return result.ToString();
        }
    }
}