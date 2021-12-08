using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLib;
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
        /* Hvad skal tests:
            * 
            * 
            * 
            * 
         */
        [Fact]
        public async void AddAsync_Idea_IdeaAdded()
        {
            // arrange
            IdeaRepository repository = new(TestStartupManager.GetRepositoryConnection());
            IdeasTbl idea = new()
            {
                ProjectName = "Test",
                Description = "Test description",
                Initials = "test",
                Priority = 1,
                Status = 1
            };

            // act
            await repository.AddAsync(idea);

            // assert
            Assert.NotNull(await repository.FindByIdAsync(idea.Id));

            // clean up
            await repository.RemoveByIdAsync(idea.Id);
        }

        [Fact]
        public async void RemoveById_RemoveIdea_IdeaRemoved()
        {
            // arrange
            IdeaRepository ideasRepository = new(TestStartupManager.GetRepositoryConnection());
            CommentsRepository commentsRepository = new(TestStartupManager.GetRepositoryConnection());
            IdeasTbl idea = new()
            {
                ProjectName = "testRemoveIdea",
                Description = "Test description",
                Initials = "TEST",
                Priority = 1,
                Status = 1
            };

            // act
            await ideasRepository.AddAsync(idea);
            await ideasRepository.RemoveByIdAsync(idea.Id);

            // assert
            Assert.Null(await ideasRepository.FindByIdAsync(idea.Id));
            Assert.Equal(await commentsRepository.ListAsync(idea.Id), new List<CommentsTbl>()); // Is comments removed
        }

        private readonly int _numOfTestIdeas = 25;
        private Stack<int> _testDBIdeasID = new();

        private async void SetupTestDB()
        {
            IdeaRepository ideasRepository = new(TestStartupManager.GetRepositoryConnection());
            CommentsRepository commentsRepository = new(TestStartupManager.GetRepositoryConnection());

            for (int i = 0; i < _numOfTestIdeas; i++)
            {
                IdeasTbl idea = new()
                {
                    ProjectName = "Test "+i,
                    Description = "Test description "+i,
                    Initials = "Ini" + i,
                    Priority = (i+2) % DBConvert.PriorityStrs.Length,
                    Status = i % DBConvert.StatusStrs.Length,
                };
                await ideasRepository.AddAsync(idea);

                _testDBIdeasID.Push(idea.Id);
                for (int c = 0; c < (i+1)%7; c++)
                {
                    CommentsTbl comment = new()
                    {
                        Idea = idea,
                        Message = "Test description " + c,
                        Initials = "Ini" + c,
                    };
                    await commentsRepository.AddAsync(comment, idea.Id);
                }
            }
        }

        private async void CleanUpTestDB()
        {
            IdeaRepository repository = new(TestStartupManager.GetRepositoryConnection());
            foreach (int id in _testDBIdeasID)
            {
                await repository.RemoveByIdAsync(id);
            }
            _testDBIdeasID.Clear();
        }

        [Fact]
        //Setup test ideas in db for this test:
        public async void Filter_FilterByPriority_PrioritisedIdeas()
        {
            // arrange
            SetupTestDB();
            IdeaRepository repository = new(TestStartupManager.GetRepositoryConnection());
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

            CleanUpTestDB();
        }
    }
}