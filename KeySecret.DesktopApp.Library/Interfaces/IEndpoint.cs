using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library
{
    public interface IEndpoint<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(int id);
        Task<T> InsertAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
    }
}