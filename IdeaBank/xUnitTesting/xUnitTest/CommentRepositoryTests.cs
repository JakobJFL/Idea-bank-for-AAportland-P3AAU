using System;
using System.Linq;
using DataBaseLib.Models;
using RepositoryLib.Implementations;
using Xunit;

namespace Testing.XUnitTest
{
    [Collection("Test Database")]
    public class CommentsRepositoryTests
    {
        [Fact]
        public async void AddAsync_Comment_IdeaWComment()
        {
            // arrange
            IdeaRepository ideaRepository = new(Utilities.GetRepositoryConnection());
            CommentsRepository commentsRepository = new(Utilities.GetRepositoryConnection());
            IdeasTbl idea = new()
            {
                ProjectName = ".UnitTest AddAsync_Comment_IdeaWComment",
                Description = "test description",
                Initials = "TEST"
            };
            CommentsTbl comment = new()
            {
                CreatedAt = DateTime.Now,
                Idea = idea,
                Initials = "TEST",
                Message = ".UnitTest Test comment."
            };

            // act
            await ideaRepository.AddAsync(idea);
            await commentsRepository.AddAsync(comment, idea.Id);

            // assert
            Assert.True((await commentsRepository.ListAsync(idea.Id)).Any());

            // clean up
            await ideaRepository.RemoveByIdAsync(idea.Id);
        }

        [Fact]
        public async void RemoveByIdAsync_RemoveComment_IdeaWNoComments()
        {
            // arrange
            IdeaRepository ideaRepository = new(Utilities.GetRepositoryConnection());
            CommentsRepository commentsRepository = new(Utilities.GetRepositoryConnection());
            IdeasTbl idea = new()
            {
                ProjectName = ".UnitTest RemoveByIdAsync_RemoveComment_IdeaWNoComments",
                Description = "test description",
                Initials = "TEST"
            };
            CommentsTbl comment = new()
            {
                CreatedAt = DateTime.Now,
                Idea = idea,
                Initials = "TEST",
                Message = ".UnitTest Test comment."
            };

            // act
            await ideaRepository.AddAsync(idea);
            await commentsRepository.AddAsync(comment, idea.Id);
            await commentsRepository.RemoveByIdAsync(comment.Id);

            // assert
            Assert.Equal(0, await commentsRepository.CountAsync(idea.Id));

            // clean up
            await ideaRepository.RemoveByIdAsync(idea.Id);
        }
    }
}