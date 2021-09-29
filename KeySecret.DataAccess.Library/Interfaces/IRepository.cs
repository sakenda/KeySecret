using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync();
        Task<T> InsertItemAsync(T item);
        Task UpdateItemAsync(T item);
        Task DeleteItemAsync(int id);
    }
}