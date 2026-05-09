using CommonCore.Application;
using CommonCore.Core;
using CommonCore.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CommonCore.API;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection service, WebApplicationBuilder builder)
    {
        var corsConfig = builder.Configuration.GetSection("Cors");
        var policyName = corsConfig["PolicyName"];
        var origins = corsConfig.GetSection("Origins").Get<string[]>();
        service.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CommonCore API", Version = "v1" });
            // Include XML comments
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            // Define the security scheme
            c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "API key header authorization. Example: 'X-API-Key: YOUR_API_KEY'",
                Name = "X-API-Key", // The name of the header you're using
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "ApiKeyScheme" // Can be any string
            });

            // Apply the security requirement globally (for all endpoints)
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            },
                        },
                        new string[]{}
                    }
                });

            //Configure JWT Bearer authentication
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // must be lower case 'bearer'
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[] {} }
                });
        });
        service.AddApplicationDI()
            .AddInfrastructureDI(builder.Configuration)
            .AddCoreDI(builder.Configuration);

        service.AddCors(options =>
        {
            options.AddPolicy(policyName, builder =>
            {
                builder
            .WithOrigins(origins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
            });
        });
        return service;
    }
}
