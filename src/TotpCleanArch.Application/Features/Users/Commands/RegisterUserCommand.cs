using MediatR;
using TotpCleanArch.Application.Common.Interfaces;

namespace TotpCleanArch.Application.Features.Users.Commands;


public record RegisterUserCommand(string Email, string Password) : IRequest<Guid>;

public class RegisterUserCommandHandler(IAuthService authService)
        : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IAuthService _authService = authService;

    public async Task<Guid> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        return await _authService.RegisterUserAsync(request.Email, request.Password);
    }
}
