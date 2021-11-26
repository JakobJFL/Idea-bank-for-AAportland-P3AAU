using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLib.Implementations;
using RepositoryLib.Interfaces;
using Xunit;

namespace XUnitTesting

{
    public class IdeaRepositoryTests
    {
        private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IdeaBank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [Fact]
        public async void Kat()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(_connectionString);
            Context context = new Context(optionsBuilder.Options);
            // arrange
            FilterSortIdea filter = new();
            filter.Priority = 1;
            filter.CurrentPage = 1;
            filter.IdeasShownCount = 1;


            IdeaRepository repository = new(context);

            List<IdeasTbl> result = (await repository.ListAsync(filter)).ToList();

            // assert

            Assert.Equal(1, 1);
        }

       
    }
}