using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseLib.Models;
using System.Linq;
using RepositoryLib.Interfaces;
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;

namespace BusinessLogicLib.Service
{
    public class IdeasDataAccess : IIdeasDataAccess
    {
        public IdeasDataAccess(IIdeaRepository repository)
        {
            Repository = repository;
        }
        public IIdeaRepository Repository { get; }

        public async Task<List<ViewIdea>> GetWFilter(FilterIdea idea)
        {
            List<IdeasTbl> ideas = (await Repository.ListAsync(idea)).ToList();
            return DBConvert.TblToViewIdea(ideas);
        }
        public async Task Insert(NewIdea idea)
        {
            await Repository.AddAsync(DBConvert.NewIdeaToTbl(idea), idea.Department, idea.BusinessUnit);
        }
        public Task Edit(ViewIdea idea)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}

