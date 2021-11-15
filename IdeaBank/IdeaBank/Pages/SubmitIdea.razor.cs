using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace IdeaBank.Pages
{
    public partial class SubmitIdea : ComponentBase
    {
        private NewIdea idea = new();
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public IIdeasDataAccess _ideas { get; set; }
        [Inject]
        public IConfiguration _config { get; set; }
        [Inject]
        public IJSRuntime JsRuntime { get; set; }


        private async void HandleValidSubmit()
        {
            try
            {
                await _ideas.Insert(idea);
                NavManager.NavigateTo("/");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                // skriv en fejl besked til brugern
            }

        }
        private async void Regret()
        {
            if (string.IsNullOrEmpty(idea.ProjectName) &&
                string.IsNullOrEmpty(idea.Description) &&
                string.IsNullOrEmpty(idea.Initials) &&
                string.IsNullOrEmpty(idea.ExpectedResults) &&
                string.IsNullOrEmpty(idea.Team))
            {
                NavManager.NavigateTo("/");
            }
            else
            {
                bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure?");
                if (confirmed)
                    NavManager.NavigateTo("/");
            }
        }
    }
}
