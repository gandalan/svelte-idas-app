using IdasApp.Data;
using IdasApp.Infrastructure;
using IdasApp.Util;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Diagnostics.HealthChecks;

DotEnv.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppOptions();
builder.Services.AddIdasAuthentication();
builder.Services.AddProblemDetails();

// The app is embedded into other applications (Shadow DOM / iframe) on other hosts,
// so the API must be callable cross-origin. Auth uses a Bearer token header, not cookies,
// so credentials are not needed. Restrict origins via Cors:AllowedOrigins (e.g. env Cors__AllowedOrigins__0).
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    if (allowedOrigins.Length == 0 || allowedOrigins.Contains("*"))
    {
        policy.AllowAnyOrigin();
    }
    else
    {
        policy.WithOrigins(allowedOrigins);
    }

    policy.AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.AddHealthChecks().AddCheck("Basic Healthcheck", () => HealthCheckResult.Healthy("Healthy"));

var app = builder.Build();

app.UseResponseCompression();
if (!app.Environment.IsDevelopment())
{
    // unexpected errors as ProblemDetails without stack trace
    app.UseExceptionHandler();
}
app.UseCors();

// The frontend (wwwroot) is public; the IDAS login happens in the browser
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health").AllowAnonymous();

// Data
var api = app.MapGroup("/api");
api.MapCounterEndpoint();

app.MapFallbackToFile("index.html").AllowAnonymous();

app.Run();

// for WebApplicationFactory<Program> in the tests
public partial class Program;
