using Bunit;
using RepositoryLib.Implementations;
using Xunit;
using System;
using System.Threading.Tasks;
using DataBaseLib.Models;

namespace Testing.BUnitTest
{
    [Collection("Test Database")]
    public class SubmitAndShowIdeaTest
    {
        [Fact]
        public async void SubmitIdeaAndShowIdeaInOverview()
        {
            // arrange
            var ctx = Utilities.InitializeTestContext();
            IdeaRepository repository = new(Utilities.GetRepositoryConnection());

            // act
            IdeasTbl idea = new()
            {
                ProjectName = ".UnitTest SubmitIdeaAndShowIdeaInOverview",
                Description = "Test description",
                Initials = "TEST",
                Priority = 1,
                Status = 1,
                CreatedAt = DateTime.Now,
            };
            await repository.AddAsync(idea);
            var cut = ctx.RenderComponent<IdeaBank.Pages.Index>();
            await Task.Delay(Utilities.WaitForDBDelay);
            var firstTableCell = cut.Find("td");

            //assert
            cut.WaitForAssertion(() =>
                {
                    cut.Render();
                    firstTableCell.MarkupMatches("<td style=\"min-width: 150px;\" class=\"select-filter-text\" >" +
                                             "<div class=\"d-flex\">" +
                                                 idea.ProjectName +
                                             "</div>" +
                                         "</td>");
                }, TimeSpan.FromSeconds(5));
            // clean up
            await repository.RemoveByIdAsync(idea.Id);
        }
    }
}
