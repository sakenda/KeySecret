using KeySecret.DataAccess.Library.Accounts.Models;
using KeySecret.DataAccess.Library.Interfaces;
using KeySecret.DataAccess.Library.Internal;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library.Accounts.Repositories
{
    public class AccountsRepository : IRepository<AccountModel>
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
            string sql = "SELECT * FROM Accounts WHERE Id=@Id";
            var parameter = new DbParameter("Id", SqlDbType.Int, id);

            var model = await _dataAccess.ExecuteQueryGetItem(_connectionString, sql, parameter);
            return model;
        }

        public async Task<List<AccountModel>> GetItemsAsync()
        {
            string sql = "SELECT * FROM Accounts";
            DbParameter parameter = null;

            var list = await _dataAccess.ExecuteQueryGetItems(_connectionString, sql, parameter);
            return list;
        }

        public async Task UpdateItemAsync(AccountModel item)
        {
            string sql = "UPDATE INTO Accounts (Name, WebAdress, Password) VALUES (@Name, @WebAdress, @Password)";
            var parameters = new DbParameter[] {
                new DbParameter("@Name", SqlDbType.NVarChar, item.Name),
                new DbParameter("@WebAdress", SqlDbType.NVarChar, item.WebAdress),
                new DbParameter("@Password", SqlDbType.NVarChar, item.Password)
            };

            await _dataAccess.ExecuteQueryVoidAsync(_connectionString, sql, parameters);
        }

        public async Task InsertItemAsync(AccountModel item)
        {
            string sql = "INSERT INTO Accounts (Name, WebAdress, Password) VALUES (@Name, @WebAdress, @Password)";
            var parameters = new DbParameter[] {
                new DbParameter("@Name", SqlDbType.NVarChar, item.Name),
                new DbParameter("@WebAdress", SqlDbType.NVarChar, item.WebAdress),
                new DbParameter("@Password", SqlDbType.NVarChar, item.Password)
            };

            await _dataAccess.ExecuteQueryVoidAsync(_connectionString, sql, parameters);
        }

        public async Task DeleteItemAsync(int id)
        {
            string sql = "DELETE FROM Accounts WHERE Id=@Id";
            var parameter = new DbParameter("@Id", SqlDbType.Int, id);

            await _dataAccess.ExecuteQueryVoidAsync(_connectionString, sql, parameter);
        }
    }
}