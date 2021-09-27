using KeySecret.DesktopApp.Library.Accounts.Models;
using KeySecret.DesktopApp.Library.Interfaces;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.DataAccess
{
    public class AccountEndpoint : IEndpoint<AccountModel, UpdateAccountModel, InsertAccountModel>
    {
        private IApiHelper _apiHelper;

        public AccountEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        /// <summary>
        /// Abfrage eines AccountItems an die API
        /// </summary>
        /// <returns>Alle Accounteintrage der DB</returns>
        public async Task<AccountModel> GetById(int id)
        {
            using (HttpResponseMessage response = await _apiHelper.Client.GetAsync("api/accounts/" + id))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                var result = await response.Content.ReadAsAsync<AccountModel>();

                return result;
            }
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

        /// <summary>
        /// Sendet eine Anfrage an die API und fügt einen neuen Account in die Datenbank ein
        /// </summary>
        /// <param name="item">Der Eintrag mit dem neuen Datensatz</param>
        /// <returns></returns>
        public async Task InsertAsync(InsertAccountModel item)
        {
            using (HttpResponseMessage response = await _apiHelper.Client.PostAsJsonAsync("api/accounts/ins", item))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Sendet eine Anfrage an die API und löscht einen Account aus der Datenbank
        /// </summary>
        /// <param name="item">Id des Accounts das zu löschen ist</param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            using (HttpResponseMessage response = await _apiHelper.Client.DeleteAsync("api/accounts/del/" + id))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);
            }
        }
    }
}