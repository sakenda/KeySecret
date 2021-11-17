using KeySecret.DataAccess.Library.Data;
using KeySecret.DataAccess.Library.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library.Repositories
{
    /// <summary>
    /// Connection class between API endpoint and database for <see cref="CategoryModel"/>
    /// </summary>
    public class CategoryDataRepository : IRepository<CategoryModel>
    {
        private readonly DataDbContext _context;

        public CategoryDataRepository(DataDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get item by id
        /// </summary>
        /// <param name="id">Id as <seealso cref="Guid"/></param>
        /// <returns><see cref="CategoryModel"/>, if found</returns>
        /// <exception cref="Exception">If the database returns no item a <seealso cref="Exception"/> is thrown</exception>
        public async Task<CategoryModel> GetItemAsync(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
                throw new Exception();

            return category;
        }

        /// <summary>
        /// Get every item from the database
        /// </summary>
        /// <returns><see cref="CategoryModel"/></returns>
        /// <exception cref="Exception">If the database returns no items a <seealso cref="Exception"/> is thrown</exception>
        public async Task<IEnumerable<CategoryModel>> GetItemsAsync()
        {
            var categoriesList = await _context.Categories.ToListAsync();

            if (categoriesList == null)
                throw new Exception();

            return categoriesList;
        }

        /// <summary>
        /// Insert a new item to the database
        /// </summary>
        /// <param name="item"><see cref="CategoryModel"/></param>
        /// <returns>The created <see cref="CategoryModel"/></returns>
        /// <exception cref="DbUpdateException"></exception>
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

        /// <summary>
        /// Updates a item on the Database
        /// </summary>
        /// <param name="item"><see cref="CategoryModel"/></param>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Deletes a item on the database
        /// </summary>
        /// <param name="id">Id as <seealso cref="Guid"/></param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="DbUpdateException"></exception>
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