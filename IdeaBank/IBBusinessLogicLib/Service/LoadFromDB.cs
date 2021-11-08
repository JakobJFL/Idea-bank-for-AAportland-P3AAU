using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using RepositoryLib.Interfaces;

namespace BusinessLogicLib
{

    public delegate bool MyFilter(IdeasTbl idea);
    public class LoadFromDB
    {
        public LoadFromDB(IIdeaRepository repository)
        {
            Repository = repository;
        }
        public string ConnectionString { get; set; }  // slet det
        public IIdeaRepository Repository { get; }

        public async Task<List<ViewIdea>> LoadAllIdeas()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(ConnectionString);
            using Context db = new (optionsBuilder.Options);
            List<IdeasTbl> dbIdeas = await db.IdeasTbl
                .Include(b => b.BusinessUnit)
                .Include(d => d.Department)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
            return DBConvert.TblToViewIdea(dbIdeas);
        }

        public async Task<List<ViewIdea>> LoadIdeas(FilterIdea idea)
        {
            List<IdeasTbl> dbIdeas = (await Repository.ListAsync(idea.BusinessUnit)).ToList();
            return DBConvert.TblToViewIdea(dbIdeas);
        }

        public async Task<List<Comment>> LoadComments(int id)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(ConnectionString);
            using Context db = new (optionsBuilder.Options);
            List<CommentsTbl> dbComments = await db.CommentsTbl
                .Where(c => c.IdeaId == id)
                .OrderByDescending(s => s.Id)
                .ToListAsync();
            return DBConvert.TblToComment(dbComments);
        }
    }
}

