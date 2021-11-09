using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace IdeaBank.Pages
{
    public partial class Modal
    {
        [Inject]
        public ICommentsDataAccess Comments { get; set; }

        public Guid Guid = Guid.NewGuid();

        public string ModalDisplay = "none;";
        public string ModalClass = "";
        private ViewIdea _idea = new();
        private Comment _comment = new();

        public async Task Open(ViewIdea idea)
        {
            StateHasChanged();
            ModalDisplay = "block;";
            await Task.Delay(150);
            ModalClass = "show";
            _idea = idea;
            _idea.Comments = await Comments.GetWFilter(_idea.Id);
            StateHasChanged();
        }

        public async Task Close()
        {
            ModalClass = "";
            await Task.Delay(250);
            ModalDisplay = "none;";
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
    }
}
