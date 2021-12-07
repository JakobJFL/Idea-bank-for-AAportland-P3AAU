using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLib.Interfaces;

namespace RepositoryLib.Implementations
{
    public class BusinessUnitsRepository : IBusinessUnitsRepository, IRepository<BusinessUnitsTbl>
    {
        public BusinessUnitsRepository(Context context)
        {
            Context = context;
        }
        public Context Context { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">If 0 all will be returned</param>
        /// <returns></returns>
        public async Task<IEnumerable<BusinessUnitsTbl>> ListAsync(int id)
        {
            if (id == 0)
                return await Context.BusinessUnitsTbl.ToListAsync();
            else
                throw new System.NotImplementedException();
        }

        public async Task AddAsync(BusinessUnitsTbl model)
        {
            await Context.BusinessUnitsTbl.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public Task CountAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<BusinessUnitsTbl> FindByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(BusinessUnitsTbl model)
        {
            throw new System.NotImplementedException();
        }
    }
}
