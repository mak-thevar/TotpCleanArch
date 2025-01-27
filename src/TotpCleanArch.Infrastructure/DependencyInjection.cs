using Microsoft.Extensions.DependencyInjection;
using TotpCleanArch.Application.Common.Interfaces;
using TotpCleanArch.Infrastructure.Services;

namespace TotpCleanArch.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // 3. Register services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITotpService, TotpService>();
        services.AddScoped<IQrCodeService, QrCodeService>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IAuthService>());


        return services;
    }
}
