using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseLib.Models;

namespace RepositoryLib.Interfaces
{
    public interface IBusinessUnitsRepository
    {
        public Task<IEnumerable<BusinessUnitsTbl>> ListAsync(int id);
        public Task AddAsync(BusinessUnitsTbl model);
        public Task<BusinessUnitsTbl> FindByIdAsync(int id);
        public Task RemoveByIdAsync(int id);
        public Task UpdateAsync(BusinessUnitsTbl model);
        public Task CountAsync();
    }
}
