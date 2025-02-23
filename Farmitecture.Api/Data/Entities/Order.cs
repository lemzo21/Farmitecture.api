namespace Farmitecture.Api.Data.Entities;

public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; }
    public bool IsPaid { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

public class OrderItem : BaseEntity
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}