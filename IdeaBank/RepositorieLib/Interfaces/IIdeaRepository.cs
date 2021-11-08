using DataBaseLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLib.Interfaces
{
    public interface IIdeaRepository
    {
        public Task<IEnumerable<IdeasTbl>> ListAsync(int id);
        public Task AddAsync(IdeasTbl model, int departmentId, int businessUnitId);

    }
}
