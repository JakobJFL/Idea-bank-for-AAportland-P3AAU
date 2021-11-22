using BusinessLogicLib.Interfaces;
using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryLib.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLib
{
    public class DBTableConfiguration : IDBTableConfiguration
    {
        public ITblsConfigRepository Repository { get; }

        public DBTableConfiguration(ITblsConfigRepository repository)
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
        }

        public async Task<List<IdentityUser>> GetUsers()
        {
            return await Repository.GetUsernameList();
        }
    }
}

