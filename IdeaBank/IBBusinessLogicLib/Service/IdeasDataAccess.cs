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

        /// <summary>
        /// Get a list of ideas that match the filter FilterIdea
        /// </summary>
        /// <param name="idea">Filter object</param>
        /// <returns>List of filtered ideas of type ViewIdea</returns>
        public async Task<List<ViewIdea>> GetWFilter(FilterIdea idea)
        {
            List<IdeasTbl> ideas = (await Repository.ListAsync(idea)).ToList();
            return DBConvert.TblToViewIdea(ideas);
        }
        /// <summary>
        /// Insert an idea.
        /// </summary>
        /// <param name="idea"></param>
        public async Task Insert(NewIdea idea)
        {
            await Repository.AddAsync(DBConvert.NewIdeaToTbl(idea));
        }
        /// <summary>
        /// Delete an idea.
        /// </summary>
        /// <param name="id">Id of the idea to be deleted.</param>
        public async Task DeleteByID(int id)
        {
            await Repository.RemoveByIdAsync(id);
        }
        /// <summary>
        /// Edit an idea.
        /// </summary>
        /// <param name="idea"></param>
        public async Task Edit(EditIdea idea)
        {
            await Repository.UpdateAsync(DBConvert.EditIdeaToTbl(idea));
        }
        /// <summary>
        /// Number of total ideas
        /// </summary>
        /// <returns>Amount of total ideas</returns>
        public int Count()
        {
            return Repository.IdeasCount;
        }
    }
}

