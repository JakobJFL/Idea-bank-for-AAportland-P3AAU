using Bunit;
using BusinessLogicLib.Models;
using Xunit;
using IdeaBank.Pages;

namespace XUnitTesting.bUnitTest
{
    [Collection("Test Database")]
    public class CommentSectionAuthTest
    {
        [Fact]
        public void CommentsDisabledShouldHideSubmit()
        {
            // arrange
            var ctx = TestStartupManager.InitializeTestContext();
            ctx.Services.GetService<Settings>().CommentsEnabled = false;

            // act
            var cut = ctx.RenderComponent<CommentSection>();
            var submitSection = cut.Find("#submitComments");

            // assert
            submitSection.MarkupMatches("<div id = \"submitComments\"></div>");
        }
    }
}
