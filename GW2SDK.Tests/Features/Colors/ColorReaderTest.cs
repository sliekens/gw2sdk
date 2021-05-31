using System.Text.Json;
using GW2SDK.Colors;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Colors.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Colors
{
    public class ColorReaderTest : IClassFixture<ColorFixture>
    {
        public ColorReaderTest(ColorFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ColorFixture _fixture;

        private static class ColorFact
        {
            public static void Base_rgb_contains_red_green_blue(Color actual) => Assert.Collection(actual.BaseRgb,
                red => Assert.InRange(red,     1, 255),
                green => Assert.InRange(green, 1, 255),
                blue => Assert.InRange(blue,   1, 255));
        }

        [Fact]
        [Trait("Feature",    "Colors")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Colors_can_be_created_from_json()
        {
            var sut = new ColorReader();

            AssertEx.ForEach(_fixture.Colors,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    ColorFact.Base_rgb_contains_red_green_blue(actual);
                });
        }
    }
}
