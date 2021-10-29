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
        public List<Idea> LoadIdeas(string connectionString);
    }
    public class DataAcces : IDataAcces
    {
        /*
        public DataAcces(IContext context)
        {
            _context = context;
            Console.WriteLine("HEKEOKD");

            foreach (var i in _context.Ideas)
            {
                Console.WriteLine(i.ProjectName);
            }
        }
        */
        
        public async Task<List<DataBaseLib.Models.Idea>> LoadIdeasAsync(string connectionString)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using (var db = new Context(optionsBuilder.Options))
            {
                Task<List<DataBaseLib.Models.Idea>> ideas = db.Ideas.ToListAsync();
                /*
                foreach (var i in )
                {
                    Idea idea = new();
                    idea.ProjectName = i.ProjectName;
                    ideas.Add(idea);
                }
                */
                return await ideas;
            }
        }

        public List<Idea> LoadIdeas(string connectionString)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using (var db = new Context(optionsBuilder.Options))
            {
                List<Idea> ideas = new();
                foreach (var i in db.Ideas)
                {
                    Idea idea = new();
                    idea.ProjectName = i.ProjectName;
                    ideas.Add(idea);
                }
                return ideas;
            }
        }
    }
}

