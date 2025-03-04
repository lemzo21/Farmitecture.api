using Farmitecture.Api.Data;
using Farmitecture.Api.Data.Entities;
using Farmitecture.Api.Data.Models;
using Farmitecture.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;
using Farmitecture.Api.Data.Dtos;
using Flurl.Http;

namespace Farmitecture.Api.Services.Providers;

public class CheckoutService(ApplicationDbContext context,IConfiguration config) : ICheckoutService
{
    private readonly string? _baseUrl = config.GetSection("Paystack:BaseUrl").Value;
    private readonly string? _secretKey = config.GetSection("Paystack:SecretKey").Value;

    public async Task<ApiResponse<string>> CreateOrderAsync(CheckoutRequest request)
    {
        var order = new Order
        {
            OrderDate = DateTime.UtcNow,
            IsPaid = false
        };

        foreach (var item in request.Items)
        {
            var product = await context.Products.FindAsync(item.ProductId);
            if (product == null)
            {
                return new ApiResponse<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    IsSuccessful = false,
                    Message = $"Product with ID {item.ProductId} not found.",
                };
            }

            if (item.Quantity > product.Stock)
            {
                return new ApiResponse<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    IsSuccessful = false,
                    Message = $"Insufficient stock for product with ID {item.ProductId}. Available stock: {product.Stock}, requested quantity: {item.Quantity}."
                };
            }

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
        
        var payStackRequest = new PaystackPaymentRequest
        {
            Amount = order.OrderItems.Sum(i => i.Quantity * GetProductPrice(i.ProductId)),
            Email = request.Email,
            Currency = "GHC",
            CallbackUrl = "https://yourapi.com/api/checkout/payment-callback",
        };

        var response = await InitializePayment(payStackRequest);
        
        if (response == null)
        {
            return new ApiResponse<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                IsSuccessful = false,
                Message = "Failed to initialize payment"
            };
        }

        return new ApiResponse<string>
        {
            Code = StatusCodes.Status200OK,
            IsSuccessful = true,
            Message = "Order created successfully",
            Data = response
        };
    }
    
    private async Task<string?> InitializePayment(PaystackPaymentRequest request)
    {
        try
        {
            var response = await $"{_baseUrl}/transaction/initialize"
                .WithOAuthBearerToken(_secretKey)
                .PostJsonAsync(request)
                .ReceiveJson<PaystackPaymentResponse>();

            if (response.Status)
            {
                return response.Data.AuthorizationUrl; 
            } 
            return null;
        }
        catch(Exception ex)
        {
            return null;
        }
    }
    

    public async Task HandlePaymentCallbackAsync(dynamic callback)
    {
        var reference = (string)callback.data.reference;
        var status = (string)callback.data.status;
        
        var order = await context.Orders.Include(o=>o.OrderItems).FirstOrDefaultAsync(o=>o.Id==Guid.Parse(reference) );
        if (order == null)
        {
            return;
        }

        if (status == "success")
        {
            order.IsPaid = true;
            // Reduce the stock
            foreach (var item in order.OrderItems)
            {
                var product = await context.Products.FindAsync(item.ProductId);
                product!.Stock -= item.Quantity;
            }
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