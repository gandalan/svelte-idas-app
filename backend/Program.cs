using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SvelteDotnetAppVite.Data;

var root = Directory.GetCurrentDirectory();
var dotEnv = Path.Combine(root, ".env");
DotEnv.Load(dotEnv);

var builder = WebApplication.CreateBuilder(args);

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
app.UseCors();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapHealthChecks("/health").AllowAnonymous();

// Data
app.MapCounterEndpoint();

app.MapFallbackToFile("index.html").AllowAnonymous();

app.Run();
