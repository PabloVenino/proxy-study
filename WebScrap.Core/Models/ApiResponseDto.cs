namespace WebScrap.Core.Models;

public class ApiResponseDto
{
    public DataDto Data { get; set; }
}

public class DataDto
{
    public ProductDto Product { get; set; }
}

public class ProductDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
    public string Url { get; set; }
    public RatingDto Rating { get; set; }
    public PriceDto Price { get; set; }
    public InstallmentDto Installment { get; set; }
}

public class RatingDto
{
    public decimal Score { get; set; }
}

public class PriceDto
{
    public string Price { get; set; }
    public string FullPrice { get; set; }
    public string BestPrice { get; set; }
}

public class InstallmentDto
{
    public string PaymentMethodDescription { get; set; }
    public int Quantity { get; set; }
    public string Amount { get; set; }
    public string TotalAmount { get; set; }
}
