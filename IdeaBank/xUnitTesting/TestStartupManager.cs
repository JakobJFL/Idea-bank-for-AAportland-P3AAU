using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using Bunit.TestDoubles;
using BusinessLogicLib;
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using BusinessLogicLib.Service;
using DataBaseLib.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLib.Implementations;
using RepositoryLib.Interfaces;

namespace XUnitTesting
{
    public static class TestStartupManager
    {
        private readonly static string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IdeaBank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static Context GetRepositoryConnection()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(_connectionString);
            return new Context(optionsBuilder.Options);
        }
        public static TestContext InitializeTestContext()
        {
            var ctx = new TestContext();
            ctx.AddTestAuthorization();
            ctx.Services.AddScoped<IIdeaRepository, IdeaRepository>();
            ctx.Services.AddScoped<ICommentsRepository, CommentsRepository>();
            ctx.Services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            ctx.Services.AddScoped<IBusinessUnitsRepository, BusinessUnitsRepository>();
            ctx.Services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
            ctx.Services.AddScoped<IDepartmentsDataAccess, DepartmentsDataAccess>();
            ctx.Services.AddScoped<IBusinessUnitsDataAccess, BusinessUnitsDataAccess>();
            ctx.Services.AddScoped<IIdeasDataAccess, IdeasDataAccess>();
            ctx.Services.AddScoped<IConfig, Config>();
            ctx.Services.AddSingleton<Settings>();
            ctx.Services.AddScoped<ICommentsDataAccess, CommentsDataAccess>();
            ctx.Services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(_connectionString);
            });
            return ctx;
        }
    }
}
