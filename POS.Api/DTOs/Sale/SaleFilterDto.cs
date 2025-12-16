using POS.Shared.Enums;

namespace POS.Shared.DTOs.Sale
{
    public class SaleFilterDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
    }
}
