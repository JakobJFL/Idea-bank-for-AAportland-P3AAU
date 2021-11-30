using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        /// Filter and sort ideas
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IdeasTbl>> ListAsync(FilterSortIdea filterSort)
        {
            IQueryable<IdeasTbl> ideas = Context.IdeasTbl
                .Include(b => b.IdeaBusinessUnit)
                .Include(d => d.IdeaDepartment)
                .Include(d => d.AuthorBusinessUnit)
                .Include(d => d.AuthorDepartment)
                .Include(i => i.Comments);

            ideas = await Filter(ideas, filterSort);

            if (!string.IsNullOrEmpty(filterSort.SearchStr))
                ideas = await Search(ideas, filterSort);

            ideas = await Sorting(ideas, filterSort);
            IdeasCount = await ideas.CountAsync();
            ideas = ideas.Skip((filterSort.CurrentPage-1) * filterSort.IdeasShownCount).Take(filterSort.IdeasShownCount);
            return await ideas.ToListAsync();
        }
        /// <summary>
        /// If business unit is equal to 0, it will get all ideas from all busines´ unit because of short circuiting. 
        /// </summary>
        /// <param name="ideas"></param>
        /// <param name="filter"></param>
        /// <returns>Returns a filltered list</returns>
        private Task<IQueryable<IdeasTbl>> Filter(IQueryable<IdeasTbl> ideas, FilterSortIdea filter)
        {
            return Task.FromResult(ideas.Where(f => filter.ShowHidden || !f.IsHidden)
                .Where(f => filter.BusinessUnit == 0 || filter.BusinessUnit == f.IdeaBusinessUnit.Id)
                .Where(f => filter.Department == 0 || filter.Department == f.IdeaDepartment.Id)
                .Where(f => filter.Priority == 0 || filter.Priority == f.Priority)
                .Where(f => filter.Status == 0 || filter.Status == f.Status)
                .Where(f => filter.Id == 0 || filter.Id == f.Id));
        }
        private Task<IQueryable<IdeasTbl>> Search(IQueryable<IdeasTbl> ideas, FilterSortIdea filter)
        {
            return Task.FromResult(ideas = ideas.Where(f => f.ProjectName.Contains(filter.SearchStr)));
        }
        private Task<IQueryable<IdeasTbl>> Sorting(IQueryable<IdeasTbl> ideas, FilterSortIdea filter)
        {
            switch (filter.Sorting)
            {
                case Sort.ProjectNameAsc:
                    ideas = ideas.OrderBy(s => EF.Functions.Collate(s.ProjectName, "Danish_Norwegian_CI_AS"));
                    break;
                case Sort.ProjectNameDesc:
                    ideas = ideas.OrderByDescending(s => EF.Functions.Collate(s.ProjectName, "Danish_Norwegian_CI_AS"));
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
            return Task.FromResult(ideas);
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
        public Task<int> CountAsync(FilterSortIdea filter)
        {
            return Context.IdeasTbl
                .Where(i => !filter.OnlyNewIdeas || i.CreatedAt.Millisecond == i.UpdatedAt.Millisecond)
                .Where(i => filter.Status == 0 || i.Status == filter.Status)
                .CountAsync();
        }
        public Task<IdeasTbl> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IdeasTbl>> ListAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task IRepository<IdeasTbl>.CountAsync()
        {
            throw new NotImplementedException();
        }
    }
}
