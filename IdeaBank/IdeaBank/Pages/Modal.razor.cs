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
    public partial class Modal : ComponentBase
    {
        [Inject]
        private ICommentsDataAccess Comments { get; set; }
        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        private Index IndexView { get; set; }

        private string _modalDisplay = "none;";
        private string _modalClass = "";
        private ViewIdea _idea = new();
        private Comment _comment = new();

        public async Task Open(ViewIdea idea, Index indexView)
        {
            StateHasChanged();
            IndexView = indexView;
            _modalDisplay = "block;";
            await Task.Delay(150);
            _modalClass = "show";
            _idea = idea;
            _idea.Comments = await Comments.GetWFilter(_idea.Id);
            StateHasChanged();
        }

        public async Task Close()
        {
            _modalClass = "";
            await Task.Delay(250);
            _modalDisplay = "none;";
            StateHasChanged();
        }
        private async void HandleValidSubmit()
        {
            _comment.CreatedAt = DateTime.Now;
            _comment.IdeaId = _idea.Id;
            Comments.Insert(_comment);
            _idea.Comments = await Comments.GetWFilter(_idea.Id);
            _comment.Initials = "";
            _comment.Message = "";
            StateHasChanged();
        }

        private async void DeleteIdea()
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure?");
            if (confirmed)
            {
                await IndexView.Ideas.DeleteByID(_idea.Id);
                await Close();
                await IndexView.Update();
                StateHasChanged();
            }
        }
    }
}
