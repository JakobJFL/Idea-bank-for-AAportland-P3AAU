using System;
using System.Threading.Tasks;
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace IdeaBank.Pages
{
    public partial class Guide : ComponentBase
    {
        [Inject]
        private IConfig Config { get; set; }
        private Settings SettingsGuideText { get; set; } = new();
        private bool IsEditing { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            SettingsGuideText = await Config.GetGuideText();
        }
        private async void HandleValidEdit()
        {
            IsEditing = false;
            await Config.EditGuideText(SettingsGuideText);
            StateHasChanged();
        }
    }
   
}
