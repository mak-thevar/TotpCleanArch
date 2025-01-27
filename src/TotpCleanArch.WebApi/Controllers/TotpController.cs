using MediatR;
using Microsoft.AspNetCore.Mvc;
using TotpCleanArch.Application.Common;
using TotpCleanArch.Application.Common.Interfaces;
using TotpCleanArch.Application.Features.TOTP.Commands;
using TotpCleanArch.Application.Features.TOTP.Queries;

namespace TotpCleanArch.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TotpController(
    ITotpService totpService,
    IQrCodeService qrCodeService,
    IAuthService authService,
    IMediator mediator) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly ITotpService _totpService = totpService;
    private readonly IQrCodeService _qrCodeService = qrCodeService;
    private readonly IMediator _mediator = mediator;

    [HttpPost("setup")]
    public async Task<IActionResult> Setup([FromBody] SetUpTotpCommand command)
    {
        // This command returns the otpauth:// URI
        var otpauthUri = await _mediator.Send(command);
        return Ok(new { SetupUri = otpauthUri });
    }

    [HttpPost("verify")]
    public async Task<IActionResult> Verify([FromBody] VerifyTotpCommand command)
    {
        var isValid = await _mediator.Send(command);
        if(!isValid)
        {
            return BadRequest("Invalid TOTP code");
        }
        await _authService.ToggleTotpEnabledAsync(command.Email, true);

        return Ok("TOTP verified successfully");
    }

    // Example: GET /api/totp/qrcode?email=user@domain.com
    [HttpGet("qrcode")]
    public async Task<IActionResult> GenerateQrCode([FromQuery] string email)
    {

        var userSecretKey = await _mediator.Send(new GetTotpSecretKeyQuery(email));

        if (string.IsNullOrWhiteSpace(userSecretKey))
        {
            return BadRequest("No TOTP key found for this user.");
        }

       
        var otpauthUri = _totpService.GenerateSetupUri(email, userSecretKey, AppConstants.TotpIssuer);

        var qrBytes = _qrCodeService.GenerateQrCode(otpauthUri);


        //Return the QR code as image
        return File(qrBytes, "image/png");
    }
}