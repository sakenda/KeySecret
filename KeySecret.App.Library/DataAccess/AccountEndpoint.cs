using KeySecret.App.Library.Helper;
using KeySecret.App.Library.Models;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KeySecret.App.Library.DataAccess
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

                var result = await response.Content.ReadAsAsync<IEnumerable<AccountDto>>();
                var resultList = new List<AccountModel>();

                foreach (var a in result)
                {
                    var account = AccountModel.DtoAsAccountModel(a);
                    resultList.Add(account);
                }

                return resultList;
            }
        }

        /// <summary>
        /// Abfrage eines AccountItems an die API
        /// </summary>
        /// <returns>Alle Accounteintrage der DB</returns>
        public async Task<AccountModel> GetById(Guid id)
        {
            using (HttpResponseMessage response = await _apiHelper.Client.GetAsync("api/accounts/" + id))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                var result = await response.Content.ReadAsAsync<AccountDto>();
                var account = AccountModel.DtoAsAccountModel(result);

                return account;
            }
        }

        /// <summary>
        /// Sendet eine Anfrage an die API und fügt einen neuen Account in die Datenbank ein
        /// </summary>
        /// <param name="item">Der Eintrag mit dem neuen Datensatz</param>
        /// <returns></returns>
        public async Task InsertAsync(AccountModel item)
        {
            var account = item.AsDto();

            using (HttpResponseMessage response = await _apiHelper.Client.PostAsJsonAsync("api/accounts/ins", account))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Sendet eine Anfrage an die API und aktualisiert ein Account auf dem Server
        /// </summary>
        /// <param name="item">Der Eintrag mit den aktualisierten Daten</param>
        /// <returns></returns>
        public async Task UpdateAsync(AccountModel item)
        {
            var account = item.AsDto();

            using (HttpResponseMessage response = await _apiHelper.Client.PutAsJsonAsync("api/accounts/upd", account))
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
        public async Task DeleteAsync(Guid id)
        {
            using (HttpResponseMessage response = await _apiHelper.Client.DeleteAsync("api/accounts/del/" + id))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);
            }
        }
    }
}