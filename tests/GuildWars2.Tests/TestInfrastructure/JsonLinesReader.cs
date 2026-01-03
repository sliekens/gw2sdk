using System.IO.Compression;

namespace GuildWars2.Tests.TestInfrastructure;

public static class JsonLinesReader
{
    public static IEnumerable<string> Read(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("Path cannot be null or empty.", nameof(path));
        }

        using Stream file = Path.GetExtension(path) == ".gz"
            ? new GZipStream(
                File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None),
                CompressionMode.Decompress
            )
            : File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None);

        using StreamReader stringReader = new(file);
        while (stringReader.ReadLine() is { Length: > 0 } line)
        {
            yield return line;
        }
    }
}
