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
        public async void Filter_FilterByPriority_PrioritisedIdeas()
        {
            // arrange
            IdeaRepository repository = GetIdeasRepositoryConnection();
            FilterSortIdea filter = new()
            {
                CurrentPage = 1,
                IdeasShownCount = 15
            };
            List<IdeasTbl> result;

            // act
            for (int i = 1; i <= 4; i++)
            {
                filter.Priority = i;
                result = (await repository.ListAsync(filter)).ToList();
                for (int j = 0; j < result.Count; j++)
                {
                    // assert
                    Assert.Equal(i, result[j].Priority);
                }
            }
        }

        [Fact]
        public async void AddAsync_Idea_IdeaAdded()
        {
            // arrange
            IdeaRepository repository = GetIdeasRepositoryConnection();
            FilterSortIdea filter = new()
            {
                CurrentPage = 1,
                IdeasShownCount = 15,
                Sorting = Sort.CreatedAtDesc
            };

            // act
            IdeasTbl idea = new()
            {
                ProjectName = new Guid().ToString(),
                Description = "Test description",
                Initials = "TEST",
                Priority = 1,
                Status = 1
            };
            await repository.AddAsync(idea);
            filter.SearchStr = idea.ProjectName;
            IdeasTbl foundIdea = (await repository.ListAsync(filter)).First();

            // assert
            Assert.Equal(idea.ProjectName, foundIdea.ProjectName);
        }

        [Fact]
        public async void RemoveById_RemoveIdea_IdeaRemoved()
        {
            // arrange
            IdeaRepository repository = GetIdeasRepositoryConnection();
            FilterSortIdea filter = new()
            {
                CurrentPage = 1,
                IdeasShownCount = 15,
                Sorting = Sort.CreatedAtDesc
            };

            // act
            IdeasTbl idea = new()
            {
                ProjectName = new Guid().ToString(),
                Description = "Test description",
                Initials = "TEST",
                Priority = 1,
                Status = 1
            };
            await repository.AddAsync(idea);
            filter.SearchStr = idea.ProjectName;
            IdeasTbl foundIdea = (await repository.ListAsync(filter)).First();

            await repository.RemoveByIdAsync(foundIdea.Id);

            // assert
            Assert.False((await repository.ListAsync(filter)).Any());
        }
    }
}