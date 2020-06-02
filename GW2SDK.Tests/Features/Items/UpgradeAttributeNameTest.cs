using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Items.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    [Collection(nameof(ItemDbCollection))]
    public class UpgradeAttributeNameTest
    {
        public UpgradeAttributeNameTest(ItemFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ItemFixture _fixture;

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Upgrade_attribute_name_can_be_created_from_json()
        {
            var expected = _fixture.Db.GetInfixUpgradeAttributeNames().ToHashSet();

            var actual = Enum.GetNames(typeof(UpgradeAttributeName)).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void Upgrade_attribute_name_has_no_default_member()
        {
            Assert.False(Enum.IsDefined(typeof(UpgradeAttributeName), default(UpgradeAttributeName)));
        }
    }
}
