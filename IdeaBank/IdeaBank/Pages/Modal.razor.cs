using System.Threading.Tasks;
using BusinessLogicLib.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BusinessLogicLib;
using Microsoft.EntityFrameworkCore;

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

        private readonly string _confirmDeleteIdea = "Er du sikker på, at du vil slette idéen?";
        private readonly string _confirmCancel = "Er du sikker på, at du vil slette dine ændringer?";
        private readonly string _dbUpdateExceptionText = "Der skete en fejl under ændringen af idén. Prøv igen senere";

        /// <summary>
        /// Opens a pop-up for an idea
        /// </summary>
        /// <param name="idea"></param>
        /// <param name="indexView"></param>
        public async Task Open(ViewIdea idea, Index indexView)
        {
            StateHasChanged();
            IndexView = indexView;
            _modalDisplay = "block;";
            await Task.Delay(150);
            _modalClass = "show";
            _idea = idea;
            await CommentSection.LoadComments(idea.Id);
            StateHasChanged();
        }

        private async Task<bool> CancelEditing()
        {
            if (IsEditing)
            {
                if (await JsRuntime.InvokeAsync<bool>("confirm", _confirmCancel))
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
            if (await JsRuntime.InvokeAsync<bool>("confirm", _confirmDeleteIdea))
            {
                try
                {
                    await IndexView.Ideas.DeleteByID(_idea.Id);
                }
                catch (DbUpdateException)
                {
                    await JsRuntime.InvokeVoidAsync("alert", _dbUpdateExceptionText);
                }
                await Close();
                await IndexView.Update();
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
            try
            {
                await IndexView.Ideas.Edit(_editIdea);
                _idea = await IndexView.Ideas.GetByIdAsync(_editIdea.Id);
                await IndexView.Update();
            }
            catch (DbUpdateException)
            {
                await JsRuntime.InvokeVoidAsync("alert", _dbUpdateExceptionText);
            }
            StateHasChanged();
        }
    }
}
