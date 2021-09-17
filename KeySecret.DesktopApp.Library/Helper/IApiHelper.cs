using System.Net.Http;

namespace KeySecret.DesktopApp.Library.Helper
{
    public interface IApiHelper
    {
        HttpClient Client { get; }
    }
}