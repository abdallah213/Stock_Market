using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Ef.Extension;
using StockMarketEntites.Dtos.Comment;
using StockMarketEntites.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(ICommentRepository commentRepository ,
        IStockRepository stockRepository,
        UserManager<AppUser> userManager) : ControllerBase
    {
        private readonly ICommentRepository _commentRepo = commentRepository;
        private readonly IStockRepository _stockRepo = stockRepository;
        private readonly UserManager<AppUser> _userManager = userManager;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(x => x.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.GetCommentByIdAsync(id);

            if (comment == null)
                return NotFound();

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.stockExisy(stockId);
            if (!stock)
            {
                return BadRequest("Stock Does Not Exist");
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var commenModel = commentDto.ToCommentFromCreateDto(stockId);
            commenModel.AppUserId = appUser.Id;
            await _commentRepo.CreateCommentAsync(commenModel);
            return CreatedAtAction(nameof(GetById) , new {id = commenModel.Id} , commenModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{commentId:int}")]
        public async Task<IActionResult> Edit([FromRoute] int commentId , [FromBody] EditCommentRequestDto editDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo
                .EditCommentAsync(commentId, editDto.ToCommentFromEditDto());

            if (comment == null)
            { return NotFound("Comment Not Found"); }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{commentId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int commentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.DeleteCommentAsync(commentId);
            if (comment == null)
            { return NotFound("Comment Does Not Exist"); }

            return NoContent();
        }
    }
}
