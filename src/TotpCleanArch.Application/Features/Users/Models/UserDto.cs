namespace TotpCleanArch.Application.Features.Users.Models;

public record UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsTotpEnabled { get; set; }
    public string? SecretKey { get; set; }
}