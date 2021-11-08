using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLib.Implementations
{
    public class IdeaRepository : IIdeaRepository
    {
        public IdeaRepository(Context context)
        {
            Context = context;
        }
        public Context Context { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IdeasTbl>> ListAsync(int id)
        {
            return await Context.IdeasTbl
                .Include(b => b.BusinessUnit)
                .Include(d => d.Department)
                .Where(f => id == 0 || id == f.BusinessUnit.Id)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="departmentId"></param>
        /// <param name="businessUnitId"></param>
        /// <returns></returns>
        public async Task AddAsync(IdeasTbl model, int departmentId, int businessUnitId)
        {
            model.Department = await Context.DepartmentsTbl
               .Where(d => d.Id == departmentId)
               .FirstAsync();
            model.BusinessUnit = await Context.BusinessUnitsTbl
                .Where(b => b.Id == businessUnitId)
                .FirstAsync();

            await Context.IdeasTbl.AddAsync(model);
            Context.SaveChanges();
        }
        public Task AddRangeAsync(IEnumerable<IdeasTbl> metrics)
        {
            throw new NotImplementedException();
        }

        public Task<IdeasTbl> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public void Update(IdeasTbl model)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(IdeasTbl model)
        {
            throw new NotImplementedException();
        }
    }
}
