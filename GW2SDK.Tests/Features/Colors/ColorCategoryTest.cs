using System;
using System.Linq;
using GW2SDK.Extensions;
using GW2SDK.Features.Colors;
using GW2SDK.Tests.Features.Colors.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Colors
{
    public class ColorCategoryTest : IClassFixture<ColorCategoryFixture>
    {
        public ColorCategoryTest(ColorCategoryFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly ColorCategoryFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Unit")]
        public void ColorCategory_ShouldNotDefineDefaultValue()
        {
            Assert.False(Enum.IsDefined(typeof(ColorCategory), default(ColorCategory)));
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public void ColorCategory_ShouldIncludeAllKnownValues()
        {
            var actual = Enum.GetNames(typeof(ColorCategory)).ToHashSet();

            _output.WriteLine("Expected: {0}", _fixture.ColorCategories.ToCsv());
            _output.WriteLine("Actual: {0}", actual.ToCsv());

            Assert.Equal(_fixture.ColorCategories, actual);
        }
    }
}
