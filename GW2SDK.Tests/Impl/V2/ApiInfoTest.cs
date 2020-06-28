using System.Linq;
using GW2SDK.Impl.V2;
using GW2SDK.Tests.Impl.V2.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Impl.V2
{
    public class ApiInfoTest : IClassFixture<ApiInfoFixture>
    {
        public ApiInfoTest(ApiInfoFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly ApiInfoFixture _fixture;

        private readonly ITestOutputHelper _output;

        private static class ApiInfoFact
        {
            public static void We_know_about_all_supported_languages(ApiInfo actual) =>
                Assert.Collection(actual.Languages,
                    english => Assert.Equal("en", english),
                    spanish => Assert.Equal("es", spanish),
                    german => Assert.Equal("de",  german),
                    french => Assert.Equal("fr",  french),
                    chinese => Assert.Equal("zh", chinese));

            public static void We_know_about_all_schema_versions(ApiInfo actual) =>
                Assert.Collection(actual.SchemaVersions,
                    v => Assert.Equal(SchemaVersion.V20190221, v.Version),
                    v => Assert.Equal(SchemaVersion.V20190322, v.Version),
                    v => Assert.Equal(SchemaVersion.V20190516, v.Version),
                    v => Assert.Equal(SchemaVersion.V20190521, v.Version),
                    v => Assert.Equal(SchemaVersion.V20190522, v.Version),
                    v => Assert.Equal(SchemaVersion.V20191219, v.Version));

            public static void We_know_about_all_routes(ApiInfo actual)
            {
                var expected = Location.GetValues().Select(l => l.Path).ToList();
                AssertEx.ForEach(actual.Routes,
                    route =>
                    {
                        Assert.Contains(route.Path, expected);
                    });
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Api_info_can_be_created_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(_output)
                .ThrowErrorOnMissingMember()
                .Build();

            var actual = JsonConvert.DeserializeObject<ApiInfo>(_fixture.ApiInfo, settings);

            ApiInfoFact.We_know_about_all_supported_languages(actual);
            ApiInfoFact.We_know_about_all_routes(actual);
            ApiInfoFact.We_know_about_all_schema_versions(actual);
        }
    }
}
