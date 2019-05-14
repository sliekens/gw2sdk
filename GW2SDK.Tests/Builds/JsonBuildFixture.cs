using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Builds;
using GW2SDK.Builds.Infrastructure;
using Xunit;

namespace GW2SDK.Tests.Builds
{
    public class BuildServiceFixture : IAsyncLifetime
    {
        public Build Build { get; private set; }

        public async Task InitializeAsync()
        {
            var buildService = new BuildService(new JsonBuildService(new HttpClient
            {
                BaseAddress = new Uri("https://api.guildwars2.com", UriKind.Absolute)
            }));

            Build = await buildService.GetBuildAsync();
        }

        public Task DisposeAsync()
        {
            // Nothing to do here
            return Task.CompletedTask;
        }
    }
}