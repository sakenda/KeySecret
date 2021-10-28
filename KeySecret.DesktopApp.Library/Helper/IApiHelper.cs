using KeySecret.App.Library.Models;
using System.Net.Http;

namespace KeySecret.App.Library.Helper
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