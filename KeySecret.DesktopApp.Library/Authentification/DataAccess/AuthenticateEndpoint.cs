using KeySecret.DesktopApp.Library.Models;
using KeySecret.DesktopApp.Library.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.DataAccess
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
            var user = new RegisterUserModel()
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
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            using (HttpResponseMessage response = await _apiHelper.Client.PostAsync("/api/Authenticate/login/", data))
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