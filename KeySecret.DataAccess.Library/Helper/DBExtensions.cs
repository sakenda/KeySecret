using System;

namespace KeySecret.DataAccess.Library.Helper
{
    internal static class DBExtensions
    {
        internal static T DBToValue<T>(this object value) where T : struct
        {
            if (value != null && value != DBNull.Value)
                return (T)value;

            return default(T);
        }

        internal static object ValueToDB<T>(this object value) where T : struct
        {
            if (value == null)
                return DBNull.Value;

            if (Nullable.GetUnderlyingType(value.GetType()) != null)
            {
                if (!((T?)value).HasValue)
                    return DBNull.Value;
            }

            return (T)value;
        }

        internal static object StringToDb(this object value)
        {
            if (value == null || value == DBNull.Value || Convert.ToString(value).Length == 0)
                return DBNull.Value;

            return Convert.ToString(value);
        }

    }
}
