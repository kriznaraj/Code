using System;
using System.Text;
using System.Web.Mvc;

namespace Controls.ControlLibrary
{
    internal class DateTimeHTMLEmitter : ControlHTMLEmitter
    {
        #region "Member Variables"

        private DateTimePropertyBag _propertyBag;
        private string _controlID = string.Empty;
        private string _dateTagID = string.Empty;
        private string _timeTagID = string.Empty;

        private TagBuilder _hiddenTag;
        private string _script = string.Empty;

        #endregion

        #region "Constructors"

        public DateTimeHTMLEmitter(string value, DateTimePropertyBag propertyBag)
            : base(propertyBag.Validators, propertyBag.Mandatory)
        {
            this._propertyBag = propertyBag;
            this.Value = value;
        }

        #endregion

        #region "Properties"

        #endregion

        #region "Implemented Methods"

        public override void Emit(out string controlHTMLString)
        {
            controlHTMLString = string.Empty;
            if (_propertyBag.Visibility)
            {
                TagBuilder divTag = new TagBuilder(TAG_DIV);
                StringBuilder control = new StringBuilder();
                control.Append(buildSpan());
                control.Append(buildScript());
                divTag.InnerHtml = control.ToString();
                controlHTMLString = divTag.ToString();
            }
        }

        #endregion

        #region "Private Methods"


        private string buildSpan()
        {
            //TagBuilder spanTag = new TagBuilder(TAG_SPAN);
            StringBuilder control = new StringBuilder();
            control.Append(buildDateTime());
            TagBuilder errorDiv = new TagBuilder(TAG_DIV);
            errorDiv.InnerHtml = GetErrorLabel(this._propertyBag);
            control.Append(errorDiv.ToString());
            //spanTag.InnerHtml = control.ToString();
            //return spanTag.ToString();
            return control.ToString();
        }



        private string buildDateTag(string value)
        {
            TagBuilder dateTag = new TagBuilder(TAG_INPUT);
            this.SetAttribute(dateTag, ATTRIBUTE_TYPE, ATTR_VAL_TEXT);
            this.SetAttribute(dateTag, ATTRIBUTE_VALUE, value);
            this.SetAttribute(dateTag, ATTRIBUTE_READONLY, VAL_TRUE);
            this.SetAttribute(dateTag, ATTRIBUTE_PLACEHOLDER, this._propertyBag.WaterMarkText);
            
            this.SetControlCssClasses(dateTag, this._propertyBag.DateProperties.DateCssClass, this._propertyBag.ControlErrorCssClass);
            this.SetID(dateTag, "date_" + this._propertyBag.ControlName, out _dateTagID);
            //this.SetFunction(dateTag, ATTRIBUTE_ONLEAVE, propertyBag.OnLeaveDateFunction);
            return dateTag.ToString(TagRenderMode.SelfClosing);
        }
        private void splitValue(string value, out string dateVal, out string timeVal)
        {
            if (false == string.IsNullOrEmpty(value))
            {
                value = value.Trim();
                int splitPoint = value.IndexOf(':') - 2;
                dateVal = value.Substring(0, splitPoint).Trim();
                timeVal = value.Substring(splitPoint).Trim();
            }
            else
            {
                dateVal = string.Empty;
                timeVal = string.Empty;
            }
        }

        private string buildTimeTag(string value)
        {
            TagBuilder timeTag = new TagBuilder(TAG_INPUT);
            this.SetAttribute(timeTag, ATTRIBUTE_TYPE, ATTR_VAL_TEXT);
            this.SetAttribute(timeTag, ATTRIBUTE_VALUE, value);
            this.SetControlCssClasses(timeTag, this._propertyBag.TimeProperties.TimeCssClass, this._propertyBag.ControlErrorCssClass);
            this.SetID(timeTag, "time_" + this._propertyBag.ControlName, out _timeTagID);
            //this.SetFunction(timeTag, ATTRIBUTE_ONLEAVE, propertyBag.OnLeaveTimeFunction);
            return timeTag.ToString(TagRenderMode.SelfClosing);
        }


        private string buildDateTime()
        {
            DateTime dt = Convert.ToDateTime(Value);
            StringBuilder control = new StringBuilder();
            //this.BuildHiddenTag(this._propertyBag.ControlName, this._propertyBag.UTCDateTime.ToString(), out _hiddenTag);
            if (dt != DateTime.MinValue) // this need to be move to control extension
            {
                this.BuildHiddenTag(this._propertyBag.ControlName, dt.ToString(), out _hiddenTag);
            }
            else {

                this.BuildHiddenTag(this._propertyBag.ControlName, string.Empty, out _hiddenTag);
            }
            if (true == this._propertyBag.ShowDate && false == this._propertyBag.ShowTime)
            {
                if (dt != DateTime.MinValue) // this need to be move to control extension
                {
                    control.Append(buildDateTag(dt.ToShortDateString()));
                }
                else
                {
                    control.Append(buildDateTag(string.Empty));
                }
                this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_DATEPROP, _propertyBag.DateProperties.GetDateProperties());
                this.SetFunction(_hiddenTag, ATTRIBUTE_DATA_ONCHANGE, _propertyBag.OnChangeFunction);
            }
            else if (false == this._propertyBag.ShowDate && true == this._propertyBag.ShowTime)
            {
                if (dt != DateTime.MinValue) // this need to be change
                {
                    control.Append(buildTimeTag(dt.ToShortTimeString()));
                }
                else
                {
                    control.Append(buildTimeTag(string.Empty));
                }
                this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_TIMEPROP, _propertyBag.TimeProperties.GetTimeProperties());
                this.SetFunction(_hiddenTag, ATTRIBUTE_DATA_ONCHANGE, _propertyBag.OnChangeFunction);
            }
            else
            {
               // DateTime dt;
                //DateTime.TryParse(Value, out dt);
                //string dateval = dt != null ? dt.ToShortDateString() : "Error";
                //string timeVal = dt != null ? dt.ToShortTimeString() : "Error";
                //this.splitValue(Value, out dateval, out timeVal);
                if (dt != DateTime.MinValue)
                {
                    control.Append(buildDateTag(dt.ToShortDateString()));
                    control.Append(buildTimeTag(dt.ToShortTimeString()));
                }
                else
                {
                    control.Append(buildDateTag(string.Empty));
                    control.Append(buildTimeTag(string.Empty));
                }
                this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_DATEPROP, _propertyBag.DateProperties.GetDateProperties());
                this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_TIMEPROP, _propertyBag.TimeProperties.GetTimeProperties());
                this.SetFunction(_hiddenTag, ATTRIBUTE_DATA_ONCHANGE, _propertyBag.OnChangeFunction);
                this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_HASDATEANDTIME, VAL_TRUE);
            }

            this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_DATETIME, VAL_TRUE);
            //this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_UTCConversion, _propertyBag.UTCConversion.ToString().ToLower());
            //this.SetAttribute(_hiddenTag, ATTRIBUTE_DATA_DateKind, _propertyBag.DateTimeKind.ToString().ToLower());

            this.SetValidations(_hiddenTag, _propertyBag.ReadOnly);
            control.Append(_hiddenTag.ToString(TagRenderMode.SelfClosing));

            return control.ToString();
        }



        private string buildScript()
        {
            TagBuilder ScriptTag = new TagBuilder(TAG_SCRIPT);
            this.SetAttribute(ScriptTag, ATTRIBUTE_LANG, SCRIPT_NAME);

            StringBuilder script = new StringBuilder();

            ScriptTag.InnerHtml = script.ToString();
            //return Convert.ToString(ScriptTag);
            return string.Empty;
        }

        #endregion

    }
}
