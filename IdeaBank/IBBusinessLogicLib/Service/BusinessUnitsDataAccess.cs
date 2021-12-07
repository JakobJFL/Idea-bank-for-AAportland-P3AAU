using BusinessLogicLib.Interfaces;
using DataBaseLib.Models;
using RepositoryLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLib
{
    public class BusinessUnitsDataAccess : IBusinessUnitsDataAccess
    {
        public BusinessUnitsDataAccess(IBusinessUnitsRepository repository)
        {
            Repository = repository;
        }

        public IBusinessUnitsRepository Repository { get; set; }

        public async Task<List<BusinessUnitsTbl>> GetAll()
        {
            return (await Repository.ListAsync(0)).ToList();
        }

        public async Task Insert(BusinessUnitsTbl model)
        {
            await Repository.AddAsync(model);
        }
    }
}

