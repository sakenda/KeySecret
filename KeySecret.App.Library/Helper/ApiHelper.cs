using KeySecret.App.Library.Models;

using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KeySecret.App.Library.Helper
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _client;
        private CurrentUser _currentUser;

        public HttpClient Client => _client;
        public CurrentUser CurrentUser => _currentUser == null ? new CurrentUser() { Username = "Guest" } : _currentUser;

        public ApiHelper()
        {
            InitializeClient();
        }

        private void InitializeClient()
        {
            string apiAdress = ConfigurationManager.AppSettings["apiAdress"];

            _client = new HttpClient();
            _client.BaseAddress = new Uri(apiAdress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_client.Timeout = TimeSpan.FromSeconds(30);
        }

        public void AddBearerToken(string token)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public void SetCurrentUser(CurrentUser user)
            => _currentUser = user;

        public void LogOffUser()
        {
            _currentUser = null;
            _client.DefaultRequestHeaders.Clear();
        }
    }
}