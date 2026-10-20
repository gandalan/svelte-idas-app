using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdasApp.Infrastructure;

/// <summary>
/// Validates the IDAS JWT that the frontend sends as Bearer token (from the weblibs auth manager).
/// Same rules as JwtTokenService.ValidationParams in Gandalan.IDAS.Contracts, which is not available on nuget.org.
/// </summary>
public static class IdasAuthentication
{
    public const string Issuer = "https://gandalan.de";

    public static IServiceCollection AddIdasAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
            .Configure<IOptions<AppOptions>>((jwt, options) =>
            {
                // keep the claim names of the token (id, mandantGuid, rights ...) instead of mapping them to URIs
                jwt.MapInboundClaims = false;
                jwt.TokenValidationParameters = ValidationParameters(options.Value.JwtPublicKey);
            });

        // Every endpoint requires a valid token unless it is marked AllowAnonymous (health, static frontend)
        services.AddAuthorizationBuilder()
            .SetFallbackPolicy(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
        return services;
    }

    public static TokenValidationParameters ValidationParameters(string publicKeyXml)
    {
        var rsa = RSA.Create();
        rsa.FromXmlString(publicKeyXml);
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Issuer,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new RsaSecurityKey(rsa),
        };
    }
}
