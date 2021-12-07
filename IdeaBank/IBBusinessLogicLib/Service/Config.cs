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
        public IConfigurationRepository ConfigeRpository { get; }
        public IBusinessUnitsDataAccess BUDataAccess { get; }
        public IDepartmentsDataAccess DepDataAccess { get; }

        public Config(IConfigurationRepository configeRpository, IBusinessUnitsDataAccess bUDataAccess, IDepartmentsDataAccess depDataAccess)
        {
            ConfigeRpository = configeRpository;
            BUDataAccess = bUDataAccess;
            DepDataAccess = depDataAccess;
        }

        private readonly string[] _businessUnits = { "Ikke Angivet", "Aalborg Portland", "Unicon DK", "Unicon NO", "Kudsk & Dahl" };
        private readonly string[] _departments = { "Ikke Angivet", "Salg", "SCM", "Produktion", "Vedligehold", "Finans", "HR", "PMO & Trans" };

        /// <summary>
        /// Create tables DepartmentTbl and BusinessUnitTbl if empty
        /// </summary>
        public async Task ConfigureDBTables()
        {
            await ConfigeRpository.IsAnyUsers();

            if (!(await ConfigeRpository.DoesDatabaseExist())) 
                throw new DbNoConnectionException();

            if (await ConfigeRpository.IsBuAndDepEmpty())
                await SetDefaultDeBuTbls();

            if (await ConfigeRpository.IsGuideTextEmpty())
                await ConfigeRpository.SetDefaultGuideText();
        }
        private async Task SetDefaultDeBuTbls()
        {
            try
            {
                for (int i = 0; i < _departments.Length; i++)
                {
                    DepartmentsTbl dep = new();
                    dep.Id = i + 1;
                    dep.Name = _departments[i];
                    await DepDataAccess.Insert(dep);
                }
                for (int i = 0; i < _businessUnits.Length; i++)
                {
                    BusinessUnitsTbl bu = new();
                    bu.Id = i + 1;
                    bu.Name = _businessUnits[i];
                    await BUDataAccess.Insert(bu);
                }
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Updating tables in database failed." + ex.Message);
            }
        }
        public async Task<List<IdentityUser>> GetUsers()
        {
            return await ConfigeRpository.GetUsernameList();
        }

        public async Task EditGuideText(Settings settings)
        {
            await ConfigeRpository.UpdateGuideText(DBConvert.SettingsToGuideTbl(settings));
        }

        public async Task<Settings> GetGuideText()
        {
            return DBConvert.GuideTblToSettings(await ConfigeRpository.GetGuideText());
        }
    }
}

