using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryLib.Interfaces;

namespace RepositoryLib.Implementations
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        public ConfigurationRepository(Context context)
        {
            Context = context;
        }
        public Context Context { get; }

        private readonly int _guideTextID = 1;
        public async Task<bool> IsAnyUsers()
        {
           return await Context.Users.AnyAsync();
        }

        public async Task<List<IdentityUser>> GetUsernameList()
        {
            return await Context.Users.ToListAsync();
        } 
        
        public async Task<bool> DoesDatabaseExist()
        {
            return await Context.Database.CanConnectAsync();
        }
        public async Task<bool> IsBuAndDepEmpty()
        {
            return !(await Context.DepartmentsTbl.AnyAsync() && await Context.BusinessUnitsTbl.AnyAsync());
        }
        public async Task UpdateGuideText(GuideTextTbl model)
        {
            GuideTextTbl guideTextToUpdate = Context.GuideTextTbl.SingleOrDefault(g => g.Id == _guideTextID);
            if (guideTextToUpdate != null)
            {
                guideTextToUpdate.HomepageGuide = model.HomepageGuide;
                guideTextToUpdate.Purpose = model.Purpose;
                guideTextToUpdate.SubmitGuide = model.SubmitGuide;
                await Context.SaveChangesAsync();
            }
            else
                throw new ArgumentNullException("Idea not found");
        }
        public Task<GuideTextTbl> GetGuideText()
        {
            return Context.GuideTextTbl.FirstOrDefaultAsync();
        }
        public async Task SetDefaultGuideText()
        {
            GuideTextTbl guideText = new();
            guideText.Id = _guideTextID;
            guideText.HomepageGuide = "Ikke angivet.";
            guideText.Purpose = "Ikke angivet.";
            guideText.SubmitGuide = "Ikke angivet.";
            await Context.GuideTextTbl.AddAsync(guideText);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> IsGuideTextEmpty()
        {
            return !await Context.GuideTextTbl.AnyAsync();
        }
    }
}
