﻿using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stopRepo)
        {
            
            _commentRepo = commentRepo;
            _stockRepo = stopRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var comments =  await _commentRepo.GetAllAsync();
           var commentDto = comments.Select(c => c.ToCommentDto()); ;
           return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if(comment == null) return NotFound();
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentDto, [FromRoute] int stockId)
        {
            if (!await _stockRepo.StockExist(stockId)) return BadRequest("Stock does not exist");
            var commentModel = commentDto.ToCommentFromCreateDto(stockId);
            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id},
                commentModel.ToCommentDto());

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            var commentModel = await _commentRepo.UpdateAsync(id, commentDto);
            if(commentModel == null) return NotFound();
            return Ok(commentModel.ToCommentDto());

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
           var commentModel = await _commentRepo.DeleteAsync(id);
           if(commentModel == null) return NotFound();
            return NoContent();
        }
    }
}