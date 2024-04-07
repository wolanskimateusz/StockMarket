using api.Data;
using api.Dtos.Comment;
using api.Helpers;
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

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject)
        {
            var comments = _context.Comments.Include(a => a.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                comments = comments.Where(s => s.Stock.Symbol.ToLower() == queryObject.Symbol.ToLower());
            }
            if(queryObject.IsDescending == true)
            {
                comments = comments.OrderByDescending(c => c.CreatedOn);
            }
            return await comments.ToListAsync();
                
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
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
