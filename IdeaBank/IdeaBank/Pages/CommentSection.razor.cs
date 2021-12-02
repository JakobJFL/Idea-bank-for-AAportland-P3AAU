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
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private ClaimsPrincipal _user;

        private Comment _comment = new();
        private List<Comment> AllComment { get; set; }
        private int IdeaId { get; set; }

        private readonly string _confirmDeleteComment = "Er du sikker på, at du vil slette kommentaren?";

        protected override async Task OnInitializedAsync()
        {
            AuthenticationState authState = await AuthenticationStateTask;
            _user = authState.User;
        }

        /// <summary>
        /// Load all comments for an idea.
        /// </summary>
        /// <param name="ideaId"></param>
        public async void LoadComments(int ideaId)
        {
            AllComment = await Comments.GetWFilter(ideaId);
            IdeaId = ideaId;
            StateHasChanged();
        }

        /// <summary>
        /// Insert comment to database and refresh component.
        /// </summary>
        private async void HandleValidSubmit()
        {
            _comment.CreatedAt = DateTime.Now;
            _comment.IdeaId = IdeaId;
            await Comments.Insert(_comment);
            AllComment = await Comments.GetWFilter(IdeaId);
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
                await Comments.DeleteByID(c.Id);
                AllComment = await Comments.GetWFilter(IdeaId);
                StateHasChanged();
            }
        }
    }
}