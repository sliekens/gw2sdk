using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure.Worlds;
using GW2SDK.Tests.Shared.Fixtures;
using Microsoft.Extensions.Http;
using Polly;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds.Fixtures
{
    public class WorldFixture : IAsyncLifetime
    {
        private readonly ConfigurationFixture _configuration = new ConfigurationFixture();

        private readonly HttpClient _http;

        public WorldFixture()
        {
            var policy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(3));
            var handler = new PolicyHttpMessageHandler(policy)
            {
                InnerHandler = new SocketsHttpHandler()
            };
            _http = new HttpClient(handler, true)
            {
                BaseAddress = _configuration.BaseAddress
            };
        }

        public string JsonArrayOfWorlds { get; private set; }

        public IListContext ListContext { get; private set; }

        public async Task InitializeAsync()
        {
            _http.UseLatestSchemaVersion();

            var service = new WorldJsonService(_http);

            var response = await service.GetAllWorlds();
            response.EnsureSuccessStatusCode();
            JsonArrayOfWorlds = await response.Content.ReadAsStringAsync();
            ListContext = response.Headers.GetListContext();
        }

        public Task DisposeAsync()
        {
            _http.Dispose();
            return Task.CompletedTask;
        }
    }
}
