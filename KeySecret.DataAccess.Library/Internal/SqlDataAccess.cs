using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace KeySecret.DataAccess.Library.Internal
{
    internal class SqlDataAccess
    {
        private IDbConnection _connection;

        public string ConnectionString
            => @"Server=DESKTOP-9QI02R2\LOCALSQLSERVER;DATABASE=KeySecretDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";

        public List<T> LoadData<T, U>(string storedProcedure, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                List<T> rows = connection.Query<T>(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Execute(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}