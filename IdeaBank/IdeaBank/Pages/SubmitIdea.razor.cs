using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using DataBaseLib.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using RepositoryLib.Interfaces;

namespace IdeaBank.Pages
{
    public partial class SubmitIdea : ComponentBase
    {
        private Idea _idea = new();
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public IIdeasDataAccess Ideas { get; set; }
        [Inject]
        private IJSRuntime JsRuntime { get; set; }
        [Inject]
        private IBusinessUnitsDataAccess BusinessUnitsDataAccess { get; set; }
        [Inject]
        private IDepartmentsDataAccess DepartmentsDataAccess { get; set; }
        private List<BusinessUnitsTbl> BusinessUnits { get; set; } = new();
        private List<DepartmentsTbl> Departments { get; set; } = new();

        private readonly string _confirmRegretSubmit = "Er du sikker på, at du vil fortryde og slette denne idé?";
        private readonly string _dbUpdateExceptionText = "Der skete en fejl under indsendelse af din idé. Prøv igen senere";
        
        protected override async Task OnInitializedAsync()
        {
            BusinessUnits.Add(new BusinessUnitsTbl() { Name = "Indlæser", Id = 0});
            Departments.Add(new DepartmentsTbl() { Name = "Indlæser", Id = 0 });
            BusinessUnits = await BusinessUnitsDataAccess.GetAll();
            Departments = await DepartmentsDataAccess.GetAll();
        }
        
        private async void HandleValidSubmit()
        {
            try
            {
                await Ideas.Insert(_idea);
                NavManager.NavigateTo("/");
            }
            catch (DbUpdateException)
            {
                await JsRuntime.InvokeVoidAsync("alert", _dbUpdateExceptionText);
            }
            catch (InvalidOperationException)
            {
                await JsRuntime.InvokeVoidAsync("alert", _dbUpdateExceptionText);
            }
        }
        /// <summary>
        /// Prompts the user if they are sure to leave submit idea page
        /// after having filled in information
        /// </summary>
        private async void CancelSubmit()
        {
            if (string.IsNullOrEmpty(_idea.ProjectName) &&
                string.IsNullOrEmpty(_idea.Description) &&
                string.IsNullOrEmpty(_idea.Initials) &&
                string.IsNullOrEmpty(_idea.ExpectedResults) &&
                string.IsNullOrEmpty(_idea.Team))
            {
                NavManager.NavigateTo("/");
            }
            else
            {
                bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", _confirmRegretSubmit);
                if (confirmed)
                    NavManager.NavigateTo("/");
            }
        }
    }
}
