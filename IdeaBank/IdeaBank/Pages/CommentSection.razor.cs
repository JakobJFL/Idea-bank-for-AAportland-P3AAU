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
    public partial class CommentSection : ComponentBase
    {
        [Inject]
        private ICommentsDataAccess Comments { get; set; }
        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        private Comment _comment = new();
        private List<Comment> AllComment { get; set; }
        private int IdeaId { get; set; }

        private readonly string _confirmDeleteComment = "Er du sikker på du vil slette kommentaren?";

        public async void LoadComments(int ideaId)
        {
            AllComment = await Comments.GetWFilter(ideaId);
            IdeaId = ideaId;
            StateHasChanged();
        }

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