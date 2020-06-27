using System;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Worlds.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Worlds;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Worlds
{
    public class WorldTest : IClassFixture<WorldFixture>
    {
        public WorldTest(WorldFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly WorldFixture _fixture;

        private readonly ITestOutputHelper _output;

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
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(_output)
                .ThrowErrorOnMissingMember()
                .Build();

            AssertEx.ForEach(_fixture.Db.Worlds,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<World>(json, settings);

                    WorldFact.Id_is_positive(actual);
                    WorldFact.Name_is_not_empty(actual);
                    WorldFact.World_population_type_is_supported(actual);
                });
        }
    }
}
