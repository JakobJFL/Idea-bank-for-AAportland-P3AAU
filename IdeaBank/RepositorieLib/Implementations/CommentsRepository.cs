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
    public class CommentsRepository : ICommentsRepository
    {
        public CommentsRepository(Context context)
        {
            Context = context;
        }
        public Context Context { get; }
        public async Task<IEnumerable<CommentsTbl>> ListAsync(int id)
        {
            return await Context.CommentsTbl
                .Where(c => c.IdeaId == id)
                .OrderByDescending(s => s.Id)
                .ToListAsync();
        }
        public async Task AddAsync(CommentsTbl model)
        {
            await Context.CommentsTbl.AddAsync(model);
            Context.SaveChanges();
        }

        public Task AddRangeAsync(IEnumerable<CommentsTbl> metrics)
        {
            throw new NotImplementedException();
        }

        public Task<CommentsTbl> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CommentsTbl model)
        {
            throw new NotImplementedException();
        }

    }
}
