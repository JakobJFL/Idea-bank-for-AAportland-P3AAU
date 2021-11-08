using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLib.Interfaces
{
    public interface IDataAccess<T>
    {
        Task<List<T>> GetWFilter();
        Task Insert();
        Task Edit();
        Task DeleteByID();
    }
}
