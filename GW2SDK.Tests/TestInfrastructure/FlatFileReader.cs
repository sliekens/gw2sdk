using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace GW2SDK.Tests.TestInfrastructure;

public static class FlatFileReader
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

        using var stringReader = new StreamReader(file);
        string line;
        while ((line = stringReader.ReadLine()) is not null)
        {
            yield return line;
        }
    }
}
