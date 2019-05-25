using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Worlds;
using GW2SDK.Infrastructure.Accounts;
using GW2SDK.Infrastructure.Worlds;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Fixtures
{
    public class AccountFullFixture : IAsyncLifetime
    {
        private readonly ConfigurationFixture _configuration = new ConfigurationFixture();

        public string AccountJsonObjectKnownSchemaFull { get; private set; }

        public string AccountJsonObjectLatestSchemaFull { get; private set; }

        public IReadOnlyList<int> WorldIds { get; private set; }

        public async Task InitializeAsync()
        {
            var http = new HttpFixture();
            var service = new AccountJsonService(http.HttpFullAccess);

            http.HttpFullAccess.UseSchemaVersion(_configuration.Configuration["KnownSchemaVersion:Account"]);
            AccountJsonObjectKnownSchemaFull = await GetAccountJson();

            http.HttpFullAccess.UseLatestSchemaVersion();
            AccountJsonObjectLatestSchemaFull = await GetAccountJson();

            var worldService = new WorldService(new WorldJsonService(http.Http));
            WorldIds = await worldService.GetWorldIds();

            async Task<string> GetAccountJson()
            {
                var response = await service.GetAccount();
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
