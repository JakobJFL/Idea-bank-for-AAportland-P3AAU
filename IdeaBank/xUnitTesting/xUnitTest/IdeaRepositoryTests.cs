using System.Collections.Generic;
using System.Linq;
using BusinessLogicLib.Service;
using DataBaseLib;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLib.Implementations;
using Xunit;
using System.Threading.Tasks;
using System;

namespace Testing.XUnitTest
{
    [Collection("Test Database")]
    public class IdeaRepositoryTests
    {
        [Fact]
        public async void AddAsync_CanAddIdea_IdeaAdded()
        {
            // arrange
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());
            IdeasTbl idea = new()
            {
                ProjectName = ".UnitTest AddAsync_CanAddIdea_IdeaAdded",
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
            IdeaRepository ideasRepository = new(Utilities.GetRepositoryConnection());
            CommentsRepository commentsRepository = new(Utilities.GetRepositoryConnection());
            IdeasTbl idea = new()
            {
                ProjectName = ".UnitTest RemoveById_RemoveIdea_IdeaRemoved",
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

        [Fact]
        public async void Filter_FilterByPriority_PrioritisedIdeas()
        {
            // arrange
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());
            List<IdeasTbl> result;
            FilterSortIdea filter = new()
            {
                ShowHidden = true,
                IdeasShownCount = 0,
            };

            // act
            for (int i = 1; i <= Config.PriorityStrs.Length; i++)
            {
                filter.Priority = i;
                result = (await repository.ListAsync(filter)).ToList();
                foreach (IdeasTbl item in result)
                {
                    // assert
                    Assert.Equal(i, item.Priority);
                }
            }
        }

        [Fact]
        public async void Filter_FilterByZeroPriority_AllIdeas()
        {
            // arrange
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());
            List<IdeasTbl> result;
            FilterSortIdea filter = new()
            {
                ShowHidden = true,
                IdeasShownCount = 0,
                Priority = 0
            };

            // act
            result = (await repository.ListAsync(filter)).ToList();
            int count = await repository.CountAsync(new FilterSortIdea());
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async void Filter_FilterByStatus_IdeasWCorrectStatus()
        {
            // arrange
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());
            List<IdeasTbl> result;
            FilterSortIdea filter = new()
            {
                ShowHidden = true,
                IdeasShownCount = 0,
            };

            for (int i = 1; i <= Config.StatusStrs.Length; i++)
            {
                filter.Status = i;
                // act
                result = (await repository.ListAsync(filter)).ToList();
                // assert
                foreach (IdeasTbl item in result)
                    Assert.Equal(i, item.Status); 
            }
        }

        [Fact]
        public async void Filter_FilterByZeroStatus_AllIdeas()
        {
            // arrange
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());
            List<IdeasTbl> result;
            FilterSortIdea filter = new()
            {
                ShowHidden = true,
                IdeasShownCount = 0,
                Status = 0
            };

            // act
            result = (await repository.ListAsync(filter)).ToList();
            int count = await repository.CountAsync(new FilterSortIdea());
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async void Filter_FilterByIsHidden_OnlyPublicIdeas()
        {
            // arrange
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());
            List<IdeasTbl> result;
            FilterSortIdea filter = new()
            {
                ShowHidden = false,
                IdeasShownCount = 0,
            };

            // act
            result = (await repository.ListAsync(filter)).ToList();
            foreach (IdeasTbl idea in result)
            {
                // assert
                Assert.False(idea.IsHidden);
            }
        }

        [Fact]
        public async void Filter_FilterByShowHidden_AllIdeas()
        {
            // arrange
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());
            List<IdeasTbl> result;
            FilterSortIdea filter = new()
            {
                ShowHidden = true,
                IdeasShownCount = 0,
            };

            // act
            result = (await repository.ListAsync(filter)).ToList();
            int count = await repository.CountAsync(new FilterSortIdea());
            Assert.Equal(count, result.Count());
        }

        [Theory]
        [InlineData("idea", 1, 2)]
        [InlineData("idea with æøå", 1, 1)]
        [InlineData("idea with \0", 1, 1)]
        public async void AddAsync_IdeaWBuDepAndProjectName_IdeaAdded(string projectName, int buId, int depId)
        {
            // arrange
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());
            IdeasTbl idea = new()
            {
                ProjectName = projectName,
                Description = "Test description",
                Initials = "test",
                AuthorBusinessUnitId = buId,
                AuthorDepartmentId = depId
            };

            // act
            await repository.AddAsync(idea);

            // assert
            Assert.NotNull(await repository.FindByIdAsync(idea.Id));

            // clean up
            await repository.RemoveByIdAsync(idea.Id);
        }

        [Fact]
        public async void AddAsync_IdeaWUnicode_IdeaAdded()
        {
            // arrange
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());
            IdeasTbl idea = new()
            {
                ProjectName = ".UnitTest " + char.ConvertFromUtf32(0x1F642) + char.ConvertFromUtf32(0x1F4AA) + char.ConvertFromUtf32(0x1F44C),
                Description = "Test description",
                Initials = "test",
                AuthorBusinessUnitId = 1,
                AuthorDepartmentId = 1
            };

            // act
            await repository.AddAsync(idea);

            // assert
            Assert.NotNull(await repository.FindByIdAsync(idea.Id));

            // clean up
            await repository.RemoveByIdAsync(idea.Id);
        }

        [Theory]
        [InlineData(".UnitTest business unit out of range", 300, 1)]
        [InlineData(".UnitTest department out of range", 1, 300)]
        [InlineData(".UnitTest both out of range", 300, 300)]
        [InlineData(".UnitTest both out of range", 0, 1)]
        [InlineData(".UnitTest Text too long Text too long Text too long Text too long Text too long", 1, 1)]
        public async Task AddAsync_IdeaWIncorrectBuDepAndProjectName_ThrowsDbUpdateException(string projectName, int buId, int depId)
        {
            // arrange
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());
            IdeasTbl idea = new()
            {
                ProjectName = projectName,
                Description = "Test description",
                Initials = "test",
                AuthorBusinessUnitId = buId,
                AuthorDepartmentId = depId
            };

            // act assert
            await Assert.ThrowsAsync<DbUpdateException>(() => repository.AddAsync(idea));

            // clean up
            await repository.RemoveByIdAsync(idea.Id);
        }

        [Fact]
        public async Task UpdateAsync_IdIsNotInDb_ThrowsArgumentNullException()
        {
            // arrange
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());
            IdeasTbl idea = new()
            {
                Id = 0,
                ProjectName = ".UnitTest IdeaProjectName",
                Description = "Test IdeaDescription",
                Initials = "TEST",
                AuthorBusinessUnitId = 1,
                AuthorDepartmentId = 1
            };
            // act assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.UpdateAsync(idea));
        }

        [Fact]
        public async Task UpdateAsync_IdeaWCorrectInputs_UpdatedIdea()
        {
            // arrange
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());
            IdeasTbl idea = new()
            {
                ProjectName = ".UnitTest before update",
                Description = "before update",
                Initials = "TEST"
            };

            // act
            await repository.AddAsync(idea);
            IdeasTbl updatedIdea = idea;
            updatedIdea.Id = idea.Id;
            updatedIdea.ProjectName = ".UnitTest after update";
            updatedIdea.Description = ".UnitTest Description after update";
            updatedIdea.Status = 2;

            await repository.UpdateAsync(updatedIdea);
            IdeasTbl updatedIdeaFromDB = await repository.FindByIdAsync(idea.Id);

            //assert
            Assert.Equal(updatedIdea, updatedIdeaFromDB);
        }
    }
}