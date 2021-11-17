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

        private readonly string _confirmDeleteIdea = "Er du sikker på du vil slette ideen?";
        private readonly string _confirmCancel = "Er du sikker på du vil slette dine ændringer?";

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

        private async Task<bool> CancelEditing()
        {
            if (IsEditing)
            {
                bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", _confirmCancel);
                if (confirmed)
                {
                    IsEditing = false;
                    StateHasChanged();
                    return true;
                }
                return false;
            }
            return true;
        }

        public async Task Close()
        {
            if (await CancelEditing())
            {
                _modalClass = "";
                await Task.Delay(250);
                _modalDisplay = "none;";
                StateHasChanged();
            }
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
            _editIdea.AuthorDepartment = _idea.AuthorDepartment;
            _editIdea.AuthorBusinessUnit = _idea.AuthorBusinessUnit;
            _editIdea.IdeaDepartment = _idea.IdeaDepartment;
            _editIdea.IdeaBusinessUnit = _idea.IdeaBusinessUnit;
            _editIdea.Status = _idea.Status;
            _editIdea.IsHidden = _idea.IsHidden;
            _editIdea.CreatedAt = _idea.CreatedAt;
            IsEditing = true;
            StateHasChanged();
        }
        private async void HandleValidEdit()
        {
            IsEditing = false;
            await IndexView.Ideas.Edit(_editIdea);
            FilterIdea filterIdea = new();
            filterIdea.Id = _editIdea.Id;
            filterIdea.CurrentPage = 1;
            filterIdea.IdeasShownCount = 1;
            _idea = (await IndexView.Ideas.GetWFilter(filterIdea)).First();
            await IndexView.Update();
            StateHasChanged();
        }
    }
}
