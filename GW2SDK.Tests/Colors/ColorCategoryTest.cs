using System;
using System.Linq;
using GW2SDK.Colors;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Colors
{
    public class ColorCategoryTest : IClassFixture<ColorCategoryFixture>
    {
        private readonly ColorCategoryFixture _fixture;

        private readonly ITestOutputHelper _logger;

        public ColorCategoryTest(ColorCategoryFixture fixture, ITestOutputHelper logger)
        {
            _fixture = fixture;
            _logger = logger;
        }

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

            _logger.WriteLine("Expected: {0}", _fixture.ColorCategories.ToCsv());
            _logger.WriteLine("Actual: {0}", actual.ToCsv());

            Assert.Equal(_fixture.ColorCategories, actual);
        }
    }
}