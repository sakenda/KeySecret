using KeySecret.DesktopApp.Library.Authentification.Models;
using KeySecret.DesktopApp.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.Helper
{
    public class ApiHelper : IApiHelper
    {
        public HttpClient Client { get; private set; }
        public AuthenticatedUserModel AuthenticatedUser { get; private set; }

        public ApiHelper()
        {
            InitializeClient();
        }

        private void InitializeClient()
        {
            string apiAdress = ConfigurationManager.AppSettings["apiAdress"];

            Client = new HttpClient();
            Client.BaseAddress = new Uri(apiAdress);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<Response> Register(string username, string email, string password)
        {
            var user = new RegisterModel()
            {
                Username = username,
                Email = email,
                Password = password
            };

            using (HttpResponseMessage response = await Client.PostAsJsonAsync("/api/Authenticate/register/", user))
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

            using (HttpResponseMessage response = await Client.PostAsync("/api/Authenticate/login/", data))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                var result = await response.Content.ReadAsAsync<AuthenticatedUserModel>();
                AuthenticatedUser = result;
                AddBearerToken(AuthenticatedUser.Token);
            }
        }

        public void AddBearerToken(string token)
        {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public void LogOffUser()
            => Client.DefaultRequestHeaders.Clear();
    }
}