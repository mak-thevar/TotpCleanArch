namespace TotpCleanArch.Application.Common.Interfaces;

public interface ITotpService
{
    /// <summary>
    /// Produces a new secret key (Base32-encoded) for the TOTP process.
    /// </summary>
    string GenerateNewSecretKey();

    /// <summary>
    /// Verifies a user-supplied TOTP code against the stored secret key.
    /// </summary>
    bool VerifyTotpCode(string secretKey, string code);

    /// <summary>
    /// Generates the otpauth:// URI that can be embedded in a QR code.
    /// </summary>
    string GenerateSetupUri(string email, string secretKey, string issuer);
}