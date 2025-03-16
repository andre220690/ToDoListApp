using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Auth.Services;
using ToDoListApp.Core.Models;

namespace ToDoListApp.Api.Controllers;

[EnableCors("CorsPolice")]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User loginUser)
    {
        var response = await _authService.LoginAsync(loginUser);

        if (response.IsAuthorized)
        {
            return Ok(response);
        }

        return Unauthorized();
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] User loginUser)
    {
        var response = await _authService.CreateUserAsync(loginUser);

        if (response.IsAuthorized)
        {
            return Ok(response);
        }

        return Unauthorized();
    }
}
