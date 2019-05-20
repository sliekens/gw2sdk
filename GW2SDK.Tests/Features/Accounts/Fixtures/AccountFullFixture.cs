using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Worlds;
using GW2SDK.Infrastructure.Accounts;
using GW2SDK.Infrastructure.Worlds;
using GW2SDK.Tests.Shared.Fixtures;
using Microsoft.Extensions.Http;
using Polly;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Fixtures
{
    public class AccountFullFixture : IAsyncLifetime
    {
        private readonly ConfigurationFixture _configuration = new ConfigurationFixture();

        private readonly HttpClient _http;

        public AccountFullFixture()
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

        public string AccountJsonObjectKnownSchemaFull { get; private set; }

        public string AccountJsonObjectLatestSchemaFull { get; private set; }

        public IReadOnlyList<int> WorldIds { get; private set; }

        public async Task InitializeAsync()
        {
            var service = new AccountJsonService(_http);

            _http.UseAccessToken(_configuration.ApiKeyFull);
            _http.UseSchemaVersion(_configuration.Configuration["KnownSchemaVersion:Account"]);
            AccountJsonObjectKnownSchemaFull = await GetAccountJson();

            _http.UseLatestSchemaVersion();
            AccountJsonObjectLatestSchemaFull = await GetAccountJson();

            var worldService = new WorldService(new WorldJsonService(_http));
            WorldIds = await worldService.GetWorldIds();

            async Task<string> GetAccountJson()
            {
                var response = await service.GetAccount();
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public Task DisposeAsync()
        {
            _http.Dispose();
            return Task.CompletedTask;
        }
    }
}
