using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDtoWithOutComment ToStockDtoNoComment(this Stock stockModel)
        {
            return new StockDtoWithOutComment
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Industry = stockModel.Industry,
                LastDivdend = stockModel.LastDivdend,
                MarketCap = stockModel.MarketCap,
                Purchase = stockModel.Purchase,
            };
        }
        // Convert stock model to StockDtoand return this Dto
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Industry = stockModel.Industry,
                LastDivdend = stockModel.LastDivdend,
                MarketCap = stockModel.MarketCap,
                Purchase = stockModel.Purchase,
                Comments = stockModel.Comments
                .Select(c => c.ToCommentDto())
                .ToList()
            };
        }

        // Convert CreateStockRequestDto To Stock model
        public static Stock ToStockFromCreateDto(this CreateStockRequestDto requestDto)
        {
            return new Stock
            {
                Symbol = requestDto.Symbol,
                CompanyName = requestDto.CompanyName,
                Industry = requestDto.Industry,
                LastDivdend = requestDto.LastDivdend,
                MarketCap = requestDto.MarketCap,
                Purchase= requestDto.Purchase,
            };
        }
    }
}
