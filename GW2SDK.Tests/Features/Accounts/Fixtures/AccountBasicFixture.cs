using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Worlds;
using GW2SDK.Infrastructure.Accounts;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Fixtures
{
    public class AccountBasicFixture : IAsyncLifetime
    {
        private readonly ConfigurationFixture _configuration = new ConfigurationFixture();

        public string AccountJsonObjectKnownSchemaBasic { get; private set; }

        public string AccountJsonObjectLatestSchemaBasic { get; private set; }

        public IReadOnlyList<int> WorldIds { get; private set; }

        public async Task InitializeAsync()
        {
            var http = new HttpFixture();

            using (var request = new GetAccountRequest())
            {
                http.HttpBasicAccess.UseSchemaVersion(_configuration.Configuration["KnownSchemaVersion:Account"]);
                using (var response = await http.HttpBasicAccess.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    AccountJsonObjectKnownSchemaBasic = await response.Content.ReadAsStringAsync();
                }
            }

            using (var request = new GetAccountRequest())
            {
                http.HttpBasicAccess.UseLatestSchemaVersion();
                using (var response = await http.HttpBasicAccess.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    AccountJsonObjectLatestSchemaBasic = await response.Content.ReadAsStringAsync();
                }
            }

            var worldService = new WorldService(http.Http);
            WorldIds = await worldService.GetWorldIds();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
