using System;
using System.Collections.Generic;
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

        private readonly string[] _businessUnits = { "Ikke Angivet", "Aalborg Portland", "Unicon DK", "Unicon NO", "Kudsk & Dahl" };
        private readonly string[] _departments = { "Ikke Angivet", "Salg", "SCM", "Produktion", "Vedligehold", "Finans", "HR", "PMO & Trans" };

        public async Task<bool> IsAnyUsers()
        {
           return await Context.Users.AnyAsync();
        }

        public async Task<List<IdentityUser>> GetUsernameList()
        {
            return await Context.Users.ToListAsync();
        } 
        public async Task SetDefaultDeBuTbls()
        {
            for (int i = 0; i < _departments.Length; i++)
            {
                DepartmentsTbl dep = new();
                dep.Id = i + 1;
                dep.Name = _departments[i];
                await Context.DepartmentsTbl.AddAsync(dep);
            }
            for (int i = 0; i < _businessUnits.Length; i++)
            {
                BusinessUnitsTbl bu = new();
                bu.Id = i + 1;
                bu.Name = _businessUnits[i];
                await Context.BusinessUnitsTbl.AddAsync(bu);
            }
            try
            {
               await Context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                //FIX Exception  HERE
                throw new DbUpdateException("Updating tables in database failed." + ex.Message);
            }
        }
        public async Task<bool> DoesDatabaseExist()
        {
            return await Context.Database.CanConnectAsync();
        }
        public async Task<bool> IsBuAndDepEmpty()
        {
            return !(await Context.DepartmentsTbl.AnyAsync() && await Context.BusinessUnitsTbl.AnyAsync());
        }
        public async Task UpdateGuideText(GuideTextTbl guideText)
        {
            Context.GuideTextTbl.Update(guideText);
            await Context.SaveChangesAsync();
        }
        public Task<GuideTextTbl> GetGuideText()
        {
            return Context.GuideTextTbl.FirstOrDefaultAsync();
        }
        public async Task SetDefaultGuideText()
        {
            GuideTextTbl guideText = new();
            guideText.Id = 1;
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
