using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Features.Subtokens;
using GW2SDK.Features.Tokens;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class SubtokenServiceTest
    {
        public SubtokenServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task CreateSubtoken_WithAccessToken_ShouldReturnCreatedSubtoken()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new SubtokenService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull, settings: settings);

            Assert.IsType<CreatedSubtoken>(actual);
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task CreateSubtoken_WithAccessTokenInDefaultRequestHeaders_ShouldReturnCreatedSubtoken()
        {
            var http = HttpClientFactory.CreateDefault();
            http.UseAccessToken(ConfigurationManager.Instance.ApiKeyFull);

            var sut = new SubtokenService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.CreateSubtoken(null, settings: settings);

            Assert.IsType<CreatedSubtoken>(actual);
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task CreateSubtoken_WithAccessTokenNull_ShouldThrowUnauthorizedOperationException()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new SubtokenService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            await Assert.ThrowsAsync<UnauthorizedOperationException>(async () =>
            {
                // Next statement should throw because argument is null and HttpClient.DefaultRequestHeaders is not configured
                _ = await sut.CreateSubtoken(null, settings: settings);
            });
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task CreateSubtoken_WithExpirationDate_ShouldReturnCreatedSubtokenWithSpecifiedExpirationDate()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new SubtokenService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var expirationDate = DateTimeOffset.Now.AddDays(1);

            // Truncate to seconds: API probably doesn't support milliseconds
            expirationDate = DateTimeOffset.FromUnixTimeSeconds(expirationDate.ToUnixTimeSeconds());

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull, absoluteExpirationDate: expirationDate, settings: settings);

            // This test is flaky: GetTokenInfo occassionally fails
            // Adding a delay seems to help, possibly because of clock skew?
            await Task.Delay(1000);

            var tokenInfo = await new TokenInfoService(http).GetTokenInfo(actual.Subtoken);

            var subtokenInfo = Assert.IsType<SubtokenInfo>(tokenInfo);

            Assert.Equal(expirationDate, subtokenInfo.ExpiresAt);
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task CreateSubtoken_WithUrls_ShouldReturnCreatedSubtokenWithSpecifiedUrls()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new SubtokenService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var urls = new List<string> { "/v2/tokeninfo", "/v2/account", "/v2/account/home/cats" };

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull, urls: urls, settings: settings);
            
            // This test is flaky: GetTokenInfo occassionally fails
            // Adding a delay seems to help, possibly because of clock skew?
            await Task.Delay(1000);

            var tokenInfo = await new TokenInfoService(http).GetTokenInfo(actual.Subtoken);

            var subtokenInfo = Assert.IsType<SubtokenInfo>(tokenInfo);

            Assert.Equal(urls, subtokenInfo.Urls.Select(url => url.ToString()).ToList());
        }
    }
}
