namespace Farmitecture.Api.Data.Dtos;
#nullable disable
public class PaystackPaymentRequest
{
    public string Email { get; set; } = string.Empty;
    public decimal Amount { get; set; } 
    public string Currency { get; set; } 
    public string CallbackUrl { get; set; }
}

public class PaystackPaymentResponse
{
    public bool Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public Data Data { get; set; } = new();
}

public class Data
{
    public string AuthorizationUrl { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
}