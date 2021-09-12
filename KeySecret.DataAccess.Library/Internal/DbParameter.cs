using System.Data;

namespace KeySecret.DataAccess.Library.Internal
{
    internal class DbParameter
    {
        public string Name { get; set; }
        public SqlDbType Type { get; set; }
        public object Value { get; set; }

        public DbParameter(string name, SqlDbType type, object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }
    }
}
