using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLib.Models;

namespace BusinessLogicLib.Interfaces
{
    public interface ICommentsDataAccess
    {
        public Task<List<Comment>> GetWFilter(int id);
        public void Insert(Comment comment);
        public Task DeleteByID(int id);
    }
}
