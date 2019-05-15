using System;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Colors;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Colors
{
    public class ColorCategoryTest : IClassFixture<ColorsFixture>
    {
        private readonly ColorsFixture _fixture;

        private readonly ITestOutputHelper _logger;

        public ColorCategoryTest(ColorsFixture fixture, ITestOutputHelper logger)
        {
            _fixture = fixture;
            _logger = logger;
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Unit")]
        public async Task DefaultEnumMember_ShouldBeUndefined()
        {
            var actual = default(ColorCategory);
            Assert.False(Enum.IsDefined(typeof(ColorCategory), actual));
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task ColorCategory_ShouldIncludeAllKnownValues()
        {
            var actual = Enum.GetNames(typeof(ColorCategory)).ToHashSet();

            _logger.WriteLine("Expected: {0}", _fixture.ColorCategories.ToCsv());
            _logger.WriteLine("Actual: {0}", actual.ToCsv());

            Assert.Equal(_fixture.ColorCategories, actual);
        }
    }
}