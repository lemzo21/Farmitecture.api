using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Models;
using Farmitecture.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Farmitecture.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckoutController(ICheckoutService checkoutService) : ControllerBase
{
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<object>))]
    public async Task<IActionResult> CreateOrder([FromBody] CheckoutRequest request)
    {
        var res = await checkoutService.CreateOrderAsync(request);
        return Ok(res);
    }

    [HttpGet]
    [Route("payment-callback")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<object>))]
    public async Task<IActionResult> HandlePaymentCallback([FromQuery] string trxref, [FromQuery] string reference)
    {
        await checkoutService.HandlePaymentCallbackAsync(reference);
        return Ok(null);
    }
    
    [HttpGet("{orderId}/status")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool?>))]
    public async Task<IActionResult> GetOrderStatus(Guid orderId)
    {
        var status = await checkoutService.GetOrderStatusAsync(orderId);
        if (status.Data == null)
        {
            return NotFound();
        }
        return Ok(new ApiResponse<bool?>
        {
            Code = StatusCodes.Status200OK,
            IsSuccessful = true,
            Data = status.Data
        });
    }

    [HttpGet("{orderId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<OrderDto>))]
    public async Task<IActionResult> GetOrderDetails(Guid orderId)
    {
        var order = await checkoutService.GetOrderDetailsAsync(orderId);
        if (order.Data == null)
        {
            return NotFound();
        }
        return Ok(new ApiResponse<OrderDto>
        {
            Code = StatusCodes.Status200OK,
            IsSuccessful = true,
            Data = order.Data
        });
    }
}