using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IBDataAccessLib.DataAccess;
using IBDataAccessLib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IBBusinessLogicLib
{
    public class dataAcces
    {
        private IContext _context;
        public dataAcces(IContext context)
        {
            _context = context;
            Console.WriteLine("HEKEOKD");

            foreach (var i in _context.Ideas)
            {
                Console.WriteLine(i.ProjectName);
            }
        }
        public async Task<Idea> loadDataGet(string connectionString)
        {
            {
                using (var db = new DbContext())
                {
                    Idea model = await db.Idea.FirstAsync(vm.Id);

                    vm.Title = model.Title;

                    if (model.Avilable)
                    {
                        // How can call the **Category** property using await operator?
                        vm.CategoryTitle = model.Category.Title;
                    }
                }
            }

            return View(vm);
        }
        public List<Idea> loadData(string connectionString)
        {
            using ()
            {
                var blogs = await _context.Blogs.Where(b => b.Rating > 3).ToListAsync();

            }
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            _context = new Context(optionsBuilder.Options);

            List<Idea> ideas = new();
            foreach (var i in _context.Ideas)
            {
                Idea idea = new();
                idea.ProjectName = i.ProjectName;
                ideas.Add(idea);
                Console.WriteLine(i.ProjectName);
            }
            return ideas;
        }
    }
}

