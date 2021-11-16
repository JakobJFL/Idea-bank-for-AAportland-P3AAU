
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using DataBaseLib.Models;
using Microsoft.JSInterop;

namespace IdeaBank.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public IIdeasDataAccess Ideas { get; set; }
        [Inject]
        public IDBTableConfiguration Config { get; set; }
        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        private EditContext _editContext;
        private List<ViewIdea> _ideaList;
        private Modal Modal { get; set; }
        private FilterIdea _filterIdea = new();
        private bool IsAuthorized { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _editContext = new EditContext(_filterIdea);
            _editContext.OnFieldChanged += EditContext_OnFieldChanged;
            _filterIdea.Sorting = Sort.CreatedAtDesc;
            await Config.ConfigureDBTables();
            if (_ideaList == null)
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

        public async Task Update()
        {
            _ideaList = await Ideas.GetWFilter(_filterIdea);
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
