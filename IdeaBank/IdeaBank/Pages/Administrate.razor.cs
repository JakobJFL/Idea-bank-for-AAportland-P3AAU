using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using BusinessLogicLib.Interfaces;
using DataBaseLib.Models;
using BusinessLogicLib.Models;
using System.Collections.Generic;

namespace IdeaBank.Pages
{
    public partial class Administrate : ComponentBase
    {
        public readonly int MaxIdesInCSVFile = 10000;

        [Inject]
        public IJSRuntime JS { get; set; }
        [Inject]
        public IIdeasDataAccess Ideas { get; set; }
        [Inject]
        public Settings Settings { get; set; }

        private async Task DownloadFileFromStream()
        {
            FilterIdea filterIdea = new();
            filterIdea.CurrentPage = 1;
            filterIdea.IdeasShownCount = MaxIdesInCSVFile;
            List<ViewIdea> ideaList = await Ideas.GetWFilter(filterIdea);

            string csvContext = "Projekt navn;" +
                                "Initialer;" +
                                "Forfatterens forretningsenhed;" +
                                "Forfatterens afdeling;" +
                                "Vedrørende forretningsenhed;" +
                                "Vedrørende afdeling;" +
                                "Prioritet;Status;" +
                                "Idébeskrivelse;" +
                                "Plan;" +
                                "Forventet resultat;" +
                                "Risiko;" +
                                "Team;" +
                                "Er skjult;" +
                                "Oprettet;" +
                                "Opdateret\n";

            foreach (ViewIdea idea in ideaList)
            {
                csvContext += idea.ProjectName + ";";
                csvContext += idea.Initials + ";";
                csvContext += idea.AuthorBusinessUnitStr + ";";
                csvContext += idea.AuthorDepartmentStr + ";";
                csvContext += idea.IdeaBusinessUnitStr + ";";
                csvContext += idea.IdeaDepartmentStr+ ";";
                csvContext += idea.PriorityStr + ";";
                csvContext += idea.StatusStr + ";";
                csvContext += idea.Description + ";";
                csvContext += idea.Plan + ";";
                csvContext += idea.ExpectedResults + ";";
                csvContext += idea.Risk + ";";
                csvContext += idea.Team + ";";
                csvContext += idea.IsHidden + ";";
                csvContext += idea.CreatedAt + ";";
                csvContext += idea.UpdatedAt+ "\n";
            }
            await JS.InvokeVoidAsync("downloadCSV", csvContext);
        }
    }
}
