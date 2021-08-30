using System.Text.Json;
using GW2SDK.Continents;
using GW2SDK.Json;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Continents
{
    public class ContinentReaderTest : IClassFixture<ContinentFixture>
    {
        public ContinentReaderTest(ContinentFixture fixture)
        {
            this.fixture = fixture;
        }

        private readonly ContinentFixture fixture;

        private static class FloorFact
        {
            public static void Texture_dimensions_contains_width_and_height(Floor actual) => Assert.Equal(2, actual.TextureDimensions.Length);

            public static void Label_coordinates_of_a_region_contains_a_point(Floor actual) => Assert.All(actual.Regions.Values,
                region =>
                {
                    Assert.Equal(2, region.LabelCoordinates.Length);
                });

            public static void Continent_rectangle_of_a_region_contains_2_points(Floor actual) => Assert.All(actual.Regions.Values,
                region =>
                {
                    Assert.Equal(2, region.ContinentRectangle.Length);
                    Assert.Equal(2, region.ContinentRectangle[0].Length);
                    Assert.Equal(2, region.ContinentRectangle[1].Length);
                });
        }

        [Fact]
        [Trait("Feature",    "Continents")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Floors_can_be_created_from_json()
        {
            var sut = new ContinentReader();

            AssertEx.ForEach(fixture.Floors,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Floor.Read(document.RootElement, MissingMemberBehavior.Error);

                    FloorFact.Texture_dimensions_contains_width_and_height(actual);
                    FloorFact.Label_coordinates_of_a_region_contains_a_point(actual);
                    FloorFact.Continent_rectangle_of_a_region_contains_2_points(actual);
                });
        }
    }
}
