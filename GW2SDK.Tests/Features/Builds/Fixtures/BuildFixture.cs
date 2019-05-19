using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Builds.Infrastructure;
using GW2SDK.Tests.Shared.Fixtures;
using Microsoft.Extensions.Http;
using Polly;
using Xunit;

namespace GW2SDK.Tests.Features.Builds.Fixtures
{
    public class BuildFixture : IAsyncLifetime
    {
        private readonly HttpClient _http;

        public BuildFixture()
        {
            var httpPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(3));
            var messageHandler = new PolicyHttpMessageHandler(httpPolicy)
            {
                InnerHandler = new SocketsHttpHandler()
            };
            _http = new HttpClient(messageHandler, true);
        }

        public string JsonBuildObject { get; set; }

        public async Task InitializeAsync()
        {
            var configuration = new ConfigurationFixture();

            _http.WithBaseAddress(configuration.BaseAddress)
                .WithLatestSchemaVersion();

            var service = new JsonBuildService(_http);

            JsonBuildObject = await service.GetBuild();
        }

        public Task DisposeAsync()
        {
            _http.Dispose();
            return Task.CompletedTask;
        }
    }
}
