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
                ProjectName = "test",
                Description = "test description",
                Initials = "TEST"
            };
            CommentsTbl comment = new()
            {
                CreatedAt = DateTime.Now,
                Idea = idea,
                Initials = "TEST",
                Message = "Test comment."
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
                ProjectName = "test",
                Description = "test description",
                Initials = "TEST"
            };
            CommentsTbl comment = new()
            {
                CreatedAt = DateTime.Now,
                Idea = idea,
                Initials = "TEST",
                Message = "Test comment."
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