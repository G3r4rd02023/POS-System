namespace POS.Shared.DTOs.CashClosing
{
    public class CashClosingDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal InitialBalance { get; set; }
        public decimal TotalCash { get; set; }
        public decimal TotalCard { get; set; }
        public decimal TotalTransfer { get; set; }
        public decimal TotalAdjustments { get; set; }
        public decimal FinalBalance { get; set; }
        public decimal FinalCash { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
