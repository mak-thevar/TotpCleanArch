namespace TotpCleanArch.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    // This property indicates if TOTP MFA is enabled
    public bool IsTotpEnabled { get; set; }

    // Navigation property for TOTP details
    public virtual TotpSettings? TotpSettings { get; set; }
}
