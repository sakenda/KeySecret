using KeySecret.DesktopApp.Library.Authentification.Models;
using KeySecret.DesktopApp.Library.Helper;
using System.Net.Http;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.Interfaces
{
    public interface IApiHelper
    {
        HttpClient Client { get; }
        AuthenticatedUserModel AuthenticatedUser { get; }

        Task<Response> Register(string username, string email, string password);
        Task Authenticate(string username, string password);
        void AddBearerToken(string token);
        void LogOffUser();
    }
}