using MediatR;
using TotpCleanArch.Application.Common.Interfaces;

namespace TotpCleanArch.Application.Features.TOTP.Commands;

public record VerifyTotpCommand(string Email, string Code) : IRequest<bool>;

public class VerifyTotpCommandHandler(
    ITotpService totpService,
    IAuthService authService)
        : IRequestHandler<VerifyTotpCommand, bool>
{
    private readonly ITotpService _totpService = totpService;
    private readonly IAuthService _authService = authService;

    public async Task<bool> Handle(
        VerifyTotpCommand request,
        CancellationToken cancellationToken)
    {

        var userDto = await _authService.FindUserDtoByEmailAsync(request.Email);
        if (userDto == null || string.IsNullOrEmpty(userDto.SecretKey))
            return false;


        var isValid = _totpService.VerifyTotpCode(userDto.SecretKey, request.Code);
        if (!isValid) return false;

        return true;

    }
}
