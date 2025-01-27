using MediatR;
using TotpCleanArch.Application.Common.Interfaces;

namespace TotpCleanArch.Application.Features.TOTP.Queries;

public record GetTotpSecretKeyQuery(string Email) : IRequest<string>;


public class GetTotpSecretKeyQueryHandler(IAuthService authService) : IRequestHandler<GetTotpSecretKeyQuery, string>
{
    private readonly IAuthService _authService = authService;

    public async Task<string> Handle(
        GetTotpSecretKeyQuery request,
        CancellationToken cancellationToken)
    {
        var userDto = await _authService.FindUserDtoByEmailAsync(request.Email);
        return userDto?.SecretKey ?? string.Empty;
    }
}