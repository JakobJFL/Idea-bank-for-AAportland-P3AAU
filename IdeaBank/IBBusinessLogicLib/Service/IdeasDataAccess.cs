using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseLib.Models;
using System.Linq;
using RepositoryLib.Interfaces;
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLib.Service
{
    public class IdeasDataAccess : IIdeasDataAccess
    {
        public IdeasDataAccess(IIdeaRepository repository)
        {
            Repository = repository;
        }
        public IIdeaRepository Repository { get; }
        public async Task<List<ViewIdea>> GetWFilter(FilterIdea filter)
        {
            List<IdeasTbl> ideas = (await Repository.ListAsync(filter)).ToList();
            return DBConvert.TblToViewIdea(ideas);
        }
        public async Task Insert(NewIdea idea)
        {
            await Repository.AddAsync(DBConvert.NewIdeaToTbl(idea));
        }
        public async Task DeleteByID(int id)
        {
            await Repository.RemoveByIdAsync(id);
        }
        public async Task Edit(EditIdea idea)
        {
            Console.WriteLine(idea.AuthorDepartment);
            await Repository.UpdateAsync(DBConvert.EditIdeaToTbl(idea));
        }
        public int GetIdeasCount()
        {
            return Repository.IdeasCount;
        }
        public async Task<int> GetCount(FilterIdea filter)
        {
            return await Repository.CountAsync(filter);
        }
    }
}

