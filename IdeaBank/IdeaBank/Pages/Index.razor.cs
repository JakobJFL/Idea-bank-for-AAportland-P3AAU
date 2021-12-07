using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using DataBaseLib.Models;
using Microsoft.JSInterop;
using System;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace IdeaBank.Pages
{
    public partial class Index : ComponentBase
    {
        public readonly int IdeasShownCount = 15;
        [Inject]
        public IIdeasDataAccess Ideas { get; set; }
        [Inject]
        public ICommentsDataAccess Comments { get; set; }
        [Inject]
        public IConfig Config { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthState { get; set; }

        private EditContext _editContext;
        private List<ViewIdea> _ideaList;
        private Modal Modal { get; set; }
        public FilterSortIdea _filterIdea = new();
        public int NumOfPages { get; set; }
        public int CurrentPage { get; set; } = 1;
        public Dashboard Dashboard { get; set; } = new Dashboard();



        protected override async Task OnInitializedAsync()
        {
            _editContext = new EditContext(_filterIdea);
            _editContext.OnFieldChanged += EditContext_OnFieldChanged;
            await SetDashboard();
            _filterIdea.Sorting = Sort.CreatedAtDesc;
            _filterIdea.CurrentPage = CurrentPage;
            _filterIdea.IdeasShownCount = IdeasShownCount;
            await Config.ConfigureDBTables();
            if (_ideaList == null)
            {
                await Update();
            }
        }
        private async Task SetDashboard()
        {
            _filterIdea.OnlyNewIdeas = true;
            Dashboard.NewIdeas = await Ideas.GetCount(_filterIdea);
            _filterIdea.OnlyNewIdeas = false;
            Dashboard.AllIdeas = await Ideas.GetCount(_filterIdea);
            _filterIdea.Status = 2;
            Dashboard.ApprovedIdeas = await Ideas.GetCount(_filterIdea);
            Dashboard.AllComments = await Comments.GetCommentsCount(0);
            _filterIdea.Status = 0;
        }

        // Note: The OnFieldChanged event is raised for each field in the model
        private async void EditContext_OnFieldChanged(object sender, FieldChangedEventArgs e)
        {
            await Update();
        }
        private async void ChangeProjectNameSort()
        {
            _filterIdea.Sorting = _filterIdea.Sorting == Sort.ProjectNameAsc ? Sort.ProjectNameDesc : Sort.ProjectNameAsc;
            await Update();
        }
        private async void ChangeCreatedAtSort()
        {
            _filterIdea.Sorting = _filterIdea.Sorting == Sort.CreatedAtDesc ? Sort.CreatedAtAsc : Sort.CreatedAtDesc;
            await Update();
        }

        private async void ChangeUpdatedAtSort()
        {
            _filterIdea.Sorting = _filterIdea.Sorting == Sort.UpdatedAtDesc ? Sort.UpdatedAtAsc : Sort.UpdatedAtDesc;
            await Update();
        }

        private async void ChangeUpdatedAtPage()
        {
            _filterIdea.CurrentPage = CurrentPage;
            _filterIdea.IdeasShownCount = IdeasShownCount;
            await Update();
        }

        /// <summary>
        /// Updates the table of ideas.
        /// </summary>
        /// <returns></returns>
        public async Task Update()
        {
            _filterIdea.ShowHidden = (await AuthState.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
            _ideaList = await Ideas.GetWFilter(_filterIdea);
            foreach(ViewIdea idea in _ideaList)
            {
               idea.CommentsCount = await Comments.GetCommentsCount(idea.Id);
            }
            _filterIdea.OnlyNewIdeas = false;
            NumOfPages = (int)Math.Ceiling((decimal)Ideas.GetIdeasCount() / IdeasShownCount);
            StateHasChanged();
        }

        /// <summary>
        /// Reset all filters to default values.
        /// </summary>
        private async void Reset()
        {
            _filterIdea.BusinessUnit = 0;
            _filterIdea.Department = 0;
            _filterIdea.Priority = 0;
            _filterIdea.Status = 0;
            _filterIdea.SearchStr = "";
            _filterIdea.Sorting = Sort.CreatedAtDesc;
            await Update();
        }
    }
}
