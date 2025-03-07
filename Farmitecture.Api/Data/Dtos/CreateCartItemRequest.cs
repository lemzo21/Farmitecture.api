using System.ComponentModel.DataAnnotations;

namespace Farmitecture.Api.Data.Dtos;

public class CreateCartItemRequest
{
    public Guid? CartId { get; set; }
    [Required]
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateCartRequest
{
    public List<UpdateCartItemRequest> Items { get; set; } 
}

public class UpdateCartItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}