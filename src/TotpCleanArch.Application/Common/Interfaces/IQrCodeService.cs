namespace TotpCleanArch.Application.Common.Interfaces;

public interface IQrCodeService
{
    /// <summary>
    /// Takes the otpauth URI and returns a PNG image as a byte array.
    /// </summary>
    byte[] GenerateQrCode(string content);
}