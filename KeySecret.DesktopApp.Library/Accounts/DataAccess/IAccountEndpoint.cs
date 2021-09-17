using KeySecret.DesktopApp.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.DataAccess
{
    public interface IAccountEndpoint
    {
        Task<List<AccountModel>> GetAllAccounts();
    }
}