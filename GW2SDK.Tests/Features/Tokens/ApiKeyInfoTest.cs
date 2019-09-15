using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Tests.Features.Tokens.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Tokens;
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
        public void ApiKeyInfo_can_be_serialized_from_json()
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
        public void Id_is_not_empty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<ApiKeyInfo>(_fixture.ApiKeyInfoJson, settings);

            Assert.NotEmpty(actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public void Name_is_GW2SDK_Full()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<ApiKeyInfo>(_fixture.ApiKeyInfoJson, settings);

            // Your API key must be named GW2SDK-Full to pass this test
            // This is not intended to improve account security, only to prevent key abuse
            // The reason is that some services like GW2BLTC.com associate keys with logins but require you to use a key name of their choice
            // If this key leaks to the outside world, it can't be (ab)used to login with GW2BLTC.com or similar sites
            Assert.Equal("GW2SDK-Full", actual.Name);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public void Token_has_full_permissions()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<ApiKeyInfo>(_fixture.ApiKeyInfoJson, settings);

            var expected = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToHashSet();

            Assert.Equal(expected, actual.Permissions.ToHashSet());
        }
    }
}
