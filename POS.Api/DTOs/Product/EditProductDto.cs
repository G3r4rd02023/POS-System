using System.ComponentModel.DataAnnotations;

namespace POS.Shared.DTOs.Product
{
    public class EditProductDto
    {
        [Required(ErrorMessage = "The Code field is required.")]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock must be greater than or equal to 0.")]
        public int CurrentStock { get; set; }

        public int StockMinimum { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }

        public string? Image { get; set; } // Base64 string for image upload
    }
}
