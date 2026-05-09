using CommonCore.API;
using CommonCore.API.Filters;
using Serilog;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddDependencyInjection(builder);

builder.Services.AddRateLimiter(options =>
{
   options.GlobalLimiter =
        PartitionedRateLimiter.Create<HttpContext, string>(
            context =>
            {
                var ip = context.Connection.RemoteIpAddress?
                    .ToString() ?? "unknown";

                return RateLimitPartition.GetSlidingWindowLimiter(
                    partitionKey: ip,
                    factory: _ => new SlidingWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromMinutes(1),
                        SegmentsPerWindow = 6,
                        //QueueProcessingOrder =
                        //    QueueProcessingOrder.OldestFirst,
                        //QueueLimit = 2
                        QueueLimit = 0
                    });
            });
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.Headers.RetryAfter = "60";

        var logger =
        context.HttpContext.RequestServices
            .GetRequiredService<ILogger<Program>>();

        logger.LogWarning(
            "Rate limit exceeded for IP {IP}",
            context.HttpContext.Connection.RemoteIpAddress);

        context.HttpContext.Response.StatusCode = 429;

        await context.HttpContext.Response.WriteAsync(
            "Too many requests.",
            token);
    };
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseMiddleware<CustomExceptionHandling>();

app.UseHttpsRedirection();

app.UseCors(builder.Configuration["Cors:PolicyName"]);

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.MapControllers();

app.Run();
