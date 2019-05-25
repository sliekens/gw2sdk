using System.Net.Http;
using GW2SDK.Extensions;
using GW2SDK.Features.Subtokens;
using GW2SDK.Features.Tokens;
using GW2SDK.Infrastructure.Subtokens;
using GW2SDK.Infrastructure.Tokens;
using GW2SDK.Tests.Shared.Fixtures;

namespace GW2SDK.Tests.Features.Subtokens.Fixtures
{
    public class SubtokenFixture
    {
        public HttpClient Http { get; }

        public SubtokenFixture()
        {
            var configuration = new ConfigurationFixture();

            Http = new HttpClient
            {
                BaseAddress = configuration.BaseAddress
            };
            Http.UseLatestSchemaVersion();

            SubtokenService = new SubtokenService(new SubtokenJsonService(Http));

            TokenInfoService = new TokenInfoService(new TokenInfoJsonService(Http));
        }

        public SubtokenService SubtokenService { get; }

        public TokenInfoService TokenInfoService { get; }
    }
}
