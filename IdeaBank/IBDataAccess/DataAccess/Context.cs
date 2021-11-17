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
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentsTbl>()
                        .HasMany(b => b.AuthorIdeas)
                        .WithOne(fa => fa.AuthorDepartment)
                        .HasForeignKey(i => i.AuthorDepartmentId)
                        .IsRequired();

            modelBuilder.Entity<DepartmentsTbl>()
                       .HasMany(b => b.IdeaIdeas)
                       .WithOne(fa => fa.IdeaDepartment)
                       .HasForeignKey(i => i.IdeaDepartmentId)
                       .IsRequired();

            modelBuilder.Entity<BusinessUnitsTbl>()
                        .HasMany(b => b.AuthorIdeas)
                        .WithOne(fa => fa.AuthorBusinessUnit)
                        .HasForeignKey(i => i.AuthorBusinessUnitId)
                        .IsRequired();

            modelBuilder.Entity<BusinessUnitsTbl>()
                       .HasMany(b => b.IdeaIdeas)
                       .WithOne(fa => fa.IdeaBusinessUnit)
                       .HasForeignKey(i => i.IdeaBusinessUnitId)
                       .IsRequired();
        }
        */
    }
}
