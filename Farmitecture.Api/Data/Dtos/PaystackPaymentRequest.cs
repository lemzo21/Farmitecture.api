using Newtonsoft.Json;

namespace Farmitecture.Api.Data.Dtos;
#nullable disable
public class PaystackPaymentRequest
{
    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("amount")]
    public string Amount { get; set; }

    [JsonProperty("callback_url")]
    public string CallbackUrl { get; set; }
    
    [JsonProperty("reference")]
    public string Reference { get; set; }
}

public class PaystackPaymentResponse
{
    [JsonProperty("status")]
    public bool Status { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("data")]
    public Data Data { get; set; } = new();
}

public class PaystackVerifyPaymentResponse
{
    [JsonProperty("status")]
    public bool Status { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("data")]
    public VerifiedData Data { get; set; } = new();
}

public class VerifiedData
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

    [JsonProperty("fees_split")]
    public object FeesSplit { get; set; }

    [JsonProperty("authorization")]
    public Authorization Authorization { get; set; }

    [JsonProperty("customer")]
    public Customer Customer { get; set; }

    [JsonProperty("plan")]
    public object Plan { get; set; }

    [JsonProperty("split")]
    public object Split { get; set; }

    [JsonProperty("order_id")]
    public object OrderId { get; set; }

    [JsonProperty("paidAt")]
    public DateTime PaidAtAlt { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAtAlt { get; set; }

    [JsonProperty("requested_amount")]
    public int RequestedAmount { get; set; }

    [JsonProperty("pos_transaction_data")]
    public object PosTransactionData { get; set; }

    [JsonProperty("source")]
    public object Source { get; set; }

    [JsonProperty("fees_breakdown")]
    public object FeesBreakdown { get; set; }

    [JsonProperty("connect")]
    public object Connect { get; set; }

    [JsonProperty("transaction_date")]
    public DateTime TransactionDate { get; set; }

    [JsonProperty("plan_object")]
    public object PlanObject { get; set; }

    [JsonProperty("subaccount")]
    public object Subaccount { get; set; }
}

public class Log
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

    [JsonProperty("input")]
    public List<object> Input { get; set; }

    [JsonProperty("history")]
    public List<History> History { get; set; }
}

public class History
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("time")]
    public int Time { get; set; }
}

public class Authorization
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

public class Customer
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("customer_code")]
    public string CustomerCode { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("metadata")]
    public string Metadata { get; set; }

    [JsonProperty("risk_action")]
    public string RiskAction { get; set; }

    [JsonProperty("international_format_phone")]
    public string InternationalFormatPhone { get; set; }
}

public class Data
{
    [JsonProperty("authorization_url")]
    public string AuthorizationUrl { get; set; } = string.Empty;

    [JsonProperty("access_code")]
    public string AccessCode { get; set; } = string.Empty;

    [JsonProperty("reference")]
    public string Reference { get; set; } = string.Empty;
}