using AutoMapper;
using Farmitecture.Api.Data;
using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Entities;
using Farmitecture.Api.Data.Models;
using Farmitecture.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Farmitecture.Api.Services.Providers
{

    public class CartService(IMapper mapper, ApplicationDbContext context) : ICartService
    {
        

        public async Task<CartDto?> GetCartBySessionId(string sessionId)
        {
            var res= await context.Carts.Include(c => c.Items).ThenInclude(i=>i.Product)
                                       .FirstOrDefaultAsync(c => c.SessionId == sessionId);
            return res!=null ? mapper.Map<CartDto>(res) : null;
        }

        private async Task CreateCart(string sessionId)
        {
            var cart = new Cart { SessionId = sessionId };
            context.Carts.Add(cart);
            await context.SaveChangesAsync();
        }

        public async Task<ApiResponse<string>> AddToCart(string sessionId, CreateCartItemRequest item)
        {
            var product = await context.Products.FindAsync(item.ProductId);
            if (product == null)
            {
                return new ApiResponse<string>
                {
                    Message = "Product not found",
                    IsSuccessful = false,
                    Code = StatusCodes.Status404NotFound

                };
            }

            var cart = await context.Carts
                .FirstOrDefaultAsync(c => c.SessionId == sessionId);
            if (cart == null)
            {
                await CreateCart(sessionId);
                cart = await context.Carts
                    .FirstOrDefaultAsync(c => c.SessionId == sessionId);
            }
            
            item.CartId = cart?.Id;
            var cartItem = mapper.Map<CartItem>(item);
            context.CartItems.Add(cartItem);
            await context.SaveChangesAsync();
            return new ApiResponse<string>
            {
                Data = sessionId,
                Message = "Item added to cart",
                IsSuccessful = true,
                Code = StatusCodes.Status200OK
            };
        }
    }
}