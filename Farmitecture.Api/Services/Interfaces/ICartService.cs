using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Models;

namespace Farmitecture.Api.Services.Interfaces;

public interface ICartService
{
    Task<CartDto?> GetCartBySessionId(string sessionId);
    Task<ApiResponse<string>> AddToCart(string sessionId, CreateCartItemRequest item);
    Task<ApiResponse<string>> UpdateCart(string sessionId, UpdateCartRequest request);
}