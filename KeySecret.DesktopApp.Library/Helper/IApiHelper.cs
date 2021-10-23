using KeySecret.DesktopApp.Library.Models;
using System.Net.Http;

namespace KeySecret.DesktopApp.Library.Helper
{
    public interface IApiHelper
    {
        HttpClient Client { get; }
        CurrentUser LoggedInUser { get; }

        void AddBearerToken(string token);
        void SetCurrentUser(CurrentUser user);
        void LogOffUser();
    }
}