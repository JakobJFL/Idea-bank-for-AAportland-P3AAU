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
using System.Reflection;
using DataBaseLib.Models;
using BusinessLogicLib;

namespace IdeaBank.Pages
{
    public partial class Modal : ComponentBase
    {

        [Inject]
        private IJSRuntime JsRuntime { get; set; }
        private Index IndexView { get; set; }

        private string _modalDisplay = "none;";
        private string _modalClass = "";
        private ViewIdea _idea = new();
        private EditIdea _editIdea = new();
        private CommentSection CommentSection { get; set; }
        private bool IsEditing { get; set; } = false;

        private readonly string _confirmDeleteIdea = "Are you sure?";

        public async Task Open(ViewIdea idea, Index indexView)
        {
            StateHasChanged();
            IndexView = indexView;
            _modalDisplay = "block;";
            await Task.Delay(150);
            _modalClass = "show";
            _idea = idea;
            CommentSection.LoadComments(idea.Id);
            StateHasChanged();
        }

        public async Task Close()
        {
            _modalClass = "";
            await Task.Delay(250);
            _modalDisplay = "none;";
            IsEditing = false;
            StateHasChanged();
        }
        private async void DeleteIdea()
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", _confirmDeleteIdea);
            if (confirmed)
            {
                await IndexView.Ideas.DeleteByID(_idea.Id);
                await Close();
                await IndexView.Update();
                StateHasChanged();
            }
        }
        private void EditIdea()
        {
            _editIdea.Id = _idea.Id;
            _editIdea.Initials = _idea.Initials;
            _editIdea.ProjectName = _idea.ProjectName;
            _editIdea.Description = DBConvert.StrBrToNewLine(_idea.Description);
            _editIdea.Risk = DBConvert.StrBrToNewLine(_idea.Risk);
            _editIdea.Plan = DBConvert.StrBrToNewLine(_idea.Plan);
            _editIdea.ExpectedResults = DBConvert.StrBrToNewLine(_idea.ExpectedResults);
            _editIdea.Team = _idea.Team;
            _editIdea.Priority = _idea.Priority;
            _editIdea.Department = _idea.Department;
            _editIdea.BusinessUnit = _idea.BusinessUnit;
            _editIdea.Status = _idea.Status;
            _editIdea.IsHidden = _idea.IsHidden;
            _editIdea.CreatedAt = _idea.CreatedAt;
            _editIdea.UpdatedAt = _idea.UpdatedAt;
            IsEditing = true;
            StateHasChanged();
        }
        private async void HandleValidEdit()
        {
            IsEditing = false;
            await IndexView.Ideas.Edit(_editIdea);
            FilterIdea filterIdea = new();
            filterIdea.Id = _editIdea.Id;
            _idea = (await IndexView.Ideas.GetWFilter(filterIdea)).First();
            StateHasChanged();
        }
    }
}
