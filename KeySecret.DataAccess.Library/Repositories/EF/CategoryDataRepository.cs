using KeySecret.DataAccess.Library.Context;
using KeySecret.DataAccess.Library.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library.Repositories
{
    public class CategoryDataRepository : IRepository<CategoryModel>
    {
        private readonly DataDbContext _context;

        public CategoryDataRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryModel> GetItemAsync(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
                throw new Exception();

            return category;
        }

        public async Task<IEnumerable<CategoryModel>> GetItemsAsync()
        {
            var categoriesList = await _context.Categories.ToListAsync();

            if (categoriesList == null)
                throw new Exception();

            return categoriesList;
        }

        public async Task<CategoryModel> InsertItemAsync(CategoryModel item)
        {
            item.ModifiedDate = DateTime.Now;
            item.CategoryId = item.CategoryId == Guid.Empty
                            ? Guid.NewGuid()
                            : item.CategoryId;

            await _context.Categories.AddAsync(item);

            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new DbUpdateException();

            return item;
        }

        public void UpdateItemAsync(CategoryModel item)
        {
            var category = _context.Categories.FirstOrDefault(a => a.CategoryId == item.CategoryId);
            if (category == null)
                throw new Exception();

            if (category.Name != item.Name)
            {
                category.Name = item.Name;
                category.ModifiedDate = DateTime.Now;
            }

            _context.SaveChanges();
        }

        public void DeleteItemAsync(Guid id)
        {
            var category = _context.Categories.FirstOrDefault(a => a.CategoryId == id);
            if (category == null)
                throw new Exception();

            _context.Categories.Remove(category);

            var result = _context.SaveChanges();
            if (result == 0)
                throw new DbUpdateException();
        }
    }
}