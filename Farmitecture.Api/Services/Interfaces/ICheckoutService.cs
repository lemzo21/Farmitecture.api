using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Entities;
using Farmitecture.Api.Data.Models;

namespace Farmitecture.Api.Services.Interfaces;

public interface ICheckoutService
{
    Task<ApiResponse<string>> CreateOrderAsync(CheckoutRequest request);
    Task HandlePaymentCallbackAsync(string reference);
    Task<ApiResponse<bool?>> GetOrderStatusAsync(Guid orderId);
    Task<ApiResponse<OrderDto?>> GetOrderDetailsAsync(Guid orderId);
}