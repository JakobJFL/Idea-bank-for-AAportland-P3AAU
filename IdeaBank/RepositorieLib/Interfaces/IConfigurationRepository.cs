using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseLib.Models;
using Microsoft.AspNetCore.Identity;

namespace RepositoryLib.Interfaces
{
    public interface IConfigurationRepository
    {
        public Task<bool> IsBuAndDepEmpty();
        public Task<bool> DoesDatabaseExist();
        public Task<List<IdentityUser>> GetUsernameList();
        public Task UpdateGuideText(GuideTextTbl guideText);
        public Task<GuideTextTbl> GetGuideText();
        public Task<bool> IsAnyUsers();
        public Task SetDefaultGuideText();
        public Task<bool> IsGuideTextEmpty();
    }
}
