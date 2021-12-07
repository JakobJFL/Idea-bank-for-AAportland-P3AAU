using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLib.Interfaces
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> ListAsync(int id);
        public Task AddAsync(T model);
        public Task<T> FindByIdAsync(int id);
        public Task RemoveByIdAsync(int id);
        public Task UpdateAsync(T model);
        public Task CountAsync();
    }
}
