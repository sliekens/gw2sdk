using System;
using System.Linq;
using GW2SDK.Features.Tokens;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Tokens.Fixtures;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Tokens
{
    public class TokenInfoTest : IClassFixture<TokenInfoFixture>
    {
        public TokenInfoTest(TokenInfoFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly TokenInfoFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public void TokenInfo_ShouldHaveNoMissingMembers()
        {
            _output.WriteLine(_fixture.JsonTokenInfo);

            var actual = new TokenInfo();

            var serializerSettings = Json.DefaultJsonSerializerSettings;
            serializerSettings.MissingMemberHandling = MissingMemberHandling.Error;

            JsonConvert.PopulateObject(_fixture.JsonTokenInfo, actual, serializerSettings);
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public void TokenInfo_Id_ShouldNotBeEmpty()
        {
            var actual = new TokenInfo();

            JsonConvert.PopulateObject(_fixture.JsonTokenInfo, actual, Json.DefaultJsonSerializerSettings);

            Assert.NotEmpty(actual.Id);
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public void TokenInfo_Name_ShouldBeGW2SDKDev()
        {
            var actual = new TokenInfo();

            JsonConvert.PopulateObject(_fixture.JsonTokenInfo, actual, Json.DefaultJsonSerializerSettings);

            // This is not intended to improve account security, only to prevent key abuse
            // The reason is that some services like GW2BLTC.com associate keys with logins but require you to use a key name of their choice
            // If this key leaks to the outside world, it still can't be (ab)used to login with GW2BLTC.com or similar sites
            Assert.Equal("GW2SDK-Dev", actual.Name);
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public void TokenInfo_Permissions_ShouldHaveFullPermissions()
        {
            var expected = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToHashSet();

            var actual = new TokenInfo();

            JsonConvert.PopulateObject(_fixture.JsonTokenInfo, actual, Json.DefaultJsonSerializerSettings);

            Assert.Equal(expected, actual.Permissions.ToHashSet());
        }
    }
}
