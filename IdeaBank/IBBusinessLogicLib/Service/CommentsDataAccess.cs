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
        public async void Insert(Comment comment)
        {
            await Repository.AddAsync(DBConvert.CommentToTbl(comment));
        }
        public Task DeleteByID(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}