using KeySecret.DesktopApp.Library.Models;
using KeySecret.DesktopApp.Library.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.DataAccess
{
    public class CategoriesEndpoint : IEndpoint<CategoryModel>
    {
        private readonly IApiHelper _apiHelper;

        public CategoriesEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        /// <summary>
        /// Ruft alle Kategorien aus der API ab
        /// </summary>
        /// <returns><see cref="IEnumerable{CategoryModel}"/></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            using (HttpResponseMessage response = await _apiHelper.Client.GetAsync("api/categories/"))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                var result = await response.Content.ReadAsAsync<IEnumerable<CategoryModel>>();

                return result;
            }
        }

        /// <summary>
        /// Ruft ein CategoryModel von der API ab
        /// </summary>
        /// <param name="id">Die ID der Kategorie</param>
        /// <returns><see cref="CategoryModel"/></returns>
        /// <exception cref="Exception">Nicht erfolgreiche Abfrage</exception>
        public async Task<CategoryModel> GetById(int id)
        {
            using (HttpResponseMessage response = await _apiHelper.Client.GetAsync("api/categories/" + id))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                var result = await response.Content.ReadAsAsync<CategoryModel>();

                return result;
            }
        }

        /// <summary>
        /// Sendet eine Insert Abfrage an die API
        /// </summary>
        /// <param name="item"><see cref="CategoryModel"/> mit den Daten</param>
        /// <returns><see cref="CategoryModel"/></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CategoryModel> InsertAsync(CategoryModel item)
        {
            using (HttpResponseMessage response = await _apiHelper.Client.PostAsJsonAsync("api/categories/ins", item))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                return await response.Content.ReadAsAsync<CategoryModel>();
            }
        }

        /// <summary>
        /// Sendet eine Abfrage an die API um eine KAtegorie zu Aktualisieren
        /// </summary>
        /// <param name="item"><see cref="CategoryModel"/> mit den aktualisierten Daten</param>
        /// <returns><see cref="NoContent"/></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateAsync(CategoryModel item)
        {
            using (HttpResponseMessage response = await _apiHelper.Client.PutAsJsonAsync("api/categories/upd", item))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Sendet eine Lösch-Abfrage an die API
        /// </summary>
        /// <param name="id"> ID des <see cref="CategoryModel"/> zum Löschen</param>
        /// <returns><see cref="NoContent"/></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteAsync(int id)
        {
            using (HttpResponseMessage response = await _apiHelper.Client.DeleteAsync("api/categories/del/" + id))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);
            }
        }
    }
}