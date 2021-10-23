using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library
{
    public static class DBExtensions
    {
        public static T DBToValue<T>(this object value) where T : struct
        {
            if (value != null && value != DBNull.Value)
                return (T)value;

            return default(T);
        }

        public static object ValueToDB<T>(this object value) where T : struct
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

        public static object StringToDb(this object value)
        {
            if (value == null || value == DBNull.Value || Convert.ToString(value).Length == 0)
                return DBNull.Value;

            return Convert.ToString(value);
        }

        public static async Task<Exception> TransactionRollbackAsync(this SqlTransaction transaction, Exception ex, [CallerMemberName] string origin = "")
        {
            try
            {
                await transaction.RollbackAsync();
            }
            catch (Exception ex2)
            {
                return new Exception(
                    $"Description:\tFailed query in {origin}, Rollback also failed." +
                    $"Caller:\t{origin}\r\n" +
                    $"Message:\t{ex2.Message}\r\n" +
                    "=> " + ex2.StackTrace);
            }

            return new Exception(
                    $"Description:\tFailed query in {origin}, Rollback successful." +
                    $"Caller:\t{origin}\r\n" +
                    $"Message:\t{ex.Message}\r\n" +
                    "=> " + ex.StackTrace);
        }
    }
}