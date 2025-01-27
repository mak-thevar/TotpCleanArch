using MediatR;
using Microsoft.AspNetCore.Mvc;
using TotpCleanArch.Application.Common.Interfaces;
using TotpCleanArch.Application.Features.TOTP.Commands;
using TotpCleanArch.Application.Features.Users.Commands;

namespace TotpCleanArch.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator, IAuthService authService) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return Ok(new { UserId = userId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(new { Token = token });
    }

    [HttpPost("login-with-totp")]
    public async Task<IActionResult> LoginWithTotp([FromBody] VerifyTotpCommand command)
    {
        var result =  await _authService.LoginWithTotpAsync(command.Email, command.Code);
        return Ok(new { Token = result });
    }
}