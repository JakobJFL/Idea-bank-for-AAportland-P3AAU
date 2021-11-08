using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLib.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> ListAsync();
        Task AddAsync(T model);
        Task AddRangeAsync(IEnumerable<T> metrics);
        Task<T> FindByIdAsync(string id);
        void Remove(T model);
        Task RemoveByIdAsync(string id);
        void Update(T model);
        void UpdateRange(IEnumerable<T> models);
    }
}
