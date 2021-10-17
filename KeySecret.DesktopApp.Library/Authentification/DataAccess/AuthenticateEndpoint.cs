using KeySecret.DesktopApp.Library.Authentification.Models;
using KeySecret.DesktopApp.Library.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.Authentification.DataAccess
{
    public class AuthenticateEndpoint : IAuthenticateEndpoint
    {
        private readonly IApiHelper _apiHelper;

        public AuthenticateEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<IAuthenticatedUserModel> Login(string username, string password)
        {
            var data = new { Username = username, Password = password };

            using (HttpResponseMessage response = await _apiHelper.Client.PostAsJsonAsync("/api/Authenticate/login/", data))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                var result = await response.Content.ReadAsAsync<AuthenticatedUserModel>();
                _apiHelper.AddBearerToken(result.Token);
                return result;
            }
        }
    }
}