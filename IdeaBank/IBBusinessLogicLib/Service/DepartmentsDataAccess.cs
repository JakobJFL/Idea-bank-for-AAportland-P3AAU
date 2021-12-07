using BusinessLogicLib.Interfaces;
using DataBaseLib.Models;
using RepositoryLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLib
{
    public class DepartmentsDataAccess : IDepartmentsDataAccess
    {
        public DepartmentsDataAccess(IDepartmentsRepository repository)
        {
            Repository = repository;
        }

        public IDepartmentsRepository Repository { get; set; }
        public async Task<List<DepartmentsTbl>> GetAll()
        {
            return (await Repository.ListAsync(0)).ToList();
        }

        public async Task Insert(DepartmentsTbl model)
        {
            await Repository.AddAsync(model);
        }
    }
}

