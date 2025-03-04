namespace Farmitecture.Api.Data.Entities;
#nullable disable
public class Cart:BaseEntity
{
    public string SessionId { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}

public class CartItem:BaseEntity
{
    public Cart Cart { get; set; }
    public Guid? CartId { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}