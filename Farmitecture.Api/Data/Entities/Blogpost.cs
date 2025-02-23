namespace Farmitecture.Api.Data.Entities;
#nullable disable
public class Blogpost : BaseEntity
{
    public string Topic { get; set; }
    public string Content { get; set; }
    public string Category { get; set; }
    public string ImageUrl { get; set; }
    public string Status { get; set; }
    public string MetaDescription { get; set; }
}