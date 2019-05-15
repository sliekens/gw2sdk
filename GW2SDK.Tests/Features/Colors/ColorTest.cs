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

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Id_ShouldBePositive()
        {
            var actual = new List<Color>();

            JsonConvert.PopulateObject(_fixture.JsonArrayOfColors, actual, ColorService.DefaultJsonSerializerSettings);

            Assert.All(actual, color => { Assert.InRange(color.Id, 1, int.MaxValue); });
        }


        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Name_ShouldNotBeEmpty()
        {
            var actual = new List<Color>();

            JsonConvert.PopulateObject(_fixture.JsonArrayOfColors, actual, ColorService.DefaultJsonSerializerSettings);

            Assert.All(actual, color => { Assert.NotEmpty(color.Name); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Cloth_ShouldNotBeNull()
        {
            var actual = new List<Color>();

            JsonConvert.PopulateObject(_fixture.JsonArrayOfColors, actual, ColorService.DefaultJsonSerializerSettings);

            Assert.All(actual, color => { Assert.NotNull(color.Cloth); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Leather_ShouldNotBeNull()
        {
            var actual = new List<Color>();

            JsonConvert.PopulateObject(_fixture.JsonArrayOfColors, actual, ColorService.DefaultJsonSerializerSettings);

            Assert.All(actual, color => { Assert.NotNull(color.Leather); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Metal_ShouldNotBeNull()
        {
            var actual = new List<Color>();

            JsonConvert.PopulateObject(_fixture.JsonArrayOfColors, actual, ColorService.DefaultJsonSerializerSettings);

            Assert.All(actual, color => { Assert.NotNull(color.Metal); });
        }

        [Fact(Skip = "Some dyes like Hydra (1594) don't have a 'fur' property. Bug in API?")]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Fur_ShouldNotBeNull()
        {
            var actual = new List<Color>();

            JsonConvert.PopulateObject(_fixture.JsonArrayOfColors, actual, ColorService.DefaultJsonSerializerSettings);

            Assert.All(actual, color => { Assert.NotNull(color.Fur); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_Categories_ShouldNotBeNull()
        {
            var actual = new List<Color>();

            JsonConvert.PopulateObject(_fixture.JsonArrayOfColors, actual, ColorService.DefaultJsonSerializerSettings);

            Assert.All(actual, color => { Assert.NotNull(color.Categories); });
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void Color_BaseRgb_ShouldBeRgbTuple()
        {
            var actual = new List<Color>();

            JsonConvert.PopulateObject(_fixture.JsonArrayOfColors, actual, ColorService.DefaultJsonSerializerSettings);

            Assert.All(actual, color =>
            {
                Assert.Collection(color.BaseRgb,
                    red => Assert.InRange(red, 1, 255),
                    green => Assert.InRange(green, 1, 255),
                    blue => Assert.InRange(blue, 1, 255));
            });
        }
    }
}
