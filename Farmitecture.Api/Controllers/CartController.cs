using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Entities;
using Farmitecture.Api.Services;
using Farmitecture.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Farmitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetCart(string sessionId)
        {
            var cart = await _cartService.GetCartBySessionId(sessionId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CreateCartItemRequest item, [FromQuery] string? sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
            }

            var res= await _cartService.AddToCart(sessionId, item);
            return Ok(res);
        }
        
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCart([FromQuery] string sessionId, [FromBody] UpdateCartRequest request)
        {
            var response = await _cartService.UpdateCart(sessionId, request);

            return Ok(response);
        }
    }
}