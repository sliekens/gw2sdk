using System;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Colors;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests
{
    public class ColorCategoryTest : IClassFixture<ColorsFixture>
    {
        private readonly ColorsFixture _colors;

        private readonly ITestOutputHelper _logger;

        public ColorCategoryTest(ColorsFixture colors, ITestOutputHelper logger)
        {
            _colors = colors;
            _logger = logger;
        }

        [Fact]
        [Trait("Category", "Integration")]
        [Trait("Feature", "Colors")]
        public async Task ColorCategory_ShouldIncludeAllKnownValues()
        {
            var knownCategories = Enum.GetNames(typeof(ColorCategory)).ToHashSet();

            _logger.WriteLine("Expected: {0}", _colors.ColorCategories.ToCsv());
            _logger.WriteLine("Actual: {0}", knownCategories.ToCsv());

            Assert.ProperSuperset(_colors.ColorCategories, knownCategories);
        }
    }
}