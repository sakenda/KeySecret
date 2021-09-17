using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KeySecret.DesktopApp.Library.Helper
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _client;

        public HttpClient Client => _client;

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
        }

        // TODO: Authenticate
    }
}