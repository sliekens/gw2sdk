using GW2SDK.Colors;
using GW2SDK.Impl.JsonConverters;
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

        [Fact]
        [Trait("Feature",    "Colors")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Colors_can_be_serialized_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            AssertEx.ForEach(_fixture.Db.Colors,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<Color>(json, settings);
                });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public void Base_rgb_contains_red_green_blue()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            AssertEx.ForEach(_fixture.Db.Colors,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Color>(json, settings);

                    Assert.Collection(actual.BaseRgb,
                        red => Assert.InRange(red,     1, 255),
                        green => Assert.InRange(green, 1, 255),
                        blue => Assert.InRange(blue,   1, 255));
                });
        }
    }
}
