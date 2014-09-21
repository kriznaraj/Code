using System;
using Controls.Types;

namespace Controls.Logging
{
    public class TypeFormatter : IFormatter
    {
        public string ToString(object type)
        {
            string retValue = string.Empty;
            if (type is Exception)
            {
                retValue = (type as Exception).GetExceptionMessage();
            }
            else
            {
                retValue = type.ToString();
            }

            return retValue;
        }
    }
}