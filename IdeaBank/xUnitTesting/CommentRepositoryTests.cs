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
        private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IdeaBank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public CommentsRepository GetCommentsRepositoryConnection()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(_connectionString);
            Context context = new Context(optionsBuilder.Options);
            CommentsRepository repository = new(context);
            return repository;
        }
        public IdeaRepository GetIdeasRepositoryConnection()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(_connectionString);
            Context context = new Context(optionsBuilder.Options);
            IdeaRepository repository = new(context);
            return repository;
        }

        [Fact]
        public async void AddAsync_Comment_IdeaWComment()
        {
            // arrange
            IdeaRepository ideaRepository = GetIdeasRepositoryConnection();
            CommentsRepository commentsRepository = GetCommentsRepositoryConnection();
            FilterSortIdea filter = new()
            {
                Sorting = Sort.CreatedAtDesc,
                CurrentPage = 1,
                IdeasShownCount = 15
            };

            // act
            IdeasTbl idea = new()
            {
                ProjectName = "test",
                Description = "test description",
                Initials = "TEST",
                Priority = 1,
                Status = 1
            };
            await ideaRepository.AddAsync(idea);
            CommentsTbl comment = new()
            {
                CreatedAt = DateTime.Now,
                Idea = idea,
                Initials = "TEST",
                Message = "Test comment."
            };
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
            IdeaRepository ideaRepository = GetIdeasRepositoryConnection();
            CommentsRepository commentsRepository = GetCommentsRepositoryConnection();
            FilterSortIdea filter = new()
            {
                Sorting = Sort.CreatedAtDesc,
                CurrentPage = 1,
                IdeasShownCount = 15
            };
            // act
            IdeasTbl idea = new()
            {
                ProjectName = "test",
                Description = "test description",
                Initials = "TEST",
                Priority = 1,
                Status = 1
            };
            await ideaRepository.AddAsync(idea);
            CommentsTbl comment = new()
            {
                CreatedAt = DateTime.Now,
                Idea = idea,
                Initials = "TEST",
                Message = "Test comment."
            };
            await commentsRepository.AddAsync(comment, idea.Id);

            comment = (await commentsRepository.ListAsync(idea.Id)).First();
            await commentsRepository.RemoveByIdAsync(comment.Id);

            // assert
            Assert.Equal(0, await commentsRepository.CountAsync(idea.Id));

            // clean up
            await ideaRepository.RemoveByIdAsync(idea.Id);
        }

        //[Fact]
        //public async void Add_and_Remove()
        //{
        //    // arrange
        //    IdeaRepository repository = GetIdeasRepositoryConnection();
        //    CommentsRepository commentsRepository = GetCommentsRepositoryConnection();
        //    FilterSortIdea filter = new()
        //    {
        //        Sorting = Sort.CreatedAtDesc,
        //        CurrentPage = 1,
        //        IdeasShownCount = 15
        //    };

        //    // act
        //    IdeasTbl idea = new()
        //    {
        //        ProjectName = "test",
        //        Description = "test description",
        //        Initials = "TEST",
        //        Priority = 1,
        //        Status = 1
        //    };
        //    await repository.AddAsync(idea);
        //    idea = (await repository.ListAsync(filter)).First();
        //    CommentsTbl comment = new()
        //    {
        //        CreatedAt = DateTime.Now,
        //        Idea = idea,
        //        Initials = "TEST",
        //        Message = "Test comment."
        //    };
        //    await commentsRepository.AddAsync(comment, idea.Id);

        //    // assert
        //    Assert.True((await commentsRepository.ListAsync(idea.Id)).Any());

        //    // act
        //    comment = (await commentsRepository.ListAsync(idea.Id)).First();
        //    await commentsRepository.RemoveByIdAsync(comment.Id);

        //    // assert
        //    Assert.Equal(0, await commentsRepository.CountAsync(idea.Id));
        //}
    }
}