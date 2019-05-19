using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Accounts.Infrastructure;
using GW2SDK.Features.Worlds;
using GW2SDK.Features.Worlds.Infrastructure;
using GW2SDK.Tests.Shared.Fixtures;
using Microsoft.Extensions.Http;
using Polly;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Fixtures
{
    public class AccountFixture : IAsyncLifetime
    {
        private readonly ConfigurationFixture _configuration = new ConfigurationFixture();

        private readonly HttpClient _http;

        public AccountFixture()
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

        public string JsonAccountObjectLatestSchema { get; private set; }

        public string JsonAccountObjectKnownSchema { get; private set; }

        public DateTimeOffset KnownSchemaVersion { get; private set; }

        public IReadOnlyList<int> WorldIds { get; private set; }

        public async Task InitializeAsync()
        {
            KnownSchemaVersion = DateTimeOffset.Parse(_configuration.Configuration["KnownSchemaVersion:Account"]);

            _http.UseAccessToken(_configuration.ApiKey);
            _http.UseSchemaVersion(KnownSchemaVersion);

            var service = new AccountJsonService(_http);

            (JsonAccountObjectKnownSchema, _) = await service.GetAccount();

            _http.UseLatestSchemaVersion();

            (JsonAccountObjectLatestSchema, _) = await service.GetAccount();

            var worldService = new WorldService(new WorldJsonService(_http));

            WorldIds = await worldService.GetWorldIds();
        }

        public Task DisposeAsync()
        {
            _http.Dispose();
            return Task.CompletedTask;
        }
    }
}
