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

        /// <summary>
        /// Gets a list of comments that belongs to an idea.
        /// </summary>
        /// <param name="id">idea id</param>
        /// <returns>List of comments</returns>
        public async Task<List<Comment>> GetWFilter(int id)
        {
            List<CommentsTbl> comments = (await Repository.ListAsync(id)).ToList();
            return DBConvert.TblToComment(comments);
        }

        /// <summary>
        /// Insert a new comment
        /// </summary>
        /// <param name="comment"></param>
        public async Task Insert(Comment comment)
        {
            await Repository.AddAsync(DBConvert.CommentToTbl(comment), comment.IdeaId);
        }
        
        /// <summary>
        /// Delete a comment.
        /// </summary>
        /// <param name="id">Comment id</param>
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