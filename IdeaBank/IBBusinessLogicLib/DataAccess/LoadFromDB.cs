using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BusinessLogicLib
{
    public class LoadFromDB
    {
        public async Task<List<ViewIdea>> LoadAllIdeas(string connectionString)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using Context db = new (optionsBuilder.Options);
            List<IdeasTbl> dbIdeas = await db.IdeasTbl.Include(b => b.BusinessUnit).Include(d => d.Department).ToListAsync();
            DBConvert convert = new();
            return convert.TblToViewIdea(dbIdeas);
        }

        public async Task<List<Comment>> LoadComments(string connectionString, int id)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using Context db = new (optionsBuilder.Options);
            List<CommentsTbl> dbComments = await db.CommentsTbl.Where(c => c.IdeaId == id).ToListAsync();
            DBConvert convert = new();
            return convert.TblToComment(dbComments);
        }
    }
}

