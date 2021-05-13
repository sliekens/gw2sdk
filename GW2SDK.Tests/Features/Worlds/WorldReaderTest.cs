using System;
using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Worlds.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Worlds;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds
{
    public class WorldReaderTest : IClassFixture<WorldFixture>
    {
        public WorldReaderTest(WorldFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly WorldFixture _fixture;

        private static class WorldFact
        {
            public static void Id_is_positive(World actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Name_is_not_empty(World actual) => Assert.NotEmpty(actual.Name);

            public static void World_population_type_is_supported(World actual) => Assert.True(Enum.IsDefined(typeof(WorldPopulation), actual.Population));
        }

        [Fact]
        [Trait("Feature",    "Worlds")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Worlds_can_be_created_from_json()
        {
            var sut = new WorldReader();

            AssertEx.ForEach(_fixture.Db.Worlds,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    WorldFact.Id_is_positive(actual);
                    WorldFact.Name_is_not_empty(actual);
                    WorldFact.World_population_type_is_supported(actual);
                });
        }
    }
}
