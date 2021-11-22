using BusinessLogicLib.Interfaces;
using DataBaseLib.DataAccess;
using DataBaseLib.Models;
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

        public async Task ConfigureDBTables()
        {
            await Repository.IsAnyUsers();

            if (!(await Repository.DoesDatabaseExist())) 
                throw new DbNoConnectionException();

            if (await Repository.IsBuAndDepEmpty())
                await Repository.SetDefaultDeBuTbls(); 
        }
    }
}

