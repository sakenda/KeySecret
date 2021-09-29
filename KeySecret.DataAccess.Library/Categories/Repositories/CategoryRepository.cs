using KeySecret.DataAccess.Library.Categories.Models;
using KeySecret.DataAccess.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library.Categories.Repositories
{
    public class CategoryRepository : IRepository<CategoryModel>
    {
        private string _connectionString;

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryModel> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryModel>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryModel> InsertItemAsync(CategoryModel item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemAsync(CategoryModel item)
        {
            throw new NotImplementedException();
        }
    }
}