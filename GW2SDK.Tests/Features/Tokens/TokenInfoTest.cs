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

            var sut = new TokenInfo();

            var serializerSettings = Json.DefaultJsonSerializerSettings;
            serializerSettings.MissingMemberHandling = MissingMemberHandling.Error;

            JsonConvert.PopulateObject(_fixture.JsonTokenInfo, sut, serializerSettings);
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public void TokenInfo_Id_ShouldNotBeEmpty()
        {
            var sut = new TokenInfo();

            JsonConvert.PopulateObject(_fixture.JsonTokenInfo, sut, Json.DefaultJsonSerializerSettings);

            Assert.NotEmpty(sut.Id);
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public void TokenInfo_Name_ShouldBeGW2SDKDev()
        {
            var sut = new TokenInfo();

            JsonConvert.PopulateObject(_fixture.JsonTokenInfo, sut, Json.DefaultJsonSerializerSettings);

            // This is not intended to improve account security, only to prevent key abuse
            // The reason is that some services like GW2BLTC.com associate keys with logins but require you to use a key name of their choice
            // If this key leaks to the outside world, it still can't be (ab)used to login with GW2BLTC.com or similar sites
            Assert.Equal("GW2SDK-Dev", sut.Name);
        }

        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Integration")]
        public void TokenInfo_Permissions_ShouldHaveFullPermissions()
        {
            var sut = new TokenInfo();

            var expected = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToHashSet();

            JsonConvert.PopulateObject(_fixture.JsonTokenInfo, sut, Json.DefaultJsonSerializerSettings);

            Assert.Equal(expected, sut.Permissions.ToHashSet());
        }
    }
}
