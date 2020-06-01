using System;
using System.Linq;
using GW2SDK.Enums;
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
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Item_restriction_can_be_serialized_from_json()
        {
            var expected = _fixture.Db.GetItemRestrictions().ToHashSet();

            var actual = Enum.GetNames(typeof(ItemRestriction)).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void Item_restriction_has_no_default_member()
        {
            Assert.False(Enum.IsDefined(typeof(ItemRestriction), default(ItemRestriction)));
        }
    }
}
