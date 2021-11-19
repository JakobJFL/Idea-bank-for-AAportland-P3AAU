using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLib.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> ListAsync(int id);
        Task AddAsync(T model);
        Task AddRangeAsync(IEnumerable<T> metrics);
        Task<T> FindByIdAsync(int id);
        Task RemoveByIdAsync(int id);
        Task UpdateAsync(T model);
        Task CountAsync();
    }
}
