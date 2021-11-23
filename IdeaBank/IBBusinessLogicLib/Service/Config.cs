using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryLib.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLib
{
    public class Config : IConfig
    {
        public IConfigurationRepository Repository { get; }

        public Config(IConfigurationRepository repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Create tables DepartmentTbl and BusinessUnitTbl if empty
        /// </summary>
        public async Task ConfigureDBTables()
        {
            await Repository.IsAnyUsers();

            if (!(await Repository.DoesDatabaseExist())) 
                throw new DbNoConnectionException();

            if (await Repository.IsBuAndDepEmpty())
                await Repository.SetDefaultDeBuTbls();

            if (await Repository.IsGuideTextEmpty())
                await Repository.SetDefaultGuideText();
        }
        public async Task<List<IdentityUser>> GetUsers()
        {
            return await Repository.GetUsernameList();
        }

        public async Task EditGuideText(Settings settings)
        {
            await Repository.UpdateGuideText(DBConvert.SettingsToGuideTbl(settings));
        }

        public async Task<Settings> GetGuideText()
        {
            return DBConvert.GuideTblToSettings(await Repository.GetGuideText());
        }
    }
}

