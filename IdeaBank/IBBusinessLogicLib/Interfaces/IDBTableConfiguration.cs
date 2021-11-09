using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLib.Interfaces
{
    public interface IDBTableConfiguration
    {
        public Task ConfigureDBTables();
    }
}
