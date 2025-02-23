using Farmitecture.Api.Data;
using Farmitecture.Api.Data.Entities;
using Farmitecture.Api.Data.Models;
using Farmitecture.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;
using Farmitecture.Api.Data.Dtos;

namespace Farmitecture.Api.Services.Providers;

public class CheckoutService(ApplicationDbContext context) : ICheckoutService
{

    public async Task CreateOrderAsync(CheckoutRequest request)
    {
        var order = new Order
        {
            OrderDate = DateTime.UtcNow,
            IsPaid = false
        };

        foreach (var item in request.Items)
        {
            var orderItem = new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Order = order
            };
            order.OrderItems.Add(orderItem);
        }

        context.Orders.Add(order);
        await context.SaveChangesAsync();

      
        // var paymentRequest = new PaymentRequest
        // {
        //     Amount = order.OrderItems.Sum(i => i.Quantity * GetProductPrice(i.ProductId)),
        //     OrderId = order.Id,
        //     CallbackUrl = "https://yourapi.com/api/checkout/payment-callback"
        // };

       // var response = await _httpClient.PostAsJsonAsync("https://api.hubtel.com/v1/merchantaccount/onlinecheckout/invoice/create", paymentRequest);
        //var response = new HttpResponseMessage();
        // if (!response.IsSuccessStatusCode)
        // {
        //     return new ApiResponse<Order>
        //     {
        //         Code = (int)response.StatusCode,
        //         IsSuccessful = false,
        //         Message = "Failed to create payment"
        //     };
        // }
        //
        // return new ApiResponse<Order>
        // {
        //     Code = 200,
        //     Data = order,
        //     IsSuccessful = true,
        //     Message = "Order created successfully"
        // };
    }

    public async Task HandlePaymentCallbackAsync(PaymentCallback callback)
    {
        var order = await context.Orders.FindAsync(callback.OrderId);
        if (order == null)
        {
            return;
        }

        if (callback.Status == "success")
        {
            order.IsPaid = true;
        }
        else
        {
            order.IsPaid = false;
        }

        await context.SaveChangesAsync();
    }

    private decimal GetProductPrice(Guid productId)
    {
        // Implement logic to get product price
        return 10.0m; // Example price
    }
}