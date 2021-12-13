using Bunit;
using Bunit.TestDoubles;
using Xunit;
using IdeaBank.Shared;

namespace Testing.BUnitTest
{
    public class NavMenuTests
    {
        [Fact]
        public void NavShouldShowAdminNavWhenLoggedIn()
        {
            // arrange
            using var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER");

            // act
            var cut = ctx.RenderComponent<NavMenu>().Find("ul");

            // assert
            cut.MarkupMatches("<ul class=\"navbar-nav\">" +
                            "<li class=\"nav-item\">" +
                                "<a href = \"/\" class=\"btn btn-outline-light rounded-cos m-1 active\">Startside</a>" +
                            "</li>" +
                            "<li class=\"nav-item\">" +
                                "<a href = \"/Guide\" class=\"btn btn-outline-light rounded-cos m-1\">Guide</a>" +
                            "</li>" +
                            "<li class=\"nav-item\">" +
                                "<a href = \"/NewIdea\" class=\"btn btn-outline-light rounded-cos m-1\">Tilføj idé</a>" +
                            "</li>" +
                            "<li class=\"nav-item\">" +
                                "<a href = \"/Administrate\" class=\"btn text-white noFocus\">Administrer</a>" +
                            "</li>" +
                            "<li class=\"nav-item\">" +
                                "<a href = \"/Identity/Account/Manage\" class=\"btn text-white noFocus\">Konto</a>" +
                            "</li>" +
                            "<li class=\"nav-item\">" +
                                "<form method = \"post\" action=\"Identity/Account/LogOut\">" +
                                "<button type = \"submit\" class=\"btn text-white noFocus\">Log ud</button>" +
                                "</form>" +
                            "</li>" +
                            "</ul>");
        }

        [Fact]
        public void NavShouldNotShowAdminNavWhenLoggedOut()
        {
            // arrange
            using var ctx = new TestContext();
            ctx.AddTestAuthorization();

            // act
            var cut = ctx.RenderComponent<NavMenu>().Find("ul");

            // assert
            cut.MarkupMatches("<ul class=\"navbar-nav\">" +
                            "<li class=\"nav-item\">" +
                                "<a href = \"/\" class=\"btn btn-outline-light rounded-cos m-1 active\">Startside</a>" +
                            "</li>" +
                            "<li class=\"nav-item\">" +
                                "<a href = \"/Guide\" class=\"btn btn-outline-light rounded-cos m-1\">Guide</a>" +
                            "</li>" +
                            "<li class=\"nav-item\">" +
                                "<a href = \"/NewIdea\" class=\"btn btn-outline-light rounded-cos m-1\">Tilføj idé</a>" +
                            "</li>" +
                            "<li class=\"nav-item\">" +
                                "<a class=\"btn text-white noFocus m-1\" href=\"Identity/Account/Login\">Admin login</a>" +
                            "</li>" +
                            "</ul>");
        }
    }
}
