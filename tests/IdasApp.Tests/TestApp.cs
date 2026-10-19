using System.Security.Claims;
using System.Security.Cryptography;
using IdasApp.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace IdasApp.Tests;

/// <summary>Backend with its own test key pair instead of the IDAS key, so tests can issue valid tokens.</summary>
public sealed class TestApp : WebApplicationFactory<Program>
{
    private readonly RSA _key = RSA.Create(2048);

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureAppConfiguration((_, config) => config.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["IDAS_API_JWT_PUBLIC_KEY"] = _key.ToXmlString(includePrivateParameters: false),
        }));
    }

    /// <summary>Token like IDAS issues it; parameters allow building invalid ones.</summary>
    public string CreateToken(string issuer = IdasAuthentication.Issuer, DateTime? expires = null, RSA? signingKey = null)
    {
        var expiresAt = expires ?? DateTime.UtcNow.AddMinutes(10);
        return new JsonWebTokenHandler().CreateToken(new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Subject = new ClaimsIdentity([new Claim("id", "test.user"), new Claim("mandantGuid", Guid.NewGuid().ToString())]),
            NotBefore = expiresAt.AddMinutes(-20),
            IssuedAt = expiresAt.AddMinutes(-20),
            Expires = expiresAt,
            SigningCredentials = new SigningCredentials(new RsaSecurityKey(signingKey ?? _key), SecurityAlgorithms.RsaSha256),
        });
    }

    protected override void Dispose(bool disposing)
    {
        _key.Dispose();
        base.Dispose(disposing);
    }
}
