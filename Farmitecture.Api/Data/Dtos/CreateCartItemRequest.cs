using System.ComponentModel.DataAnnotations;

namespace Farmitecture.Api.Data.Dtos;

public class CreateCartItemRequest
{
    public Guid? CartId { get; set; }
    [Required]
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}