using Farmitecture.Api.Data.Dtos;
using Farmitecture.Api.Data.Entities;
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

    [HttpPost]
    [Route("payment-callback")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<object>))]
    public async Task<IActionResult> HandlePaymentCallback([FromBody] dynamic callback)
    {
        await checkoutService.HandlePaymentCallbackAsync(callback);
        return Ok(default);
    }
}