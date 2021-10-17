using KeySecret.DesktopApp.Library.Authentification;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.Interfaces
{
    public interface IAuthenticateEndpoint
    {
        Task<IAuthenticatedUserModel> Login(string username, string password);
    }
}