using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.Interfaces
{
    public interface IEndpoint<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(T item);
        Task InsertAsync(T item);
        Task DeleteAsync(int id);
    }
}