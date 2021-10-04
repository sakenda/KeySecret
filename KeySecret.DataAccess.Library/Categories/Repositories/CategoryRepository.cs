using KeySecret.DataAccess.Library.Categories.Models;
using KeySecret.DataAccess.Library.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library.Categories.Repositories
{
    public class CategoryRepository : IRepository<CategoryModel>
    {
        private readonly string _connectionString;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(string connectionString, ILogger<CategoryRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
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