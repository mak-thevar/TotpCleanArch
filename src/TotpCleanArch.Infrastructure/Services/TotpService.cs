using OtpNet;
using TotpCleanArch.Application.Common.Interfaces;

namespace TotpCleanArch.Infrastructure.Services;

public class TotpService : ITotpService
{
    public string GenerateNewSecretKey()
    {
        // Generate a random 20-byte key and convert it to Base32
        byte[] keyBytes = KeyGeneration.GenerateRandomKey(20);
        return Base32Encoding.ToString(keyBytes);
    }

    public bool VerifyTotpCode(string secretKey, string code)
    {
        // Convert the stored Base32 secret back into bytes
        var keyBytes = Base32Encoding.ToBytes(secretKey);
        var totp = new Totp(keyBytes);

        // Verification allows for a small window of time steps
        return totp.VerifyTotp(
            code,
            out long timeStepMatched,
            new VerificationWindow(previous: 1, future: 0));
    }

    public string GenerateSetupUri(string email, string secretKey, string issuer)
    {
        // The standard TOTP URI format is:
        // otpauth://totp/{issuer}:{email}?secret={secretKey}&issuer={issuer}
        return $"otpauth://totp/{issuer}:{email}?secret={secretKey}&issuer={issuer}";
    }
}