using IBDataAccessLib.Models;
using Microsoft.EntityFrameworkCore;

namespace IBDataAccessLib.DataAccess
{
    public interface IContext
    {
        DbSet<BusinessUnit> BusinessUnits { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<Idea> Ideas { get; set; }
    }
}