using System.Threading.Tasks;
using GW2SDK.Infrastructure.Tokens;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Tokens.Fixtures
{
    public class TokenInfoFixture : IAsyncLifetime
    {
        public string JsonTokenInfoObject { get; private set; }

        public async Task InitializeAsync()
        {
            var http = new HttpFixture();

            var service = new TokenInfoJsonService(http.HttpFullAccess);

            var response = await service.GetTokenInfo();
            response.EnsureSuccessStatusCode();
            JsonTokenInfoObject = await response.Content.ReadAsStringAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
