namespace Farmitecture.Api.Data.Dtos;

public class PaymentRequest
{
    public decimal Amount { get; set; }
    public Guid OrderId { get; set; }
    public string CallbackUrl { get; set; }
}

public class PaymentCallback
{
    public Guid OrderId { get; set; }
    public string Status { get; set; }
}