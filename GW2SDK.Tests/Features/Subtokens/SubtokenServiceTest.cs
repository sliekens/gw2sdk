using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Exceptions;
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
        public async Task CreateSubtoken_WithAccessToken_ShouldReturnCreatedSubtoken()
        {
            var services = new Container();
            var sut = services.Resolve<SubtokenService>();

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull);

            Assert.IsType<CreatedSubtoken>(actual);
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task CreateSubtoken_WithAccessTokenInDefaultRequestHeaders_ShouldReturnCreatedSubtoken()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<SubtokenService>();

            var actual = await sut.CreateSubtoken(null);

            Assert.IsType<CreatedSubtoken>(actual);
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task CreateSubtoken_WithAccessTokenNull_ShouldThrowUnauthorizedOperationException()
        {
            var services = new Container();
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
        public async Task CreateSubtoken_WithExpirationDate_ShouldReturnCreatedSubtokenWithSpecifiedExpirationDate()
        {
            var services = new Container();
            var sut = services.Resolve<SubtokenService>();

            var expirationDate = DateTimeOffset.Now.AddDays(1);

            // Truncate to seconds: API probably doesn't support milliseconds
            expirationDate = DateTimeOffset.FromUnixTimeSeconds(expirationDate.ToUnixTimeSeconds());

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull, absoluteExpirationDate: expirationDate);

            // This test is flaky: GetTokenInfo occassionally fails right after the subtoken is created
            // Adding a delay seems to help, possibly because of clock skew?
            await Task.Delay(1000);

            var tokenInfo = await services.Resolve<TokenInfoService>().GetTokenInfo(actual.Subtoken);

            var subtokenInfo = Assert.IsType<SubtokenInfo>(tokenInfo);

            Assert.Equal(expirationDate, subtokenInfo.ExpiresAt);
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task CreateSubtoken_WithUrls_ShouldReturnCreatedSubtokenWithSpecifiedUrls()
        {
            var services = new Container();
            var sut = services.Resolve<SubtokenService>();

            var urls = new List<string> { "/v2/tokeninfo", "/v2/account", "/v2/account/home/cats" };

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull, urls: urls);

            // This test is flaky: GetTokenInfo occassionally fails right after the subtoken is created
            // Adding a delay seems to help, possibly because of clock skew?
            await Task.Delay(1000);

            var tokenInfo = await services.Resolve<TokenInfoService>().GetTokenInfo(actual.Subtoken);

            var subtokenInfo = Assert.IsType<SubtokenInfo>(tokenInfo);

            Assert.Equal(urls, subtokenInfo.Urls.Select(url => url.ToString()).ToList());
        }
    }
}
