using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.Interfaces
{
    public interface IEndpoint<T, K>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(K item);
    }
}