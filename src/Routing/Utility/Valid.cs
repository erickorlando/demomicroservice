using System;

namespace Utility
{
    public static class Valid
    {
        public static int ToInt(this string value)
        {
            return Convert.ToInt32(string.IsNullOrEmpty(value) ? "-1" : value.Trim());
        }

        public static decimal ToDecimal(this string value)
        {
            return Convert.ToInt32(string.IsNullOrEmpty(value) ? "0.00" : value.Trim());
        }

        public static string ToDecimalString(this string value, string format)
        {
            return Convert.ToDecimal(string.IsNullOrEmpty(value) ? "0.00" : value.Trim()).ToString(format);
        }

        public static string isNull(this string value)
        {
            return (value == null || value.Equals("null")) ? string.Empty : value.Trim();
        }

    }
}
