using BusinessLogicLib.Models;
using System;

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
        public IJSRuntime JsRuntime { get; set; }

        private readonly string _confirmRegretSubmit = "Er du sikker på du vil fortryde og slette denne ide?";
        
        private async void HandleValidSubmit()
        {
            try
            {
                await Ideas.Insert(_idea);
                NavManager.NavigateTo("/");
            }
            catch (Exception ex)
            {
                throw new Exception("HandleValidSubmit failed");
            }
        }
        private async void Regret()
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
