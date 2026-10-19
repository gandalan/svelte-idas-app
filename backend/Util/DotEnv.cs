public static class DotEnv
{
    public static void Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        foreach (var rawLine in File.ReadAllLines(filePath))
        {
            var line = rawLine.Trim();
            if (line.Length == 0 || line.StartsWith('#'))
            {
                continue;
            }

            // Split only at the first '=' so values like connection strings may contain '='
            var separator = line.IndexOf('=');
            if (separator <= 0)
            {
                continue;
            }

            var key = line[..separator].Trim();
            var value = line[(separator + 1)..].Trim();
            if (value.Length >= 2 && (value[0] == '"' || value[0] == '\'') && value[^1] == value[0])
            {
                value = value[1..^1];
            }

            Environment.SetEnvironmentVariable(key, value);
        }
    }
}
