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
        public string JsonAccount { get; private set; }

        public async Task InitializeAsync()
        {
            var configuration = new ConfigurationFixture();

            var http = new HttpClient()
                .WithBaseAddress(configuration.BaseAddress)
                .WithAccessToken(configuration.ApiKey)
                .WithLatestSchemaVersion();

            var service = new JsonAccountsService(http);

            JsonAccount = await service.GetAccount();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
