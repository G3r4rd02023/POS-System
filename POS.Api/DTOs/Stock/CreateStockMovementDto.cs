using POS.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace POS.Shared.DTOs.Stock
{
    public class CreateStockMovementDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [Required]
        public MovementType MovementType { get; set; }

        [MaxLength(200)]
        public string? Notes { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
