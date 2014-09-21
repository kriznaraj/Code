using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    public class TimePropertyBag : ITimePropertyBag, ISerializable
    {
        private string _maxTime = "24:00";
        private bool _showAmPm;
        private int _step;

        #region "Constuctor"

        public TimePropertyBag()
        {            
            
        }

        public TimePropertyBag(bool ShowAmPm, string TimeFormat, bool ShowDuration, int Step, string TimeCssClass)
        {
            this.ShowAmPm = ShowAmPm;
            this.TimeFormat = TimeFormat;
            this.ShowDuration = ShowDuration;
            this.Step = Step;
            this.TimeCssClass = TimeCssClass;
        }

        public TimePropertyBag(SerializationInfo info, StreamingContext context)
        {
            this.ShowAmPm = (bool)info.GetValue(ControlLibConstants.SHOW_AM_PM, typeof(bool));
            this.TimeFormat = (string)info.GetValue(ControlLibConstants.TIME_FORMAT, typeof(string));
            this.ShowDuration = (bool)info.GetValue(ControlLibConstants.SHOW_DURATION, typeof(bool));
            this.Step = (int)info.GetValue(ControlLibConstants.STEP, typeof(int));
            this.TimeCssClass = (string)info.GetValue(ControlLibConstants.TIME_CSS_CLASS, typeof(string));
        }

        #endregion

        #region "Implemented Properties - ITimePropertyBag"

        [XmlAttribute("ShowAmPm")]
        public bool ShowAmPm
        {
            get
            {
                return _showAmPm;
            }
            set
            {
                if (value)
                {
                    TimeFormat = "h:i:a";
                }
                else
                {
                    TimeFormat = "H:i";
                }
                this._showAmPm = value;
            }
        }

        [XmlAttribute("TimeFormat")]
        public string TimeFormat { get; set; }

        [XmlAttribute("ShowDuration")]
        public bool ShowDuration { get; set; }

        [XmlAttribute("Step")]
        public int Step 
        {
            get
            {
                return _step; ;
            }
            set
            {
                if (value > 0)
                {
                    _step = value;
                }
                else
                {
                    _step = 5;
                }
            }
        }

        [XmlAttribute("TimeCssClass")]
        public string TimeCssClass { get; set; }

        #endregion

        #region "Implemented methods"

        #endregion

        public string GetTimeProperties()
        {
            StringBuilder json = new StringBuilder();

            //json.AppendFormat(@"{{""minTime"": ""{0}"", ""maxTime"": ""{1}"", ""showDuration"": {2}, ""step"": ""{3}"", ""timeFormat"": ""{4}""}}", "00:00", _maxTime, ShowDuration.ToString().ToLower(), Step, TimeFormat);

            json.AppendFormat(@"[""{0}"", ""{1}"", {2}, {3}, ""{4}""]", "00:00", _maxTime, ShowDuration.ToString().ToLower(), Step, TimeFormat);
            return json.ToString();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ControlLibConstants.SHOW_AM_PM, this.ShowAmPm, typeof(bool));
            info.AddValue(ControlLibConstants.TIME_FORMAT, this.TimeFormat, typeof(string));
            info.AddValue(ControlLibConstants.SHOW_DURATION, this.ShowDuration, typeof(bool));
            info.AddValue(ControlLibConstants.STEP, this.Step, typeof(int));
            info.AddValue(ControlLibConstants.TIME_CSS_CLASS, this.TimeCssClass, typeof(string));
        }


    }
}