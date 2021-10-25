using IBDataAccessLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBDataAccessLib.DataAccess
{
    public class IdeaContext : DbContext
    {
        public IdeaContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Idea> Ideas { get; set; }
    }
}
