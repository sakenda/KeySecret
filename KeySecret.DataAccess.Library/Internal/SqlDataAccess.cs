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
        /// Executes a database query and returns a AccountModel
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="sql">SQL Query as string</param>
        /// <param name="parameters">Parameters for executing the query</param>
        /// <returns>AccountModel</returns>
        public async Task<AccountModel> ExecuteQueryGetItem(string connectionString, string sql, params object[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                AccountModel model = null;

                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction(nameof(ExecuteQueryGetItem));
                SqlCommand command = new SqlCommand(sql, connection, transaction);
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                try
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            model = new AccountModel()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                WebAdress = reader["WebAdress"].ToString(),
                                Password = reader["Password"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                            };
                        }
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    throw await TransactionRollbackAsync(transaction, ex, nameof(ExecuteQueryGetItem));
                }

                return model;
            }
        }

        /// <summary>
        /// Executes a database query and returns the table
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="sql">SQL Query as string</param>
        /// <param name="parameters">Parameters for executing the query</param>
        /// <returns>List of AccountModels</returns>
        public async Task<List<AccountModel>> ExecuteQueryGetItems(string connectionString, string sql, params object[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var list = new List<AccountModel>();

                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction(nameof(ExecuteQueryGetItems));
                SqlCommand command = new SqlCommand(sql, connection, transaction);
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                try
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            list.Add(new AccountModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                WebAdress = reader["WebAdress"].ToString(),
                                Password = reader["Password"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                            });
                        }
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    throw await TransactionRollbackAsync(transaction, ex, nameof(ExecuteQueryGetItems));
                }

                return list;
            }
        }

        /// <summary>
        /// Executes a Insert query and returns the Identity
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="sql">SQL Query as string</param>
        /// <param name="parameters">Parameters for executing the query</param>
        /// <returns>Insert identity</returns>
        public async Task<int> InsertQueryReturnIdentityAsync(string connectionString, string sql, params object[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                object identity = null;

                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction(nameof(ExecuteQueryGetItem));
                SqlCommand command = new SqlCommand(sql, connection, transaction);
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                try
                {
                    await command.ExecuteNonQueryAsync();
                    await transaction.CommitAsync();

                    transaction = connection.BeginTransaction("GetIdentifier");
                    command = new SqlCommand("select @@IDENTITY", connection, transaction);
                    identity = await command.ExecuteScalarAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await TransactionRollbackAsync(transaction, ex, nameof(ExecuteQueryGetItem));
                }

                return Convert.ToInt32(identity);
            }
        }

        /// <summary>
        /// Executes a database query
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="sql">SQL Query as string</param>
        /// <param name="parameters">Parameters for executing the query</param>
        /// <returns>Void</returns>
        public async Task ExecuteQueryVoidAsync(string connectionString, string sql, params object[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction(nameof(ExecuteQueryVoidAsync));
                SqlCommand command = new SqlCommand(sql, connection, transaction);
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                try
                {
                    await command.ExecuteNonQueryAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await TransactionRollbackAsync(transaction, ex, nameof(ExecuteQueryVoidAsync));
                }
            }
        }

        /// <summary>
        /// Tries a Rollback. If it fails, the method will return a Exception
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="ex"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        private async Task<Exception> TransactionRollbackAsync(SqlTransaction transaction, Exception ex, string origin)
        {
            try
            {
                await transaction.RollbackAsync();
            }
            catch (Exception ex2)
            {
                return new Exception(
                    "\r\n\r\n" +
                    $"Caller:\t{origin}\r\n" +
                    $"Message:\t{ex2.Message}\r\n\r\n" +
                    ex2.StackTrace
                );
            }

            return new Exception(
                    "\r\n\r\n" +
                    $"Caller:\t{origin}\r\n" +
                    $"Message:\t{ex.Message}\r\n\r\n" +
                    ex.StackTrace
                );
        }
    }
}