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
        private NewIdea Idea = new();
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public IIdeasDataAccess Ideas { get; set; }
        [Inject]
        public IConfiguration Config { get; set; }
        [Inject]
        public IJSRuntime JsRuntime { get; set; }


        private async void HandleValidSubmit()
        {
            try
            {
                await Ideas.Insert(Idea);
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
            if (string.IsNullOrEmpty(Idea.ProjectName) &&
                string.IsNullOrEmpty(Idea.Description) &&
                string.IsNullOrEmpty(Idea.Initials) &&
                string.IsNullOrEmpty(Idea.ExpectedResults) &&
                string.IsNullOrEmpty(Idea.Team))
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
