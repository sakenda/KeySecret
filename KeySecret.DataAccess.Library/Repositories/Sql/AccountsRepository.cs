using KeySecret.DataAccess.Library.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library.Repositories
{
    public class AccountsRepository// : IRepository<AccountModel>
    {
        //    private readonly string _connectionString;
        //    private readonly ILogger _logger;

        //    private const string _spGetOne = "KeySecretDB.dbo.spAccounts_GetOneItem";
        //    private const string _spGetAll = "KeySecretDB.dbo.spAccounts_GetAllItems";
        //    private const string _spInsert = "KeySecretDB.dbo.spAccounts_InsertItem";
        //    private const string _spUpdate = "KeySecretDB.dbo.spAccounts_UpdateItem";
        //    private const string _spDelete = "KeySecretDB.dbo.spAccounts_DeleteItem";

        //    public AccountsRepository(string connectionString, ILogger logger)
        //    {
        //        _connectionString = connectionString;
        //        _logger = logger;
        //    }

        //    /// <summary>
        //    /// Requests a single account from the database
        //    /// </summary>
        //    /// <param name="id"></param>
        //    /// <returns></returns>
        //    public async Task<AccountModel> GetItemAsync(int id)
        //    {
        //        var loggerCtx = new LoggerContext(_logger);

        //        using (SqlConnection connection = new SqlConnection(_connectionString))
        //        {
        //            connection.Open();

        //            AccountModel model = null;
        //            SqlTransaction transaction = connection.BeginTransaction(nameof(GetItemAsync));

        //            SqlCommand command = new SqlCommand(_spGetOne, connection, transaction);
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;

        //            try
        //            {
        //                using (var reader = await command.ExecuteReaderAsync())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        model = new AccountModel()
        //                        {
        //                            Id = reader["Id"].DBToValue<int>(),
        //                            Name = reader["Name"].ToString(),
        //                            WebAdress = reader["WebAdress"].ToString(),
        //                            Password = reader["Password"].ToString(),
        //                            CategoryId = reader["CategoryId"].DBToValue<int>(),
        //                            CreatedDate = reader["CreatedDate"].DBToValue<DateTime>()
        //                        };
        //                    }
        //                }

        //                await transaction.CommitAsync();

        //                loggerCtx.Level = LogLevel.Information;
        //                loggerCtx.Message = $"Successfully retrieved item. Id: {id}";
        //                loggerCtx.WriteLog();
        //            }
        //            catch (Exception ex)
        //            {
        //                Exception exception = await transaction.TransactionRollbackAsync(ex);
        //                loggerCtx.WriteException(exception);
        //                throw exception;
        //            }

        //            return model;
        //        }
        //    }

        //    /// <summary>
        //    /// Requests all accounts from the database
        //    /// </summary>
        //    /// <returns></returns>
        //    public async Task<IEnumerable<AccountModel>> GetItemsAsync()
        //    {
        //        var loggerCtx = new LoggerContext(_logger);

        //        using (SqlConnection connection = new SqlConnection(_connectionString))
        //        {
        //            connection.Open();

        //            var list = new List<AccountModel>();
        //            SqlTransaction transaction = connection.BeginTransaction(nameof(GetItemsAsync));

        //            SqlCommand command = new SqlCommand(_spGetAll, connection, transaction);
        //            command.CommandType = CommandType.StoredProcedure;

        //            try
        //            {
        //                using (var reader = await command.ExecuteReaderAsync())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        list.Add(new AccountModel
        //                        {
        //                            Id = reader["Id"].DBToValue<int>(),
        //                            Name = reader["Name"].ToString(),
        //                            WebAdress = reader["WebAdress"].ToString(),
        //                            Password = reader["Password"].ToString(),
        //                            CategoryId = reader["CategoryId"].DBToValue<int>(),
        //                            CreatedDate = reader["CreatedDate"].DBToValue<DateTime>()
        //                        });
        //                    }
        //                }

        //                await transaction.CommitAsync();

        //                loggerCtx.Level = LogLevel.Information;
        //                loggerCtx.Message = $"Successfully retrieved {list.Count} items.";
        //                loggerCtx.WriteLog();
        //            }
        //            catch (Exception ex)
        //            {
        //                Exception exception = await transaction.TransactionRollbackAsync(ex);
        //                loggerCtx.WriteException(exception);
        //                throw exception;
        //            }

        //            return list;
        //        }
        //    }

        //    /// <summary>
        //    /// Insert a account to the database
        //    /// </summary>
        //    /// <param name="item"></param>
        //    /// <returns></returns>
        //    public async Task<AccountModel> InsertItemAsync(AccountModel item)
        //    {
        //        var loggerCtx = new LoggerContext(_logger);

        //        using (SqlConnection connection = new SqlConnection(_connectionString))
        //        {
        //            connection.Open();

        //            SqlTransaction transaction = connection.BeginTransaction(nameof(InsertItemAsync));

        //            SqlCommand command = new SqlCommand(_spInsert, connection, transaction);
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar)).Value = item.Name.StringToDb();
        //            command.Parameters.Add(new SqlParameter("@WebAdress", SqlDbType.NVarChar)).Value = item.WebAdress.StringToDb();
        //            command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar)).Value = item.Password.StringToDb();
        //            command.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int)).Value = item.CategoryId.ValueToDB<int>();

        //            try
        //            {
        //                await command.ExecuteNonQueryAsync();
        //                await transaction.CommitAsync();

        //                transaction = connection.BeginTransaction("GetIdentifier");
        //                command = new SqlCommand("select @@IDENTITY", connection, transaction);

        //                item.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
        //                item.CreatedDate = DateTime.Now;

        //                loggerCtx.Level = LogLevel.Information;
        //                loggerCtx.Message = $"Item successfully inserted.";
        //                loggerCtx.WriteLog();
        //            }
        //            catch (Exception ex)
        //            {
        //                Exception exception = await transaction.TransactionRollbackAsync(ex);
        //                loggerCtx.WriteException(exception);
        //                throw exception;
        //            }

        //            return item;
        //        }
        //    }

        //    /// <summary>
        //    /// Updates a account in the database
        //    /// </summary>
        //    /// <param name="item"></param>
        //    /// <returns></returns>
        //    public async Task UpdateItemAsync(AccountModel item)
        //    {
        //        var loggerCtx = new LoggerContext(_logger);

        //        using (SqlConnection connection = new SqlConnection(_connectionString))
        //        {
        //            connection.Open();

        //            SqlTransaction transaction = connection.BeginTransaction(nameof(UpdateItemAsync));
        //            SqlCommand command = new SqlCommand(_spUpdate, connection, transaction);
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar)).Value = item.Id;
        //            command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar)).Value = item.Name.StringToDb();
        //            command.Parameters.Add(new SqlParameter("@WebAdress", SqlDbType.NVarChar)).Value = item.WebAdress.StringToDb();
        //            command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar)).Value = item.Password.StringToDb();
        //            command.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int)).Value = item.CategoryId.ValueToDB<int>();

        //            try
        //            {
        //                await command.ExecuteNonQueryAsync();
        //                await transaction.CommitAsync();

        //                loggerCtx.Level = LogLevel.Information;
        //                loggerCtx.Message = $"Item successfully updated.";
        //                loggerCtx.WriteLog();
        //            }
        //            catch (Exception ex)
        //            {
        //                Exception exception = await transaction.TransactionRollbackAsync(ex);
        //                loggerCtx.WriteException(exception);
        //                throw exception;
        //            }
        //        }
        //    }

        //    /// <summary>
        //    /// Deletes a account in the database
        //    /// </summary>
        //    /// <param name="id"></param>
        //    /// <returns></returns>
        //    public async Task DeleteItemAsync(int id)
        //    {
        //        var loggerCtx = new LoggerContext(_logger);

        //        using (SqlConnection connection = new SqlConnection(_connectionString))
        //        {
        //            connection.Open();

        //            string sql = "DELETE FROM KeySecretDB.dbo.Accounts WHERE Id=@Id";
        //            SqlTransaction transaction = connection.BeginTransaction(nameof(DeleteItemAsync));
        //            SqlCommand command = new SqlCommand(sql, connection, transaction);
        //            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar)).Value = id;

        //            try
        //            {
        //                await command.ExecuteNonQueryAsync();
        //                await transaction.CommitAsync();

        //                loggerCtx.Level = LogLevel.Information;
        //                loggerCtx.Message = $"Item successfully deleted. Id {id}";
        //                loggerCtx.WriteLog();
        //            }
        //            catch (Exception ex)
        //            {
        //                Exception exception = await transaction.TransactionRollbackAsync(ex);
        //                loggerCtx.WriteException(exception);
        //                throw exception;
        //            }
        //        }
        //    }
    }
}