namespace Farmitecture.Api.Data.Dtos;
#nullable disable
public class CheckoutRequest
{
    public string Email { get; set; }
    public List<CheckoutItem> Items { get; set; } = new List<CheckoutItem>();
}

public class CheckoutItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}