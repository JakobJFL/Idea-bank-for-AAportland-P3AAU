using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using RepositoryLib.Implementations;
using RepositoryLib.Interfaces;
using Xunit;

namespace XUnitTesting

{
    public class IdeaRepositoryTests : IIdeaRepositoryTests
    {
        public IIdeaRepository Repository { get; set; }
        public IdeaRepositoryTests(IIdeaRepository repository)
        {
            Repository = repository;
        }

        [Fact]
        public async void StrNewLineToBr_StringWNL_StringWBr()
        {
            // arrange
            FilterSortIdea filter = new();
            filter.Priority = 1;

            List<IdeasTbl> ideasDB = MakeDB();

            int[] resultId = new int[] { 2, 3 };

            // act
            IQueryable<IdeasTbl> ideas = ideasDB.AsQueryable();
            IQueryable<IdeasTbl> mus = await Repository.Filter(ideas, filter);
            List<IdeasTbl> result = mus.ToList();

            // assert
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.Equal(result[i].Id, resultId[i]);
            }
        }

       
    }
}