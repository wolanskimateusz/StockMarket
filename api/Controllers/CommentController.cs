﻿using api.Dtos.Comment;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(ICommentRepository commentRepo, IStockRepository stopRepo, UserManager<AppUser> userManager)
        {
            
            _commentRepo = commentRepo;
            _stockRepo = stopRepo;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery]CommentQueryObject queryObject)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comments =  await _commentRepo.GetAllAsync(queryObject);
           var commentDto = comments.Select(c => c.ToCommentDto()); ;
           return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentRepo.GetByIdAsync(id);
            if(comment == null) return NotFound();
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentDto, [FromRoute] int stockId)
        {

            var username = User.GetUsername();
            var appuser = await _userManager.FindByNameAsync(username);

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _stockRepo.StockExist(stockId)) return BadRequest("Stock does not exist");
            var commentModel = commentDto.ToCommentFromCreateDto(stockId);
            commentModel.AppUserId = appuser.Id;

            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id},
                commentModel.ToCommentDto());

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var commentModel = await _commentRepo.UpdateAsync(id, commentDto.ToCommentFromUpdateDto());
            if(commentModel == null) return NotFound();
            return Ok(commentModel.ToCommentDto());

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var commentModel = await _commentRepo.DeleteAsync(id);
           if(commentModel == null) return NotFound();
            return NoContent();
        }
    }
}
