using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLib.Interfaces
{
    public interface IDBTableConfiguration
    {
        public Task ConfigureDBTables();
        public Task<List<IdentityUser>> GetUsers();
    }
}
