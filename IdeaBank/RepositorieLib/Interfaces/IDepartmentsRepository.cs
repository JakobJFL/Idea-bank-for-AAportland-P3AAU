using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseLib.Models;

namespace RepositoryLib.Interfaces
{
    public interface IDepartmentsRepository
    {
        public Task<IEnumerable<DepartmentsTbl>> ListAsync(int id);
        public Task AddAsync(DepartmentsTbl model);
        public Task<DepartmentsTbl> FindByIdAsync(int id);
        public Task RemoveByIdAsync(int id);
        public Task UpdateAsync(DepartmentsTbl model);
        public Task CountAsync();
    }
}
