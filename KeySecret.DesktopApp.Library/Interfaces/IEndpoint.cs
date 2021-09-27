using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.Interfaces
{
    public interface IEndpoint<T, K, U>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(K item);
        Task InsertAsync(U item);
        Task DeleteAsync(int id);
    }
}