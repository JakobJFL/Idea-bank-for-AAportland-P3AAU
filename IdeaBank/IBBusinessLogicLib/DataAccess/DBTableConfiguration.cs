using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLib
{
    public class DBTableConfiguration
    {
        private readonly string[] BusinessUnits = { "Aalborg Portland", "Unicon DK", "Unicon NO", "Kudsk & Dahl" };
        private readonly string[] Departments = { "Salg", "SCM", "Produktion", "Vedligehold", "Finans", "HR", "PMO & Transformation" };
        public void SetDefaultTbls(string connectionString)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using Context db = new(optionsBuilder.Options);
            for (int i = 0; i < Departments.Length; i++)
            {
                DepartmentsTbl dep = new();
                dep.Id = i+1;
                dep.Name = Departments[i];
                db.DepartmentsTbl.Add(dep);
            }
            for (int i = 0; i < BusinessUnits.Length; i++)
            {
                BusinessUnitsTbl bu = new();
                bu.Id = i+1;
                bu.Name = BusinessUnits[i];
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

