using KeySecret.App.Library.Models;
using KeySecret.App.Library.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http.Json;

namespace KeySecret.App.Library.DataAccess
{
    public class AuthenticateEndpoint : IAuthenticateEndpoint
    {
        private readonly IApiHelper _apiHelper;

        public AuthenticateEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<Response> Register(string username, string email, string password)
        {
            var user = new RegisterModel()
            {
                Username = username,
                Email = email,
                Password = password
            };

            using (HttpResponseMessage response = await _apiHelper.Client.PostAsJsonAsync("/api/Authenticate/register/", user))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                var apiResponse = await response.Content.ReadAsAsync<Response>();
                return apiResponse;
            }
        }

        public async Task Authenticate(string username, string password)
        {
            LoginModel user = new LoginModel()
            {
                Username = username,
                Password = password
            };

            using (HttpResponseMessage response
                = await _apiHelper.Client.PostAsJsonAsync("/api/Authenticate/login/", user))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                var result = await response.Content.ReadAsAsync<CurrentUser>();
                _apiHelper.SetCurrentUser(result);
                _apiHelper.AddBearerToken(result.Token);
            }
        }
    }
}