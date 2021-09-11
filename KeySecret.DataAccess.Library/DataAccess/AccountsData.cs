using KeySecret.DataAccess.Library.Internal;
using KeySecret.DataAccess.Library.Models;
using System.Collections.Generic;

namespace KeySecret.DataAccess.Library.DataAccess
{
    public interface IAccountsData
    {
        List<AccountModel> GetAccessDataModels();
    }

    public class AccountsData : IAccountsData
    {
        public List<AccountModel> GetAccessDataModels()
        {
            return new SqlDataAccess().LoadData<AccountModel, dynamic>("dbo.spAccounts_GetAllItems", new { });
        }
    }
}