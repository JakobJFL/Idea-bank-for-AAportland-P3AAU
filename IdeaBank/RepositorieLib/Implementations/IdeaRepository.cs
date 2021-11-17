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
        public int IdeasCount { get; set; } = 1;
        public Context Context { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///
        public async Task<IEnumerable<IdeasTbl>> ListAsync(FilterIdea filter)
        {
            IQueryable<IdeasTbl> ideas = Context.IdeasTbl
                .Include(b => b.IdeaBusinessUnit)
                .Include(d => d.IdeaDepartment)
                .Include(d => d.AuthorBusinessUnit)
                .Include(d => d.AuthorDepartment)
                .Include(i => i.Comments)
                .Where(f => filter.BusinessUnit == 0 || filter.BusinessUnit == f.IdeaBusinessUnit.Id)
                .Where(f => filter.Department == 0 || filter.Department == f.IdeaDepartment.Id)
                .Where(f => filter.Priority == 0 || filter.Priority == f.Priority)
                .Where(f => filter.Status == 0 || filter.Status == f.Status)
                .Where(f => filter.Id == 0 || filter.Id == f.Id);
            if (!string.IsNullOrEmpty(filter.SearchStr))
            {
                ideas = ideas.Where(f => f.ProjectName.Contains(filter.SearchStr));
            }
            switch (filter.Sorting)
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
            IdeasCount = await ideas.CountAsync();
            ideas = ideas.Skip((filter.CurrentPage-1) * filter.IdeasShownCount).Take(filter.IdeasShownCount);
            return await ideas.ToListAsync();
        }
        public async Task AddAsync(IdeasTbl model)
        {
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
        public async Task UpdateAsync(IdeasTbl model)
        {
            IdeasTbl ideaToUpdate = Context.IdeasTbl.SingleOrDefault(b => b.Id == model.Id);
            if (ideaToUpdate != null)
            {
                Context.Entry(ideaToUpdate).CurrentValues.SetValues(model);
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
    }
}
