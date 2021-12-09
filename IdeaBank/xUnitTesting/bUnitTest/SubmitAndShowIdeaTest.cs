using Bunit;
using RepositoryLib.Implementations;
using Xunit;
using System;
using System.Threading.Tasks;
using DataBaseLib.Models;

namespace XUnitTesting.bUnitTest
{
    [Collection("Test Database")]
    public class SubmitAndShowIdeaTest
    {
        [Fact]
        public async void SubmitIdeaAndShowIdeaInOverview()
        {
            // arrange
            var ctx = TestStartupManager.InitializeTestContext();
            IdeaRepository repository = new(TestStartupManager.GetRepositoryConnection());

            // act
            IdeasTbl idea = new()
            {
                ProjectName = "testMega",
                Description = "Test description",
                Initials = "TEST",
                Priority = 1,
                Status = 1,
                CreatedAt = DateTime.Now,
            };
            await repository.AddAsync(idea);
            var cut = ctx.RenderComponent<IdeaBank.Pages.Index>();
            var firstTableCell = cut.Find("td");

            //assert
            cut.WaitForAssertion(() => firstTableCell.MarkupMatches("<td style=\"min-width: 150px;\" class=\"select-filter-text\" >" +
                                            "<div class=\"d-flex\">" +
                                                idea.ProjectName +
                                            "</div>" +
                                        "</td>"), TimeSpan.FromSeconds(2));
            // clean up
            await repository.RemoveByIdAsync(idea.Id);
        }
    }
}
