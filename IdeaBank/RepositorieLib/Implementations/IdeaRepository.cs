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
        /// <param name="businessUnitID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IdeasTbl>> ListAsync(int businessUnitID)
        {
            return await Context.IdeasTbl
                .Include(b => b.BusinessUnit)
                .Include(d => d.Department)
                .Where(f => businessUnitID == 0 || businessUnitID == f.BusinessUnit.Id)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        public Task AddAsync(IdeasTbl model)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<IdeasTbl> metrics)
        {
            throw new NotImplementedException();
        }

        public Task<IdeasTbl> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(IdeasTbl model)
        {
            throw new NotImplementedException();
        }

        public Task RemoveByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(IdeasTbl model)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<IdeasTbl> models)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IdeasTbl>> ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
