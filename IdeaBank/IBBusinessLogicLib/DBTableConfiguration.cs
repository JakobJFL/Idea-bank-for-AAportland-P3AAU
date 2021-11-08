using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLib
{
    public class DBTableConfiguration
    {
        private readonly string[] _businessUnits = { "Ikke Angivet", "Aalborg Portland", "Unicon DK", "Unicon NO", "Kudsk & Dahl" };
        private readonly string[] _departments = { "Ikke Angivet", "Salg", "SCM", "Produktion", "Vedligehold", "Finans", "HR", "PMO & Transformation" };
        public void SetDefaultTbls(string connectionString)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using Context db = new(optionsBuilder.Options);
            for (int i = 0; i < _departments.Length; i++)
            {
                DepartmentsTbl dep = new();
                dep.Id = i+1;
                dep.Name = _departments[i];
                db.DepartmentsTbl.Add(dep);
            }
            for (int i = 0; i < _businessUnits.Length; i++)
            {
                BusinessUnitsTbl bu = new();
                bu.Id = i+1;
                bu.Name = _businessUnits[i];
                db.BusinessUnitsTbl.Add(bu);
            }
            try
            {
                db.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                //FIX Exception  HERE
                throw new System.Exception("FUCK "+ ex.Message);
            }
        }
        public async Task<bool> IsBuAndDepEmpty(string connectionString)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using Context db = new(optionsBuilder.Options);
            return !(await db.DepartmentsTbl.AnyAsync() && await db.BusinessUnitsTbl.AnyAsync());
        }
    }
}

