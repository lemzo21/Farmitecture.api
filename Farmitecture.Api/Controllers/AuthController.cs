using Farmitecture.Api.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farmitecture.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Implement your login logic here
        return Ok(new { Token = "dummy-jwt-token" });
    }

    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Signup([FromBody] SignupRequest request)
    {
        // Implement your signup logic here
        return Ok(new { Message = "User created successfully" });
    }
}