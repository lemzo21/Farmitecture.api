namespace Farmitecture.Api.Data.Dtos;
#nullable disable
public class CartDto
{
    public string SessionId { get; set; }
    public List<CartItemDto> Items { get; set; }
}

public class CartItemDto
{
    public ProductDto Product { get; set; }
    public int Quantity { get; set; }
}