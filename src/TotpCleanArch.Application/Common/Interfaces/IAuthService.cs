using TotpCleanArch.Application.Features.Users.Models;

namespace TotpCleanArch.Application.Common.Interfaces;
public interface IAuthService
{
    // Handles registration
    Task<Guid> RegisterUserAsync(string email, string password);

    // Handles basic login (checks password, might return JWT or session)
    Task<string> LoginAsync(string email, string password);

    // Handles login when TOTP is enabled (requires code)
    Task<string> LoginWithTotpAsync(string email, string totpCode);

    // For storing new secret keys
    Task SaveTotpSecretKeyAsync(string email, string secretKey);

    // For retrieving user by email (optional convenience)
    Task<UserDto?> FindUserDtoByEmailAsync(string email);

    // To toggle TOTP enabled for a user
    Task ToggleTotpEnabledAsync(string email, bool isEnabled);

}
