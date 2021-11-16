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
            try
            {
                List<IdeasTbl> ideas = (await Repository.ListAsync(idea)).ToList();
                return DBConvert.TblToViewIdea(ideas);
            }
            catch
            {
                throw new Exception("GetWFilter failed");
            }
        }
        public async Task Insert(NewIdea idea)
        {
            try
            {
                await Repository.AddAsync(DBConvert.NewIdeaToTbl(idea), idea.Department, idea.BusinessUnit);
            }
            catch
            {
                throw new Exception("Insert failed");
            }
        }
        public async Task DeleteByID(int id)
        {
            await Repository.RemoveByIdAsync(id);
        }
        public async Task Edit(EditIdea idea)
        {
            Console.WriteLine(idea.Department);
            await Repository.UpdateAsync(DBConvert.EditIdeaToTbl(idea), idea.Department, idea.BusinessUnit);
        }
    }
}

