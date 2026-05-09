using CommonCore.Core.Entities;
using CommonCore.Core.Options;
using CommonCore.Infrastructure.Data;
using CommonCore.Infrastructure.Filters;
using CommonCore.Infrastructure.RegisterServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection service, IConfiguration _iConfiguration)
    {
        service.AddDbContext<CommonCoreDbContext>((provider, opt) =>
        {
            opt.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.DefaultConnection);
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        // Configure JWT Authentication
        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _iConfiguration["JwtSettings:Issuer"],
                    ValidAudience = _iConfiguration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfiguration["JwtSettings:SecretKey"]))
                };
            });

        service.AddControllers(opts => {
            opts.Filters.Add<APIKeyMiddleware>();
            opts.Filters.Add<AuthorizeAttribute>();
        });

        service.AddIdentity<AspNetUser, AspNetRole>(options =>
        {
            options.Password.RequireDigit = false; // Disable digit requirement
            options.Password.RequiredLength = 6;   // Set minimum length to 6
            options.Password.RequireNonAlphanumeric = false; // Disable non-alphanumeric requirement
            options.Password.RequireUppercase = false; // Disable uppercase letter requirement
            options.Password.RequireLowercase = false; // Disable lowercase letter requirement
        }).AddEntityFrameworkStores<CommonCoreDbContext>().AddDefaultTokenProviders();

        service.RegisterService();

        return service;
    }
}
