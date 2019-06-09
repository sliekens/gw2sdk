using System;
using System.Linq;
using GW2SDK.Features.Items;
using GW2SDK.Tests.Features.Items.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    [Collection(nameof(ItemDbCollection))]
    public class UpgradeComponentFlagTest
    {
        public UpgradeComponentFlagTest(ItemFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ItemFixture _fixture;

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void DefaultMember_ShouldBeUndefined()
        {
            Assert.False(Enum.IsDefined(typeof(UpgradeComponentFlag), default(UpgradeComponentFlag)));
        }

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void AllMembers_ShouldNotHaveMissingMembers()
        {
            var expected = _fixture.Db.GetUpgradeComponentFlags().ToHashSet();

            var actual = Enum.GetNames(typeof(UpgradeComponentFlag)).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}