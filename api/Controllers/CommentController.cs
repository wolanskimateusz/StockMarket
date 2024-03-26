﻿using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepo)
        {
            
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var comments =  await _commentRepo.GetAllAsync();
           var commentDto = comments.Select(c => c.ToCommentDto()); ;
           return Ok(commentDto);
        }
    }
}
