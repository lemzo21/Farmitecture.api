using System.ComponentModel.DataAnnotations;

namespace Farmitecture.Api.Data.Dtos;
#nullable disable
public class CreateProductRequest
{
    [StringLength(500)]public string Name { get; set; }
    [StringLength(500)]public string Description { get; set; }
    [StringLength(500)]public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    [StringLength(500)]public string Category { get; set; }
}