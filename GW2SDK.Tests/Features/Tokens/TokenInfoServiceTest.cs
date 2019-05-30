using System;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Features.Tokens;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Tokens.Fixtures;
using GW2SDK.Tests.Shared;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Tokens
{
    public class TokenInfoServiceTest : IClassFixture<TokenInfoFixture>
    {
        public TokenInfoServiceTest(TokenInfoFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly TokenInfoFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_WithApiKey_ShouldReturnApiKeyInfo()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new TokenInfoService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetTokenInfo(ConfigurationManager.Instance.ApiKeyFull, settings);

            Assert.IsType<ApiKeyInfo>(actual);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_WithApiKeyInDefaultRequestHeaders_ShouldReturnApiKeyInfo()
        {
            var http = HttpClientFactory.CreateDefault();
            http.UseAccessToken(ConfigurationManager.Instance.ApiKeyFull);

            var sut = new TokenInfoService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetTokenInfo(null, settings);

            Assert.IsType<ApiKeyInfo>(actual);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_WithAccessTokenNull_ShouldThrowUnauthorizedOperationException()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new TokenInfoService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            await Assert.ThrowsAsync<UnauthorizedOperationException>(async () =>
            {
                // Next statement should throw because argument is null and HttpClient.DefaultRequestHeaders is not configured
                _ = await sut.GetTokenInfo(null, settings);
            });
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_WithSubtoken_ShouldReturnSubtokenInfo()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new TokenInfoService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            
            var actual = await sut.GetTokenInfo(_fixture.SubtokenBasic.Subtoken, settings);

            Assert.IsType<SubtokenInfo>(actual);
        }

        [Fact]
        [Trait("Feature",    "Tokens")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public async Task TokenInfo_ShouldHaveNoMissingMembers()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new TokenInfoService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            // Next statement throws if there are missing members
            _ = await sut.GetTokenInfo(ConfigurationManager.Instance.ApiKeyFull, settings);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task TokenInfo_Id_ShouldNotBeEmpty()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new TokenInfoService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            var actual = await sut.GetTokenInfo(ConfigurationManager.Instance.ApiKeyFull, settings);

            Assert.NotEmpty(actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task TokenInfo_Name_ShouldBeGW2SDKDev()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new TokenInfoService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            var actual = await sut.GetTokenInfo(ConfigurationManager.Instance.ApiKeyFull, settings);

            // This is not intended to improve account security, only to prevent key abuse
            // The reason is that some services like GW2BLTC.com associate keys with logins but require you to use a key name of their choice
            // If this key leaks to the outside world, it still can't be (ab)used to login with GW2BLTC.com or similar sites
            Assert.StartsWith("GW2SDK-", actual.Name);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public async Task TokenInfo_Permissions_ShouldHaveFullPermissions()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new TokenInfoService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            var expected = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToHashSet();

            var actual = await sut.GetTokenInfo(ConfigurationManager.Instance.ApiKeyFull, settings);

            Assert.Equal(expected, actual.Permissions.ToHashSet());
        }
    }
}
