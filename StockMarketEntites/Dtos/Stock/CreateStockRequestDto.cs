using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10 , ErrorMessage ="Symbol Cannot be over 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(25, ErrorMessage = "Company Name Cannot be over 25 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1,100000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        // أرباح الأسهم
        public decimal LastDivdend { get; set; }
        [MaxLength(25, ErrorMessage = "Industry Name Cannot be over 25 characters")]
        // صناعة
        public string Industry { get; set; } = string.Empty;
        // Market capitalization القيمة السوقية
        [Required]
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
    }
}
