using System;
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

namespace IdeaBank.Pages
{
    public partial class SubmitIdea : ComponentBase
    {
        private NewIdea _idea = new();
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public IIdeasDataAccess Ideas { get; set; }
        [Inject]
        public IConfiguration Config { get; set; }
        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        private readonly string _confirmRegretSubmit = "Er du sikker på du vil fortryde og slette denne ide?";
        private readonly string _dbUpdateExceptionText = "Der skete en fejl under indsendelse af din idé. Prøv igen senere";

        private async void HandleValidSubmit()
        {
            try
            {
                await Ideas.Insert(_idea);
                NavManager.NavigateTo("/");
            }
            catch (DbUpdateException)
            {
                await JsRuntime.InvokeVoidAsync("alert", _dbUpdateExceptionText);
            }
        }
        private async void Regret() // please new name
        {
            if (string.IsNullOrEmpty(_idea.ProjectName) &&
                string.IsNullOrEmpty(_idea.Description) &&
                string.IsNullOrEmpty(_idea.Initials) &&
                string.IsNullOrEmpty(_idea.ExpectedResults) &&
                string.IsNullOrEmpty(_idea.Team))
            {
                NavManager.NavigateTo("/");
            }
            else
            {
                bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", _confirmRegretSubmit);
                if (confirmed)
                    NavManager.NavigateTo("/");
            }
        }
    }
}
