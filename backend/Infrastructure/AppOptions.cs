using System.ComponentModel.DataAnnotations;

namespace IdasApp.Infrastructure;

/// <summary>Backend settings; validated on start, so a missing variable does not only show up with the first request.</summary>
public sealed class AppOptions
{
    /// <summary>Public key (RSA XML) of the IDAS environment that issues the tokens; dev and prod differ.</summary>
    [Required(ErrorMessage = "JWT public key missing (IDAS_API_JWT_PUBLIC_KEY)")]
    public string JwtPublicKey { get; set; } = string.Empty;

    /// <summary>Optional: database of the app. Rename the variable for your app, like IDAS_FEEDBACK_DB_CONNECTION_STRING.</summary>
    public string? DbConnectionString { get; set; }

    // Same variable names as the other IDAS microservices (e.g. IDASFeedbackTool)
    public static void Bind(AppOptions options, IConfiguration config)
    {
        options.JwtPublicKey = config["IDAS_API_JWT_PUBLIC_KEY"] ?? string.Empty;
        options.DbConnectionString = config["IDAS_APP_DB_CONNECTION_STRING"];
    }
}

public static class AppOptionsExtensions
{
    public static IServiceCollection AddAppOptions(this IServiceCollection services)
    {
        services.AddOptions<AppOptions>()
            .Configure<IConfiguration>(AppOptions.Bind)
            .ValidateDataAnnotations()
            // placeholder from .env.sample
            .Validate(o => o.JwtPublicKey != "TODO", "JWT public key missing (IDAS_API_JWT_PUBLIC_KEY is still TODO)")
            .ValidateOnStart();
        return services;
    }
}
