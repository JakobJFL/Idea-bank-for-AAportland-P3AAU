using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLib
{
    public class InsertToDB
    {
        public async Task InsertIdea(string connectionString, NewIdea idea)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using Context db = new(optionsBuilder.Options);
            IdeasTbl dbIdea = DBConvert.NewIdeaToTbl(idea);

            // https://stackoverflow.com/a/60807518
            dbIdea.Department = await db.DepartmentsTbl.Where(x => x.Id == idea.Department).FirstAsync();
            dbIdea.BusinessUnit = await db.BusinessUnitsTbl.Where(x => x.Id == idea.BusinessUnit).FirstAsync();

            await db.IdeasTbl.AddAsync(dbIdea);
            db.SaveChanges();
        }
        public void InsertComment(string connectionString, Comment comment)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using Context db = new(optionsBuilder.Options);
            db.CommentsTbl.Add(DBConvert.CommentToTbl(comment));
            db.SaveChanges();
        }
    }
}