using DataBaseLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLib.Interfaces
{
    public interface IIdeaRepository : IRepository<IdeasTbl>
    {
        public Task<IEnumerable<IdeasTbl>> ListAsync(int businessUnitID);
    }
}
