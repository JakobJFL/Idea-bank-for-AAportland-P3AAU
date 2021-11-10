using System.Threading.Tasks;
using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLib.Interfaces;

namespace RepositoryLib.Implementations
{
    public class TblsConfigRepository : ITblsConfigRepository
    {
        public TblsConfigRepository(Context context)
        {
            Context = context;
        }
        public Context Context { get; }

        private readonly string[] _businessUnits = { "Ikke Angivet", "Aalborg Portland", "Unicon DK", "Unicon NO", "Kudsk & Dahl" };
        private readonly string[] _departments = { "Ikke Angivet", "Salg", "SCM", "Produktion", "Vedligehold", "Finans", "HR", "PMO & Transformation" };
        public void SetDefaultTbls()
        {
            for (int i = 0; i < _departments.Length; i++)
            {
                DepartmentsTbl dep = new();
                dep.Id = i + 1;
                dep.Name = _departments[i];
                Context.DepartmentsTbl.Add(dep);
            }
            for (int i = 0; i < _businessUnits.Length; i++)
            {
                BusinessUnitsTbl bu = new();
                bu.Id = i + 1;
                bu.Name = _businessUnits[i];
                Context.BusinessUnitsTbl.Add(bu);
            }
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                //FIX Exception  HERE
                throw new DbUpdateException("Updating tables in database failed." + ex.Message);
            }
        }
        public async Task<bool> IsBuAndDepEmpty()
        {
            return !(await Context.DepartmentsTbl.AnyAsync() && await Context.BusinessUnitsTbl.AnyAsync());
        }
    }
}
