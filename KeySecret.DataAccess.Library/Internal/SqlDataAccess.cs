using KeySecret.DataAccess.Library.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library.Internal
{
    internal class SqlDataAccess
    {
        /// <summary>
        /// Executes a database query
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="sql">SQL Query as string</param>
        /// <param name="parameters">Parameters for executing the query</param>
        /// <returns>Void</returns>
        public async Task ExecuteQueryVoidAsync(string connectionString, string sql, DbParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                        cmd.Parameters.Add(parameter.Name, parameter.Type).Value = parameter.Value;
                }

                await cmd.ExecuteNonQueryAsync();
            }
        }
        /// <summary>
        /// Executes a database query
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="sql">SQL Query as string</param>
        /// <param name="parameter">A single parameter for executing the query</param>
        /// <returns>Void</returns>
        public async Task ExecuteQueryVoidAsync(string connectionString, string sql, DbParameter parameter = null)
            => await ExecuteQueryVoidAsync(connectionString, sql, new DbParameter[] { parameter });

        /// <summary>
        /// Executes a database query and returns the table
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="sql">SQL Query as string</param>
        /// <param name="parameters">Parameters for executing the query</param>
        /// <returns>List of AccountModels</returns>
        public async Task<List<AccountModel>> ExecuteQueryGetItems(string connectionString, string sql, DbParameter[] parameters = null)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        if (parameter == null)
                            continue;

                        cmd.Parameters.Add(parameter.Name, parameter.Type).Value = parameter.Value;
                    }
                }

                var list = new List<AccountModel>();
                var query = await cmd.ExecuteReaderAsync();

                while (query.Read())
                {
                    list.Add(new AccountModel
                    {
                        Id = Convert.ToInt32(query["Id"]),
                        Name = query["Name"].ToString(),
                        WebAdress = query["WebAdress"].ToString(),
                        Password = query["Password"].ToString(),
                        CreatedDate = Convert.ToDateTime(query["CreatedDate"])
                    });
                }

                return list;
            }
        }
        /// <summary>
        /// Executes a database query and returns the table
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="sql">SQL Query as string</param>
        /// <param name="parameter">A single parameter for executing the query</param>
        /// <returns>List of AccountModels</returns>
        public async Task<List<AccountModel>> ExecuteQueryGetItems(string connectionString, string sql, DbParameter parameters = null)
            => await ExecuteQueryGetItems(connectionString, sql, new DbParameter[] { parameters });

        /// <summary>
        /// Executes a database query and returns a AccountModel
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="sql">SQL Query as string</param>
        /// <param name="parameters">Parameters for executing the query</param>
        /// <returns>AccountModel</returns>
        public async Task<AccountModel> ExecuteQueryGetItem(string connectionString, string sql, DbParameter[] parameters = null)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        if (parameter == null)
                            continue;

                        cmd.Parameters.Add(parameter.Name, parameter.Type).Value = parameter.Value;
                    }
                }

                var model = new AccountModel();
                var query = await cmd.ExecuteReaderAsync();

                while (query.Read())
                {
                    model.Id = Convert.ToInt32(query["Id"]);
                    model.Name = query["Name"].ToString();
                    model.WebAdress = query["WebAdress"].ToString();
                    model.Password = query["Password"].ToString();
                    model.CreatedDate = Convert.ToDateTime(query["CreatedDate"]);
                }

                return model;
            }
        }
        /// <summary>
        /// Executes a database query and returns a AccountModel
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="sql">SQL Query as string</param>
        /// <param name="parameter">A single parameters for executing the query</param>
        /// <returns>AccountModel</returns>
        public async Task<AccountModel> ExecuteQueryGetItem(string connectionString, string sql, DbParameter parameter = null)
            => await ExecuteQueryGetItem(connectionString, sql, new DbParameter[] { parameter });
    }
}