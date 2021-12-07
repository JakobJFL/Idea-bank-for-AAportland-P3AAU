using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseLib.Models;

namespace BusinessLogicLib.Interfaces
{
    public interface IDepartmentsDataAccess
    {
        public Task<List<DepartmentsTbl>> GetAll();
        public Task Insert(DepartmentsTbl model);

    }
}
