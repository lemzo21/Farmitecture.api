namespace Farmitecture.Api.Data.Models;

public class BaseFilter
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string SortColumn { get; set; } = "createdAt";
    public string SortDir { get; set; } = "desc";
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

}