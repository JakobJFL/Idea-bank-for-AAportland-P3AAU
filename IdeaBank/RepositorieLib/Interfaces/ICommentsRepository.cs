using DataBaseLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLib.Interfaces
{
    public interface ICommentsRepository
    {
        public Task<IEnumerable<CommentsTbl>> ListAsync(int id);
        public Task AddAsync(CommentsTbl model, int ideaId);
        public Task RemoveByIdAsync(int id);
        public Task UpdateAsync(IdeasTbl model);
        public Task<IdeasTbl> FindByIdAsync(int id);
        public Task<int> CountAsync(FilterIdea filter);
        public Task<int> CountAsync(int ideaId);
    }
}
