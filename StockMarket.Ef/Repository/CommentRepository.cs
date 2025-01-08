using api.Data;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository(AppDbContext context) : ICommentRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Comment> CreateCommentAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        }

        public async Task<Comment?> DeleteCommentAsync(int commentId)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(i=> i.Id == commentId);

            if(comment == null)
                return null;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment?> EditCommentAsync(int commentId, Comment comentModel)
        {
            var existingComment = await _context.Comments
                .FindAsync(commentId);
            if(existingComment == null)
            {
                return null;
            }

            existingComment.Title = comentModel.Title;
            existingComment.Content = comentModel.Content;
            await _context.SaveChangesAsync();
            return existingComment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments
                .Include(u=>u.AppUser)
                .ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _context.Comments
                .Include(u => u.AppUser) 
                .FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
