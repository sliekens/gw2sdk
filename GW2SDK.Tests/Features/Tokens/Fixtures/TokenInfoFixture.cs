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

            using (var request = new GetTokenInfoRequest())
            using (var response = await http.HttpFullAccess.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                JsonTokenInfoObject = await response.Content.ReadAsStringAsync();
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
