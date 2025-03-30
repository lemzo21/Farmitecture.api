namespace Farmitecture.Api.Data.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public bool? IsPaid { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}