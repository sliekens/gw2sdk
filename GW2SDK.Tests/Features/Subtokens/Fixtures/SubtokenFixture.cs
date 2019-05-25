using System.Net.Http;
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
            var http = new HttpFixture();

            Http = http.HttpFullAccess;

            SubtokenService = new SubtokenService(new SubtokenJsonService(Http));

            TokenInfoService = new TokenInfoService(new TokenInfoJsonService(Http));
        }

        public SubtokenService SubtokenService { get; }

        public TokenInfoService TokenInfoService { get; }
    }
}
