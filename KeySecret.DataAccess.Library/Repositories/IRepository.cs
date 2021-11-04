using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetItemAsync(Guid id);
        Task<IEnumerable<T>> GetItemsAsync();
        Task<T> InsertItemAsync(T item);
        void UpdateItemAsync(T item);
        void DeleteItemAsync(Guid id);
    }
}