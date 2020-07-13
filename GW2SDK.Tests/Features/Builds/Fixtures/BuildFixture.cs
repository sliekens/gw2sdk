using System;
using System.IO;
using System.Text.Json;

namespace GW2SDK.Tests.Features.Builds.Fixtures
{
    public class BuildFixture : IDisposable
    {
        public BuildFixture()
        {
            Build = JsonDocument.Parse(File.ReadAllText("Data/build.json"));
        }

        public JsonDocument Build { get; }

        public void Dispose() => Build.Dispose();
    }
}
