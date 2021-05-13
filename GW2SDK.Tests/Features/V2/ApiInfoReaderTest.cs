using System.Linq;
using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Tests.Features.V2.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.V2;
using Xunit;

namespace GW2SDK.Tests.Features.V2
{
    public class ApiInfoReaderTest : IClassFixture<ApiInfoFixture>
    {
        public ApiInfoReaderTest(ApiInfoFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ApiInfoFixture _fixture;


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
                    v => Assert.Equal(SchemaVersion.V20191219, v.Version),
                    v => Assert.Equal(SchemaVersion.V20201117, v.Version),
                    v => Assert.Equal(SchemaVersion.V20210406, v.Version));

            public static void We_know_about_all_routes(ApiInfo expected)
            {
                var actual = Location.GetValues().Select(l => l.Path).ToList();
                Assert.All(expected.Routes,
                    expectedRoute =>
                    {
                        Assert.Contains(expectedRoute.Path, actual);
                    });
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Api_info_can_be_created_from_json()
        {
            var sut = new ApiInfoReader();

            using var document = JsonDocument.Parse(_fixture.ApiInfo);

            var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

            ApiInfoFact.We_know_about_all_supported_languages(actual);
            ApiInfoFact.We_know_about_all_routes(actual);
            ApiInfoFact.We_know_about_all_schema_versions(actual);
        }
    }
}
