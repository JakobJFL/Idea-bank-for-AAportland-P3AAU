using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLib.Interfaces;

namespace RepositoryLib.Implementations
{
    public class DepartmentsRepository : IDepartmentsRepository, IRepository<DepartmentsTbl>
    {
        public DepartmentsRepository(Context context)
        {
            Context = context;
        }
        public Context Context { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">If 0 all will be returned</param>
        /// <returns></returns>
        public async Task<IEnumerable<DepartmentsTbl>> ListAsync(int id)
        {
            if (id == 0)
                return await Context.DepartmentsTbl.ToListAsync();
            else
                throw new System.NotImplementedException();
        }

        public async Task AddAsync(DepartmentsTbl model)
        {
            await Context.DepartmentsTbl.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public Task CountAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<DepartmentsTbl> FindByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(DepartmentsTbl model)
        {
            throw new System.NotImplementedException();
        }
    }

}
