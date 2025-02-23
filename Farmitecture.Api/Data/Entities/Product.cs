using System.ComponentModel.DataAnnotations;

namespace Farmitecture.Api.Data.Entities;
#nullable disable
public class Product:BaseEntity
{
    [StringLength(500)]public string Name { get; set; }
    [StringLength(500)]public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    [StringLength(500)]public string ImageUrl { get; set; }
    [StringLength(500)]public string Category { get; set; }
}