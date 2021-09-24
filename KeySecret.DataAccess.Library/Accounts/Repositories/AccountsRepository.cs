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

        public async Task<AccountModel> GetItemAsync(int id)
        {
            string sql = "SELECT * FROM KeySecretDB.dbo.Accounts WHERE Id=@Id";
            var parameter = new[] { new DbParameter("Id", SqlDbType.Int, id) };

            var model = await _dataAccess.ExecuteQueryGetItem(_connectionString, sql, parameter);
            return model;
        }

        public async Task<IEnumerable<AccountModel>> GetItemsAsync()
        {
            string sql = "SELECT * FROM KeySecretDB.dbo.Accounts";
            DbParameter[] parameter = null;

            var list = await _dataAccess.ExecuteQueryGetItems(_connectionString, sql, parameter);
            return list;
        }

        public async Task<int> InsertItemAsync(InsertAccountModel item)
        {
            string sql = "INSERT INTO KeySecretDB.dbo.Accounts (Name, WebAdress, Password) VALUES (@Name, @WebAdress, @Password)";
            var parameters = new DbParameter[] {
                new DbParameter("@Name", SqlDbType.NVarChar, item.Name),
                new DbParameter("@WebAdress", SqlDbType.NVarChar, item.WebAdress),
                new DbParameter("@Password", SqlDbType.NVarChar, item.Password)
            };

            return await _dataAccess.InsertQueryReturnIdentityAsync(_connectionString, sql, parameters);
        }

        public async Task UpdateItemAsync(UpdateAccountModel item)
        {
            string sql = "UPDATE KeySecretDB.dbo.Accounts SET [Name] = @Name, [WebAdress] = @WebAdress, [Password] = @Password WHERE [Id] = @Id";
            var parameters = new DbParameter[] {
                new DbParameter("@Id", SqlDbType.NVarChar, item.Id),
                new DbParameter("@Name", SqlDbType.NVarChar, item.Name),
                new DbParameter("@WebAdress", SqlDbType.NVarChar, item.WebAdress),
                new DbParameter("@Password", SqlDbType.NVarChar, item.Password)
            };

            await _dataAccess.ExecuteQueryVoidAsync(_connectionString, sql, parameters);
        }

        public async Task DeleteItemAsync(int id)
        {
            string sql = "DELETE FROM KeySecretDB.dbo.Accounts WHERE Id=@Id";
            var parameter = new[] { new DbParameter("@Id", SqlDbType.Int, id) };

            await _dataAccess.ExecuteQueryVoidAsync(_connectionString, sql, parameter);
        }
    }
}