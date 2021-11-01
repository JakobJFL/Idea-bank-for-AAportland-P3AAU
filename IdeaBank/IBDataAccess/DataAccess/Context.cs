using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLib.DataAccess
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<IdeasTbl> IdeasTbl { get; set; }
        public DbSet<DepartmentsTbl> DepartmentsTbl { get; set; }
        public DbSet<CommentsTbl> CommentsTbl { get; set; }
        public DbSet<BusinessUnitsTbl> BusinessUnitsTbl { get; set; }
    }
}
