using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using BusinessLogicLib.Service;

namespace IdeaBank.Pages
{
    public partial class Administrate : ComponentBase
    {
        [Inject]
        private ICsvService CsvService { get; set; }
        [Inject]
        private IJSRuntime JS { get; set; }
        [Inject]
        private IConfig Config { get; set; }
        [Inject]
        private Settings Settings { get; set; }
        public List<IdentityUser> UserList {get; set;}
        private MemoryStream CsvStream { get; set; }
        private string DisableStr { get; set; } = "";
        protected override async Task OnInitializedAsync()
        {
            UserList = await Config.GetUsers();
        }

        /// <summary>
        /// Downloads ideas as CSV file
        /// </summary>
        private async Task DownloadIdeasToCSV()
        {
            DisableStr = "disabled"; // Disabled download btn
            CsvStream = await CsvService.CreateCsvFileAsync();
            await JS.SaveAs("Ideer.csv", CsvStream.ToArray());
            DisableStr = ""; // Enable download btn
        }
    }
    public static class FileUtils
    {
        public static ValueTask<object> SaveAs(this IJSRuntime js, string filename, byte[] data)
        {
            return js.InvokeAsync<object>("saveAsFile", filename, Convert.ToBase64String(data));
        }
 
    }
}
