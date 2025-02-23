using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Entities;
using Farmitecture.Api.Data.Models;

namespace Farmitecture.Api.Services.Interfaces;

public interface ICheckoutService
{
    Task CreateOrderAsync(CheckoutRequest request);
    Task HandlePaymentCallbackAsync(PaymentCallback callback);
}