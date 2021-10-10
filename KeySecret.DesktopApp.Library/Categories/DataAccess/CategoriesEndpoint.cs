using KeySecret.DesktopApp.Library.Categories.Models;
using KeySecret.DesktopApp.Library.Interfaces;

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

        public async Task UpdateAsync(CategoryModel item)
        {
            using (HttpResponseMessage response = await _apiHelper.Client.PutAsJsonAsync("api/categories/upd", item))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task InsertAsync(CategoryModel item)
        {
            using (HttpResponseMessage response = await _apiHelper.Client.PostAsJsonAsync("api/categories/ins", item))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);
            }
        }

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