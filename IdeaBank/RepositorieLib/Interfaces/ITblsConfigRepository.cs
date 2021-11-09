using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLib.Interfaces
{
    public interface ITblsConfigRepository
    {
        public void SetDefaultTbls();
        public Task<bool> IsBuAndDepEmpty();
    }
}
