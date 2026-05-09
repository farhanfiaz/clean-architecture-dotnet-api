using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CommonCore.Infrastructure.Filters;

public class AuthorizeAttribute(IConfiguration configuration) : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token == null)
        {
            context.Result = new JsonResult(new { message = "Unauthorized: JWT token missing..!" }) { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }

        if (token != null)
        {
            var (isValidToken, message) = AttachUserToContext(context.HttpContext, token);
            if (!isValidToken)
            {
                context.Result = new JsonResult(new { message = $"Unauthorized: {message}" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
        }


    }
    private (bool, string) AttachUserToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("jwt")["SecretKey"] ?? string.Empty);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero,
                ValidAudience = configuration.GetSection("jwt")["Isuser"] ?? string.Empty,
                ValidIssuer = configuration.GetSection("jwt")["Isuser"] ?? string.Empty
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var expiryClaim = jwtToken.Claims.First(x => x.Type == "exp").Value;
            var userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (expiryClaim != null && long.TryParse(expiryClaim, out var expiryUnix))
            {
                var expiryDateTime = DateTimeOffset.FromUnixTimeSeconds(expiryUnix).UtcDateTime;
                if (expiryDateTime < DateTime.UtcNow)
                {
                    return (true, "Token expiry.");
                }
            }
            // attach user to context on successful jwt validation
            // context.Items["UserEmail"] = _userManager.FindByIdAsync(userId);
            return (true, "success");
        }
        catch (Exception ex)
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
            return (false, ex.Message);
        }
    }
}
