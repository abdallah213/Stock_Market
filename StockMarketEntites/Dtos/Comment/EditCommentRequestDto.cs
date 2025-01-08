using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketEntites.Dtos.Comment
{
    public class EditCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title Must be 5 characters")]
        [MaxLength(255, ErrorMessage = "Title cant be over 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content Must be 5 characters")]
        [MaxLength(255, ErrorMessage = "Content cant be over 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}
