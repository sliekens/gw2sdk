using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Tokens.Infrastructure;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Tokens.Fixtures
{
    public class TokenInfoFixture : IAsyncLifetime
    {
        public string JsonTokenInfoObject { get; private set; }

        public async Task InitializeAsync()
        {
            var configuration = new ConfigurationFixture();

            var http = new HttpClient()
                .WithBaseAddress(configuration.BaseAddress)
                .WithAccessToken(configuration.ApiKey)
                .WithLatestSchemaVersion();

            var service = new JsonTokenInfoService(http);

            JsonTokenInfoObject = await service.GetTokenInfo();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
