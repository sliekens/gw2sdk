using System.Text.Json;
using GW2SDK.Builds;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Builds.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Builds
{
    public class BuildReaderTest : IClassFixture<BuildFixture>
    {
        public BuildReaderTest(BuildFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly BuildFixture _fixture;

        private static class BuildFact
        {
            public static void Id_is_positive(Build actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
        }

        [Fact]
        [Trait("Feature",    "Builds")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Build_can_be_created_from_json()
        {
            var sut = new BuildReader();

            using var document = JsonDocument.Parse(_fixture.Build);

            var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

            BuildFact.Id_is_positive(actual);
        }
    }
}
