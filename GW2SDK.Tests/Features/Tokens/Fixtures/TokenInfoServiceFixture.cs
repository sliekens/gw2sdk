using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Subtokens;
using GW2SDK.Tests.Shared;
using Xunit;

namespace GW2SDK.Tests.Features.Tokens.Fixtures
{
    public class TokenInfoServiceFixture : IAsyncLifetime
    {
        public CreatedSubtoken SubtokenBasic { get; private set; }

        public async Task InitializeAsync()
        {
            var http = new Container().Resolve<IHttpClientFactory>().CreateClient("GW2SDK");

            var subtokenService = new SubtokenService(http);

            SubtokenBasic = await subtokenService.CreateSubtoken(ConfigurationManager.Instance.ApiKeyBasic);

            // GetTokenInfo occassionally fails right after the subtoken is created
            // Adding a delay seems to help, possibly because of clock skew?
            await Task.Delay(1000);
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
