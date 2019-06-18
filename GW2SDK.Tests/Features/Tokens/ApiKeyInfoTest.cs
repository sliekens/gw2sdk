using System;
using System.Linq;
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
    public class ApiKeyInfoTest : IClassFixture<ApiKeyInfoFixture>
    {
        public ApiKeyInfoTest(ApiKeyInfoFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly ApiKeyInfoFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Tokens")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Class_ShouldHaveNoMissingMembers()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            // Next statement throws if there are missing members
            _ = JsonConvert.DeserializeObject<ApiKeyInfo>(_fixture.ApiKeyInfoJson, settings);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public void Id_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<ApiKeyInfo>(_fixture.ApiKeyInfoJson, settings);

            Assert.NotEmpty(actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public void Name_ShouldBeGW2SDKFull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<ApiKeyInfo>(_fixture.ApiKeyInfoJson, settings);

            // This is not intended to improve account security, only to prevent key abuse
            // The reason is that some services like GW2BLTC.com associate keys with logins but require you to use a key name of their choice
            // If this key leaks to the outside world, it still can't be (ab)used to login with GW2BLTC.com or similar sites
            Assert.Equal("GW2SDK-Full", actual.Name);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public void Permissions_ShouldHaveFullPermissions()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<ApiKeyInfo>(_fixture.ApiKeyInfoJson, settings);

            var expected = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToHashSet();

            Assert.Equal(expected, actual.Permissions.ToHashSet());
        }
    }
}
