using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DesktopApp.Library.Interfaces
{
    public interface IEndpoint<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
}