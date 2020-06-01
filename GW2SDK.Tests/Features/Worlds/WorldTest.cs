using System;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
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

        [Fact]
        [Trait("Feature",    "Worlds")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Worlds_can_be_serialized_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .Build();

            AssertEx.ForEach(_fixture.Db.Worlds,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<World>(json, settings);
                });
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public void Id_is_positive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            AssertEx.ForEach(_fixture.Db.Worlds,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<World>(json, settings);
                    Assert.InRange(actual.Id, 1, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public void Name_is_not_empty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            AssertEx.ForEach(_fixture.Db.Worlds,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<World>(json, settings);
                    Assert.NotEmpty(actual.Name);
                });
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Integration")]
        public void World_population_type_is_supported()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            AssertEx.ForEach(_fixture.Db.Worlds,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<World>(json, settings);
                    Assert.True(Enum.IsDefined(typeof(WorldPopulation), actual.Population));
                });
        }
    }
}
