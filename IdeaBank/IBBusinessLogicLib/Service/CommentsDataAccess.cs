using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using DataBaseLib.Models;
using RepositoryLib.Interfaces;

namespace BusinessLogicLib.Service
{
    public class CommentsDataAccess : ICommentsDataAccess
    {
        public CommentsDataAccess(ICommentsRepository repository)
        {
            Repository = repository;
        }
        public ICommentsRepository Repository { get; }
        public async Task<List<Comment>> GetWFilter(int id)
        {
            List<CommentsTbl> comments = (await Repository.ListAsync(id)).ToList();
            return DBConvert.TblToComment(comments);
        }
        public async Task Insert(Comment comment)
        {
            await Repository.AddAsync(DBConvert.CommentToTbl(comment), comment.IdeaId);
        }
        public async Task DeleteByID(int id)
        {
            await Repository.RemoveByIdAsync(id);
        }
        public async Task<int> GetCommentsCount(int ideaId)
        {
            return await Repository.CountAsync(ideaId);
        }
    }
}