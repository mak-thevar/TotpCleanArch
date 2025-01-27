using MediatR;
using TotpCleanArch.Application.Common;
using TotpCleanArch.Application.Common.Interfaces;

namespace TotpCleanArch.Application.Features.TOTP.Commands;

public record SetUpTotpCommand(string Email) : IRequest<string>;

public class SetUpTotpCommandHandler(
    ITotpService totpService,
    IAuthService authService) : IRequestHandler<SetUpTotpCommand, string>
{
    private readonly ITotpService _totpService = totpService;
    private readonly IAuthService _authService = authService; // Might store secret in DB

    public async Task<string> Handle(
        SetUpTotpCommand request,
        CancellationToken cancellationToken)
    {
        var userDto = await _authService.FindUserDtoByEmailAsync(request.Email);

        //if totp is already enabled, return error
        if (userDto.IsTotpEnabled)
            throw new Exception("TOTP already enabled.");

        // Generate a fresh secret key
        var secretKey = _totpService.GenerateNewSecretKey();

        // Store secret key in DB but keep TOTP disabled until verification
        await _authService.SaveTotpSecretKeyAsync(request.Email, secretKey);

        var uri = _totpService.GenerateSetupUri(request.Email, secretKey, AppConstants.TotpIssuer);

        return uri;
    }
}