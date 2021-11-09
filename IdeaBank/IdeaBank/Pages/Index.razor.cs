using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
namespace IdeaBank.Pages
{
    public partial class Index
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
            await Config.ConfigureDBTables(); // Måske det skal være et andet sted

            if (IdeaList == null)
            {
                IdeaList = await Ideas.GetWFilter(_filterIdea);
                StateHasChanged();
            }
        }
        // Note: The OnFieldChanged event is raised for each field in the model
        private async void EditContext_OnFieldChanged(object sender, FieldChangedEventArgs e)
        {
            IdeaList = await Ideas.GetWFilter(_filterIdea);
            StateHasChanged();
        }
    }
}