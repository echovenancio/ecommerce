namespace RO.DevTest.Application.DTOs;

public class SalesReportDto {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int TotalUnitsSold { get; set; }
        public decimal TotalSales { get; set; }
        public DateTime StartSaleDate { get; set; }
        public DateTime EndSaleDate { get; set; }
    }
