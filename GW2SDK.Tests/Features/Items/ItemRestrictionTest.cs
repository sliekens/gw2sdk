using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Items;
using GW2SDK.Tests.Features.Items.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    [Collection(nameof(ItemDbCollection))]
    public class ItemRestrictionTest
    {
        public ItemRestrictionTest(ItemFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ItemFixture _fixture;

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void DefaultMember_ShouldBeUndefined()
        {
            Assert.False(Enum.IsDefined(typeof(ItemRestriction), default(ItemRestriction)));
        }

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Enum_ShouldHaveNoMissingMembers()
        {
            var expected = _fixture.Db.GetItemRestrictions().ToHashSet();

            var actual = Enum.GetNames(typeof(ItemRestriction)).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}