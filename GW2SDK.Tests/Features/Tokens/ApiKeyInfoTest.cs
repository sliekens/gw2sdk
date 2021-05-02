using System;
using System.Linq;
using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Tokens.Fixtures;
using GW2SDK.Tokens;
using Xunit;

namespace GW2SDK.Tests.Features.Tokens
{
    public class ApiKeyInfoTest : IClassFixture<ApiKeyInfoFixture>
    {
        public ApiKeyInfoTest(ApiKeyInfoFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ApiKeyInfoFixture _fixture;

        [Fact]
        [Trait("Feature",    "Tokens")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void ApiKeyInfo_can_be_created_from_json()
        {
            var sut = new TokenInfoReader();

            using var document = JsonDocument.Parse(_fixture.ApiKeyInfoJson);

            var actual = Assert.IsType<ApiKeyInfo>(sut.Read(document.RootElement, MissingMemberBehavior.Error));

            Assert.NotEmpty(actual.Id);

            // Your API key must be named GW2SDK-Full to pass this test
            // This is not intended to improve account security, only to prevent key abuse
            // The reason is that some services like GW2BLTC.com associate keys with logins but require you to use a key name of their choice
            // If this key leaks to the outside world, it can't be (ab)used to login with GW2BLTC.com or similar sites
            Assert.Equal("GW2SDK-Full", actual.Name);

            var expectedPermissions = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToHashSet();

            Assert.Equal(expectedPermissions, actual.Permissions.ToHashSet());
        }
    }
}
