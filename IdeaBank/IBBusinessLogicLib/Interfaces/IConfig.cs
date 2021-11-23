using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLib.Models;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLib.Interfaces
{
    public interface IConfig
    {
        public Task ConfigureDBTables();
        public Task<List<IdentityUser>> GetUsers();
        public Task EditGuideText(Settings settings);
        public Task<Settings> GetGuideText();
    }
}
