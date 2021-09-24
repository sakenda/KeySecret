using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library.Interfaces
{
    public interface IRepository<T, K, U>
    {
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync();
        Task<int> InsertItemAsync(K item);
        Task UpdateItemAsync(U item);
        Task DeleteItemAsync(int id);
    }
}