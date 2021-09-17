using KeySecret.DesktopApp.Library.Helper;
using KeySecret.DesktopApp.Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace KeySecret.DesktopApp.Library.DataAccess
{
    public class AccountEndpoint : IAccountEndpoint
    {
        private IApiHelper _apiHelper;

        public AccountEndpoint(IServiceProvider provider)
        {
            _apiHelper = provider.GetService<IApiHelper>();
        }

        public async Task<List<AccountModel>> GetAllAccounts()
        {
            using (HttpResponseMessage response = await _apiHelper.Client.GetAsync("api/accounts"))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                var result = await response.Content.ReadAsAsync<List<AccountModel>>();

                return result;
            }
        }
    }
}