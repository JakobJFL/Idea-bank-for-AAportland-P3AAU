using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogicLib;
using BusinessLogicLib.Service;
using DataBaseLib.Models;
using RepositoryLib.Implementations;
using Testing.xUnitTest;
using Xunit;

namespace Testing
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private readonly int _numOfTestIdeas = 25;
        private List<int> _testDBIdeasID = new();
        public static int NumOfRuns = 0;// SLET

        public async Task InitializeAsync()
        {
            NumOfRuns++;// SLET
            if (NumOfRuns > 1) // SLET
                return;// SLET
            IdeaRepository ideasRepository = new(Utilities.GetRepositoryConnection());
            CommentsRepository commentsRepository = new(Utilities.GetRepositoryConnection());

            for (int i = 0; i < _numOfTestIdeas; i++)
            {
                IdeasTbl idea = new()
                {
                    ProjectName = "Test " + i,
                    Description = "Test description " + i,
                    Initials = "Ini" + i,
                    Priority = Convert.ToByte(((i + 2) % Config.PriorityStrs.Length) + 1),
                    Status = Convert.ToByte((i % Config.StatusStrs.Length) + 1),
                    IsHidden = i % 5 == 0
                };
                await ideasRepository.AddAsync(idea);

                _testDBIdeasID.Add(idea.Id);
                for (int c = 0; c < i % 7; c++)
                {
                    CommentsTbl comment = new()
                    {
                        Idea = idea,
                        Message = "Test description. CommentIndex:" + c + ", IdeaIndex: " + i,
                        Initials = "Ini" + c,
                    };
                    await commentsRepository.AddAsync(comment, idea.Id);
                }
            }
        }

        public async Task DisposeAsync()
        {
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());
            foreach (int id in _testDBIdeasID)
            {
                await repository.RemoveByIdAsync(id);
            }
            _testDBIdeasID.Clear();
        }
    }
}