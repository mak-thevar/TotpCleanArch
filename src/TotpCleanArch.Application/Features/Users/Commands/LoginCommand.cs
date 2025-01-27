using MediatR;
using TotpCleanArch.Application.Common.Interfaces;

namespace TotpCleanArch.Application.Features.Users.Commands;

public record LoginCommand(string Email, string Password) : IRequest<string>;

public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, string>
{
    private readonly IAuthService _authService = authService;

    public async Task<string> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        // This login will only succeed if TOTP is not required
        // If TOTP is active, user should go through a separate TOTP route
        return await _authService.LoginAsync(request.Email, request.Password);
    }
}

