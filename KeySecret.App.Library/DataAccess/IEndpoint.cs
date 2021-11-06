using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.App.Library
{
    public interface IEndpoint<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(Guid id);
        Task InsertAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(Guid id);
    }
}