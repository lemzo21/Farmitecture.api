using System.Globalization;
using Farmitecture.Api.Data;
using Farmitecture.Api.Data.Entities;
using Farmitecture.Api.Data.Models;
using Farmitecture.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using AutoMapper;
using Farmitecture.Api.Data.Dtos;
using Flurl.Http;
using Newtonsoft.Json;

namespace Farmitecture.Api.Services.Providers;

public class CheckoutService(ApplicationDbContext context,IConfiguration config, IMapper mapper) : ICheckoutService
{
    private readonly string? _baseUrl = config.GetSection("Paystack:BaseUrl").Value;
    private readonly string? _secretKey = config.GetSection("Paystack:SecretKey").Value;
    private readonly string? _clientUrl = config.GetSection("ClientUrl").Value;

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

        order.Total =
            (order.OrderItems.Sum(i => i.Quantity * GetProductPrice(i.ProductId)) * 100).ToString("F0",
                CultureInfo.InvariantCulture);
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        
        var payStackRequest = new PaystackPaymentRequest
        {
            Amount = order.Total,
            Email = request.Email,
            CallbackUrl = $"{_clientUrl}/orders",
            Reference = order.Id.ToString(),
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
            var jsonPayload = JsonConvert.SerializeObject(request);
            var strResponse = await $"{_baseUrl}/transaction/initialize"
                .WithHeader("Authorization", $"Bearer {_secretKey}")
                .WithHeader("Content-Type", "application/json")
                .AllowAnyHttpStatus()
                .PostAsync(new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

            var response = JsonConvert.DeserializeObject<PaystackPaymentResponse>(await strResponse.GetStringAsync());
            if (response!.Status)
            {
                return response.Data.Reference; 
            } 
            return null;
        }
        catch(Exception ex)
        {
            return null;
        }
    }
    

    public async Task HandlePaymentCallbackAsync(string reference)
    {
        var order = await context.Orders.Include(o=>o.OrderItems).FirstOrDefaultAsync(o=>o.Id==Guid.Parse(reference) );
        if (order == null)
        {
            return;
        }

        var response = await VerifyPayment(reference);
        if (response is { Status: "success" } && response.Amount.ToString() == order.Total)
        {
            order.IsPaid = true;
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
        order.VerifiedData = mapper.Map<OrderVerifiedData>(response);

        await context.SaveChangesAsync();
    }

    private async Task<VerifiedData?> VerifyPayment(string reference)
    {
        try
        {
            var strResponse = await $"{_baseUrl}/transaction/verify/{reference}"
                .WithHeader("Authorization", $"Bearer {_secretKey}")
                .WithHeader("Content-Type", "application/json")
                .AllowAnyHttpStatus()
                .GetAsync();
    
            var response = JsonConvert.DeserializeObject<PaystackVerifyPaymentResponse>(await strResponse.GetStringAsync());
            return response?.Data;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private decimal GetProductPrice(Guid productId)
    {
        var product = context.Products.Find(productId);
        if (product == null)
        {
            throw new ArgumentException($"Product with ID {productId} not found.");
        }
        return product.Price;
    }
    
    public async Task<ApiResponse<bool?>> GetOrderStatusAsync(Guid orderId)
    {
        var order = await context.Orders.FindAsync(orderId);
        return new ApiResponse<bool?>
        {
            Code = StatusCodes.Status200OK,
            IsSuccessful = true,
            Message = "Order created successfully",
            Data = order?.IsPaid
        };
    }

    public async Task<ApiResponse<OrderDto?>> GetOrderDetailsAsync(Guid orderId)
    {
        var order = await context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null)
        {
            return null;
        }

        var orderDto = mapper.Map<OrderDto>(order);

        return new ApiResponse<OrderDto?>
        {
            Code = StatusCodes.Status200OK,
            IsSuccessful = true,
            Message = "Order created successfully",
            Data = orderDto
        };
    }
}