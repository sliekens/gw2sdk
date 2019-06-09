using System;
using System.Linq;
using GW2SDK.Features.Items;
using GW2SDK.Tests.Features.Items.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    [Collection(nameof(ItemDbCollection))]
    public class ItemRarityTest
    {
        public ItemRarityTest(ItemFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ItemFixture _fixture;

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void DefaultMember_ShouldBeUndefined()
        {
            Assert.False(Enum.IsDefined(typeof(ItemRarity), default(ItemRarity)));
        }

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void AllMembers_ShouldNotHaveMissingMembers()
        {
            var expected = _fixture.Db.GetItemRarities().ToHashSet();

            var actual = Enum.GetNames(typeof(ItemRarity)).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}
