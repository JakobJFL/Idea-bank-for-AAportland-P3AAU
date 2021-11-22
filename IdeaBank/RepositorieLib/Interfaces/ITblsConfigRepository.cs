using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RepositoryLib.Interfaces
{
    public interface ITblsConfigRepository
    {
        public Task SetDefaultDeBuTbls();
        public Task<bool> IsBuAndDepEmpty();
        public Task<bool> DoesDatabaseExist();
        public Task<List<IdentityUser>> GetUsernameList();
        public Task<bool> IsAnyUsers();
    }
}
