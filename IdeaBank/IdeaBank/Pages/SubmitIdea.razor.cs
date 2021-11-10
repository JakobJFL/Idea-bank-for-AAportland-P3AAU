using BusinessLogicLib.Models;
using System;

namespace IdeaBank.Pages
{
    public partial class SubmitIdea

    {
        private NewIdea idea = new();
        private async void HandleValidSubmit()
        {
            try
            {
                await _ideas.Insert(idea);
                NavManager.NavigateTo("/");
            }
            catch
            {
                throw new Exception("HandleValidSubmit failed");
            }
        }
    }
}