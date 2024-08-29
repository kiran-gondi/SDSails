namespace SDSails.Core.Data.Utility
{
    using System;

    public static class DbTypeValue
    {
        public static object ToDbValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else if (value.GetType() == typeof(string) && string.IsNullOrEmpty(value.ToString()))
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }
    }
}
