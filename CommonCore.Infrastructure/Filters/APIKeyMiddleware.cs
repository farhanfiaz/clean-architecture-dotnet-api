using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Infrastructure.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class APIKeyMiddleware(IConfiguration configuration)
: Attribute, IAuthorizationFilter
{
    private const string API_KEY_HEADER_NAME = "X-API-Key";
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var submittedApiKey = GetSubmittedApiKey(context.HttpContext);

        if (string.IsNullOrWhiteSpace(submittedApiKey))
        {
            context.Result = new JsonResult(new { message = "Api Key missing..!" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        else
        {
            var apiKey = GetApiKey(context.HttpContext);

            if (!IsApiKeyValid(apiKey, submittedApiKey))
            {
                context.Result = new JsonResult(new { message = "Api Key inValid..!" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
    private static string GetSubmittedApiKey(HttpContext context)
    {
        return context.Request?.Headers[API_KEY_HEADER_NAME];
    }
    private string GetApiKey(HttpContext context)
    {
        return configuration.GetSection("AppSettings:ApiKey").Value ?? string.Empty;
    }

    private static bool IsApiKeyValid(string apiKey, string submittedApiKey)
    {
        if (string.IsNullOrEmpty(submittedApiKey)) return false;
        // api secret key match
        return apiKey.Equals(submittedApiKey);
    }
}
