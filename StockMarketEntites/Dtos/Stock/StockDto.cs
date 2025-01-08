using api.Dtos.Comment;
using api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        // أرباح الأسهم
        public decimal LastDivdend { get; set; }
        // صناعة
        public string Industry { get; set; } = string.Empty;
        // Market capitalization القيمة السوقية
        public long MarketCap { get; set; }
        public List<CommentDto> Comments { get; set; }

    }
}
