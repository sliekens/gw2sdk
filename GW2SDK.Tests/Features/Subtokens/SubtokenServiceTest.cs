using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Subtokens;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Tokens;
using Xunit;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class SubtokenServiceTest
    {
        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task It_can_create_a_subtoken()
        {
            await using var services = new Container();
            var sut = services.Resolve<SubtokenService>();

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull);

            Assert.IsType<CreatedSubtoken>(actual);
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task It_cant_create_a_subtoken_without_an_access_token()
        {
            await using var services = new Container();
            var sut = services.Resolve<SubtokenService>();

            await Assert.ThrowsAsync<UnauthorizedOperationException>(async () =>
            {
                // Next statement should throw because argument is null and HttpClient.DefaultRequestHeaders is not configured
                _ = await sut.CreateSubtoken(null);
            });
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task It_can_create_a_subtoken_with_custom_expiration_date()
        {
            await using var services = new Container();
            var sut = services.Resolve<SubtokenService>();

            var expirationDate = DateTimeOffset.Now.AddDays(1);

            // Truncate to seconds: API probably doesn't support milliseconds
            expirationDate = DateTimeOffset.FromUnixTimeSeconds(expirationDate.ToUnixTimeSeconds());

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull, absoluteExpirationDate: expirationDate);

            var tokenInfo = await services.Resolve<TokenInfoService>().GetTokenInfo(actual.Subtoken);

            var subtokenInfo = Assert.IsType<SubtokenInfo>(tokenInfo);

            Assert.Equal(expirationDate, subtokenInfo.ExpiresAt);
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task It_can_create_a_subtoken_with_url_filters()
        {
            await using var services = new Container();
            var sut = services.Resolve<SubtokenService>();

            var urls = new List<string> { "/v2/tokeninfo", "/v2/account", "/v2/account/home/cats" };

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull, urls: urls);

            var tokenInfo = await services.Resolve<TokenInfoService>().GetTokenInfo(actual.Subtoken);

            var subtokenInfo = Assert.IsType<SubtokenInfo>(tokenInfo);

            Assert.Equal(urls, subtokenInfo.Urls.Select(url => url.ToString()).ToList());
        }
    }
}
