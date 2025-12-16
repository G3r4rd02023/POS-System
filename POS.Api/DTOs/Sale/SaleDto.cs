namespace POS.Shared.DTOs.Sale
{
    public class SaleDto
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string UserName { get; set; } = string.Empty;
        public ICollection<SaleDetailDto> SaleDetails { get; set; } = new List<SaleDetailDto>();
    }
}
