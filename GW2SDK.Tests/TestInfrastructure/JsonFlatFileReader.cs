using System;
using System.Collections.Generic;
using System.IO;
using GW2SDK.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.TestInfrastructure
{
    public sealed class JsonFlatFileReader
    {
        public IEnumerable<string> Read([NotNull] string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", nameof(path));
            }

            using (var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None))
            using (var stringReader = new StreamReader(file))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                jsonReader.SupportMultipleContent = true;
                while (jsonReader.Read())
                {
                    yield return JToken.ReadFrom(jsonReader).ToString(Formatting.Indented);
                }
            }
        }
    }
}
