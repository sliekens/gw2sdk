using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Subtokens;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Tokens;
using Polly;
using Xunit;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class SubtokenServiceTest
    {
        private static async Task<IReplica<TokenInfo>> GetTokenInfo(TokenInfoService tokenInfoService, CreatedSubtoken actual)
        {
            // BUG: /v2/tokeninfo sometimes fails with "Invalid access token" for recently created subtokens
            // I guess this is a timing problem, because a retry usually does pass.
            // Maybe they are they checking the "iat" claim and treating it as "nbf", without considering clock skew? One can only guess.
            // Just to be sure that this error is real, retry up to 10 times with 200ms delays before throwing an exception.
            //    Because 2 seconds should be enough to compensate for clock skew, without adding too much delay to true error results.
            return await Policy.Handle<UnauthorizedOperationException>()
                .WaitAndRetryAsync(10, attempt => TimeSpan.FromMilliseconds(200))
                .ExecuteAsync(async () => await tokenInfoService.GetTokenInfo(actual.Subtoken).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        [Fact]
        [Trait("Feature", "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task It_can_create_a_subtoken()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SubtokenService>();

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull);

            Assert.IsType<CreatedSubtoken>(actual.Value);
        }

        [Fact]
        [Trait("Feature", "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task It_can_create_a_subtoken_with_custom_expiration_date()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SubtokenService>();
            var tokenInfoService = services.Resolve<TokenInfoService>();

            var expirationDate = DateTimeOffset.Now.AddDays(1);

            // Truncate to seconds: API probably doesn't support milliseconds
            expirationDate = DateTimeOffset.FromUnixTimeSeconds(expirationDate.ToUnixTimeSeconds());

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull,
                absoluteExpirationDate: expirationDate);

            var tokenInfo = await GetTokenInfo(tokenInfoService, actual.Value);

            var subtokenInfo = Assert.IsType<SubtokenInfo>(tokenInfo.Value);

            Assert.Equal(expirationDate, subtokenInfo.ExpiresAt);
        }

        [Fact]
        [Trait("Feature", "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task It_can_create_a_subtoken_with_url_filters()
        {
            await using var services = new Composer();
            var sut = services.Resolve<SubtokenService>();
            var tokenInfoService = services.Resolve<TokenInfoService>();

            var urls = new List<string>
            {
                "/v2/tokeninfo",
                "/v2/account",
                "/v2/account/home/cats"
            };

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull, urls: urls);

            var tokenInfo = await GetTokenInfo(tokenInfoService, actual.Value);

            var subtokenInfo = Assert.IsType<SubtokenInfo>(tokenInfo.Value);

            Assert.Equal(urls, subtokenInfo.Urls.Select(url => url.ToString()).ToList());
        }
    }
}
