using CommonCore.Core.Entities;
using CommonCore.Core.Interfaces;
using CommonCore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CommonCore.Infrastructure.RegisterServices;

internal static class RegisterServices
{
    public static IServiceCollection RegisterService(this IServiceCollection service)
    {
        service.AddScoped<IAuthRepository, AuthRepository>();
        service.AddScoped<IJwtService, JwtService>();
        service.AddTransient<IPasswordHasher<AspNetUser>, PasswordHasher<AspNetUser>>();
        service.AddTransient<IAuthTokenRepository, AuthTokenRepository>();
        service.AddTransient<IFireBaseRepository, FireBaseRepository>();
        service.Scan(scan => scan
                    .FromAssemblyOf<IAuthRepository>()
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        service.Scan(scan => scan
                    .FromAssemblyOf<IJwtService>()
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        return service;
    }
}
