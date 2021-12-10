using System;
using System.Collections.Generic;
using BusinessLogicLib.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseLib.Models;
using DataBaseLib;

namespace BusinessLogicLib.Interfaces
{
    public interface IIdeasDataAccess
    {
        public Task<List<ViewIdea>> GetWFilter(FilterSortIdea filter);
        public Task Insert(Idea idea);
        public Task Edit(EditIdea idea);
        public Task DeleteByID(int id);
        public Task<int> GetCount(FilterSortIdea filter);
        public int GetIdeasCount();
        public Task<ViewIdea> GetByIdAsync(int id);
    }
}
