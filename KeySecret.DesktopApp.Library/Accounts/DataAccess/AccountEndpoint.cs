using KeySecret.DesktopApp.Library.Accounts.Models;
using KeySecret.DesktopApp.Library.Interfaces;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.DataAccess
{
    public class AccountEndpoint : IEndpoint<AccountModel, UpdateAccountModel>
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
        public async Task<IEnumerable<AccountModel>> GetAllAsync()
        {
            using (HttpResponseMessage response = await _apiHelper.Client.GetAsync("api/accounts/"))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                var result = await response.Content.ReadAsAsync<IEnumerable<AccountModel>>();

                return result;
            }
        }

        /// <summary>
        /// Sendet eine Anfrage an die API und aktualisiert ein Account auf dem Server
        /// </summary>
        /// <param name="item">Der Eintrag mit den aktualisierten Daten</param>
        /// <returns></returns>
        public async Task UpdateAsync(UpdateAccountModel item)
        {
            using (HttpResponseMessage response = await _apiHelper.Client.PutAsJsonAsync("api/accounts/upd", item))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);
            }
        }
    }
}