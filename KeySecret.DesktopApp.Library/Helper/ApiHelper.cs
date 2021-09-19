using KeySecret.DesktopApp.Library.Interfaces;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KeySecret.DesktopApp.Library.Helper
{
    public class ApiHelper : IApiHelper
    {
        public HttpClient Client { get; private set; }

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
        }

        // TODO: Authenticate
    }
}