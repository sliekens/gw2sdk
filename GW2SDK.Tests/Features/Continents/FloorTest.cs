using GW2SDK.Continents;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Tests.Features.Continents.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Continents
{
    [Collection(nameof(ContinentDbCollection))]
    public class FloorTest
    {
        public FloorTest(FloorFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly FloorFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Continents")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Floors_can_be_serialized_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            AssertEx.ForEach(_fixture.Db.Floors,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<Floor>(json, settings);
                });
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public void Texture_dimensions_contains_width_and_height()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            AssertEx.ForEach(_fixture.Db.Floors,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Floor>(json, settings);

                    Assert.Equal(2, actual.TextureDimensions.Length);
                });
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public void Label_coordinates_of_a_region_contains_a_point()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Floors,
                json =>
                {
                    var floor = JsonConvert.DeserializeObject<Floor>(json, settings);
                    Assert.All(floor.Regions.Values,
                        actual =>
                        {
                            Assert.Equal(2, actual.LabelCoordinates.Length);
                        });
                });
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public void Continent_rectangle_of_a_region_contains_2_points()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            AssertEx.ForEach(_fixture.Db.Floors,
                json =>
                {
                    var floor = JsonConvert.DeserializeObject<Floor>(json, settings);
                    Assert.All(floor.Regions.Values,
                        actual =>
                        {
                            Assert.Equal(2, actual.ContinentRectangle.Length);
                            Assert.Equal(2, actual.ContinentRectangle[0].Length);
                            Assert.Equal(2, actual.ContinentRectangle[1].Length);
                        });
                });
        }
    }
}
