using DataBaseLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLib.Interfaces
{
    public interface IIdeaRepository
    {
        public int IdeasCount { get; set; }
        public Task<IEnumerable<IdeasTbl>> ListAsync(FilterIdea idea);
        public Task AddAsync(IdeasTbl model);
        public Task RemoveByIdAsync(int id);
        public Task UpdateAsync(IdeasTbl model);
        public Task<IdeasTbl> FindByIdAsync(int id);
        public Task AddRangeAsync(IEnumerable<IdeasTbl> metrics);
        public Task<int> CountAsync(FilterIdea idea);

    }
}

