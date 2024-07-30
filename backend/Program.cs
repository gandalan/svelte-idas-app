using Microsoft.Extensions.Diagnostics.HealthChecks;
using SvelteDotnetAppVite.Data;

var root = Directory.GetCurrentDirectory();
var dotEnv = Path.Combine(root, ".env");
DotEnv.Load(dotEnv);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHealthChecks().AddCheck("Basic Healthcheck", () => HealthCheckResult.Healthy("Healthy"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(options =>
{
    options.AllowAnyMethod().AllowAnyHeader();
    options.SetIsOriginAllowed((host) => true);
    options.AllowCredentials();
});

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");
app.MapFallbackToFile("index.html").AllowAnonymous();
app.MapHealthChecks("/health").AllowAnonymous();

// Data
app.MapCounterEndpoint();

app.Run();
