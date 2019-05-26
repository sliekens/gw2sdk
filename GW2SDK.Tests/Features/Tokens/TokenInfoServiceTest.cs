using System;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Features.Tokens;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using GW2SDK.Tests.Shared.Fixtures;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Tokens
{
    public class TokenInfoServiceTest : IClassFixture<HttpFixture>
    {
        public TokenInfoServiceTest(HttpFixture http, ITestOutputHelper output)
        {
            _http = http;
            _output = output;
        }

        private readonly HttpFixture _http;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public async Task GetTokenInfo_ShouldReturnTokenInfo()
        {
            var sut = new TokenInfoService(_http.HttpFullAccess);
            
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetTokenInfo(settings);

            Assert.IsAssignableFrom<TokenInfo>(actual);
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public async Task TokenInfo_ShouldHaveNoMissingMembers()
        {
            var sut = new TokenInfoService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();
                
            // Next statement throws if there are missing members
            _ = await sut.GetTokenInfo(settings);
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public async Task TokenInfo_Id_ShouldNotBeEmpty()
        {
            var sut = new TokenInfoService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();
                
            var actual = await sut.GetTokenInfo(settings);

            Assert.NotEmpty(actual.Id);
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public async Task TokenInfo_Name_ShouldBeGW2SDKDev()
        {
            var sut = new TokenInfoService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();
                
            var actual = await sut.GetTokenInfo(settings);

            // This is not intended to improve account security, only to prevent key abuse
            // The reason is that some services like GW2BLTC.com associate keys with logins but require you to use a key name of their choice
            // If this key leaks to the outside world, it still can't be (ab)used to login with GW2BLTC.com or similar sites
            Assert.StartsWith("GW2SDK-", actual.Name);
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public async Task TokenInfo_Permissions_ShouldHaveFullPermissions()
        {
            var sut = new TokenInfoService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();
                
            var expected = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToHashSet();

            var actual = await sut.GetTokenInfo(settings);

            Assert.Equal(expected, actual.Permissions.ToHashSet());
        }
    }
}
