using KeySecret.App.Library.Models;

using System.Threading.Tasks;

namespace KeySecret.App.Library.DataAccess
{
    public interface IAuthenticateEndpoint
    {
        Task<Response> Register(string username, string email, string password);
        Task Authenticate(string username, string password);
    }
}