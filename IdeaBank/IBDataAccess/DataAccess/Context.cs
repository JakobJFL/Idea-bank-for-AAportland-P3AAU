using DataBaseLib.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLib.DataAccess
{
    public class Context : IdentityDbContext, IContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<IdeasTbl> IdeasTbl { get; set; }
        public DbSet<DepartmentsTbl> DepartmentsTbl { get; set; }
        public DbSet<CommentsTbl> CommentsTbl { get; set; }
        public DbSet<BusinessUnitsTbl> BusinessUnitsTbl { get; set; }
    }
}
