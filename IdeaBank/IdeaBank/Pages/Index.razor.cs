
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using DataBaseLib.Models;

namespace IdeaBank.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private IIdeasDataAccess Ideas { get; set; }
        [Inject]
        private IDBTableConfiguration Config { get; set; }

        private EditContext _editContext;
        public List<ViewIdea> IdeaList;
        private Modal Modal { get; set; }
        private FilterIdea _filterIdea = new();
        
        protected override async Task OnInitializedAsync()
        {
            _editContext = new EditContext(_filterIdea);
            _editContext.OnFieldChanged += EditContext_OnFieldChanged;
            _filterIdea.Sorting = Sort.CreatedAtDesc;
            await Config.ConfigureDBTables(); // Måske det skal være et andet sted
            
            if (IdeaList == null)
            {
                await Update();
            }
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

        private async Task Update()
        {
            IdeaList = await Ideas.GetWFilter(_filterIdea);
            StateHasChanged();
        }

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
