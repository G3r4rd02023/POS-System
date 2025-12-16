using System.ComponentModel.DataAnnotations;

namespace POS.Shared.DTOs.CashClosing
{
    public class CreateCashClosingDto
    {
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Final cash must be non-negative")]
        public decimal FinalCash { get; set; }

        public string? Notes { get; set; }

        public int UserId { get; set; }
    }
}
