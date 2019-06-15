using System;
using System.Linq;
using GW2SDK.Features.Colors;
using GW2SDK.Tests.Features.Colors.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Colors
{
    [Collection(nameof(ColorDbCollection))]
    public class ColorCategoryNameTest
    {
        private readonly ColorFixture _fixture;

        public ColorCategoryNameTest(ColorFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public void DefaultMember_ShouldBeUndefined()
        {
            Assert.False(Enum.IsDefined(typeof(ColorCategoryName), default(ColorCategoryName)));
        }

        [Fact]
        [Trait("Feature",    "Colors")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void AllMembers_ShouldNotHaveMissingMembers()
        {
            var expected = _fixture.Db.GetColorCategoryNames().ToHashSet();

            var actual = Enum.GetNames(typeof(ColorCategoryName)).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}
