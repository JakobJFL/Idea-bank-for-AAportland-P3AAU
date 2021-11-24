using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataBaseLib.Models;
using RepositoryLib.Implementations;
using Xunit;

namespace XUnitTesting

{
    public class IdeaRepositoryTests
    {
       
        [Fact]
        public async void StrNewLineToBr_StringWNL_StringWBr()
        {
            // arrange
            FilterSortIdea filter = new();
            filter.Priority = 1;

            List<IdeasTbl> ideasDB = MakeDB();

            int[] resultId = new int[] { 2, 3 };

            // act
            IdeaRepository ideaRepository = new();
            IQueryable<IdeasTbl> ideas = ideasDB.AsQueryable();
            IQueryable<IdeasTbl> mus = await ideaRepository.Filter(ideas, filter);
            List<IdeasTbl> result = mus.ToList();

            // assert
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.Equal(result[i].Id, resultId[i]);
            }
        }

        private List<IdeasTbl> MakeDB()
        {
            List<IdeasTbl> dBIdeas = new();
            dBIdeas.Add(MakeIdea(1, 1, 2, false, 2, 3));
            dBIdeas.Add(MakeIdea(2, 3, 1, false, 4, 4));
            dBIdeas.Add(MakeIdea(3, 2, 3, false, 3, 4));
            return dBIdeas;
        }

        private IdeasTbl MakeIdea(int id, int priority, int status, bool isHidden, int departmentId, int businessUnitId)
        {
            IdeasTbl idea = new();
            idea.Id = id;
            idea.Priority = priority;
            idea.Status = status;
            idea.IsHidden = isHidden;
            idea.IdeaDepartment = new DepartmentsTbl() { Id = departmentId };
            idea.IdeaBusinessUnit = new BusinessUnitsTbl() { Id = businessUnitId };
            idea.Initials = "dssa";
            idea.Description = "sdsdsd";
            return idea;
        }
    }
}