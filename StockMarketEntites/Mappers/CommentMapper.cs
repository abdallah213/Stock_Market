using api.Dtos.Comment;
using api.Models;
using StockMarketEntites.Dtos.Comment;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
                CreatedBy = comment.AppUser.UserName,
                Title = comment.Title 
            };
        }

        public static Comment ToCommentFromCreateDto(this CreateCommentDto commentDto , int stockId)
        {
            return new Comment
            {
                Content = commentDto.Content,
                StockId = stockId,
                Title = commentDto.Title
            };
        }

        public static Comment ToCommentFromEditDto(this EditCommentRequestDto commentDto)
        {
            return new Comment
            {
                Content = commentDto.Content,
                Title = commentDto.Title,
            };
        }
    }
}
