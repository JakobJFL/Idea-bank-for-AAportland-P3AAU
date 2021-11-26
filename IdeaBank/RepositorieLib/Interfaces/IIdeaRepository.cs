using DataBaseLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLib.Interfaces
{
    public interface IIdeaRepository
    {
        public int IdeasCount { get; set; }
        public Task<IEnumerable<IdeasTbl>> ListAsync(FilterSortIdea idea);
        public Task AddAsync(IdeasTbl model);
        public Task RemoveByIdAsync(int id);
        public Task UpdateAsync(IdeasTbl model);
        public Task<IdeasTbl> FindByIdAsync(int id);
        public Task<int> CountAsync(FilterSortIdea filter);
        public Task<IQueryable<IdeasTbl>> Filter(IQueryable<IdeasTbl> ideas, FilterSortIdea filter);
        public Task<IQueryable<IdeasTbl>> Search(IQueryable<IdeasTbl> ideas, FilterSortIdea filter);
        public Task<IQueryable<IdeasTbl>> Sorting(IQueryable<IdeasTbl> ideas, FilterSortIdea filter);

    }
}

