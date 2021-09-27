using KeySecret.DataAccess.Library.Accounts.Models;
using KeySecret.DataAccess.Library.Interfaces;
using KeySecret.DataAccess.Library.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library.Accounts.Repositories
{
    public class AccountsRepository : IRepository<AccountModel, InsertAccountModel, UpdateAccountModel>
    {
        private string _connectionString;
        private SqlDataAccess _dataAccess;

        public AccountsRepository(string connectionString)
        {
            _connectionString = connectionString;
            _dataAccess = new SqlDataAccess();
        }

        /// <summary>
        /// Requests a single account from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AccountModel> GetItemAsync(int id)
        {
            string sql = "SELECT * FROM KeySecretDB.dbo.Accounts WHERE Id=@Id";
            var parameter = new SqlParameter[]
            {
                new SqlParameter("Id", SqlDbType.Int) { Value = id }
            };

            var model = await _dataAccess.ExecuteQueryGetItem(_connectionString, sql, parameter);
            return model;
        }

        /// <summary>
        /// Requests all accounts from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AccountModel>> GetItemsAsync()
        {
            string sql = "SELECT * FROM KeySecretDB.dbo.Accounts";
            var list = await _dataAccess.ExecuteQueryGetItems(_connectionString, sql, null);
            return list;
        }

        /// <summary>
        /// Insert a account to the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> InsertItemAsync(InsertAccountModel item)
        {
            string sql = "INSERT INTO KeySecretDB.dbo.Accounts (Name, WebAdress, Password) VALUES (@Name, @WebAdress, @Password)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = item.Name },
                new SqlParameter("@WebAdress", SqlDbType.NVarChar) { Value = item.WebAdress },
                new SqlParameter("@Password", SqlDbType.NVarChar) { Value = item.Password }
            };

            return await _dataAccess.InsertQueryReturnIdentityAsync(_connectionString, sql, parameters);
        }

        /// <summary>
        /// Updates a account in the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task UpdateItemAsync(UpdateAccountModel item)
        {
            string sql = "UPDATE KeySecretDB.dbo.Accounts SET [Name] = @Name, [WebAdress] = @WebAdress, [Password] = @Password WHERE [Id] = @Id";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", SqlDbType.NVarChar) { Value = item.Id },
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = item.Name == null ? DBNull.Value : item.Name },
                new SqlParameter("@WebAdress", SqlDbType.NVarChar) { Value = item.WebAdress == null ? DBNull.Value : item.WebAdress },
                new SqlParameter("@Password", SqlDbType.NVarChar) { Value = item.Password == null ? DBNull.Value : item.Password }
            };

            await _dataAccess.ExecuteQueryVoidAsync(_connectionString, sql, parameters);
        }

        /// <summary>
        /// Deletes a account in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteItemAsync(int id)
        {
            string sql = "DELETE FROM KeySecretDB.dbo.Accounts WHERE Id=@Id";
            var parameter = new SqlParameter[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = id }
            };

            await _dataAccess.ExecuteQueryVoidAsync(_connectionString, sql, parameter);
        }
    }
}