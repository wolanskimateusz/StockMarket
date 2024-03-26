﻿using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllAsync();
        public Task<Comment?> GetByIdAsync(int id);
        public Task<Comment> CreateAsync(Comment comentModel);
        public Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto);
        public Task<Comment?> DeleteAsync(int id);
    }
}
