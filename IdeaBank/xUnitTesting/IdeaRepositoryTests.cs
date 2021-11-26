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

        public IdeaRepository GetIdeasRepositoryConnection()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(_connectionString);
            Context context = new Context(optionsBuilder.Options);
            IdeaRepository repository = new(context);
            return repository;
        }

        [Fact]
        public async void Kat()
        {
            
            IdeaRepository repository = GetIdeasRepositoryConnection();
            // arrange
            FilterSortIdea filter = new();
            filter.Priority = 1;
            filter.CurrentPage = 1;
            filter.IdeasShownCount = 1;
            List<IdeasTbl> result = (await repository.ListAsync(filter)).ToList();

            // assert
            for (int i = 0; i < result.Count(); i++)
            {
            Assert.Equal(1, result[i].Priority);
            }
        }

       
    }
}