using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Items.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    [Collection(nameof(ItemDbCollection))]
    public class UpgradeTypeTest
    {
        public UpgradeTypeTest(ItemFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ItemFixture _fixture;

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Upgrade_type_can_be_serialized_from_json()
        {
            var expected = _fixture.Db.GetUpgradedItemTypes().ToHashSet();

            var actual = Enum.GetNames(typeof(UpgradeType)).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void Upgrade_type_has_no_default_member()
        {
            Assert.False(Enum.IsDefined(typeof(UpgradeType), default(UpgradeType)));
        }
    }
}