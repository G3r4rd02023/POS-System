using System.ComponentModel.DataAnnotations;
using POS.Shared.Enums;

namespace POS.Shared.DTOs.Sale
{
    public class CreateSaleDto
    {
        [Required]
        public int UserId { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Efectivo;

        [Required]
        public List<SaleDetailDto> SaleDetails { get; set; } = new();
    }
}
