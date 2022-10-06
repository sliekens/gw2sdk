using System.Text.Json;
using GW2SDK.Maps;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Maps;

public class ContinentReaderTest : IClassFixture<ContinentFixture>
{
    public ContinentReaderTest(ContinentFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly ContinentFixture fixture;

    private static class FloorFact
    {
        public static void Texture_dimensions_contains_width_and_height(Floor actual) =>
            Assert.False(actual.TextureDimensions.IsEmpty);
    }

    [Fact]
    public void Floors_can_be_created_from_json() =>
        AssertEx.ForEach(
            fixture.Floors,
            json =>
            {
                using var document = JsonDocument.Parse(json);

                var actual = document.RootElement.GetFloor(MissingMemberBehavior.Error);

                FloorFact.Texture_dimensions_contains_width_and_height(actual);
            }
        );
}
