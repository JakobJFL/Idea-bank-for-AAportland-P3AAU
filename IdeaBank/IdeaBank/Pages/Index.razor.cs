using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using DataBaseLib.Models;
using System;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

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
        [Inject]
        private IBusinessUnitsDataAccess BusinessUnitsDataAccess { get; set; }
        [Inject]
        private IDepartmentsDataAccess DepartmentsDataAccess { get; set; }
        [Inject]
        private IJSRuntime JsRuntime { get; set; }
        public List<BusinessUnitsTbl> BusinessUnits { get; set; } = new();
        public List<DepartmentsTbl> Departments { get; set; } = new();

        private EditContext _editContext;
        private List<ViewIdea> _ideaList;
        private Modal Modal { get; set; }
        public FilterSortIdea FilterIdea { get; set; } = new();
        public int NumOfPages { get; set; }
        public int CurrentPage { get; set; } = 1;
        public Dashboard Dashboard { get; set; } = new Dashboard();
        private readonly string _generalExceptionText = "Der skete en fejl. Prøv igen";

        protected override async Task OnInitializedAsync()
        {
            _editContext = new EditContext(FilterIdea);
            _editContext.OnFieldChanged += EditContext_OnFieldChanged;
            await Config.ConfigureDBTables();
            BusinessUnits.Add(new BusinessUnitsTbl() { Name = "Indlæser", Id = 0 });
            Departments.Add(new DepartmentsTbl() { Name = "Indlæser", Id = 0 });
            BusinessUnits = await BusinessUnitsDataAccess.GetAll();
            Departments = await DepartmentsDataAccess.GetAll();
            await SetDashboard();
            FilterIdea.Sorting = Sort.CreatedAtDesc;
            FilterIdea.CurrentPage = CurrentPage;
            FilterIdea.IdeasShownCount = IdeasShownCount;
            if (_ideaList == null)
            {
                await Update();
            }
        }
        private async Task SetDashboard()
        {
            try
            {
                FilterIdea.OnlyNewIdeas = true;
                Dashboard.NewIdeas = await Ideas.GetCount(FilterIdea);
                FilterIdea.OnlyNewIdeas = false;
                Dashboard.AllIdeas = await Ideas.GetCount(FilterIdea);
                FilterIdea.Status = 2;
                Dashboard.ApprovedIdeas = await Ideas.GetCount(FilterIdea);
                Dashboard.AllComments = await Comments.GetCommentsCount(0);
                FilterIdea.Status = 0;
            }
            catch (InvalidOperationException)
            {
                await JsRuntime.InvokeVoidAsync("alert", _generalExceptionText);
            }

        }

        // Note: The OnFieldChanged event is raised for each field in the model
        private async void EditContext_OnFieldChanged(object sender, FieldChangedEventArgs e)
        {
            await Update();
        }
        private async Task ChangeProjectNameSort()
        {
            FilterIdea.Sorting = FilterIdea.Sorting == Sort.ProjectNameAsc ? Sort.ProjectNameDesc : Sort.ProjectNameAsc;
            await Update();
        }
        private async Task ChangeCreatedAtSort()
        {
            FilterIdea.Sorting = FilterIdea.Sorting == Sort.CreatedAtDesc ? Sort.CreatedAtAsc : Sort.CreatedAtDesc;
            await Update();
        }

        private async Task ChangeUpdatedAtSort()
        {
            FilterIdea.Sorting = FilterIdea.Sorting == Sort.UpdatedAtDesc ? Sort.UpdatedAtAsc : Sort.UpdatedAtDesc;
            await Update();
        }

        private async Task ChangeUpdatedAtPage()
        {
            FilterIdea.CurrentPage = CurrentPage;
            FilterIdea.IdeasShownCount = IdeasShownCount;
            await Update();
        }

        /// <summary>
        /// Updates the table of ideas.
        /// </summary>
        /// <returns></returns>
        public async Task Update()
        {
            FilterIdea.ShowHidden = (await AuthState.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
            _ideaList = await Ideas.GetWFilter(FilterIdea);
            foreach (ViewIdea idea in _ideaList)
            {
                idea.CommentsCount = await Comments.GetCommentsCount(idea.Id);
            }
            FilterIdea.OnlyNewIdeas = false;
            NumOfPages = (int)Math.Ceiling((decimal)Ideas.GetIdeasCount() / IdeasShownCount);
            StateHasChanged();
        }

        /// <summary>
        /// Reset all filters to default values.
        /// </summary>
        private async void Reset()
        {
            FilterIdea.BusinessUnit = 0;
            FilterIdea.Department = 0;
            FilterIdea.Priority = 0;
            FilterIdea.Status = 0;
            FilterIdea.SearchStr = "";
            FilterIdea.Sorting = Sort.CreatedAtDesc;
            await Update();
        }
    }
}
