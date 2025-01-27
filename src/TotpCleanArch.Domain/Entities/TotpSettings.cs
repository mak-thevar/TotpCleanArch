namespace TotpCleanArch.Domain.Entities;

public class TotpSettings
{
    public Guid Id { get; set; }

    // Unique secret key for TOTP generation
    public string SecretKey { get; set; } = string.Empty;

    // Reference to the user
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
}