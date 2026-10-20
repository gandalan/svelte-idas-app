namespace IdasApp.Tests;

public sealed class StartupTests
{
    [Theory]
    [InlineData("TODO")]
    [InlineData("not a key")]
    [InlineData("<RSAKeyValue><Modulus>abc</Modulus></RSAKeyValue>")]
    public void Start_fails_without_a_valid_public_key(string publicKey)
    {
        using var app = new TestApp { PublicKey = publicKey };

        var error = Assert.ThrowsAny<Exception>(() => app.CreateClient());

        Assert.Contains("IDAS_API_JWT_PUBLIC_KEY", error.ToString());
    }
}
