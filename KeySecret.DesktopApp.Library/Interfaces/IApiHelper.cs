using System.Net.Http;

namespace KeySecret.DesktopApp.Library.Interfaces
{
    public interface IApiHelper
    {
        HttpClient Client { get; }
    }
}