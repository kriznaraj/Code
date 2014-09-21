using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Controls.ControlLibrary
{
    [Serializable]
    public class DatePropertyBag : IDatePropertyBag, ISerializable
    {
        private string _dateFormat;

        #region "Constuctor"

        public DatePropertyBag()
        {
        }

        public DatePropertyBag(string DateFormat, int NumberOfMonths, bool ShowButtonPanel, string MaxDate, string MinDate,
            bool ChangeMonth, bool ChangeYear, bool ChangeDate, string DateCssClass, string yearRange)
        {
            this.DateFormat = DateFormat;
            this.NumberOfMonths = NumberOfMonths;
            this.ShowButtonPanel = ShowButtonPanel;
            this.MaxDate = MaxDate;
            this.MinDate = MinDate;
            this.ChangeMonth = ChangeMonth;
            this.ChangeYear = ChangeYear;
            this.ChangeDate = ChangeDate;
            this.DateCssClass = DateCssClass;
            this.YearRange = yearRange;
        }

        public DatePropertyBag(SerializationInfo info, StreamingContext context)
        {
            this.DateFormat = (string)info.GetValue(ControlLibConstants.DATE_FORMAT, typeof(string));
            this.NumberOfMonths = (int)info.GetValue(ControlLibConstants.NO_OF_MONTHS, typeof(int));
            this.ShowButtonPanel = (bool)info.GetValue(ControlLibConstants.SHOW_BUTTON_PANEL, typeof(bool));
            this.MaxDate = (string)info.GetValue(ControlLibConstants.MAX_DATE, typeof(int));
            this.MinDate = (string)info.GetValue(ControlLibConstants.MIN_DATE, typeof(string));
            this.ChangeMonth = (bool)info.GetValue(ControlLibConstants.CHANGE_MONTH, typeof(bool));
            this.ChangeYear = (bool)info.GetValue(ControlLibConstants.CHANGE_YEAR, typeof(bool));
            this.ChangeDate = (bool)info.GetValue(ControlLibConstants.CHANGE_DATE, typeof(bool));
            this.DateCssClass = (string)info.GetValue(ControlLibConstants.DATE_CSS_CLASS, typeof(string));
        }

        #endregion "Constuctor"

        #region "Implemented Properties - IDatePropertyBag"

        [XmlAttribute("DateFormat")]
        public string DateFormat
        {
            get
            {
                return ValidateDateFormat(this._dateFormat);
            }
            set
            {
                this._dateFormat = value;
            }
        }

        [XmlAttribute("NumberOfMonths")]
        public int NumberOfMonths { get; set; }

        [XmlAttribute("ShowButtonPanel")]
        public bool ShowButtonPanel { get; set; }

        [XmlAttribute("MaxDate")]
        public string MaxDate { get; set; }

        [XmlAttribute("MinDate")]
        public string MinDate { get; set; }

        [XmlAttribute("ChangeMonth")]
        public bool ChangeMonth { get; set; }

        [XmlAttribute("ChangeYear")]
        public bool ChangeYear { get; set; }

        [XmlAttribute("ChangeDate")]
        public bool ChangeDate { get; set; }

        [XmlAttribute("DateCssClass")]
        public string DateCssClass { get; set; }

        public string YearRange { get; set; }

        #endregion "Implemented Properties - IDatePropertyBag"

        #region "Private Methods"

        private string toJsonString(object val)
        {
            if (val != null)
            {
                return val.ToString().ToLower();
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion "Private Methods"

        #region "Public Methods"

        public string GetDateProperties()
        {
            StringBuilder json = new StringBuilder();

            json.AppendFormat("[{0}, {1}, {2}, {3}, \"{4}\", \"{5}\", \"{6}\", \"{7}\"]", toJsonString(ChangeMonth), toJsonString(ChangeYear), toJsonString(ChangeDate), toJsonString(ShowButtonPanel), toJsonString(DateFormat), toJsonString(MinDate), toJsonString(MaxDate), toJsonString(YearRange));

            return json.ToString();
        }

        public string ValidateDateFormat(string format)
        {
            List<string> validDateFormats = new List<string>() { "mm/dd/yy", "dd/mm/yy", "yy/mm/dd", "yy-mm-dd", "dd-mm-yy", "mm-dd-yy", "d M, y", "d MM, y", "DD, d MM, yy", "", "dd-MM", "mm-yy" };

            if (validDateFormats.Contains(format))
            {
                return format;
            }
            else
            {
                return "yy-mm-dd";
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(ControlLibConstants.DATE_FORMAT, this.DateFormat, typeof(string));
            info.AddValue(ControlLibConstants.NO_OF_MONTHS, this.NumberOfMonths, typeof(int));
            info.AddValue(ControlLibConstants.SHOW_BUTTON_PANEL, this.ShowButtonPanel, typeof(bool));
            info.AddValue(ControlLibConstants.MAX_DATE, this.MaxDate, typeof(string));
            info.AddValue(ControlLibConstants.MIN_DATE, this.MinDate, typeof(string));
            info.AddValue(ControlLibConstants.CHANGE_MONTH, this.ChangeMonth, typeof(bool));
            info.AddValue(ControlLibConstants.CHANGE_YEAR, this.ChangeYear, typeof(bool));
            info.AddValue(ControlLibConstants.CHANGE_DATE, this.ChangeDate, typeof(bool));
            info.AddValue(ControlLibConstants.DATE_CSS_CLASS, this.DateCssClass, typeof(string));
        }

        #endregion "Public Methods"
    }
}