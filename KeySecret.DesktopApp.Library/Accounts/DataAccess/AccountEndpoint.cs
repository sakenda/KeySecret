using KeySecret.DesktopApp.Library.Interfaces;
using KeySecret.DesktopApp.Library.Models;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.DataAccess
{
    public class AccountEndpoint : IEndpoint<AccountModel>
    {
        private IApiHelper _apiHelper;

        public AccountEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        /// <summary>
        /// Abfrage aller AccountItems an die API
        /// </summary>
        /// <returns>Alle Accounteintrage der DB</returns>
        public async Task<IEnumerable<AccountModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.Client.GetAsync("api/accounts/"))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                var result = await response.Content.ReadAsAsync<IEnumerable<AccountModel>>();

                return result;
            }
        }
    }
}