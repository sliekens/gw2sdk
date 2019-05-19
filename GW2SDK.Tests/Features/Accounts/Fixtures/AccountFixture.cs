using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Accounts.Infrastructure;
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

        public async Task InitializeAsync()
        {
            KnownSchemaVersion = DateTimeOffset.Parse(_configuration.Configuration["KnownSchemaVersion:Account"]);

            _http.UseAccessToken(_configuration.ApiKey);
            _http.UseSchemaVersion(KnownSchemaVersion);

            var service = new JsonAccountsService(_http);

            JsonAccountObjectKnownSchema = await service.GetAccount();

            _http.UseLatestSchemaVersion();

            JsonAccountObjectLatestSchema = await service.GetAccount();
        }

        public Task DisposeAsync()
        {
            _http.Dispose();
            return Task.CompletedTask;
        }
    }
}
