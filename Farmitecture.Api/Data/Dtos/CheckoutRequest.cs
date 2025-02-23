namespace Farmitecture.Api.Data.Dtos;

public class CheckoutRequest
{
    public List<CheckoutItem> Items { get; set; } = new List<CheckoutItem>();
}

public class CheckoutItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}