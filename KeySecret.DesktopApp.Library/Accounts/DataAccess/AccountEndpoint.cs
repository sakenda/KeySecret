using KeySecret.DesktopApp.Library.Accounts.Models;
using KeySecret.DesktopApp.Library.Interfaces;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.DataAccess
{
    public class AccountEndpoint : IEndpoint<AccountModel>
    {
        private readonly IApiHelper _apiHelper;

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

                var result = await response.Content.ReadAsAsync<IEnumerable<EndpointAccountModel>>();
                var resultList = new List<AccountModel>();

                foreach (var a in result)
                {
                    var account = new AccountModel(a.Name, a.WebAdress, a.Password, a.CreatedDate);
                    account.SetId(a.Id);
                    resultList.Add(account);
                }

                return resultList;
            }
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

                var result = await response.Content.ReadAsAsync<EndpointAccountModel>();
                var account = new AccountModel(result.Name, result.WebAdress, result.Password, result.CreatedDate);
                account.SetId(result.Id);

                return account;
            }
        }

        /// <summary>
        /// Sendet eine Anfrage an die API und fügt einen neuen Account in die Datenbank ein
        /// </summary>
        /// <param name="item">Der Eintrag mit dem neuen Datensatz</param>
        /// <returns></returns>
        public async Task<AccountModel> InsertAsync(AccountModel item)
        {
            if (item.Id != -1)
                throw new ArgumentException("Id darf nicht größer als -1 sein");

            using (HttpResponseMessage response = await _apiHelper.Client.PostAsJsonAsync("api/accounts/ins", item))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                var result = await response.Content.ReadAsAsync<AccountModel>();
                var account = new AccountModel(result.Name, result.WebAdress, result.Password, result.CreatedDate);
                account.SetId(result.Id);

                return account;
            }
        }

        /// <summary>
        /// Sendet eine Anfrage an die API und aktualisiert ein Account auf dem Server
        /// </summary>
        /// <param name="item">Der Eintrag mit den aktualisierten Daten</param>
        /// <returns></returns>
        public async Task UpdateAsync(AccountModel item)
        {
            using (HttpResponseMessage response = await _apiHelper.Client.PutAsJsonAsync("api/accounts/upd", item))
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