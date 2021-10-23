using KeySecret.DesktopApp.Library.Helper;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.DataAccess
{
    public interface IAuthenticateEndpoint
    {
        Task<Response> Register(string username, string email, string password);
        Task Authenticate(string username, string password);
    }
}