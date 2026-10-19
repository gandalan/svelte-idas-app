using IdasApp.Util;

namespace IdasApp.Tests;

public sealed class DotEnvTests : IDisposable
{
    private readonly string _file = Path.GetTempFileName();
    private readonly string _prefix = $"DOTENV_TEST_{Guid.NewGuid():N}_";

    [Fact]
    public void Reads_values_containing_equals_signs_comments_and_quotes()
    {
        File.WriteAllLines(_file,
        [
            "# comment",
            "",
            $"{_prefix}KEY=<RSAKeyValue><Modulus>abc==</Modulus></RSAKeyValue>",
            $"{_prefix}QUOTED=\"Server=x;Database=y\"",
            "no separator",
        ]);

        DotEnv.Load(_file);

        Assert.Equal("<RSAKeyValue><Modulus>abc==</Modulus></RSAKeyValue>", Environment.GetEnvironmentVariable($"{_prefix}KEY"));
        Assert.Equal("Server=x;Database=y", Environment.GetEnvironmentVariable($"{_prefix}QUOTED"));
    }

    [Fact]
    public void Existing_environment_variables_win()
    {
        Environment.SetEnvironmentVariable($"{_prefix}SET", "from environment");
        File.WriteAllLines(_file, [$"{_prefix}SET=from file"]);

        DotEnv.Load(_file);

        Assert.Equal("from environment", Environment.GetEnvironmentVariable($"{_prefix}SET"));
    }

    [Fact]
    public void Missing_file_is_ignored()
    {
        DotEnv.Load(Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.env"));
    }

    public void Dispose()
    {
        File.Delete(_file);
        foreach (var name in new[] { "KEY", "QUOTED", "SET" })
        {
            Environment.SetEnvironmentVariable(_prefix + name, null);
        }
    }
}
