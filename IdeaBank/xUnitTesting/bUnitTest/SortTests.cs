using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using Bunit.TestDoubles;
using BusinessLogicLib;
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using BusinessLogicLib.Service;
using DataBaseLib.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLib.Implementations;
using RepositoryLib.Interfaces;
using Xunit;
using IdeaBank.Pages;
using Microsoft.AspNetCore.Components.Web;

namespace XUnitTesting.bUnitTest
{
    public class SortTests
    {
        [Fact]
        public async void SortIconShouldShowDescWhenClicked()
        {
            // arrange
            using var ctx = Utilities.InitializeTestContext();

            // act
            var cut = ctx.RenderComponent<IdeaBank.Pages.Index>().Find("th");
            await Task.Delay(Utilities.WaitForDBDelay);
            await cut.ClickAsync(new MouseEventArgs());
            //cut.Click();
            // assert
            cut.MarkupMatches("<th scope=\"col\" style=\"min-width: 150px;\" class=\"sort-select bg-primary select-filter-text align-middle\">" +
                          "Projektnavn" +
                          "<img src=\"/svg/alfa-desc.svg\" />" +
                        "</th>");

        }

        [Fact]
        public async void SortIconShouldShowAscWhenClickedTwice()
        {
            // arrange
            using var ctx = Utilities.InitializeTestContext();

            // act
            var cut = ctx.RenderComponent<IdeaBank.Pages.Index>().Find("th");;
            await Task.Delay(Utilities.WaitForDBDelay); // 
            await cut.ClickAsync(new MouseEventArgs());
            await cut.ClickAsync(new MouseEventArgs());
            // assert
            cut.MarkupMatches("<th scope=\"col\" style=\"min-width: 150px;\" class=\"sort-select bg-primary select-filter-text align-middle\">" +
                          "Projektnavn" +
                          "<img src=\"/svg/alfa-asc.svg\" />" +
                        "</th>");
        }
    }
}
