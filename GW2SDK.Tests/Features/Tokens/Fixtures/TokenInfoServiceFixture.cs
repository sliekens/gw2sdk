using System.Threading.Tasks;
using GW2SDK.Subtokens;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Tokens.Fixtures
{
    public class TokenInfoServiceFixture : IAsyncLifetime
    {
        public CreatedSubtoken SubtokenBasic { get; private set; }

        public async Task InitializeAsync()
        {
            await using var services = new Composer();

            var subtokenService = services.Resolve<SubtokenService>();

            var result = await subtokenService.CreateSubtoken(ConfigurationManager.Instance.ApiKeyBasic);

            SubtokenBasic = result.Value;
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
