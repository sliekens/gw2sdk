using GW2SDK.Colors;
using GW2SDK.Tests.Features.Colors.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Colors
{
    [Collection(nameof(ColorDbCollection))]
    public class ColorTest
    {
        public ColorTest(ColorFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly ColorFixture _fixture;

        private readonly ITestOutputHelper _output;

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
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(_output)
                .ThrowErrorOnMissingMember()
                .Build();

            AssertEx.ForEach(_fixture.Db.Colors,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Color>(json, settings);
                    ColorFact.Base_rgb_contains_red_green_blue(actual);
                });
        }
    }
}
