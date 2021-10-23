using KeySecret.DataAccess.Library.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library.Repositories
{
    public class CategoryRepository : IRepository<CategoryModel>
    {
        private readonly string _connectionString;
        private ILogger _logger;

        private const string _spGetOne = "KeySecretDB.dbo.spCategories_GetOneItem";
        private const string _spGetAll = "KeySecretDB.dbo.spCategories_GetAllItems";
        private const string _spInsert = "KeySecretDB.dbo.spCategories_InsertItem";
        private const string _spUpdate = "KeySecretDB.dbo.spCategories_UpdateItem";
        private const string _spDelete = "KeySecretDB.dbo.spCategories_DeleteItem";

        public CategoryRepository(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        /// <summary>
        /// Sends query to the Database to request one category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CategoryModel> GetItemAsync(int id)
        {
            var loggerCtx = new LoggerContext(_logger);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                CategoryModel model = null;
                SqlTransaction transaction = connection.BeginTransaction(nameof(GetItemAsync));

                SqlCommand command = new SqlCommand(_spGetOne, connection, transaction);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;

                try
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            model = new CategoryModel()
                            {
                                Id = reader["Id"].DBToValue<int>(),
                                Name = reader["Name"].ToString()
                            };
                        }
                    }

                    await transaction.CommitAsync();

                    loggerCtx.Level = LogLevel.Information;
                    loggerCtx.Message = $"Successful retrieved item. ID: {model.Id}";
                    loggerCtx.WriteLog();
                }
                catch (Exception ex)
                {
                    Exception exception = await transaction.TransactionRollbackAsync(ex);
                    loggerCtx.WriteException(exception);
                    throw exception;
                }

                return model;
            }
        }

        /// <summary>
        /// Sends query to database to request all categories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryModel>> GetItemsAsync()
        {
            var loggerCtx = new LoggerContext(_logger);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var list = new List<CategoryModel>();
                SqlTransaction transaction = connection.BeginTransaction(nameof(GetItemsAsync));

                SqlCommand command = new SqlCommand(_spGetAll, connection, transaction);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            list.Add(new CategoryModel
                            {
                                Id = reader["Id"].DBToValue<int>(),
                                Name = reader["Name"].ToString()
                            });
                        }
                    }

                    await transaction.CommitAsync();

                    loggerCtx.Level = LogLevel.Information;
                    loggerCtx.Message = $"{list.Count} items successfully retrieved.";
                    loggerCtx.WriteLog();
                }
                catch (Exception ex)
                {
                    Exception exception = await transaction.TransactionRollbackAsync(ex);
                    loggerCtx.WriteException(exception);
                    throw exception;
                }

                return list;
            }
        }

        /// <summary>
        /// Sends query to insert a new category to the databse
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<CategoryModel> InsertItemAsync(CategoryModel item)
        {
            var loggerCtx = new LoggerContext(_logger);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction(nameof(InsertItemAsync));

                SqlCommand command = new SqlCommand(_spInsert, connection, transaction);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar)).Value = item.Name.StringToDb();

                try
                {
                    await command.ExecuteNonQueryAsync();
                    await transaction.CommitAsync();

                    transaction = connection.BeginTransaction("GetIdentifier");
                    command = new SqlCommand("select @@IDENTITY", connection, transaction);

                    item.Id = Convert.ToInt32(await command.ExecuteScalarAsync());

                    loggerCtx.Level = LogLevel.Information;
                    loggerCtx.Message = $"Item successfullyinserted.";
                    loggerCtx.WriteLog();
                }
                catch (Exception ex)
                {
                    Exception exception = await transaction.TransactionRollbackAsync(ex);
                    loggerCtx.WriteException(exception);
                    throw exception;
                }

                return item;
            }
        }

        /// <summary>
        /// Sends a query to the database to update a category
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task UpdateItemAsync(CategoryModel item)
        {
            var loggerCtx = new LoggerContext(_logger);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction(nameof(UpdateItemAsync));
                SqlCommand command = new SqlCommand(_spUpdate, connection, transaction);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar)).Value = item.Id;
                command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar)).Value = item.Name.StringToDb();

                try
                {
                    await command.ExecuteNonQueryAsync();
                    await transaction.CommitAsync();

                    loggerCtx.Level = LogLevel.Information;
                    loggerCtx.Message = $"Item successfully updated.";
                    loggerCtx.WriteLog();
                }
                catch (Exception ex)
                {
                    Exception exception = await transaction.TransactionRollbackAsync(ex);
                    loggerCtx.WriteException(exception);
                    throw exception;
                }
            }
        }

        /// <summary>
        /// Sends a query to delete a category in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteItemAsync(int id)
        {
            var loggerCtx = new LoggerContext(_logger);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction(nameof(DeleteItemAsync));
                SqlCommand command = new SqlCommand(_spDelete, connection, transaction);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar)).Value = id;

                try
                {
                    await command.ExecuteNonQueryAsync();
                    await transaction.CommitAsync();

                    loggerCtx.Level = LogLevel.Information;
                    loggerCtx.Message = $"Item successfully deleted. Id: {id}";
                    loggerCtx.WriteLog();
                }
                catch (Exception ex)
                {
                    Exception exception = await transaction.TransactionRollbackAsync(ex);
                    loggerCtx.WriteException(exception);
                    throw exception;
                }
            }
        }
    }
}