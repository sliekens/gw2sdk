using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GW2SDK.Tests.TestInfrastructure
{
    public sealed class STJsonFlatFileReader
    {
        public IEnumerable<JsonDocument> Read(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", nameof(path));
            }

            using var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None);
            using var stringReader = new StreamReader(file);
            string line;
            while ((line = stringReader.ReadLine()) is string)
            {
                using var json = JsonDocument.Parse(line);
                yield return json.Indent();
            }
        }
    }
}
