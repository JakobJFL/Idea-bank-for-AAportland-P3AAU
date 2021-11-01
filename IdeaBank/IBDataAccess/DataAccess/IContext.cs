using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLib.DataAccess
{
    public interface IContext
    {
        DbSet<BusinessUnitsTbl> BusinessUnitsTbl { get; set; }
        DbSet<CommentsTbl> CommentsTbl { get; set; }
        DbSet<DepartmentsTbl> DepartmentsTbl { get; set; }
        DbSet<IdeasTbl> IdeasTbl { get; set; }
    }
}