using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Farmitecture.Api.Data.Dtos;

public class UpdateProductRequest
{
    [StringLength(500)]public string Name { get; set; }
    [StringLength(500)]public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    [StringLength(500)]public string Category { get; set; }
}