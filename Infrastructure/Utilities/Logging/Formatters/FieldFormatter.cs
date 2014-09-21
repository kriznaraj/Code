namespace Controls.Logging
{
    public class FieldFormatter : IFormatter
    {
        public string ToString(object type)
        {
            string retValue = string.Empty;
            if (null != type)
            {
                retValue = type.ToString();
            }

            return retValue;
        }
    }
}