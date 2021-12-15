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
        private ICsvService Service { get; set; }
        [Inject]
        private IJSRuntime JS { get; set; }
        [Inject]
        public IConfig Config { get; set; }
        [Inject]
        public Settings Settings { get; set; }
        public List<IdentityUser> UserList {get; set;}
        private MemoryStream _csvStream;

        private string _disableStr = "";

        private readonly string _errorMessageDownload = "Stop den er ved at Downloade okay";

        protected override async Task OnInitializedAsync()
        {
            UserList = await Config.GetUsers();
        }

        /// <summary>
        /// Downloads ideas as CSV file
        /// </summary>
        private async Task DownloadIdeasToCSV()
        {
            _disableStr = "disabled";
            _csvStream = await Service.CreateCsvFileAsync();
            await JS.SaveAs("Ideer.csv", _csvStream.ToArray());
            _disableStr = "";
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
