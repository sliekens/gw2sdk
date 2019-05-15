using System.Collections.Generic;
using GW2SDK.Features.Colors;
using GW2SDK.Tests.Features.Colors.Fixtures;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Colors
{
    public class ColorTest : IClassFixture<ColorFixture>
    {
        public ColorTest(ColorFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        private readonly ColorFixture _fixture;

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_ShouldHaveNoMissingMembers()
        {
            _output.WriteLine(_fixture.JsonArrayOfColors);

            var actual = new List<Color>();

            var serializerSettings = ColorService.DefaultJsonSerializerSettings;
            serializerSettings.MissingMemberHandling = MissingMemberHandling.Error;

            JsonConvert.PopulateObject(_fixture.JsonArrayOfColors, actual, serializerSettings);
        }
    }
}
