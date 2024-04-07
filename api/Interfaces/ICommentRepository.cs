using api.Dtos.Comment;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject);
        public Task<Comment?> GetByIdAsync(int id);
        public Task<Comment> CreateAsync(Comment comentModel);
        public Task<Comment?> UpdateAsync(int id, Comment commentModel);
        public Task<Comment?> DeleteAsync(int id);
    }
}
