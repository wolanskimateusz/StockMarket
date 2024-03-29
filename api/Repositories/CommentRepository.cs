using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDBContext _context;
        public CommentRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comentModel)
        {
            await _context.Comments.AddAsync(comentModel);
            await _context.SaveChangesAsync();
            return comentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) return null;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return null;
            return comment;
        }

      

         public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
         {
             var comment = await _context.Comments.FindAsync(id);
             if (comment == null) return null;

             
             comment.Title = commentModel.Title;
             comment.Content = commentModel.Content;
             

             await _context.SaveChangesAsync();
             return comment;
         }
    }
}
