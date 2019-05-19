using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Accounts.Infrastructure;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Fixtures
{
    public class AccountFixture : IAsyncLifetime
    {
        public string JsonAccountObjectLatestSchema { get; private set; }

        public string JsonAccountObjectKnownSchema { get; private set; }

        public DateTimeOffset KnownSchemaVersion { get; private set; }

        public async Task InitializeAsync()
        {
            var configuration = new ConfigurationFixture();

            KnownSchemaVersion = DateTimeOffset.Parse(configuration.Configuration["KnownSchemaVersion:Account"]);

            var http = new HttpClient()
                .WithBaseAddress(configuration.BaseAddress)
                .WithAccessToken(configuration.ApiKey)
                .WithSchemaVersion(KnownSchemaVersion);

            var service = new JsonAccountsService(http);

            JsonAccountObjectKnownSchema = await service.GetAccount();

            http.UseLatestSchemaVersion();

            JsonAccountObjectLatestSchema = await service.GetAccount();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
