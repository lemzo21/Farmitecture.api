namespace Farmitecture.Api.Data.Models;
#nullable disable
public class ApiResponse<T>
{
    public int Code { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public bool  IsSuccessful { get; set; }
}