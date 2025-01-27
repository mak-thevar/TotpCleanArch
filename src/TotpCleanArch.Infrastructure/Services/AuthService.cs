using Microsoft.EntityFrameworkCore;
using TotpCleanArch.Application.Common.Interfaces;
using TotpCleanArch.Application.Features.Users.Models;
using TotpCleanArch.Domain.Entities;
using TotpCleanArch.Infrastructure.Persistence;

namespace TotpCleanArch.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly ITotpService _totpService;

    public AuthService(ApplicationDbContext context, ITotpService totpService)
    {
        _context = context;
        this._totpService = totpService;
    }

    public async Task<Guid> RegisterUserAsync(string email, string password)
    {
        // Hash password in real scenario
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = password,
            IsTotpEnabled = false
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        // Basic check for user
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            throw new Exception("User not found.");

        // Password check in real scenario
        if (user.PasswordHash != password)
            throw new Exception("Invalid password.");

        // If TOTP is enabled, we can return partial or an indicator
        if (user.IsTotpEnabled)
            throw new Exception("TOTP required.");

        // If TOTP not required, generate a token or session string
        return "BasicLoginTokenOrJwt";
    }

    public async Task<string> LoginWithTotpAsync(string email, string totpCode)
    {
        var user = await _context.Users
            .Include(u => u.TotpSettings)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            throw new Exception("User not found.");

        if (!user.IsTotpEnabled || user.TotpSettings == null)
            throw new Exception("TOTP is not enabled for this account.");

        if (!_totpService.VerifyTotpCode(user.TotpSettings.SecretKey, totpCode))
            throw new Exception("Invalid or expired TOTP code.");

        // Return JWT or session token upon successful verification
        return "TotpEnabledTokenOrJwt";
    }

    public async Task SaveTotpSecretKeyAsync(string email, string secretKey)
    {
        var user = await _context.Users
            .Include(u => u.TotpSettings)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            throw new Exception("User not found.");

        if (user.TotpSettings == null)
        {
            var totpSettings =  new TotpSettings
            {
                Id = Guid.NewGuid(),
                SecretKey = secretKey,
            };
            user.TotpSettings = totpSettings;
            _context.TotpSettings.Add(totpSettings);
        }
        else
        {
            _context.Entry(user.TotpSettings).State = EntityState.Modified;
            user.TotpSettings.SecretKey = secretKey;
        }

        // Keep it disabled until verified
        user.IsTotpEnabled = false;

        await _context.SaveChangesAsync();
    }

    public async Task<UserDto?> FindUserDtoByEmailAsync(string email)
    {
        var user = await _context.Users
            .Include(u => u.TotpSettings)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            IsTotpEnabled = user.IsTotpEnabled,
            SecretKey = user.TotpSettings?.SecretKey // if needed
        };
    }

    public async Task ToggleTotpEnabledAsync(string email, bool isEnabled)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            throw new Exception("User not found.");

        user.IsTotpEnabled = isEnabled;

        await _context.SaveChangesAsync();
    }
}