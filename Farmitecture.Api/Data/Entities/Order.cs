using System.ComponentModel.DataAnnotations;
using Farmitecture.Api.Data.Dtos;
using Newtonsoft.Json;

namespace Farmitecture.Api.Data.Entities;
#nullable disable
public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; }
    public bool? IsPaid { get; set; }
    [StringLength(100)]public string Total { get; set; } 
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public OrderVerifiedData VerifiedData { get; set; }
}

public class OrderItem : BaseEntity
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}

public class OrderVerifiedData:BaseEntity
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("domain")]
    public string Domain { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("reference")]
    public string Reference { get; set; }

    [JsonProperty("receipt_number")]
    public string ReceiptNumber { get; set; }

    [JsonProperty("amount")]
    public int Amount { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("gateway_response")]
    public string GatewayResponse { get; set; }

    [JsonProperty("paid_at")]
    public DateTime PaidAt { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("channel")]
    public string Channel { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("ip_address")]
    public string IpAddress { get; set; }

    [JsonProperty("metadata")]
    public string Metadata { get; set; }

    [JsonProperty("log")]
    public Log Log { get; set; }

    [JsonProperty("fees")]
    public int Fees { get; set; }

    [JsonProperty("authorization")]
    public Authorization Authorization { get; set; }

    [JsonProperty("customer")]
    public Customer Customer { get; set; }

    [JsonProperty("paidAt")]
    public DateTime PaidAtAlt { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAtAlt { get; set; }

    [JsonProperty("requested_amount")]
    public int RequestedAmount { get; set; }

    [JsonProperty("transaction_date")]
    public DateTime TransactionDate { get; set; }
}

public class Log:BaseEntity
{
    [JsonProperty("start_time")]
    public long StartTime { get; set; }

    [JsonProperty("time_spent")]
    public int TimeSpent { get; set; }

    [JsonProperty("attempts")]
    public int Attempts { get; set; }

    [JsonProperty("errors")]
    public int Errors { get; set; }

    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("mobile")]
    public bool Mobile { get; set; }

    [JsonProperty("history")]
    public List<History> History { get; set; }
}

public class History:BaseEntity
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("time")]
    public int Time { get; set; }
}

public class Authorization:BaseEntity
{
    [JsonProperty("authorization_code")]
    public string AuthorizationCode { get; set; }

    [JsonProperty("bin")]
    public string Bin { get; set; }

    [JsonProperty("last4")]
    public string Last4 { get; set; }

    [JsonProperty("exp_month")]
    public string ExpMonth { get; set; }

    [JsonProperty("exp_year")]
    public string ExpYear { get; set; }

    [JsonProperty("channel")]
    public string Channel { get; set; }

    [JsonProperty("card_type")]
    public string CardType { get; set; }

    [JsonProperty("bank")]
    public string Bank { get; set; }

    [JsonProperty("country_code")]
    public string CountryCode { get; set; }

    [JsonProperty("brand")]
    public string Brand { get; set; }

    [JsonProperty("reusable")]
    public bool Reusable { get; set; }

    [JsonProperty("signature")]
    public string Signature { get; set; }

    [JsonProperty("account_name")]
    public string AccountName { get; set; }

    [JsonProperty("mobile_money_number")]
    public string MobileMoneyNumber { get; set; }
}