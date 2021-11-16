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
    public class IdeaRepository : IIdeaRepository, IRepository<IdeasTbl>
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
                .Include(i => i.Comments)
                .Where(f => idea.BusinessUnit == 0 || idea.BusinessUnit == f.BusinessUnit.Id)
                .Where(f => idea.Department == 0 || idea.Department == f.Department.Id)
                .Where(f => idea.Priority == 0 || idea.Priority == f.Priority)
                .Where(f => idea.Status == 0 || idea.Status == f.Status)
                .Where(f => idea.Id == 0 || idea.Id == f.Id);
            if (!string.IsNullOrEmpty(idea.SearchStr))
            {
                ideas = ideas.Where(f => f.ProjectName.Contains(idea.SearchStr));
            }
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
                    throw new ArgumentException("Sorting was not was not within range of Sort");
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
            await Context.SaveChangesAsync();
        }
        public async Task RemoveByIdAsync(int ideaId)
        {
            IdeasTbl ideaToRemove = Context.IdeasTbl.SingleOrDefault(x => x.Id == ideaId);
            if (ideaToRemove != null)
            {
                Context.IdeasTbl.Remove(ideaToRemove);
                List<CommentsTbl> commentsToRemove = Context.CommentsTbl.Where(c => c.Idea == ideaToRemove).ToList();
                foreach (CommentsTbl comment in commentsToRemove)
                {
                    Context.CommentsTbl.Remove(comment);
                }
                await Context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(IdeasTbl model, int departmentId, int businessUnitId)
        {
            IdeasTbl result = Context.IdeasTbl.SingleOrDefault(b => b.Id == model.Id);
            if (result != null)
            {
                result.Department = await Context.DepartmentsTbl
                   .Where(d => d.Id == departmentId)
                   .FirstAsync();
                result.BusinessUnit = await Context.BusinessUnitsTbl
                    .Where(b => b.Id == businessUnitId)
                    .FirstAsync();
                Context.Entry(result).CurrentValues.SetValues(model);
                await Context.SaveChangesAsync();
            }
            else
                throw new ArgumentNullException("Idea not found");
        }
        public Task AddRangeAsync(IEnumerable<IdeasTbl> metrics)
        {
            throw new NotImplementedException();
        }

        public Task<IdeasTbl> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IdeasTbl>> ListAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(IdeasTbl model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdeasTbl model)
        {
            throw new NotImplementedException();
        }
    }
}
