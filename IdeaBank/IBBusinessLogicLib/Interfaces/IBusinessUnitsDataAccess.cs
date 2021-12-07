using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseLib.Models;

namespace BusinessLogicLib.Interfaces
{
    public interface IBusinessUnitsDataAccess
    {
        public Task<List<BusinessUnitsTbl>> GetAll();
        public Task Insert(BusinessUnitsTbl model);
    }
}
