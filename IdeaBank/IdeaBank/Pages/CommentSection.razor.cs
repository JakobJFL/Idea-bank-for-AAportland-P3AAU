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
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace IdeaBank.Pages
{
    public partial class CommentSection : ComponentBase
    {
        [Inject]
        private ICommentsDataAccess Comments { get; set; }
        [Inject]
        private IJSRuntime JsRuntime { get; set; }
        [Inject]
        private Settings Settings { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthState { get; set; }

        private Comment _comment = new();
        private List<Comment> AllComment { get; set; }
        private int IdeaId { get; set; }
        private ClaimsPrincipal _user;

        private readonly string _confirmDeleteComment = "Er du sikker på, at du vil slette kommentaren?";
        private readonly string _dbUpdateExceptionTextComment = "Der skete en fejl under indsendelse af din kommentar. Prøv igen senere";
        private readonly string _dbUpdateExceptionTextDel = "Der skete en fejl under ændringen af kommentaren. Prøv igen senere";
        private readonly string _generalExceptionText = "Der skete en fejl. Prøv igen";

        protected override async Task OnInitializedAsync()
        {
            _user = (await AuthState.GetAuthenticationStateAsync()).User;
        }

        /// <summary>
        /// Load all comments for an idea.
        /// </summary>
        /// <param name="ideaId"></param>
        public async Task LoadComments(int ideaId)
        {
            try
            {
                AllComment = await Comments.GetWFilter(ideaId);
            }
            catch (InvalidOperationException)
            {
                await JsRuntime.InvokeVoidAsync("alert", _generalExceptionText);
            }
            IdeaId = ideaId;
            StateHasChanged();
        }

        /// <summary>
        /// Insert comment to database and refresh component.
        /// </summary>
        private async Task HandleValidSubmit()
        {
            _comment.CreatedAt = DateTime.Now;
            _comment.IdeaId = IdeaId;
            try
            {
                await Comments.Insert(_comment);
                AllComment = await Comments.GetWFilter(IdeaId);
            }
            catch (DbUpdateException)
            {
                await JsRuntime.InvokeVoidAsync("alert", _dbUpdateExceptionTextComment);
            }
            catch (InvalidOperationException)
            {
                await JsRuntime.InvokeVoidAsync("alert", _generalExceptionText);
            }
            _comment.Initials = "";
            _comment.Message = "";
            StateHasChanged();
        }

        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="c"></param>
        private async void DeleteComment(Comment c)
        {
            if (await JsRuntime.InvokeAsync<bool>("confirm", _confirmDeleteComment))
            {
                try
                {
                    await Comments.DeleteByID(c.Id);
                    AllComment = await Comments.GetWFilter(IdeaId);
                }
                catch (DbUpdateException)
                {
                    await JsRuntime.InvokeVoidAsync("alert", _dbUpdateExceptionTextDel);
                }
                catch (InvalidOperationException)
                {
                    await JsRuntime.InvokeVoidAsync("alert", _generalExceptionText);
                }
                StateHasChanged();
            }
        }
    }
}