using DataBaseLib;
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
        /// 
        /// </summary>
<<<<<<< HEAD
        /// <param name="id"></param>
        /// <returns>Returns a list of ideas</returns>
=======
        /// <param name="filterSort"></param>
        /// <returns></returns>
>>>>>>> 0143d1fef93c5306cbe3a9dae8b7e19e8dcd0c09
        public async Task<IEnumerable<IdeasTbl>> ListAsync(FilterSortIdea filterSort)
        {
            IQueryable<IdeasTbl> ideas = Context.IdeasTbl
                .Include(b => b.IdeaBusinessUnit)
                .Include(d => d.IdeaDepartment)
                .Include(b => b.AuthorBusinessUnit)
                .Include(d => d.AuthorDepartment);

            ideas = await Filter(ideas, filterSort);

            if (!string.IsNullOrEmpty(filterSort.SearchStr))
                ideas = await Search(ideas, filterSort);

            ideas = await Sorting(ideas, filterSort);
            IdeasCount = await ideas.CountAsync();
            if (filterSort.IdeasShownCount != 0)
                ideas = ideas.Skip((filterSort.CurrentPage-1) * filterSort.IdeasShownCount).Take(filterSort.IdeasShownCount);
            return await ideas.ToListAsync();
        }
        /// <summary>
        /// If BusinessUnit, Department, Priority, Status, Id is equal to 0, it will get all ideas that because of short circuiting. 
        /// </summary>
        /// <param name="ideas"></param>
        /// <param name="filter"></param>
        /// <returns>Returns a filtered list</returns>
        private Task<IQueryable<IdeasTbl>> Filter(IQueryable<IdeasTbl> ideas, FilterSortIdea filter)
        {
            return Task.FromResult(ideas.Where(f => filter.ShowHidden || !f.IsHidden)
                .Where(i => filter.BusinessUnit == 0 || filter.BusinessUnit == i.IdeaBusinessUnit.Id)
                .Where(i => filter.Department == 0 || filter.Department == i.IdeaDepartment.Id)
                .Where(i => filter.Priority == 0 || filter.Priority == i.Priority)
                .Where(i => filter.Status == 0 || filter.Status == i.Status));
        }
        /// <summary>
        /// Search for an idea
        /// </summary>
        /// <param name="ideas"></param>
        /// <param name="filter"></param>
        /// <returns>Returns the idea that was searched for</returns>
        private Task<IQueryable<IdeasTbl>> Search(IQueryable<IdeasTbl> ideas, FilterSortIdea filter)
        {
            return Task.FromResult(ideas = ideas.Where(f => f.ProjectName.Contains(filter.SearchStr)));
        }
        /// <summary>
        /// Sorts the ideas
        /// </summary>
        /// <param name="ideas"></param>
        /// <param name="filter"></param>
        /// <returns>Returns the table of sorted ideas</returns>
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
        /// <summary>
        /// Removes an idea by id
        /// </summary>
        /// <param name="ideaId"></param>
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
        /// <summary>
        /// Updates an idea
        /// </summary>
        /// <param name="model"></param>
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
        /// <summary>
        /// Counts amount of ideas
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<int> CountAsync(FilterSortIdea filter)
        {
            return Context.IdeasTbl
                .Where(i => !filter.OnlyNewIdeas || i.CreatedAt.Millisecond == i.UpdatedAt.Millisecond)
                .Where(i => filter.Status == 0 || i.Status == filter.Status)
                .CountAsync();
        }
        /// <summary>
        /// Finds an idea by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The found idea</returns>
        public async Task<IdeasTbl> FindByIdAsync(int id)
        {
            return await Context.IdeasTbl
                .Include(b => b.IdeaBusinessUnit)
                .Include(d => d.IdeaDepartment)
                .Include(b => b.AuthorBusinessUnit)
                .Include(d => d.AuthorDepartment)
                .Where(i => i.Id == id).FirstOrDefaultAsync();
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
