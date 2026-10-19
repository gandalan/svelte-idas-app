using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace IdasApp.Tests;

public sealed class ApiTests(TestApp app) : IClassFixture<TestApp>
{
    private HttpClient ClientWithToken(string token)
    {
        var client = app.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    [Fact]
    public async Task Health_is_public()
    {
        var response = await app.CreateClient().GetAsync("/health", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Api_requires_a_token()
    {
        var response = await app.CreateClient().GetAsync("/api/counter", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Api_accepts_a_valid_idas_token()
    {
        var response = await ClientWithToken(app.CreateToken()).GetAsync("/api/counter", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Api_rejects_a_token_of_another_issuer()
    {
        var response = await ClientWithToken(app.CreateToken(issuer: "https://example.com")).GetAsync("/api/counter", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Api_rejects_an_expired_token()
    {
        var response = await ClientWithToken(app.CreateToken(expires: DateTime.UtcNow.AddMinutes(-10))).GetAsync("/api/counter", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Api_rejects_a_token_signed_with_another_key()
    {
        using var otherKey = RSA.Create(2048);

        var response = await ClientWithToken(app.CreateToken(signingKey: otherKey)).GetAsync("/api/counter", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Cors_preflight_from_an_embedding_host_is_allowed()
    {
        using var request = new HttpRequestMessage(HttpMethod.Options, "/api/counter");
        request.Headers.Add("Origin", "https://host.example.com");
        request.Headers.Add("Access-Control-Request-Method", "GET");
        request.Headers.Add("Access-Control-Request-Headers", "authorization");

        var response = await app.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal("*", response.Headers.GetValues("Access-Control-Allow-Origin").Single());
        Assert.False(response.Headers.Contains("Access-Control-Allow-Credentials"));
    }
}
