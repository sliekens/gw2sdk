using System.Net.Http;
using GW2SDK.Features.Subtokens;
using GW2SDK.Features.Tokens;
using GW2SDK.Tests.Shared.Fixtures;

namespace GW2SDK.Tests.Features.Subtokens.Fixtures
{
    public class SubtokenFixture
    {
        public SubtokenFixture()
        {
            var http = new HttpFixture();

            Http = http.HttpFullAccess;

            SubtokenService = new SubtokenService(Http);

            TokenInfoService = new TokenInfoService(Http);
        }

        public HttpClient Http { get; }

        public SubtokenService SubtokenService { get; }

        public TokenInfoService TokenInfoService { get; }
    }
}
