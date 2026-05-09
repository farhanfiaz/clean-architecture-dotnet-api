using CommonCore.InterfaceAdapters.Dtos;
using System.Net;
using System.Text.Json;

namespace CommonCore.API.Filters;

public class CustomExceptionHandling
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandling> _logger;

    public CustomExceptionHandling(RequestDelegate next, ILogger<CustomExceptionHandling> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            string errorMessage = $"Error: {ex.Message} \n";
            if (ex.InnerException != null)
            {
                errorMessage += $" Inner Exception: {ex.InnerException.Message}";
            }
            // Serialize the ApiResult object to JSON
            var jsonResponse = JsonSerializer.Serialize(ApiResult.CustomException(errorMessage));
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
