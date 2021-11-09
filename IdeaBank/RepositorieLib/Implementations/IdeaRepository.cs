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
        ///
        public async Task<IEnumerable<IdeasTbl>> ListAsync(FilterIdea idea)
        {
            IQueryable<IdeasTbl> ideas = Context.IdeasTbl
                .Include(b => b.BusinessUnit)
                .Include(d => d.Department)
                .Where(f => idea.BusinessUnit == 0 || idea.BusinessUnit == f.BusinessUnit.Id)
                .Where(f => idea.Department == 0 || idea.Department == f.Department.Id)
                .Where(f => idea.Priority == 0 || idea.Priority == f.Priority)
                .Where(f => idea.Status == 0 || idea.Status == f.Status);
            switch (idea.Sorting)
            {
                case Sort.ProjectNameAsc:
                    ideas = ideas.OrderBy(s => s.ProjectName);
                    break;
                case Sort.ProjectNameDesc:
                    ideas = ideas.OrderByDescending(s => s.ProjectName);
                    break;
                case Sort.CreatedAtAsc:
                    ideas = ideas.OrderBy(s => s.CreatedAt);
                    break;
                case Sort.CreatedAtDesc:
                    ideas = ideas.OrderByDescending(s => s.CreatedAt);
                    break;
                case Sort.UpdatedAtAsc:
                    ideas = ideas.OrderBy(s => s.UpdatedAt);
                    break;
                case Sort.UpdatedAtDesc:
                    ideas = ideas.OrderByDescending(s => s.UpdatedAt);
                    break;
                default:
                    throw new ArgumentException("Skriv noget her");
            }
            return await ideas.ToListAsync();
        }
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
