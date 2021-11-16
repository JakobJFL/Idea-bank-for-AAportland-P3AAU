using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLib.Interfaces
{
    public interface ITblsConfigRepository
    {
        public Task SetDefaultDeBuTbls();
        public Task<bool> IsBuAndDepEmpty();
        public Task<bool> DoesDatabaseExist();
        public Task<bool> Kat();
    }
}
