using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseLib.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLib
{
    public interface IDataAcces
    {
        public Task<List<DataBaseLib.Models.Idea>> LoadAllIdeas(string connectionString);
        public Task<DataBaseLib.Models.Idea> LoadIdeaByID(string connectionString, string id);
    }
    public class DataAcces : IDataAcces
    {
        public async Task<List<DataBaseLib.Models.Idea>> LoadAllIdeas(string connectionString)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using var db = new Context(optionsBuilder.Options);
            return await db.Ideas.ToListAsync();
        }

        public async Task<DataBaseLib.Models.Idea> LoadIdeaByID(string connectionString, string id)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using var db = new Context(optionsBuilder.Options);
            string sql = "SELECT * FROM Ideas WHERE Id='" + id + "'";
            return await db.Ideas.FromSqlRaw(sql).SingleAsync();
        }
    }
}

